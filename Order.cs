using System;
using System.Collections.Generic;

namespace GmailQuickstart {
    public class Order {

        public string Service        { get; set; } //Grubhub,DoorDash, or UberEats
        public string OrderNumber    { get; set; }

        public string CustomerName   { get; set; }
        public string ContactNumber  { get; set; }
        public string DeliverAddress { get; set; }
        
        public int TotalItemCount { get; set; }
        public List<Item> ItemList;

        //Constructor
        public Order() {
            this.Service = null;
            this.OrderNumber = null;
            this.CustomerName = null;
            this.ContactNumber = null;
            this.DeliverAddress = null;
            this.TotalItemCount = 0;
            this.ItemList = new List<Item>();
        }
        
        public void PrintField(string field, string data) {
            if (data != null) {
                Console.WriteLine(field + ": " + data);
            } else {
                Console.WriteLine(field + ": null");
            }
            
        }

        public void PrintField(string field, int data) {
            Console.WriteLine(field + ": " + data);
        }
    }

    public class Item {
        public string ItemType        { get; set; } //Drink or Snack
        public string ItemName        { get; set; }
        public string Size            { get; set; } //Regular or Large
        public bool   IsHot           { get; set; }
        public string SugarLevel      { get; set; } // 100%, 50%, 30%, 0%
        public string IceLevel        { get; set; } // 100%, 80%, 30%, 0%
        public string MilkSubsitution { get; set; } // none, Wholemilk, or Soymilk
        public string SpecialInstructions { get; set; }

        public List<string> AddOnList;

        //Constructor
        public Item() {
            this.ItemType = null;
            this.ItemName = null;
            this.Size       = "Regular";
            this.IsHot      = false;
            this.SugarLevel = "Standard";
            this.IceLevel   = "Standard";
            this.MilkSubsitution = "None";
            this.SpecialInstructions = "None";
        }
    }

    public class GrubHubOrder : Order {
        public bool   IsDelivery         { get; set; } // Could be PickUp
        public string ReadyForPickUpTime { get; set; }
        
        //Constructor
        public GrubHubOrder() {
            this.Service = "GrubHub";
        }

        //Prints all information of the order
        public void PrintOrder() {
            
        }



    }

    public class DoorDashOrder : Order {
    }

    public class UberEatsOrder : Order {
    }
}