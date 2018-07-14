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

        static void Main(string[] args) {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read)) {
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

            //Example orders to base off of
            /* 16405c305bf594bc - has extra SCHEDULED ORDER <table>
             * 16480ed086d23503
             * 160e78db8539e9da
             * 164867c4762cffcc - lot of instructions
             * 164860dc2feb5155
             */

            string orderId = "16405c305bf594bc";
            string orderStorageDir = @"C:\Users\Derek\Desktop\T4 Tech Upgrade Ideas\Gmail_API\html_grubhub_orders";
            string htmlFile = orderStorageDir + "\\" + orderId + ".html";

            var emailResponse = GetMessage(service, "t4milpitas@gmail.com", orderId);
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
            ScanGrubHub(decodedBody, order);
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
        public static void ScanGrubHub(string html, GrubHubOrder order) {
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
                //Console.WriteLine("tr elem: " + i);
                var tdNodes = orderContentNodes[i].Elements("td");
               
                Item item = new Item();
                ParseQuantity( tdNodes.ElementAt(0), item);
                ParseName(     tdNodes.ElementAt(1), item);
                ParsePrice(    tdNodes.ElementAt(2), item);
                order.ItemList.Add(item);
            }
        }


        public static void ParseOrderNumber(HtmlNode node, Order order) {
            order.OrderNumber = node.InnerHtml;
            //PrintNode("Order Number", node);
        }

        public static void ParsePickupName(HtmlNode node, Order order) {
            order.CustomerName = node.InnerHtml;
            //PrintNode("Customer Name", node);
        }

        public static void ParseContactNumber(HtmlNode node, Order order) {
            order.ContactNumber = node.InnerHtml;
            //PrintNode("Contact Number", node);
        }

        public static void ParseQuantity(HtmlNode node, Item item) {
            item.Quantity = Int32.Parse(node.Element("div").InnerHtml);
            //PrintNode("Quantity", node.Element("div"));
        }

        public static void ParseName(HtmlNode node, Item item) {
            var divNodes = node.Elements("div"); 
            var divNodeCount = divNodes.Count();

            item.ItemName = divNodes.ElementAt(0).InnerHtml;
            //PrintNode("Item Name", divNodes.ElementAt(0));

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
                ParseSpecialInstruction(divNodes.ElementAt(1), item);
            }

        }

        public static void ParseAddOns(HtmlNode node, Item item) {
            var liNodes = node.Elements("li");

            if (liNodes != null) {
                item.AddOnList = new List<string>();

                foreach (var liNode in liNodes) {
                    item.AddOnList.Add(liNode.InnerHtml);
                    //PrintNode("Add On", liNode);
                }
            }
        }

        public static void ParseSpecialInstruction(HtmlNode node, Item item) {
            item.SpecialInstructions = node.InnerHtml;
            //PrintNode("Special Instruction", node);
        }

        public static void ParsePrice(HtmlNode node, Item item) {
            node.InnerHtml = node.InnerHtml.Trim(); //trim the white space
            item.Price = node.InnerHtml;
            //PrintNode("Price", node);
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