using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using static Google.Apis.Gmail.v1.UsersResource.HistoryResource.ListRequest;

namespace GmailQuickstart {

    class Program {
        
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/gmail-dotnet-quickstart.json
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "Gmail API .NET Quickstart";
        static string historyIDPath = @"C:\Users\Derek\Desktop\T4 Projects\Online Order Printer\historyID\historyID.txt";

        static Timer timer;

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
             * GrubHub:
             * 16496c1551e4bdb6 - delivery
             * 16494e24be61d2ca - pickup - 
             * 164a0be8486f56d7
             * DoorDash:
             * 164b501111cebfe1
             * 164aebfdb8b7a59a
             */

            string messageId = "164aebfdb8b7a59a";
            string userId = "t4milpitasonline@gmail.com";

            const string GrubHubStorageDir = @"C:\Users\Derek\Desktop\T4 Projects\Online Order Printer\GrubHub Orders";
            const string DoorDashStorageDir = @"C:\Users\Derek\Desktop\T4 Projects\Online Order Printer\DoorDash Orders";

            bool   isGrubHubOrder = false;
            string base64Input    = null;  //the input to be converted to base64url encoding format
            string fileName       = null;  //the file name without the complete path
            string storageDir     = null;  //the file saving directory
            string filePath       = null;  //the full path to the file

            List<string> messageIdList = null; //the list the messageIds to parse

            var emailResponse = GetMessage(service, userId, messageId);

            //If the Parts is not null, then this is a DoorDash email
            if (emailResponse.Payload.Parts != null) {
                Console.WriteLine("Email Type: DoorDash");

                var attachId = emailResponse.Payload.Parts[1].Body.AttachmentId;

                //Need to do another API call to get the actual attachment from the attachment id
                MessagePartBody attachPart = GetAttachment(service, userId, messageId, attachId);

                base64Input = attachPart.Data;
                fileName = messageId + ".pdf";
                storageDir = DoorDashStorageDir;

            } else { //GrubHub email
                Console.WriteLine("Email Type: GrubHub");
                isGrubHubOrder = true;

                var body = emailResponse.Payload.Body.Data;

                base64Input = body;
                fileName = messageId + ".html";
                storageDir = GrubHubStorageDir;
            }

            byte[] data = FromBase64ForUrlString(base64Input);
            filePath = Path.Combine(storageDir, fileName);
            
            //Saves the order to file if it doesn't exist
            if (!File.Exists(filePath)) {
                Console.WriteLine("Writing new file: " + fileName);
                File.WriteAllBytes(filePath, data);
            } else {
                Console.WriteLine("File already exists: " + fileName);
            }
            Console.WriteLine("----------------------");

            if (isGrubHubOrder) {
                GrubHubOrder order = new GrubHubOrder();
                GrubHubParser grubHubParser = new GrubHubParser();

                string decodedBody = Encoding.UTF8.GetString(data);
                grubHubParser.ParseOrder(decodedBody, order);

                order.PrintOrder();
            } else {
                DoorDashOrder order = new DoorDashOrder();
                DoorDashParser doorDashParser = new DoorDashParser();

                List<string> lines = doorDashParser.ExtractTextFromPDF(filePath, messageId);
                doorDashParser.ParseOrder(lines, order);

                order.PrintOrder();
            }

            int dueTime = 0;
            int period = 2000; //in miliseconds
            //timer = new Timer(TimerCallback, "4Head", dueTime, period);

            //If the file doesn't exist, this is the first time running the app, so execute full sync
            if (!File.Exists(historyIDPath)) {
                List<Message> messageList = ListMessages(service, userId, "", 30);

                string newestMessageId = messageList[0].Id;

                Message newestMessage = GetMessage(service, userId, newestMessageId);

                ulong historyId = (ulong)newestMessage.HistoryId;

                Console.WriteLine("Wrote History ID: " + historyId.ToString());

                //Save the historyId to file
                UpdateHistoryId(historyId.ToString());

            }else {//Do partial sync
                string historyIdAsString = File.ReadAllText(historyIDPath);
                ulong historyId = Convert.ToUInt64(historyIdAsString, 10);
                Console.WriteLine("Read History ID: " + historyIdAsString);

                messageIdList = ListHistory(service, userId, historyId);
                

            }

            Console.Read();
        }

        /* Retrieves the attachment file(s) with attachemntId, of the messageId */
        private static MessagePartBody GetAttachment(GmailService service, string userId, string messageId, string attachmentId) {
            try {
                return service.Users.Messages.Attachments.Get(userId, messageId, attachmentId).Execute();
            } catch (Exception e) {
                Console.WriteLine("An error occured: " + e.Message);
            }

            return null;
        }

        /* Retrieves the email with messageId, of the given user with userId */
        public static Message GetMessage(GmailService service, String userId, String messageId) {
            try {
                return service.Users.Messages.Get(userId, messageId).Execute();
            } catch (Exception e) {
                Console.WriteLine("An error occurred: " + e.Message);
            }

            return null;
        }

        /*Retrieves a list of messages given the userId, query, and max results to be returned */
        public static List<Message> ListMessages(GmailService service, String userId, String query, int maxResults) {
            List<Message> result = new List<Message>();
            UsersResource.MessagesResource.ListRequest request = service.Users.Messages.List(userId);
            request.MaxResults = maxResults;
            //request.Q = query; //sets Query

            try {

                ListMessagesResponse response = request.Execute();
                result.AddRange(response.Messages);

            } catch (Exception e) {
                Console.WriteLine("An error occured: " + e.Message);
            }

            return result;
        }

        /* Returns the list of messagesId since startHistoryId. Used in partial sync */
        public static List<string> ListHistory(GmailService service, String userId, ulong startHistoryId) {

            List<string> messageIdList = new List<string>();

            UsersResource.HistoryResource.ListRequest request = service.Users.History.List(userId);
            request.StartHistoryId = startHistoryId;
            request.HistoryTypes = HistoryTypesEnum.MessageAdded;
            
            do {
                try {
                    ListHistoryResponse response = request.Execute();

                    //If there's a list of history records, then we need to store the messageId of each new record
                    if (response.History != null) {

                        foreach (var historyRecord in response.History) {

                            string newMessageId = historyRecord.MessagesAdded[0].Message.Id;
                            messageIdList.Add(newMessageId);
                        }
                    }

                    //Then we update the historyId with the latest one 
                    ulong retrievedHistId = (ulong)response.HistoryId;
                    UpdateHistoryId(retrievedHistId.ToString());

                    request.PageToken = response.NextPageToken;

                } catch (Exception e) {
                    Console.WriteLine("An error occurred: " + e.Message);
                }
            } while (!String.IsNullOrEmpty(request.PageToken));

            return messageIdList;
        }

        /* Converting from RFC 4648 base64 to base64url encoding
         * see http://en.wikipedia.org/wiki/Base64#Implementations_and_history
         */
        public static byte[] FromBase64ForUrlString(string base64ForUrlInput) {
            int padChars = (base64ForUrlInput.Length % 4) == 0 ? 0 : (4 - (base64ForUrlInput.Length % 4));
            StringBuilder result = new StringBuilder(base64ForUrlInput, base64ForUrlInput.Length + padChars);
            result.Append(String.Empty.PadRight(padChars, '='));
            result.Replace('-', '+');
            result.Replace('_', '/');
            return Convert.FromBase64String(result.ToString());
        }

        private static void TimerCallback(object state) {
            Console.WriteLine("dis game doo doo");
            Console.WriteLine(state);
        }

        /* Saves the newly fetched historyId to file for future partial syncs */
        private static void UpdateHistoryId(string historyId) {
            File.WriteAllText(historyIDPath, historyId);
        }

    }
}