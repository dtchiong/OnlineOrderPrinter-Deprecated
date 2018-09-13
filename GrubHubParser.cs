using HtmlAgilityPack;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace GmailQuickstart {

    public class GrubHubParser {

        public static GrubHubMenu menu;

        //Constructor
        public GrubHubParser() {
            menu = new GrubHubMenu();
        }

        /* Extracts the order from an grubhub html file and returns an Order object */
        public Order ParseOrder(string html, DateTime dateTime, string messageId) {
            Order order = new Order();

            order.Service = "GrubHub";
            order.TimeReceived = dateTime;
            order.MessageId = messageId;

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            //We begin setting the possible locations of the relevant information, and then choose which one depending on the format on the html doc
            string metaInfoLoc1     = "//body/table/tbody/tr/td/table/tbody/tr/td/table[3]/tbody/tr/th[2]/table/tbody/tr/th/div/div[2]/div/div";
            string metaInfoLoc2     = "//body/table/tbody/tr/td/table/tbody/tr/td/table[4]/tbody/tr/th[2]/table/tbody/tr/th/div/div/div/div";
            string metaInfoLoc3     = "//body/table/tbody/tr/td/table/tbody/tr/td/table[5]/tbody/tr/th[2]/table/tbody/tr/th/div/div/div/div";

            string metaInfoBaseLoc1 = "//body/table/tbody/tr/td/table/tbody/tr/td/table[3]";
            string metaInfoBaseLoc2 = "//body/table/tbody/tr/td/table/tbody/tr/td/table[4]";
            string metaInfoBaseLoc3 = "//body/table/tbody/tr/td/table/tbody/tr/td/table[5]";

            string delivMethodLoc1  = @"/tbody/tr/th/table/tbody/tr/th/div/div[2]/div/span/span";
            string delivMethodLoc2  = @"/tbody/tr/th/table/tbody/tr/th/div/div/div/span/span";
            string delivMethodLoc3  = @"/tbody/tr/th/table/tbody/tr/th/div/div/div/span/span";

            string pickUpTimeLoc1   = @"/tbody/tr/th/table/tbody/tr/th/div/div[2]/div[2]/span";
            string pickUpTimeLoc2   = @"/tbody/tr/th/table/tbody/tr/th/div/div/div[2]/span";
            string pickUpTimeLoc3   = @"/tbody/tr/th/table/tbody/tr/th/div/div/div[2]/span";

            HtmlNodeCollection metaInfoNodes      = null;
            HtmlNode           deliveryMethodNode = null;
            HtmlNode           pickupTimeNode     = null;
            
            //If this is not null, the order is in standard format
            if (htmlDoc.DocumentNode.SelectNodes(metaInfoLoc1) != null) {
                //Debug.WriteLine("Using Standard format");
                metaInfoNodes      = htmlDoc.DocumentNode.SelectNodes(metaInfoLoc1);
                deliveryMethodNode = htmlDoc.DocumentNode.SelectSingleNode(metaInfoBaseLoc1 + delivMethodLoc1);
                pickupTimeNode     = htmlDoc.DocumentNode.SelectSingleNode(metaInfoBaseLoc1 + pickUpTimeLoc1);
            }
            //Otherwise the previous loc is null and there's an extra <table> "SCHEDULED ORDER" before the pickup/delivery <table> 
            //so we use location 2
            else if (htmlDoc.DocumentNode.SelectNodes(metaInfoLoc2) != null) {
                //Debug.WriteLine("Using Scheduled Order format");
                metaInfoNodes = htmlDoc.DocumentNode.SelectNodes(metaInfoLoc2);
                deliveryMethodNode = htmlDoc.DocumentNode.SelectSingleNode(metaInfoBaseLoc2 + delivMethodLoc2);
                pickupTimeNode = htmlDoc.DocumentNode.SelectSingleNode(metaInfoBaseLoc2 + pickUpTimeLoc2);
            }
            //Otherwise the previous loc is null and there's also an extra <table> "ORDER ADJUSTMENT" before the relevant <table>
            //so we use location 3
            else if (htmlDoc.DocumentNode.SelectNodes(metaInfoLoc3) != null) {
                //Debug.WriteLine("Using Adjusted Order format");
                metaInfoNodes = htmlDoc.DocumentNode.SelectNodes(metaInfoLoc3);
                deliveryMethodNode = htmlDoc.DocumentNode.SelectSingleNode(metaInfoBaseLoc3 + delivMethodLoc3);
                pickupTimeNode = htmlDoc.DocumentNode.SelectSingleNode(metaInfoBaseLoc3 + pickUpTimeLoc3);
            }
            //If location 3 is still null, then the format is not recognized
            else {
                //Debug.WriteLine("Cannot parse order - format not recognized");
                return null;
            }

            var orderNumberNode = htmlDoc.DocumentNode.SelectSingleNode("//body/table/tbody/tr/td/table/tbody/tr/td/table[2]/tbody/tr/th/table/tbody/tr/th/div/div[4]/span[2]");
            ParseOrderNumber(orderNumberNode, order);
            ParsePickUpTime(pickupTimeNode, order);

            int metaDivCount = metaInfoNodes.Count; //the # of <div> elems 
            int nonItemCount = 5; //the last 5 <tr>'s of the <tbody> is meta information
            int lastAddressIndex = metaDivCount - 3;

            //We need to know whether it's DELIVERY or PICKUP because there's a difference in the html structure
            if (deliveryMethodNode.InnerHtml.Trim() == "DELIVERY") {
                nonItemCount = 6;
            }else {
                order.DeliveryMethod = "Pickup";
            }

            var pickupByNameNode = metaInfoNodes.ElementAt(1);
            ParsePickupName(pickupByNameNode, order);
            ParseContactNumber(metaInfoNodes.ElementAt(metaDivCount - 1), order);
           
            var orderContentNodes = htmlDoc.DocumentNode.SelectNodes("//td[@class='orderSummary__data']");
            //order.UniqueItemCount = orderContentNodes.Count - nonItemCount;

            //Loops through all the td nodes with class = "orderSummary__data"
            //Parses itemName from td's with inner structure <div>
            //Parses   addOns from td's with inner structure <div> <ul>
            //Does nothing for td's with no css child nodes
            //Stops parsing when hitting the td with inner node <span>, or when there are no more tdNodes to scan

            bool parsedItemName = false;
            Item item = null;
            for (int i = 0; i < orderContentNodes.Count; i++) {

                var tdNode = orderContentNodes[i];
                
                string spanNodePath = tdNode.XPath + "/span";
                var spanNode = tdNode.SelectSingleNode( spanNodePath );

                if (spanNode != null) {
                    //Debug.WriteLine("Hit td with <span> - breaking");
                    break;
                }

                var divNode = tdNode.Element("div");

                if (divNode == null) {
                    //Debug.WriteLine("No childs nodes - no addons");
                    continue;
                }

                if (divNode.Element("ul") != null) {
                    //Debug.WriteLine("contains addOns");
                    ParseAddOns(divNode.Element("ul"), item);
                }else {
                    //Since Special Instructions and ItemNames have the same html strucuture, 
                    //we need manually check the innerHTML to decide which it is
                    if (divNode.InnerHtml.Trim().StartsWith("Instructions")) {
                        ParseSpecialInstruction(divNode, item);
                    }else {
                        //Debug.WriteLine("is an itemName");
                        if (item != null) {
                            order.ItemList.Add(item); //add the previous item if it's not null, then handle the new item
                        }
                        item = new Item();
                        ParseItemName(divNode, item);
                        ParseQuantity(tdNode, item);
                        ParseItemType(divNode, item);
                    }

                }
                //SetItemCount(i + 1, order.UniqueItemCount, item);

            }
            order.ItemList.Add(item); //add the last item since it won't be added without a next item

            DoorDashParser.SetDrinkAndSnackCount(order);
            DoorDashParser.SetOrderSize(order); //Need to move function to be general
            return order;
        }

        public static void ParseOrderNumber(HtmlNode node, Order order) {
             
            string tmp = node.InnerHtml; // #64020523 — 4398339
            tmp = Regex.Replace(tmp, @"[#\s]*", ""); //removes all whitespace and '#' chars
            tmp = tmp.Replace((char)0x2014, '-');
            order.OrderNumber = tmp;
        }

        public static void ParsePickUpTime(HtmlNode node, Order order) {
            try {
                order.PickUpTime = DateTime.Parse(node.InnerHtml);
                //Debug.WriteLine(order.PickUpTime.ToString());
            }catch(Exception e) {
                Debug.WriteLine("ParsePickUpTime: " + e.ToString());
            }
        }

        public static void ParsePickupName(HtmlNode node, Order order) {
            order.CustomerName = node.InnerHtml;
        }

        public static void ParseContactNumber(HtmlNode node, Order order) {
            order.ContactNumber = node.InnerHtml;
        }

        /* Child nodes of parentNode from startInd to endInd are part of the address */
        public static void ParseDeliveryAddress(HtmlNodeCollection parentNode, int startInd, int endInd, Order order) {
            string address = "";
            for (int i = startInd; i <= endInd; i++) {

                string addressPart = parentNode.ElementAt(i).InnerHtml;
                string actual1 = Regex.Replace(addressPart, @"\t|\n|\r", "");
                string actual2 = Regex.Replace(actual1, @"\s+", " ");
                address = address + actual2 + ",";
            }
            order.DeliverAddress = address;
        }

        /* We get the name of the item, then correct it through the menu dictionary */
        public static void ParseItemName(HtmlNode node, Item item) {

            string name = node.InnerHtml.Trim();
            item.ItemName = name;

            string correctedName = menu.GetCorrectedItemName(name);
            if (correctedName != null) {
                item.ItemName = correctedName;
            }
        }

        /* Looks up the item in the menu dictionary to set the item type */
        //Obsolete
        public static void ParseItemType(HtmlNode node, Item item) {
            string name = node.InnerHtml.Trim();
            string type = menu.GetItemType(name);

            item.ItemType = type;
        }

        /* Returns true if the quantity is parsed from the node */
        public static bool ParseQuantity(HtmlNode node, Item item) {
            var parentNode = node.ParentNode;
            var quantityNode = parentNode.Element("td").Element("div");

            try {
                item.Quantity = Int32.Parse(quantityNode.InnerHtml);
                return true;
            } catch(Exception e) {
                return false;
            } 
        }

        public static void SetItemCount(int itemIndex, int totalItems, Item item) {
            item.ItemCount = itemIndex + "/" + totalItems;
        }

        /* Sorts each addon into a topping, or an item attribute such as size, sugar level, etc */
        public static void ParseAddOns(HtmlNode node, Item item) {
            var liNodes = node.Elements("li");

            if (liNodes != null) {
                item.AddOnList = new List<string>();

                foreach (var liNode in liNodes) {
                    string addOnName = liNode.InnerHtml;
                    string addOnType = menu.GetAddOnType(addOnName);

                    if (addOnType != null) {
                        ParseAddOnTypeAndName(addOnType, addOnName, item);
                    } else {
                        item.AddOnList.Add(addOnName);
                        Debug.WriteLine("Unidentified add on type: " + addOnName);
                    }
                }
            }
        }

        /* Categorizes each addon and sets the associated field in the item */
        public static void ParseAddOnTypeAndName(string type, string name, Item item) {

            string correctedAddOnName = menu.GetCorrectedAddOnName(name);
            if (correctedAddOnName != null) {
                name = correctedAddOnName;
            }

            //Debug.WriteLine("Got addOn type: " + type);
            switch (type) {
                case "Temperature":
                    item.Temperature = name;
                    break;
                case "Size":
                    item.Size = name;
                    break;
                case "Ice":
                    item.IceLevel = name;
                    break;
                case "Sugar":
                    item.SugarLevel = name;
                    break;
                case "Topping":
                    item.AddOnList.Add(name);
                    break;
                case "Milk Subsitute":
                    item.MilkSubsitution = name;
                    break;
                case "Tea":
                    //Debug.WriteLine("Identified Tea Type: " + name);
                    item.ItemName = Regex.Replace(item.ItemName, @"\s+", " "); //replaces multiple spaces with a single one
                    item.ItemName = item.ItemName.Trim().Replace("Tea", name);
                    break;
                default:
                    Debug.WriteLine("Unidentified add on type: " + name);
                    break;
            }
        }

        public static void ParseSpecialInstruction(HtmlNode node, Item item) {

            string instructions = node.InnerHtml.Replace("Instructions: ", "");      
            instructions = Regex.Replace(instructions, @"\t|\n|\r", "");
            instructions = instructions.Trim();
            instructions = "\"" + instructions + "\"";

            item.SpecialInstructions = instructions;
        }

        public static void ParsePrice(HtmlNode node, Item item) {
            node.InnerHtml = node.InnerHtml.Trim(); //trim the white space
            item.Price = node.InnerHtml;
        }

        //Node printing function for debugging
        public static void PrintNode(string title, HtmlNode node) {
            if (node != null)
                Debug.WriteLine(title + ": " + node.InnerHtml);
            else
                Debug.WriteLine(title + ": NULL");
        }

    }
}
