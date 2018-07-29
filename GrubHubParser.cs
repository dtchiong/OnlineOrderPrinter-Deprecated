using HtmlAgilityPack;

using System;
using System.Collections.Generic;
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
        public GrubHubOrder ParseOrder(string html) {
            GrubHubOrder order = new GrubHubOrder();

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var metaInfoNodes = htmlDoc.DocumentNode.SelectNodes("//body/table/tbody/tr/td/table/tbody/tr/td/table[3]/tbody/tr/th[2]/table/tbody/tr/th/div/div[2]/div/div");

            //If this is null, then there's an extra <table> "SCHEDULED ORDER: PREVIEW" before the pickup/delivery <table>
            if (metaInfoNodes == null) {
                metaInfoNodes = htmlDoc.DocumentNode.SelectNodes("//body/table/tbody/tr/td/table/tbody/tr/td/table[4]/tbody/tr/th[2]/table/tbody/tr/th/div/div/div/div");
            }
            var pickupByNameNode = metaInfoNodes.ElementAt(1);

            var orderNumberNode = htmlDoc.DocumentNode.SelectSingleNode("//body/table/tbody/tr/td/table/tbody/tr/td/table[2]/tbody/tr/th/table/tbody/tr/th/div/div[4]/span[2]");
            ParseOrderNumber(orderNumberNode, order);

            const int PickupOrderDivCount = 4; //the # of <div> elems associated with a PickUp order
            int metaDivCount = metaInfoNodes.Count; //the # of <div> elems 
            bool isDeliveryOrder = metaDivCount > PickupOrderDivCount;
            int nonItemCount = 5; //the last 5 <tr>'s of the <tbody> is meta information
            int lastAddressIndex = metaDivCount - 3;

            ParsePickupName(pickupByNameNode, order);
            ParseContactNumber(metaInfoNodes.ElementAt(metaDivCount - 1), order);
            //There's one more <tr> of meta information if the order is Delivery
            if (isDeliveryOrder) {
                nonItemCount = 6;
                order.DeliveryMethod = "Delivery";

                ParseDeliveryAddress(metaInfoNodes, 2, lastAddressIndex, order);
            }

            var orderContentNodes = htmlDoc.DocumentNode.SelectNodes("//tbody[@class='orderSummary__body']/tr");
            order.TotalItemCount = orderContentNodes.Count - nonItemCount;

            for (int i = 0; i < order.TotalItemCount; i++) {

                var tdNodes = orderContentNodes[i].Elements("td");

                Item item = new Item();
                ParseQuantity(tdNodes.ElementAt(0), item);
                ParseItem(tdNodes.ElementAt(1), item);
                ParsePrice(tdNodes.ElementAt(2), item);

                SetItemCount(i + 1, order.TotalItemCount, item);

                order.ItemList.Add(item);
            }
            return order;
        }

        public static void ParseOrderNumber(HtmlNode node, Order order) {
            order.OrderNumber = node.InnerHtml;
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
        public static void ParseName(HtmlNode node, Item item) {

            string name = node.InnerHtml;
            item.ItemName = name;

            string correctedName = menu.GetCorrectedItemName(name);
            if (correctedName != null) {
                item.ItemName = correctedName;
            }
        }

        /* Looks up the item in the menu dictionary to set the item type */
        public static void ParseType(HtmlNode node, Item item) {
            string name = node.InnerHtml;
            string type = menu.GetItemType(name);

            item.ItemType = type;
        }

        public static void ParseQuantity(HtmlNode node, Item item) {
            item.Quantity = Int32.Parse(node.Element("div").InnerHtml);
        }

        /* Parse the item name, type, and addons */
        public static void ParseItem(HtmlNode node, Item item) {
            var divNodes = node.Elements("div");
            var divNodeCount = divNodes.Count();
            var nameNode = divNodes.ElementAt(0);

            ParseName(nameNode, item);
            ParseType(nameNode, item);

            //If there's 2 div nodes, then the 2nd is either addons or special instructions
            if (divNodeCount == 2) {

                //If there's add-ons, then they're enclosed in a <ul>
                var addOnNode = divNodes.ElementAt(1).Element("ul");

                //Parse the add-on node if it exists, otherwise parse the special instructions instead
                if (addOnNode != null) {
                    ParseAddOns(addOnNode, item);
                } else {
                    ParseSpecialInstruction(divNodes.ElementAt(1), item);
                }
            } else if (divNodeCount == 4) { //There's both addons and special instructions
                ParseAddOns(divNodes.ElementAt(1).Element("ul"), item);
                ParseSpecialInstruction(divNodes.ElementAt(3), item);
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
                        Console.WriteLine("Unidentified add on type: " + addOnName);
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

            //Console.WriteLine("Got addOn type: " + type);
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
                    //Console.WriteLine("Identified Tea Type: " + name);
                    item.ItemName = item.ItemName.Replace("Tea", name);
                    break;
                default:
                    Console.WriteLine("Unidentified add on type: " + name);
                    break;
            }
        }

        public static void ParseSpecialInstruction(HtmlNode node, Item item) {
            string instructions = node.InnerHtml.Replace("Instructions: ", "");
            item.SpecialInstructions = instructions;
        }

        public static void ParsePrice(HtmlNode node, Item item) {
            node.InnerHtml = node.InnerHtml.Trim(); //trim the white space
            item.Price = node.InnerHtml;
        }

        //Node printing function for debugging
        public static void PrintNode(string title, HtmlNode node) {
            if (node != null)
                Console.WriteLine(title + ": " + node.InnerHtml);
            else
                Console.WriteLine(title + ": NULL");
        }

    }
}
