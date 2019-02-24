using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace OnlineOrderPrinter {

    /* The keys stored in these hashtables are the exact string as they appear in the associated menu
     * So these tables serve 2 purposes:
     * - to look up the correct(in case of a typo) and standardized name
     * - to get the type of the item or addon, so that we can set the appropriate fields
     */
    public class Menu {

        /* MenuTable
         * Key - name of item on menu 
         * Value - {Corrected Name, Type of Item = {Drink|Snack} }
         * 
         * AddOnTable
         * Key - name of addon on the menu
         * Value - {Corrected Name, Type of Addon = {Size|Temp|Ice|Sugar|Milk Sub|Topping} }
         */
        public Dictionary<string, Tuple<string, string>> MenuTable;
        public Dictionary<string, Tuple<string, string>> AddOnTable;

        //Constructor
        public Menu() {
        }

        /* Gets the corrected name of the given item */
        public string GetCorrectedItemName(string name) {           
            Tuple<string, string> value;

            if (MenuTable.TryGetValue(name, out value)) {
                return value.Item1;
            }
            return null;
        }

        /* Gets the item type of the given item */
        public string GetItemType(string name) { 
            Tuple<string, string> value;

            if (MenuTable.TryGetValue(name, out value)) {
                return value.Item2;
            }
            return null;
        }

        /* Gets the corrected name of the given addon */
        public string GetCorrectedAddOnName(string name) {
            Tuple<string, string> value;

            if (AddOnTable.TryGetValue(name, out value)) {
                return value.Item1;
            }
            return null;
        }

        /* Gets the type of the given addon */
        public string GetAddOnType(string name) {
            Tuple<string, string> value;

            if (AddOnTable.TryGetValue(name, out value)) {
                return value.Item2;
            }
            return null;
        }
    }

    public class GrubHubMenu : Menu {
        
        //Constructor
        public GrubHubMenu() {
            MenuTable = new Dictionary<string, Tuple<string, string>>() {

                //Milk Tea
                { "Pearl Milk Tea"          , new Tuple<string, string>( null, "Drink")  },
                { "Earl Grey Milk Tea"      , new Tuple<string, string>( null, "Drink")},
                { "Roasted Oolong Milk Tea" , new Tuple<string, string>( null, "Drink") },
                { "Almond Red Bean Milk Tea", new Tuple<string, string>( null, "Drink")},
                { "Caramel Coffee Milk Tea" , new Tuple<string, string>( null, "Drink") },
                { "Peppermint Milk Tea"     , new Tuple<string, string>( null, "Drink") },
                { "French Pudding Milk Tea" , new Tuple<string, string>( null, "Drink") },
                { "Potted Milk Tea"         , new Tuple<string, string>( null, "Drink") },
                { "Panda Milk Tea"          , new Tuple<string, string>( null, "Drink") },
                { "Fresh Taro Milk Tea"     , new Tuple<string, string>( null, "Drink") },
                { "Okinawa Milk Tea"        , new Tuple<string, string>( null, "Drink") },
                { "Matcha Fresh Milk"       , new Tuple<string, string>( null, "Drink") },
                { "Green Tea with Mango Ice Cream", new Tuple<string, string>( null, "Drink") },
                { "Green Pearl Milk Tea"    , new Tuple<string, string>( null, "Drink") },
                { "Jasmine Green Milk Tea"  , new Tuple<string, string>( null, "Drink") },
                { "Royal Fresh Milk Tea"    , new Tuple<string, string>( null, "Drink") },
                { "Winter Melon Milk Tea"   , new Tuple<string, string>( null, "Drink") },
                { "Coffee Milk Tea"         , new Tuple<string, string>( null, "Drink") },
                { "Classical Rose Milk Tea" , new Tuple<string, string>( null, "Drink") },
                { "Red Bean Milk Tea"       , new Tuple<string, string>( null, "Drink") },
                { "Honey Dew Milk Tea"      , new Tuple<string, string>( null, "Drink") },
                { "Thai Milk Tea"           , new Tuple<string, string>( null, "Drink") },
                { "Taro Pearl Milk Tea"     , new Tuple<string, string>( null, "Drink") },
                { "Mulberry Milk Tea"       , new Tuple<string, string>( null, "Drink") },
                { "Brown Sugar Iced Milk with Grass Jelly", new Tuple<string, string>(null, "Drink") },
                { "Earl Grey Milk Tea W/3Q" , new Tuple<string, string>("Earl Grey Milk Tea with 3Q", "Drink") },
                
                //Non-Dairy Milk Tea
                { "Soy Milk Tea black Tea"      , new Tuple<string, string>( "Soy Milk Black Tea", "Drink") },
                { "Soy Milk Green Tea"          , new Tuple<string, string>( null, "Drink") },
                { "Soy Milk Roasted Oolong Tea" , new Tuple<string, string>( null, "Drink") },
                
                //Cream Beverages
                { "Green Tea Topped with Cream"         , new Tuple<string, string>( null, "Drink") },
                { "Oolong Tea Topped with Cream"        , new Tuple<string, string>( null, "Drink") },
                { "Mango Royal Tea Topped with Cream"   , new Tuple<string, string>( null, "Drink") },
                { "WaterMelon Topped with Cream"        , new Tuple<string, string>( "Watermelon Juice Topped with Cream", "Drink") },
                { "Jadeite Royal Tea Topped with Cream" , new Tuple<string, string>( null, "Drink") },
                { "Chocolate Milk Topped with Cream"    , new Tuple<string, string>( null, "Drink") },
                { "Winter Melon Topped with Cream"      , new Tuple<string, string>( null, "Drink") },
                
                //Golden Reputation
                { "Jadeite Royal Tea"      , new Tuple<string, string>( null, "Drink") },
                { "Passion Fruit Tea"      , new Tuple<string, string>( null, "Drink") },
                { "Mango Tea"              , new Tuple<string, string>( null, "Drink") },
                { "Fresh Lime Green Tea"   , new Tuple<string, string>( null, "Drink") },
                { "Earl Grey Tea"          , new Tuple<string, string>( null, "Drink") },
                { "Roasted Oolong Tea"     , new Tuple<string, string>( null, "Drink") },
                { "Lychee Tea"             , new Tuple<string, string>( null,"Drink") },
                { "Honey Green Tea W/Aiyu" , new Tuple<string, string>( "Honey Green Tea with Aiyu", "Drink") },
                { "Pineapple Royal tea"    , new Tuple<string, string>( "Pineapple Royal Tea", "Drink") },
                { "Honey Peach Tea"        , new Tuple<string, string>( null, "Drink") },
                { "Grapefruit Tea"         , new Tuple<string, string>( null, "Drink") },
                { "Elegant Rose Royal Tea" , new Tuple<string, string>( null, "Drink") },
                { "Fresh Lemon Bomb Green Tea W/Aloe", new Tuple<string, string>("Fresh Lemon Bomb Green Tea w/ Aloe", "Drink") },
                { "Jasmine Green Tea"      , new Tuple<string, string>( null, "Drink") },
                { "Yogurt Green Tea"       , new Tuple<string, string>( null, "Drink") },
                { "Mulberry Fruit Tea"     , new Tuple<string, string>( null, "Drink") },
                { "Apple Black Tea"        , new Tuple<string, string>( null, "Drink") },
                { "Strawberry Green Tea"   , new Tuple<string, string>( null, "Drink") },
                { "Guava Green Tea"        , new Tuple<string, string>( null, "Drink") },
                
                //Smoothies
                { "Mango Smoothie W/Pearl"         , new Tuple<string, string>( "Mango Smoothie w/ Pearl", "Drink") },
                { "Honey Peach Smoothie"           , new Tuple<string, string>( null, "Drink") },
                { "Lychee Smoothie W/Aloe"         , new Tuple<string, string>( "Lychee Smoothie w/ Aloe", "Drink") },
                { "Passion Fruit Milkshake W/Aiyu" , new Tuple<string, string>( "Passion Fruit Milkshake w/ Aiyu", "Drink") },
                { "Red Bean Milkshake"             , new Tuple<string, string>( null, "Drink") },
                { "Avocado Milkshake"              , new Tuple<string, string>( null, "Drink") },
                { "Strawberry Smoothie"            , new Tuple<string, string>( null, "Drink") },
                { "Fresh Taro Milkshake w/Peal"    , new Tuple<string, string>( "Fresh Taro Milkshake w/ Pearl", "Drink") },
                { "Mulberry Smoothie w/Aiyu"       , new Tuple<string, string>( "Mulberry Smoothie w/ Aiyu", "Drink") },
                { "Matcha Green Tea Smoothie With Red bean", new Tuple<string,  string>("Matcha Green Tea Smoothie w/ Red Bean", "Drink") },
                { "Lemon Smoothie with Aloe"       , new Tuple<string, string>( null, "Drink") },
                { "Passion Fruit Smoothie W/Aloe"  , new Tuple<string, string>( "Passion Fruit Smoothie w/ Aloe", "Drink") },
                { "Chocolate Smoothie"             , new Tuple<string, string>( null, "Drink") },
                { "Apple Smoothie W/Aloe"          , new Tuple<string, string>( "Apple Smoothie with Aloe", "Drink") },
                { "Strawberry Milkshake"           , new Tuple<string, string>( null, "Drink") },
                { "Strawberry Lemonade Smoothie"   , new Tuple<string, string>( null, "Drink") },
                { "Fresh Mango Smoothie w/Aiyu"    , new Tuple<string, string>( "Fresh Mango Smoothie w/ Aiyu", "Drink") },
                
                //Taiwan Classic
                { "Hot Grass Jelly Tea"                 , new Tuple<string, string>( "Hot Grass Jelly", "Drink") },
                { "Winter Melon Lemon Tea W/Basil Seed" , new Tuple<string, string>( "Winter Melon Lemon Tea w/ Basil Seed", "Drink") },
                { "Kumquat Lemon Tea W/Basil Seed"      , new Tuple<string, string>( "Kumquat Lemon Tea w/ Basil Seed", "Drink") },
                { "Ginger Milk Tea"                     , new Tuple<string, string>( null, "Drink") },
                { "Honey Aloe Tea"                      , new Tuple<string, string>( "Honey Aloe", "Drink") },
                { "Winter Melon Tea"                    , new Tuple<string, string>( null, "Drink") },
                { "Honey Grass Jelly Tea"               , new Tuple<string, string>( "Honey Grass Jelly", "Drink") },
                { "Ginger Tea"                          , new Tuple<string, string>( "Hot Ginger Tea", "Drink") },
                { "Longan Red Date Tea"                 , new Tuple<string, string>( null, "Drink") },
                
                //Snacks
                { "Basil Popcorn Chicken"    , new Tuple<string, string>( null, "Snack") },
                { "Tempura Calamari"         , new Tuple<string, string>( null, "Snack") },
                { "Honey Chicken Wing"       , new Tuple<string, string>( "Honey Chicken Wings", "Snack") },
                { "Takoyaki"                 , new Tuple<string, string>( null, "Snack") },
                { "Salt Pepper Fried Tofu"   , new Tuple<string, string>( null, "Snack") },
                { "Fried Lobster Balls"      , new Tuple<string, string>( null, "Snack") },
                { "Curly Fries"              , new Tuple<string, string>( null, "Snack") },
                { "Egg Puff"                 , new Tuple<string, string>( null, "Snack") },
                { "Matcha Egg Puff"          , new Tuple<string, string>( null, "Snack") },
                { "Red Bean Egg Puff"        , new Tuple<string, string>( null, "Snack") },
                { "Honey Dew Egg Puff"       , new Tuple<string, string>( null, "Snack") },
                { "Curly Chicken Karaage"    , new Tuple<string, string>( "Curry Chicken Karaage", "Snack") },
                { "Fried Calamari"           , new Tuple<string, string>( null, "Snack") },
                { "Garlic Chicken Wings"     , new Tuple<string, string>( null, "Snack") },
                { "Curry Fish Balls"         , new Tuple<string, string>( null, "Snack") },
                { "Sweet Potato Fries"       , new Tuple<string, string>( null, "Snack") },
                { "Volcano French Fries"     , new Tuple<string, string>( null, "Snack") },
                { "French Fries"             , new Tuple<string, string>( null, "Snack") },
                { "Oolong Tea Eggs"          , new Tuple<string, string>( null, "Snack") },
                { "Coffee Egg Puff"          , new Tuple<string, string>( null, "Snack") },
                { "Mocha Egg Puff"           , new Tuple<string, string>( null, "Snack") },
                { "Chocolate Egg Puff"       , new Tuple<string, string>( null, "Snack") },
                { "Five piece Tofu"          , new Tuple<string, string>( "Five Spice Tofu", "Snack") },
                { "Fried Flour Bun with Condensed Milk", new Tuple<string, string>(null, "Snack") },
                
                //Rice Dish
                { "Basil Popcorn Chicken Over Rice" , new Tuple<string, string>( null, "Snack") },
                { "Japanese Style Eel Over Rice"    , new Tuple<string, string>( null, "Snack") },
                { "Minced Beef Over Rice"           , new Tuple<string, string>( "Minced Beef w/ House Sauce Over Rice", "Snack") },
                { "Taiwanese Minced Pork Over Rice" , new Tuple<string, string>( null, "Snack") },
                { "Curry Fish Balls Over Rice"      , new Tuple<string, string>( null, "Snack") },
                { "Curly Chicken Karaage Over Rice" , new Tuple<string, string>( "Curry Chicken Karaage Over Rice", "Snack") },
                
                //Juices
                { "Watermelon Juice", new Tuple<string, string>( "Fresh Water Melon Juice", "Drink") }
            };

            AddOnTable = new Dictionary<string, Tuple<string, string>> {
                
                //Tempurature
                { "Make It Hot", new Tuple<string, string>( "Hot", "Temperature") },
                
                //Size
                { "Up Size", new Tuple<string, string>( "Large", "Size") },
                
                //Ice
                { "0% Ice"  , new Tuple<string, string>( "O% I", "Ice") },
                { "30% Ice" , new Tuple<string, string>( "30% I", "Ice") },
                { "80% Ice" , new Tuple<string, string>( "80% I", "Ice") },
                { "100%Ice" , new Tuple<string, string>( "Standard", "Ice") },
                { "Standard: Between 80% and More Ice", new Tuple<string, string>( "Standard", "Ice") },
                { "More: The Greatest Amount of Ice"  , new Tuple<string, string>( "More"    , "Ice") },

                //Sugar
                { "0% Sweetness"   , new Tuple<string, string>( "0% S", "Sugar") },
                { "30% Sweetness"  , new Tuple<string, string>( "30% S", "Sugar") },
                { "50% Sweetness"  , new Tuple<string, string>( "50% S", "Sugar") },
                { "80% Sweetness"  , new Tuple<string, string>( "80% S", "Sugar") },
                { "100% Sweetness" , new Tuple<string, string>( "Standard", "Sugar") },
                
                //Toppings
                { "Pearls"               , new Tuple<string, string>( null, "Topping") },
                { "Coffee Jelly"         , new Tuple<string, string>( null, "Topping") },
                { "Pudding"              , new Tuple<string, string>( null, "Topping") },
                { "Aloe"                 , new Tuple<string, string>( null, "Topping") },
                { "Sea salt creama"      , new Tuple<string, string>( "Sea Salt Creama", "Topping") },
                { "HoneyDew Poping boba" , new Tuple<string, string>( "Honeydew Popping Boba", "Topping") },
                { "Coconut Jelly"        , new Tuple<string, string>( null, "Topping") },
                { "Grass Jelly"          , new Tuple<string, string>( null, "Topping") },
                { "Red Bean"             , new Tuple<string, string>( null, "Topping") },
                { "Potted"               , new Tuple<string, string>( null, "Topping") },
                { "Oreo"                 , new Tuple<string, string>( null, "Topping") },
                { "Cheese creama"        , new Tuple<string, string>( "Cheese Creama", "Topping") },
                { "Rainbow Jelly"        , new Tuple<string, string>( null, "Topping") },
                { "Fig Jelly"            , new Tuple<string, string>( null, "Topping") },
                { "Taro"                 , new Tuple<string, string>( null, "Topping") },
                { "Angel White Pearl"    , new Tuple<string, string>( "Angel Pearls", "Topping") },
                { "Basil Seed"           , new Tuple<string, string>( null, "Topping") },
                
                //Milk Subsitute
                { "Substitute with Whole Milk" , new Tuple<string, string>("Whole Milk Sub", "Milk Subsitute") },
                { "Substitute with Soy Milk"   , new Tuple<string, string>("Soy Milk Sub"  , "Milk Subsitute") },

                //Tea
                { "Black Tea"  , new Tuple<string, string>(null, "Tea") },
                { "Green Tea"  , new Tuple<string, string>(null, "Tea") },
                { "Oolong Tea" , new Tuple<string, string>(null, "Tea") },
                { "Royal Tea"  , new Tuple<string, string>(null, "Tea") },

                //Rice Addons
                { "Beef"            , new Tuple<string, string>(null, "Food Addons") },
                { "Corn"            , new Tuple<string, string>(null, "Food Addons") },
                { "Extra Sauce"     , new Tuple<string, string>(null, "Food Addons") },
                { "Extra Vegetable" , new Tuple<string, string>(null, "Food Addons") },
                { "Green Onion"     , new Tuple<string, string>(null, "Food Addons") },
                { "Pork"            , new Tuple<string, string>(null, "Food Addons") },
                { "Semi-Soft Egg( 1 whole egg)", new Tuple<string, string>("Semi-Soft Egg", "Food Addons") },

                //Ramen Addons
                { "Charshu (1 Pc)" , new Tuple<string, string>(null, "Food Addons") },
                { "Extra Noodles"  , new Tuple<string, string>(null, "Food Addons") },
                { "Extra Soup"     , new Tuple<string, string>(null, "Food Addons") },
                { "Extra Spicy"    , new Tuple<string, string>(null, "Food Addons") },
                { "Seaweed"        , new Tuple<string, string>(null, "Food Addons") },
                { "Semi-Soft Egg (1 Whole Egg)", new Tuple<string, string>("Semi-Soft Egg", "Food Addons") },
            };    
        }     
    }

    public class DoorDashMenu : Menu {

        //Constructor
        public DoorDashMenu() {
            MenuTable = new Dictionary<string, Tuple<string, string>>() {

                //Milk Tea
                { "Pearl Milk Tea"             , new Tuple<string, string>( null, "Drink")  },
                { "Earl Grey Milk Tea with 3Q" , new Tuple<string, string>( null, "Drink")},
                { "Jasmine Green Milk Tea"     , new Tuple<string, string>( null, "Drink") },
                { "Roasted Oolong Milk Tea"    , new Tuple<string, string>( null, "Drink") },
                { "Panda Milk Tea"             , new Tuple<string, string>( null, "Drink") },
                { "Classical Rose Milk Tea"    , new Tuple<string, string>( null, "Drink") },
                { "Winter Melon Milk Tea"      , new Tuple<string, string>( null, "Drink") },
                { "Red Bean Milk Tea"          , new Tuple<string, string>( null, "Drink") },
                { "Thai Milk Tea"              , new Tuple<string, string>( null, "Drink") },
                { "Fresh Taro Milk Tea"        , new Tuple<string, string>( null, "Drink") },
                { "Taro Pearl Milk Tea"        , new Tuple<string, string>( null, "Drink") },
                { "Okinawa Milk Tea"           , new Tuple<string, string>( null, "Drink") },
                { "Potted Milk Tea"            , new Tuple<string, string>( null, "Drink") },
                { "Caramel Milk Tea"           , new Tuple<string, string>( null, "Drink") },
                { "Brown Sugar Iced Milk with Grass Jelly", new Tuple<string, string>(null, "Drink") },
                { "Coffee Milk Tea"            , new Tuple<string, string>( null, "Drink") },
                { "Caramel Coffee Milk Tea"    , new Tuple<string, string>( null, "Drink") },
                { "Mulberry Milk Tea"          , new Tuple<string, string>( null, "Drink") },
                { "Matcha Latte"               , new Tuple<string, string>( null, "Drink") },
                { "Peppermint Milk Tea"        , new Tuple<string, string>( null, "Drink") },
                { "Royal Fresh Milk Tea"       , new Tuple<string, string>( null, "Drink") },
                { "French Pudding Milk Tea"    , new Tuple<string, string>( null, "Drink") },
                /*
                { "Almond Red Bean Milk Tea", new Tuple<string, string>( null, "Drink")},
                { "Green Tea with Mango Ice Cream", new Tuple<string, string>( null, "Drink") },                
                { "Honey Dew Milk Tea"      , new Tuple<string, string>( null, "Drink") },
                */

                //Cream Beverages
                { "Earl Grey Tea with Topped Cream"    , new Tuple<string, string>( null, "Drink") },
                { "Green Tea with Topped Cream"        , new Tuple<string, string>( null, "Drink") },
                { "Oolong Tea with Topped Cream"       , new Tuple<string, string>( null, "Drink") },
                { "Jadeite Tea with Topped Cream"      , new Tuple<string, string>( null, "Drink") },
                { "Winter Melon Tea with Topped Cream" , new Tuple<string, string>( null, "Drink") },
                { "Mango Royal Tea with Topped Cream"  , new Tuple<string, string>( null, "Drink") },
                { "Watermelon Juice with Topped Cream" , new Tuple<string, string>( null, "Drink") },

                //Golden Reputation
                { "Honey Peach Tea"                   , new Tuple<string, string>( null, "Drink") },
                { "Elegant Peach Royal Tea with Aloe" , new Tuple<string, string>( null, "Drink") },
                { "Passion Fruit Tea"                 , new Tuple<string, string>( null, "Drink") },
                { "Grapefruit Tea"                    , new Tuple<string, string>( null, "Drink") },
                { "Mango Tea with Ai Yu"              , new Tuple<string, string>( null, "Drink") },
                { "Elegant Rose Royal Tea with Aloe"  , new Tuple<string, string>( null, "Drink") },
                { "Mulberry Fruit Tea"                , new Tuple<string, string>( null, "Drink") },
                { "Pineapple Royal Tea"               , new Tuple<string, string>( null, "Drink") },
                { "Jadeite Royal Tea"                 , new Tuple<string, string>( null, "Drink") },
                { "Guava Green Tea"                   , new Tuple<string, string>( null, "Drink") },

                //Special Flavor
                { "Earl Grey Tea"              , new Tuple<string, string>( null, "Drink") },
                { "Jasmine Green Tea"          , new Tuple<string, string>( null, "Drink") },
                { "Roasted Oolong Tea"         , new Tuple<string, string>( null, "Drink") },
                { "Lychee Tea"                 , new Tuple<string, string>( null, "Drink") },
                { "Apple Tea"                  , new Tuple<string, string>( null, "Drink") },
                { "Honey Green Tea with Ai Yu" , new Tuple<string, string>( null, "Drink") },
                { "Yogurt Green Tea"           , new Tuple<string, string>( null, "Drink") },
                { "Strawberry Green Tea"       , new Tuple<string, string>( null, "Drink") }, //not in menu yet

                //Smoothies
                { "Fresh Mango Smoothie with Ai Yu"         , new Tuple<string, string>( null, "Drink") },
                { "Mango Smoothie with Pearl"               , new Tuple<string, string>( null, "Drink") },
                { "Fresh Taro Milkshake with Pearl"         , new Tuple<string, string>( null, "Drink") },
                { "Matcha Green Tea Smoothie with Red Bean" , new Tuple<string,  string>(null, "Drink") },
                { "Honey Peach Smoothie"                    , new Tuple<string, string>( null, "Drink") },
                { "Lychee Smoothie with Aloe"               , new Tuple<string, string>( null, "Drink") },
                { "Apple Smoothie with Aloe"                , new Tuple<string, string>( null, "Drink") },
                { "Passion Fruit Smoothie with Ai Yu"       , new Tuple<string, string>( null, "Drink") },
                { "Passion Fruit Milkshake with Ai Yu"      , new Tuple<string, string>( null, "Drink") },
                { "Strawberry Smoothie"                     , new Tuple<string, string>( null, "Drink") },
                { "Strawberry Milkshake"                    , new Tuple<string, string>( null, "Drink") },
                { "Strawberry Lemon Smoothie with Ai Yu"    , new Tuple<string, string>( null, "Drink") },
                { "Pineapple Smoothie with Ai Yu"           , new Tuple<string, string>( null, "Drink") },
                { "Avocado Milkshake with Pearl"            , new Tuple<string, string>( null, "Drink") },
                { "Red Bean Milkshake"                      , new Tuple<string, string>( null, "Drink") },
                { "Chocolate Smoothie"                      , new Tuple<string, string>( null, "Drink") },
                { "Coconut Smoothie with Red Bean"          , new Tuple<string, string>( null, "Drink") },
                /*
                { "Mulberry Smoothie w/Aiyu"       , new Tuple<string, string>( "Mulberry Smoothie w/ Aiyu", "Drink") },
                { "Lemon Smoothie with Aloe"       , new Tuple<string, string>( null, "Drink") },
                */

                //Fresh Fruit Series
                { "Fresh Lime Green Tea with Aloe"        , new Tuple<string, string>( null, "Drink") },
                { "Fresh Lemon Bomb Green Tea with Aloe"  , new Tuple<string, string>( null, "Drink") },
                { "Fresh Watermelon Juice"                , new Tuple<string, string>( null, "Drink") },
                { "Honey Lemon Green Tea with Basil Seed" , new Tuple<string, string>( null, "Drink") },
                
                //Taiwan Classic
                { "Hot Grass Jelly"                    , new Tuple<string, string>( null, "Drink") },
                { "Honey Grass Jelly"                  , new Tuple<string, string>( null, "Drink") },
                { "Kumquat Lemon with Basil Seed"      , new Tuple<string, string>( null, "Drink") },
                { "Kumquat Lemon"                      , new Tuple<string, string>( null, "Drink") },
                { "Winter Melon Lemon with Basil Seed" , new Tuple<string, string>( null, "Drink") },
                { "Winter Melon Tea"                   , new Tuple<string, string>( null, "Drink") },
                { "Ginger Milk Tea"                    , new Tuple<string, string>( null, "Drink") },
                { "Longan Red Date Tea"                , new Tuple<string, string>( null, "Drink") },
                { "Honey Aloe"                         , new Tuple<string, string>( null, "Drink") },
                { "Ginger Tea"                         , new Tuple<string, string>( null, "Drink") }, //not in menu yet
                
                //Snacks
                { "Basil Popcorn Chicken"    , new Tuple<string, string>( null, "Snack") },
                { "Curry Chicken Karaage"    , new Tuple<string, string>( null, "Snack") },
                { "Fried Calamari"           , new Tuple<string, string>( null, "Snack") },
                { "Tempura Calamari"         , new Tuple<string, string>( null, "Snack") },
                { "Chicken Wings"            , new Tuple<string, string>( null, "Snack") },
                { "Garlic Chicken Wings"     , new Tuple<string, string>( null, "Snack") },
                { "Honey Chicken Wings"      , new Tuple<string, string>( null, "Snack") },
                { "Salt & Pepper Fried Tofu" , new Tuple<string, string>( null, "Snack") },
                { "Five Spice Tofu"          , new Tuple<string, string>( null, "Snack") },
                { "Curry Fish Balls"         , new Tuple<string, string>( "Curry Fish Balls (9)", "Snack") },
                { "Takoyaki"                 , new Tuple<string, string>( "Takoyaki (6)", "Snack") },
                { "Fried Cheese Sticks"      , new Tuple<string, string>( null, "Snack") },
                { "Fried Lobster Balls"      , new Tuple<string, string>( "Fried Lobster Balls (8)", "Snack") },
                { "French Fries"             , new Tuple<string, string>( null, "Snack") },
                { "Sweet Potato Fries"       , new Tuple<string, string>( null, "Snack") },
                { "Volcano Fries"            , new Tuple<string, string>( null, "Snack") },
                { "Curly Fries"              , new Tuple<string, string>( null, "Snack") },
                { "Fried Flour Bun"          , new Tuple<string, string>( null, "Snack") },
                { "Oolong Tea Eggs"          , new Tuple<string, string>( null, "Snack") },
                { "Egg Puff (Original)"      , new Tuple<string, string>( null, "Snack") },
                { "Egg Puff (Flavored)"      , new Tuple<string, string>( null, "Snack") },
                { "Egg Puff"                 , new Tuple<string, string>( null, "Snack") },
                { "Chocolate Egg Puff"       , new Tuple<string, string>( null, "Snack") },
                { "Coffee Egg Puff"          , new Tuple<string, string>( null, "Snack") },
                { "Honeydew Egg Puff"        , new Tuple<string, string>( null, "Snack") },
                { "Matcha Egg Puff"          , new Tuple<string, string>( null, "Snack") },
                { "Mocha Egg Puff"           , new Tuple<string, string>( null, "Snack") },
                { "Red Bean Egg Puff"        , new Tuple<string, string>( null, "Snack") },
                { "Taro Egg Puff"            , new Tuple<string, string>( null, "Snack") },

                //Rice Dish
                { "Minced Beef with House Sauce Over Rice" , new Tuple<string, string>( null, "Snack") },
                { "Curry Chicken Karaage Over Rice"        , new Tuple<string, string>( null, "Snack") },
                { "Basil Popcorn Chicken Over Rice"        , new Tuple<string, string>( null, "Snack") },
                { "Taiwanese Minced Pork Over Rice"        , new Tuple<string, string>( null, "Snack") },
                { "Japanese Style Eel Over Rice"           , new Tuple<string, string>( null, "Snack") },
                { "Curry Fish Balls Over Rice"             , new Tuple<string, string>( null, "Snack") },
            };

        }
    }

    public class UberEatsMenu : Menu {

        //Constructor
        public UberEatsMenu() {
        }
    }
}


