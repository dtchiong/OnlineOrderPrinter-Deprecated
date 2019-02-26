using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderPrinter {
    class Requests {
        public delegate void CallBackFunction(string s); //declares the type of the CallBackFunction()

        public static void DoRequest(string request, CallBackFunction cb) {
            cb(request);
        }

        private static HttpClient client = new HttpClient();

        public static async Task ConfirmGrubHubOrder() {
            string data = "testingOMEGALUL";
            HttpContent content = new StringContent(data);

            HttpResponseMessage res = await client.PostAsync(
                "https://api-order-processing-gtm.grubhub.com/order/email/confirm/a5aea472-18ad-4bb8-9233-7d9e6f8f93bc/8b73fb4e-1953-3ae5-85e0-8539dd84609b", content);
            try {
                res.EnsureSuccessStatusCode();
            } catch(Exception e) {
                Debug.WriteLine(e.Message);
            }
            return;
        }
    }
}
