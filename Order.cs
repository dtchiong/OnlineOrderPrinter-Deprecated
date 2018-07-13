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
        public string ItemType        { get; set; } // Drink or Snack
        public string ItemName        { get; set; }
        public int    Quantity        { get; set; }
        public string Size            { get; set; } // Regular or Large
        public string Temperature     { get; set; } // Cold or Hot
        public string SugarLevel      { get; set; } // 100%, 50%, 30%, 0%
        public string IceLevel        { get; set; } // 100%, 80%, 30%, 0%
        public string MilkSubsitution { get; set; } // null, Wholemilk, or Soymilk
        public string SpecialInstructions { get; set; }
        public string Price           { get; set; }

        public List<string> AddOnList;

        //Constructor
        public Item() {
            this.ItemType    = null;
            this.ItemName    = null;
            this.Quantity    = 1;
            this.Size        = "Regular";
            this.Temperature = "Cold";
            this.SugarLevel  = "Standard";
            this.IceLevel    = "Standard";
            this.MilkSubsitution = null;
            this.SpecialInstructions = null;
            this.AddOnList   = null;
            this.Price       = null;
        }
    }

    public class GrubHubOrder : Order {
        public string DeliveryMethod     { get; set; } // Delivery or Pickup
        public string ReadyForPickUpTime { get; set; }
        
        //Constructor
        public GrubHubOrder() {
            this.Service = "GrubHub";
        }

        //Prints all information of the order
        public void PrintOrder() {
            PrintField("Service", this.Service);
            PrintField("Order Number", this.OrderNumber);
            PrintField("Delivery Method", this.DeliveryMethod);

            Console.WriteLine("-----------------------");

            PrintField("CustomerName", this.CustomerName);
            PrintField("Contact Number", this.ContactNumber);

            if (this.DeliveryMethod == "Delivery")
                PrintField("Delivery Address", this.DeliverAddress);

            PrintField("Total Item Count", this.TotalItemCount);

            Console.WriteLine("-----------------------");

            for (int i=0; i<ItemList.Count; i++) {
                var item = ItemList[i];
                Console.WriteLine("Item " + i + 1 + ":");

                PrintField("Item Name", item.ItemName);
                PrintField("Quantity", item.Quantity);

                if (item.ItemType == "Drink") {
                    PrintField("Size", item.Size);
                    if (item.Temperature == "Hot")
                        PrintField("Temperature", item.Temperature);
                    PrintField("Temperature", item.Temperature);
                    PrintField("Sugar Level", item.SugarLevel);
                    PrintField("Ice Level", item.IceLevel);
                    if (item.MilkSubsitution != null) {
                        PrintField("Milk Subsitution", item.MilkSubsitution);
                    }   
                }

                Console.WriteLine("Add Ons: ");
                foreach (var addOn in item.AddOnList) {
                    Console.WriteLine("   " + addOn);
                }

                Console.WriteLine(); 
            }



        }



    }

    public class DoorDashOrder : Order {
    }

    public class UberEatsOrder : Order {
    }
}