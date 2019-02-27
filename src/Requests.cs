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

        //Declares the type of type of function ConfirmOrderCallBack()
        public delegate void ConfirmOrderCallBack(OrderContainer orderCon, HttpStatusCode code);

        /* The DoorDash confirm api is a GET request, so we make that request 
         * to confirm the DoorDash order, then we call the callback function with
         * the orderContainer and the retrieved HttpStatus code to update the UI.
         */
        public static async Task ConfirmDoorDashOrder(OrderContainer orderCon, ConfirmOrderCallBack cb) {
            string apiURL = orderCon.Order.ConfirmURL;

            HttpResponseMessage res = await client.GetAsync(apiURL);
            cb(orderCon, res.StatusCode);
            return;
        }
        
        /* The GrubHub confirm api is a POST request, so we make that request
         * to confirm the GrubHub order, then we call the callback function with
         * the orderContainer and the retrieved HttpStatus code to update the UI.
         */
        public static async Task ConfirmGrubHubOrder(OrderContainer orderCon, ConfirmOrderCallBack cb) {
            string apiURL = orderCon.Order.ConfirmURL;

            //The 2nd StringContent param is required for PostAsync, it doesn't seem to do anything though
            HttpResponseMessage res = await client.PostAsync(apiURL, new StringContent(""));

            cb(orderCon, res.StatusCode);
            return;
        }
    }
}
