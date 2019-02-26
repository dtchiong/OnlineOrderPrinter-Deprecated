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
            //[0]: "https, [1]: "", [2]: "orderemails.grubhub.com", [3]: {part1}, [4]: {part2}
            string[] splitURL = orderCon.Order.ConfirmURL.Split('/');
            string path = splitURL[3] + "/" + splitURL[4]; //reform the parts we need
            string apiURL = "https://api-order-processing-gtm.grubhub.com/order/email/confirm/" + path;

            //The 2nd StringContent param is required for PostAsync, it doesn't seem to do anything though
            HttpResponseMessage res = await client.PostAsync(apiURL, new StringContent(""));

            cb(orderCon, res.StatusCode);
            return;
        }
    }
}
