using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

using System;

using System.IO;
using System.Text;
using System.Threading;

namespace GmailQuickstart {

    class Program {
        
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/gmail-dotnet-quickstart.json
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "Gmail API .NET Quickstart";


        static void Main(string[] args) {

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
             * 16494e24be61d2ca - pickup - doesn't get instructions
             * 164a0be8486f56d7
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
            } else {
                Console.WriteLine("File already exists: " + orderId + ".html");
            }
            Console.WriteLine("----------------------");

            GrubHubOrder order = new GrubHubOrder();
            GrubHubParser grubHubParser = new GrubHubParser();

            grubHubParser.ParseGrubHubOrder(decodedBody, order);
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

    }
}