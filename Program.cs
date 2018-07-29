using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using static Google.Apis.Gmail.v1.UsersResource.HistoryResource.ListRequest;

namespace GmailQuickstart {

    class Program {
        
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/gmail-dotnet-quickstart.json
        static string[] Scopes = { GmailService.Scope.GmailReadonly };
        static string ApplicationName = "Gmail API .NET Quickstart";

        static GmailService service;

        static string historyIDPath = @"C:\Users\Derek\Desktop\T4 Projects\Online Order Printer\historyID\historyID.txt";

        static string GrubHubStorageDir = @"C:\Users\Derek\Desktop\T4 Projects\Online Order Printer\GrubHub Orders";
        static string DoorDashStorageDir = @"C:\Users\Derek\Desktop\T4 Projects\Online Order Printer\DoorDash Orders";

        /* Example order ids to test from
        * GrubHub:
        * 16496c1551e4bdb6 - delivery
        * 16494e24be61d2ca - pickup - 
        * 164a0be8486f56d7
        * DoorDash:
        * 164b501111cebfe1
        * 164aebfdb8b7a59a
        */
        static string testMessageId = "164aebfdb8b7a59a";
        static string userId = "t4milpitasonline@gmail.com";

        static Timer timer;

        static Queue<Order> orderQ = new Queue<Order>(); // the queue of parsed orders
        static PrinterUtility printerUtil = new PrinterUtility();

        //If true: tests the app by only handling the email with testMessageId once, then stops, else app runs in full sync mode
        private static bool debugMailMode = false;

        static void Main(string[] args) {

            UserCredential credential;

            //Initialize credentials
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
            service = new GmailService(new BaseClientService.Initializer() {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            //If this mode is on, then we're just going to check the testMessageID and exit
            if (debugMailMode) {
                HandleMessage(testMessageId);  
                //printerUtil.TestPrint();
                Console.Read();
                return;
            }

            //We check if we need to perform a full sync or a partial sync
            //If the file doesn't exist, this is the first time running the app, so execute full sync
            if (!File.Exists(historyIDPath)) {

                FullSyncAppToEmail();

            } else {

                PartialSyncAppToEmail();
                
            }

            const int dueTime = 5000;
            const int period = 4000; //in miliseconds
            timer = new Timer(CheckEmail, "4Head", dueTime, period);

            Console.Read();
        }

        /* Full sync grabs the latest messages and stores the historyId. 
         * Still need to save messageIds to list though 
         */
        private static void FullSyncAppToEmail() {

            const int maxResults = 30;
            const string query = "";

            List<Message> messageList = ListMessages(service, userId, query, maxResults);

            string historyId = GetNewestHistoryId(messageList[0].Id);

            Console.WriteLine("Wrote History ID: " + historyId);

            //Save the historyId to file
            UpdateHistoryId(historyId);

            
        }

        /* Partial sync uses the saved historyId to only retrieve emails newer than the id,
         * parses any order emails and inserts them to the Order queue. If there are any
         * orders in the order queue, they are dequeued into the Print queue to be printed
         */
        private static void PartialSyncAppToEmail() {

            string historyIdAsString = File.ReadAllText(historyIDPath);
            ulong historyId = Convert.ToUInt64(historyIdAsString, 10);
            Console.WriteLine("Read History ID: " + historyIdAsString);

            List<string> messageIdList = ListHistory(service, userId, historyId);

            if (messageIdList != null) {
                HandleMessages(messageIdList);

                if (orderQ.Count > 0) {
                    printerUtil.AddToPrintQueue(orderQ);
                    printerUtil.PrintOrders();
                }
            } else {
                Console.WriteLine("Email up to date: No new messages");
            }
        }

        /* Returns the associated historyId given the messageId */
        private static string GetNewestHistoryId(string newestMessageId) {

            Message newestMessage = GetMessage(service, userId, newestMessageId);

            ulong historyId = (ulong)newestMessage.HistoryId;

            return historyId.ToString();
        }

        /* Saves the newly fetched historyId to file for future partial syncs */
        private static void UpdateHistoryId(string historyId) {
            File.WriteAllText(historyIDPath, historyId);
        }

        /* Calls HandleMessage() for each messageId in the list */
        private static void HandleMessages(List<string> messageIdList) {
            foreach (string messageId in messageIdList) {
                Console.WriteLine("***************************** START MESSAGE **********************************");
                HandleMessage(messageId);
                Console.WriteLine("***************************** END MESSAGE ************************************");
            }
        }

        /* Checks if the email needs to be parsed for Door Dash or GrubHub,
         * and saves the order to file for reference
         */
        private static void HandleMessage(string messageId) {

            Console.WriteLine("Handling message: " + messageId);

            bool isGrubHubOrder = false;
            string base64Input = null;  //the input to be converted to base64url encoding format
            string fileName = null;  //the file name without the complete path
            string storageDir = null;  //the file saving directory
            string filePath = null;  //the full path to the file

            var emailResponse = GetMessage(service, userId, messageId);
            if (emailResponse == null) {
                Console.WriteLine("Message deleted, returning");
                return;
            }

            var headers = emailResponse.Payload.Headers;
            MessagePartHeader header = headers.FirstOrDefault(item => item.Name == "From");
            string senderAddress = header.Value;
            
            //Check if the email is from GrubHub
            if (header.Value == "orders@eat.grubhub.com") {

                Console.WriteLine("Email Type: GrubHub");
                isGrubHubOrder = true;
                try {
                    var body = emailResponse.Payload.Body.Data;

                    base64Input = body;
                    fileName = messageId + ".html";
                    storageDir = GrubHubStorageDir;
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            //Otherwise check if the email is from DoorDash
            } else if (header.Value == @"DoorDash <orders@doordash.com>") {

                Console.WriteLine("Email Type: DoorDash");
                try {
                    
                    var attachId = emailResponse.Payload.Parts[1].Body.AttachmentId;

                    //Need to do another API call to get the actual attachment from the attachment id
                    MessagePartBody attachPart = GetAttachment(service, userId, messageId, attachId);

                    base64Input = attachPart.Data;
                    fileName = messageId + ".pdf";
                    storageDir = DoorDashStorageDir;
                }catch(Exception e) {
                    Console.WriteLine(e.Message);
                }
            }else {
                Console.WriteLine("Not an order, returning");
                return;
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
                
                GrubHubParser grubHubParser = new GrubHubParser();
                string decodedBody = Encoding.UTF8.GetString(data);
                GrubHubOrder order = grubHubParser.ParseOrder(decodedBody);
                 
                order.PrintOrder();

                orderQ.Enqueue(order);
            } else {
                DoorDashParser doorDashParser = new DoorDashParser();
                //List<string> lines = doorDashParser.ExtractTextFromPDF(filePath, messageId);
                //DoorDashOrder order = doorDashParser.ParseOrder(lines);

                //order.PrintOrder();
            }
        }

        /* The callback function of the timer that checks if there are new emails to handle
         * by calling the PartialSync()
         */
        private static void CheckEmail(object state) {
            //Console.WriteLine(state);
            PartialSyncAppToEmail();
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

            List<string> messageIdList = null;

            UsersResource.HistoryResource.ListRequest request = service.Users.History.List(userId);
            request.StartHistoryId = startHistoryId;
            request.HistoryTypes = HistoryTypesEnum.MessageAdded;
            
            do {
                try {
                    ListHistoryResponse response = request.Execute();

                    //If there's a list of history records, then we need to store the messageId of each new record
                    if (response.History != null) {

                        messageIdList = new List<string>();
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
    }
}