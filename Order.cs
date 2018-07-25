using System;
using System.Collections.Generic;

namespace GmailQuickstart {
    
    /* Base class for orders */
    public class Order {

        public const int Sixteen = 16; //used to format printing with padding
        public const int Twelve  = 12;

        public string Service        { get; set; } //Grubhub,DoorDash, or UberEats
        public string OrderNumber    { get; set; }

        public string CustomerName   { get; set; }
        public string ContactNumber  { get; set; }
        public string DeliverAddress { get; set; }
        
        public int TotalItemCount { get; set; }
        public List<Item> ItemList;

        //Constructor
        public Order() {
            this.Service        = null;
            this.OrderNumber    = null;
            this.CustomerName   = null;
            this.ContactNumber  = null;
            this.DeliverAddress = null;
            this.TotalItemCount = 0;
            this.ItemList       = new List<Item>();
        }
        
        public void PrintField(string field, string data, int padAmount) {
            if (data != null) {
                Console.WriteLine(field.PadRight(padAmount) + ": " + data);
            } else {
                Console.WriteLine(field.PadRight(padAmount) + ": null");
            }
            
        }

        public void PrintField(string field, int data, int padAmount) {
            Console.WriteLine(field.PadRight(padAmount) + ": " + data);
        }
    }

    public class Item {
        public string ItemCount       { get; set; } // "Item Index / Total Items"
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
            this.DeliveryMethod = "Pickup";
            this.ReadyForPickUpTime = null;
        }

        //Prints all information of the order
        public void PrintOrder() {
            PrintField("Service"        , this.Service, Sixteen);
            PrintField("Order Number"   , this.OrderNumber, Sixteen);
            PrintField("Delivery Method", this.DeliveryMethod, Sixteen);

            Console.WriteLine("-----------------------");

            PrintField("Customer Name" , this.CustomerName, Sixteen);
            PrintField("Contact Number", this.ContactNumber, Sixteen);

            if (this.DeliveryMethod == "Delivery")
                PrintField("Delivery Address", this.DeliverAddress, Sixteen);

            PrintField("Total Item Count", this.TotalItemCount, Sixteen);

            Console.WriteLine("-----------------------");

            for (int i=0; i<ItemList.Count; i++) {
                var item = ItemList[i];
  
                PrintField("Item Count", item.ItemCount, Twelve);
                PrintField("Item Name", item.ItemName, Twelve);
                PrintField("Item Type", item.ItemType, Twelve);
                PrintField("Quantity" , item.Quantity, Twelve);

                if (item.ItemType == "Drink") {
                    PrintField("Size", item.Size, Twelve);
                    PrintField("Temperature", item.Temperature, Twelve);
                    PrintField("Sugar Level", item.SugarLevel, Twelve);
                    PrintField("Ice Level"  , item.IceLevel, Twelve);
                    if (item.MilkSubsitution != null) {
                        PrintField("Milk Subsitution", item.MilkSubsitution, Twelve);
                    }   
                }

                bool AddOnListIsEmpty = item.AddOnList == null || item.AddOnList.Count == 0;
                if (!AddOnListIsEmpty) {
                    Console.WriteLine("Add Ons".PadRight(Twelve) + ":");
                    foreach (var addOn in item.AddOnList) {
                        Console.WriteLine("---" + addOn + "---");
                    }
                }
                PrintField("Instructions", item.SpecialInstructions, Twelve);
                PrintField("Price", item.Price, Twelve);
            
                Console.WriteLine(); 
            }
        }
    }

    public class DoorDashOrder : Order {

        //Constructor
        public DoorDashOrder() {
            this.Service = "DoorDash";
        }

        public void PrintOrder() {
            Console.WriteLine("DoorDash.PrintOrder() - Unimplemented");
        }
    }

    public class UberEatsOrder : Order {
    }
}