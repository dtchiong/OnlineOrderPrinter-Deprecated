using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderPrinter {
    class Requests {
        private static HttpClient client = new HttpClient();

        //declares the type of type of function ConfirmOrderCallBack()
        public delegate void ConfirmOrderCallBack(OrderContainer orderCon, HttpStatusCode code);


        /* Makes a GET request to confirm the DoorDash order, then calls the callback function with
         * the orderContainer and the retrieved HttpStatus code to update the UI.
         * It seems like the request to confirm the order is the GET request provided in DoorDash's PDF.
         */
        public static async Task ConfirmDoorDashOrder(OrderContainer orderCon, ConfirmOrderCallBack cb) {
            return;
        }
        
        /* Makes a POST request to confirm the GrubHub order, then calls the callback function with
         * the orderContainer and the retrieved HttpStatus code to update the UI.
         * It seems like the request to confirm the order is not the GET request provided in the GrubHub email,
         * but rather the redirected POST request, so we have to change the base URL
         */
        public static async Task ConfirmGrubHubOrder(OrderContainer orderCon, ConfirmOrderCallBack cb) {
            string data = "filler"; //required as the 2nd param for clientAsync, doesn't seem to do anything
            HttpContent content = new StringContent(data);

            HttpResponseMessage res = await client.PostAsync(
                "https://api-order-processing-gtm.grubhub.com/order/email/confirm/a5aea472-18ad-4bb8-9233-7d9e6f8f93bc/8b73fb4e-1953-3ae5-85e0-8539dd84609b", content);
        

            cb(orderCon, res.StatusCode);
            return;
        }


    }
}
