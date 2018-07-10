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

            // Define parameters of request.
            UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");
            /*
            // List labels.
            IList<Label> labels = request.Execute().Labels;
            Console.WriteLine("Labels:");
            if (labels != null && labels.Count > 0) {
                foreach (var labelItem in labels) {
                    Console.WriteLine("{0}", labelItem.Name);
                }
            } else {
                Console.WriteLine("No labels found.");
            }
            */


            //Example orders to base off of
            /* 16405c305bf594bc
             * 16480ed086d23503
             * 160e78db8539e9da
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

            ScanGrubHub(decodedBody);

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

        
        /* Takes grubhub order as an html file in text form and extracts the the relevant information
         */
        public static void ScanGrubHub(string html) {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var orderNumberNode   = htmlDoc.DocumentNode.SelectSingleNode("//body/table/tbody/tr/td/table/tbody/tr/td/table[2]/tbody/tr/th/table/tbody/tr/th/div/div[4]/span[2]");
            var pickupByNameNode  = htmlDoc.DocumentNode.SelectSingleNode("//body/table/tbody/tr/td/table/tbody/tr/td/table[3]/tbody/tr/th[2]/table/tbody/tr/th/div/div[2]/div/div[2]");
            var contactNumberNode = htmlDoc.DocumentNode.SelectSingleNode("//body/table/tbody/tr/td/table/tbody/tr/td/table[3]/tbody/tr/th[2]/table/tbody/tr/th/div/div[2]/div/div[4]");

            //If this is null, then there's an extra <table> "SCHEDULED ORDER: PREVIEW" before the pickup/delivery <table>
            if (pickupByNameNode == null) {
                Console.WriteLine("--Extra table detected-using table 4");
                pickupByNameNode  = htmlDoc.DocumentNode.SelectSingleNode("//body/table/tbody/tr/td/table/tbody/tr/td/table[4]/tbody/tr/th[2]/table/tbody/tr/th/div/div/div/div[2]");
                contactNumberNode = htmlDoc.DocumentNode.SelectSingleNode("//body/table/tbody/tr/td/table/tbody/tr/td/table[4]/tbody/tr/th[2]/table/tbody/tr/th/div/div/div/div[4]");
            }

            var orderContentNodes  = htmlDoc.DocumentNode.SelectNodes("//tbody[@class='orderSummary__body']/tr");

            PrintNode("Order Number", orderNumberNode);
            PrintNode("Pickup By Name", pickupByNameNode);
            PrintNode("Contact Number", contactNumberNode);

            for (int i=0; i<orderContentNodes.Count; i++) {
                Console.WriteLine("tr elem: "+ i);
            }
  
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