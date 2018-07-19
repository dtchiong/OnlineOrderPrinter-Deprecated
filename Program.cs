using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

using HtmlAgilityPack;

using System;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GmailQuickstart {
    class Program {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/gmail-dotnet-quickstart.json
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "Gmail API .NET Quickstart";
        public static GrubHubMenu menu = new GrubHubMenu();


        static void Main(string[] args){
            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read)) {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/gmail-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Gmail API service.
            var service = new GmailService(new BaseClientService.Initializer() {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            /* Example order ids to test from
             * 16496c1551e4bdb6 - delivery
             * 16494e24be61d2ca - pickup
             */
            string orderId = "164a0be8486f56d7";

            string orderStorageDir = @"C:\Users\Derek\Desktop\T4 Projects\Online Order Printer\GrubHub Orders";
            if (System.Environment.MachineName == "your machine name") {
                orderStorageDir = "";
            }

            string htmlFile = orderStorageDir + "\\" + orderId + ".html";

            var emailResponse = GetMessage(service, "t4milpitasonline@gmail.com", orderId);
            var body = emailResponse.Payload.Body.Data;
            byte[] data = FromBase64ForUrlString(body);
            string decodedBody = Encoding.UTF8.GetString(data);

            //Saves the order to file if it doesn't exist
            if (!File.Exists(htmlFile)) {
                Console.WriteLine("Writing new file: " + orderId + ".html");
                System.IO.File.WriteAllText(htmlFile, decodedBody);
            }else {
                Console.WriteLine("File already exists: " + orderId + ".html");
            }
            Console.WriteLine("----------------------");

            GrubHubOrder order = new GrubHubOrder();
            ParseGrubHubOrder(decodedBody, order);
            order.PrintOrder();
            
            Console.Read();
        }

        public static Message GetMessage(GmailService service, String userId, String messageId) {
            try {
                return service.Users.Messages.Get(userId, messageId).Execute();
            } catch (Exception e) {
                Console.WriteLine("An error occurred: " + e.Message);
            }

            return null;
        }

        //Converting function
        public static byte[] FromBase64ForUrlString(string base64ForUrlInput) {
            int padChars = (base64ForUrlInput.Length % 4) == 0 ? 0 : (4 - (base64ForUrlInput.Length % 4));
            StringBuilder result = new StringBuilder(base64ForUrlInput, base64ForUrlInput.Length + padChars);
            result.Append(String.Empty.PadRight(padChars, '='));
            result.Replace('-', '+');
            result.Replace('_', '/');
            return Convert.FromBase64String(result.ToString());
        }

        
        /* Takes grubhub order as an html file in a string and extracts the the relevant information
         */
        public static void ParseGrubHubOrder(string html, GrubHubOrder order) {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var orderNumberNode   = htmlDoc.DocumentNode.SelectSingleNode("//body/table/tbody/tr/td/table/tbody/tr/td/table[2]/tbody/tr/th/table/tbody/tr/th/div/div[4]/span[2]");
            var pickupByNameNode  = htmlDoc.DocumentNode.SelectSingleNode("//body/table/tbody/tr/td/table/tbody/tr/td/table[3]/tbody/tr/th[2]/table/tbody/tr/th/div/div[2]/div/div[2]");
            var contactNumberNode = htmlDoc.DocumentNode.SelectSingleNode("//body/table/tbody/tr/td/table/tbody/tr/td/table[3]/tbody/tr/th[2]/table/tbody/tr/th/div/div[2]/div/div[4]");
            var metaInfoNodes = htmlDoc.DocumentNode.SelectNodes("//body/table/tbody/tr/td/table/tbody/tr/td/table[3]/tbody/tr/th[2]/table/tbody/tr/th/div/div[2]/div/div");
            var orderContentNodes = htmlDoc.DocumentNode.SelectNodes("//tbody[@class='orderSummary__body']/tr");

            //If this is null, then there's an extra <table> "SCHEDULED ORDER: PREVIEW" before the pickup/delivery <table>
            if (pickupByNameNode == null) {
                pickupByNameNode  = htmlDoc.DocumentNode.SelectSingleNode("//body/table/tbody/tr/td/table/tbody/tr/td/table[4]/tbody/tr/th[2]/table/tbody/tr/th/div/div/div/div[2]");
                contactNumberNode = htmlDoc.DocumentNode.SelectSingleNode("//body/table/tbody/tr/td/table/tbody/tr/td/table[4]/tbody/tr/th[2]/table/tbody/tr/th/div/div/div/div[4]");
                metaInfoNodes     = htmlDoc.DocumentNode.SelectNodes("//body/table/tbody/tr/td/table/tbody/tr/td/table[4]/tbody/tr/th[2]/table/tbody/tr/th/div/div/div/div");
            }

            ParseOrderNumber(orderNumberNode, order);
            ParsePickupName(pickupByNameNode, order);
            ParseContactNumber(contactNumberNode, order);

            int metaDivCount              = metaInfoNodes.Count; //the # of <div> elems
            const int PickupOrderDivCount = 4; //the # of <div> elems associated with a PickUp order
            bool isDeliveryOrder          = metaDivCount > PickupOrderDivCount;
            int nonItemCount              = 5; //the last 5 <tr>'s of the <tbody> is meta information

            //There's one more <tr> of meta information if the order is Delivery
            if (isDeliveryOrder) {
                nonItemCount = 6;
                order.DeliveryMethod = "Delivery";
            }

            order.TotalItemCount = orderContentNodes.Count - nonItemCount;

            for (int i=0; i<order.TotalItemCount; i++) {

                var tdNodes = orderContentNodes[i].Elements("td");
               
                Item item = new Item();
                ParseQuantity( tdNodes.ElementAt(0), item);
                ParseItem(     tdNodes.ElementAt(1), item);
                ParsePrice(    tdNodes.ElementAt(2), item);

                SetItemCount(i + 1, order.TotalItemCount, item);

                order.ItemList.Add(item);
            }
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

        public static void ParseName(HtmlNode node, Item item) {

            string name = node.InnerHtml;
            item.ItemName = name;

            string correctedName = menu.GetCorrectedItemName(name);
            if (correctedName != null) {
                item.ItemName = correctedName;
            }
        }

        public static void ParseType(HtmlNode node, Item item) {
            string name = node.InnerHtml;
            string type = menu.GetItemType(name);

            item.ItemType = type; 
        }

        public static void ParseQuantity(HtmlNode node, Item item) {
            item.Quantity = Int32.Parse(node.Element("div").InnerHtml);
        }

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
                }else {
                    ParseSpecialInstruction(divNodes.ElementAt(1), item);
                }
            }else if (divNodeCount == 3) { //There's both addons and special instructions
                ParseAddOns(divNodes.ElementAt(1).Element("ul"), item);
                ParseSpecialInstruction(divNodes.ElementAt(3), item);
            }

        }

        public static void SetItemCount(int itemIndex, int totalItems, Item item) {
            item.ItemCount = itemIndex + "/" + totalItems;
        }

        public static void ParseAddOns(HtmlNode node, Item item) {
            var liNodes = node.Elements("li");

            if (liNodes != null) {
                item.AddOnList = new List<string>();

                foreach (var liNode in liNodes) {
                    string addOnName = liNode.InnerHtml;
                    string addOnType = menu.GetAddOnType(addOnName);

                    if (addOnType != null) {
                        ParseAddOnTypeAndName(addOnType, addOnName, item);
                    }else {
                        item.AddOnList.Add(addOnName);
                        Console.WriteLine("Unidentified add on type: " + addOnName);
                    }    
                }
            }
        }

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
            node.InnerHtml  = node.InnerHtml.Trim(); //trim the white space
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