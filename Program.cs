using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;

using System;

using System.IO;
using System.Text;
using System.Threading;

using iText.License;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Listener;



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

            string pathToItextKeyLicense = @"C:\Users\Derek\Documents\dev\MICROSOFT_VISUAL_STUDIO\Order Parser\Order Parser\itextkeyLicense\itextkey1532408848616_0";

            try {
                LicenseKey.LoadLicenseFile(pathToItextKeyLicense);
            }catch(LicenseKeyException e) {
                Console.WriteLine(e);
            }

            /* Example order ids to test from
             * GrubHub:
             * 16496c1551e4bdb6 - delivery
             * 16494e24be61d2ca - pickup - 
             * 164a0be8486f56d7
             * DoorDash:
             * 164b501111cebfe1
             */
            string messageId = "164b501111cebfe1";

            const string GrubHubStorageDir = @"C:\Users\Derek\Desktop\T4 Projects\Online Order Printer\GrubHub Orders";
            const string DoorDashStorageDir = @"C:\Users\Derek\Desktop\T4 Projects\Online Order Printer\DoorDash Orders";

            bool   isGrubHubOrder = false;
            string base64Input    = null;  //the input to be converted to base64url encoding format
            string fileName       = null;
            string storageDir     = null;  //the file saving directory
            string filePath       = null;  //the full path to the file

            var emailResponse = GetMessage(service, "t4milpitasonline@gmail.com", messageId);

            //If the Parts is not null, then this is a DoorDash email
            if (emailResponse.Payload.Parts != null) {
                Console.WriteLine("Email Type: DoorDash");

                var attachId = emailResponse.Payload.Parts[1].Body.AttachmentId;

                //Need to do another API call to get the actual attachment from the attachment id
                MessagePartBody attachPart = GetAttachment(service, "t4milpitasonline@gmail.com", messageId, attachId);

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

                string decodedBody = Encoding.UTF8.GetString(data);
                doorDashParser.ParseOrder(decodedBody, order);

                ExtractTextFromPdf(filePath);

                order.PrintOrder();
            }
            Console.Read();
        }

        private static MessagePartBody GetAttachment(GmailService service, string userId, string messageId, string attachmentId) {
            try {
                return service.Users.Messages.Attachments.Get(userId, messageId, attachmentId).Execute();
            } catch (Exception e) {
                Console.WriteLine("An error occured: " + e.Message);
            }

            return null;
        }

        public static Message GetMessage(GmailService service, String userId, String messageId) {
            try {
                return service.Users.Messages.Get(userId, messageId).Execute();
            } catch (Exception e) {
                Console.WriteLine("An error occurred: " + e.Message);
            }

            return null;
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


        public static void ExtractTextFromPdf(string path) {

            PdfReader reader = new PdfReader(path);
            PdfDocument doc = new PdfDocument(reader);

            for (int i=1; i<=doc.GetNumberOfPages(); i++) {
                PdfPage page = doc.GetPage(i);
                string text = PdfTextExtractor.GetTextFromPage(page);
                //Console.WriteLine("Page " + i + " text: " + text);
                string[] lines = text.Split('\n');
                for (int j=0; j<lines.Length; j++) {
                    Console.WriteLine("Page " + i + ", Line " + j + " " + lines[j]);
                }
            }


        }

    }
}