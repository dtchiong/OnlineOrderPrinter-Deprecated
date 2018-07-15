using System;
using System.Tuple;
using System.Collections.Generic.Dictionary;

namespace GmailQuickstart {

    public class Menu {



        public Dictionary<string, Tuple<string, string>> MenuTable;

        //Constructor
        public Menu() {
        }

    }

    public class Menu : GrubHubMenu {
        
        //Constructor
        public GrubHubMenu() {
            MenuTable = new Dictionary<string, Tuple<string, string>> {
                { "Pearl Milk Tea", new Tuple<string, string>("Pearl Milk Tea", "Drink")  },
                { "Earl Grey Milk Tea", new Tuple<string, string>("Earl Grey Milk Tea", "Drink")},
                {"Roasted Oolong Milk Tea", new Tuple<string, string>("Roasted Oolong Milk Tea", "Drink") },
                { "Almond Red Bean Milk Tea", new Tuple<string, string>("Almond Red Bean Milk Tea", "Drink")},
                {"Caramel Coffee Milk Tea", new Tuple<string, string>("Caramel Coffee Milk Tea", "Drink") },
                {"Peppermint Milk Tea", new Tuple<string, string>("Peppermint Milk Tea", "Drink") },
                {"French Pudding Milk Tea", new Tuple<string, string>("French Pudding Milk Tea") },
                {"Potted Milk Tea", new Tuple<string, string>("Potted Milk Tea", "Drink") },
                {"Panda Milk Tea", new Tuple<string, string>("Panda Milk Tea", "Drink") },
                {"Fresh Taro Milk Tea", new Tuple<string, string>("Fresh Taro Milk Tea", "Drink") },
                {"Okinawa Milk Tea", new Tuple<string, string>("Okinawa Milk Tea", "Drink") },
                {"Matcha Fresh Milk", new Tuple<string, string>("Matcha Fresh Milk", "Drink") },
                {"Green Tea with Mango Ice Cream", new Tuple<string, string>("Green Tea with Mango Ice Cream") },
                {"Green Pearl Milk Tea", new Tuple<string, string>("Green Pearl Milk Tea", "Drink") },
                {"Jasmine Green Milk Tea", new Tuple<string, string>("Jasmine Green Milk Tea", "Drink") },
                {"Royal Fresh Milk Tea", new Tuple<string, string>("Royal Fresh Milk Tea", "Drink") },
                {"Winter Melon Milk Tea", new Tuple<string, string>("Winter Melon Milk Tea", "Drink") },
                {"Coffee Milk Tea",new Tuple<string, string>("Coffee Milk Tea", "Drink") },
                {"Classical Rose Milk Tea", new Tuple<string, string>("Classical Rose Milk Tea", "Drink") },
                {"Red Bean Milk Tea", new Tuple<string, string>("Red Bean Milk Tea", "Drink") },
                {"Honey Dew Milk Tea", new Tuple<string, string>("Honey Dew Milk Tea", "Drink") },
                {"Thai Milk Tea", new Tuple<string, string>("Thai Milk Tea", "Drink") },
                {"Taro Pearl Milk Tea", new Tuple<string, string>("Taro Pearl Milk Tea", "Drink") },
                {"Mulberry Milk Tea", new Tuple<string, string>("Mulberry Milk Tea", "Drink") },
                {"Brown Sugar Iced Milk with Grass Jelly", new Tuple<string, string>("Brown Sugar Iced Milk with Grass Jelly", "Drink") },
                {"Earl Grey Milk Tea W/3Q", new Tuple<string, string>("Earl Grey Milk Tea with 3Q", "Drink") },
                //Non-Dairy Milk Tea
                {"Soy Milk Tea black Tea", new Tuple<string, string>("Soy Milk Black Tea", "Drink") },
                {"Soy Milk Green Tea", new Tuple<string, string>("Soy Milk Green Tea", "Drink") },
                { "Soy Milk Roasted Oolong Tea", new Tuple<string, string>("Soy Milk Roasted Oolong Tea") },
                //Cream Beverages
                {"Green Tea Topped with Cream", new Tuple<string, string>("Green Tea Topped with Cream", "Drink") },
                {"Oolong Tea Topped with Cream", new Tuple<string, string>("Oolong Tea Topped with Cream", "Drink") },
                {"Mango Royal Tea Topped with Cream", new Tuple<string, string>("Mango Royal Tea Topped with Cream", "Drink") },
                {"WaterMelon Topped with Cream", new Tuple<string, string>("Watermelon Juice Topped with Cream", "Drink") },
                {"Jadeite Royal Tea Topped with Cream", new Tuple<string, string>("Jadeite Royal Tea Topped with Cream", "Drink") },
                {"Chocolate Milk Topped with Cream", new Tuple<string, string>("Chocolate Milk Topped with Cream", "Drink") },
                {"Winter Melon Topped with Cream", new Tuple<string, string>("Winter Melon Topped with Cream", "Drink") },
                //Golden Reputation
                {"Jadeite Royal Tea", new Tuple<string, string>("Jadeite Royal Tea", "Drink") },
                {"Passion Fruit Tea", new Tuple<string, string>("Passion Fruit Tea", "Drink") },
                {"Mango Tea", new Tuple<string, string>("Mango Tea", "Drink") },
                {"Fresh Lime Green Tea", new Tuple<string, string>("Fresh Lime Green Tea", "Drink") },
                {"Earl Grey Tea", new Tuple<string, string>("Earl Grey Tea", "Drink") },
                {"Roasted Oolong Tea", new Tuple<string, string>("Roasted Oolong Tea", "Drink") },
                {"Lychee Tea", new Tuple<string, string>("Lyche Tea","Drink") },
                {"Honey Green Tea W/Aiyu", new Tuple<string, string>("Honey Green Tea with Aiyu", "Drink") },
                {"Pineapple Royal tea", new Tuple<string, string>("Pineapple Royal Tea", "Drink") },
                {"Honey Peach Tea", new Tuple<string, string>("Honey Peach Tea", "Drink") },
                {"Grapefruit Tea", new Tuple<string, string>("Grapefruit Tea", "Drink") },
                {"Elegant Rose Royal Tea", new Tuple<string, string>("Elegant Rose Royal Tea", "Drink") },
                {"Fresh Lemon Bomb Green Tea W/Aloe", new Tuple<string, string>("Fresh Lemon Bomb  Green Tea w/ Aloe") },
                {"Jasmine Green Tea", new Tuple<string, string>("Jasmine Green Tea", "Drink") },
                {"Yogurt Green Tea", new Tuple<string, string>("Yogurt Green Tea", "Drink") },
                {"Mulberry Fruit Tea", new Tuple<string, string>("Mulberry Fruit Tea", "Drink") },
                {"Apple Black Tea", new Tuple<string, string>("Apple Black Tea", "Drink") },
                {"Strawberry Green Tea", new Tuple<string, string>("Strawberry Green Tea", "Drink") },
                //Smoothies
                {"Mango Smoothie W/Pearl", new Tuple<string, string>("Mango Smoothie w/ Pearl", "Drink" },
                {"Honey Peach Smoothie", new Tuple<string, string>("Honey Peach Smoothie", "Drink") },
                {"Lychee Smoothie W/Aloe", new Tuple<string, string>("Lychee Smoothie w/ Aloe", "Drink") },
                {"Passion Fruit Milkshake W/Aiyu", new Tuple<string, string>("Passion Fruit Milkshake w/ Aiyu", "Drink") },
                {"Red Bean Milkshake", new Tuple<string, string>("Red Bean Milkshake", "Drink") },
                {"Avocado Milkshake", new Tuple<string, string>("Avocado Milkshake", "Drink") },
                {"Strawberry Smoothie", new Tuple<string, string>("Strawberry Smoothie", "Drink") },
                {"Fresh Taro Milkshake w/Peal", new Tuple<string, string>("Fresh Taro Milkshake w/ Pearl", "Drink") },
                {"Mulberry Smoothie w/Aiyu", new Tuple<string, string>("Mulberry Smoothie w/Aiyu"), "Drink") },
                {"Matcha Green Tea Smoothie With Red bean", new Tuple<string, string>("Matcha Green Tea Smoothie w/ Red Bean", "Drink") },
                {"Lemon Smoothie with Aloe", new Tuple<string, string>("Lemon Smoothie w/ Aloe", "Drink") },
                {"Passion Fruit Smoothie W/Aloe", new Tuple<string, string>("Passion Fruit Smoothie w/ Aloe", "Drink") },
                {"Chocolate Smoothie", new Tuple<string, string>("Chocolate Smoothie", "Drink") },
                {"Apple Smoothie W/Aloe", new Tuple<string, string>("Apple Smoothie w/ Aloe", "Drink") },
                {"Strawberry Milkshake", new Tuple<string, string>("Strawberry Milkshake", "Drink") },
                {"Strawberry Lemonade Smoothie", new Tuple<string, string>("Strawberry Lemonade Smoothie", "Drink") },
                {"Fresh Mango Smoothie w/Aiyu", new Tuple<string, string>("Fresh Mango Smoothie w/ Aiyu", "Drink") },
                //Taiwan Classic
                {"Hot Grass Jelly Tea", new Tuple<string, string>("Hot Grass Jelly", "Drink") },
                {"Winter Melon Lemon Tea W/Basil Seed", new Tuple<string, string>("Winter Melon Lemon Tea w/ Basil Seed", "Drink") },
                {"Kumquat Lemon Tea W/Basil Seed", new Tuple<string, string>("Kumquat Lemon Tea w/ Basil Seed", "Drink") },
                {"Ginger Milk Tea", new Tuple<string, string>("Ginger Milk Tea", "Drink") },
                {"Honey Aloe Tea", new Tuple<string, string>("Honey Aloe Tea", "Drink") },
                {"Winter Melon Tea", new Tuple<string, string>("Winter Melon Tea", "Drink") },
                {"Honey Grass Jelly Tea", new Tuple<string, string>("Honey Grass Jelly", "Drink") },
                {"Ginger Tea", new Tuple<string, string>("Hot Ginger Tea", "Drink") },
                {"Longan Red Date Tea", new Tuple<string, string>("Longan Red Date Tea", "Drink") },
                //Snacks
                {"Basil Popcorn Chicken", new Tuple<string, string>("Basil Popcorn Chicken", "Snack") },
                {"Tempura Calamari", new Tuple<string, string>("Tempura Calamari", "Snack") },
                {"Honey Chicken Wings", new Tuple<string, string>("Honey Chicken Wings", "Snack") },
                {"Takoyaki", new Tuple<string, string>("Takoyaki", "Snack") },
                {"Salt Pepper Fried Tofu", new Tuple<string, string>("Salt Pepper Fried Tofu", "Snack") },
                {"Fried Lobster Balls", new Tuple<string, string>("Fried Lobster Balls", "Snack") },
                {"Curly Fries", new Tuple<string, string>("Curly Fries", "Snack") },
                {"Egg Puff", new Tuple<string, string>("Egg Puff", "Snack") },
                {"Matcha Egg Puff", new Tuple<string, string>("Matcha Egg Puff", "Snack") },
                {"Red Bean Egg Puff", new Tuple<string, string>("Red Bean Egg Puff", "Snack") },
                {"Honey Dew Egg Puff", new Tuple<string, string>("Honey Dew Egg Puff", "Snack") },
                {"Curly Chicken Karaage", new Tuple<string, string>("Curry Chicken Karaage", "Snack") },
                {"Fried Calamari", new Tuple<string, string>("Fried Calamari", "Snack") },
                {"Garlic Chicken Wings", new Tuple<string, string>("Garlic Chicken Wings", "Snack") },
                {"Curry Fish Balls", new Tuple<string, string>("Curry Fish Balls", "Snack") },
                {"Sweet Potato Fries", new Tuple<string, string>("Sweet Potato Fries", "Snack") },
                {"Volcano French Fries", new Tuple<string, string>("Volcano French Fries", "Snack") },
                {"French Fries", new Tuple<string, string>("French Fries", "Snack") },
                {"Oolong Tea Eggs", new Tuple<string, string>("Oolong Tea Eggs", "Snack") },
                {"Coffee Egg Puff", new Tuple<string, string>("Coffee Egg Puff", "Snack") },
                {"Mocha Egg Puff", new Tuple<string, string>("Mocha Egg Puff", "Snack") },
                {"Chocolate Bean Egg Puff", new Tuple<string, string>("Chocolate Egg Puff", "Snack") },
                {"Five piece Tofu", new Tuple<string, string>("Five Spice Tofu", "Snack") },
                {"Fried Flour Bun with Condensed Milk", new Tuple<string, string>("Fried Flour Bun w/ Condensed Milk", "Snack") },
                //Rice Dish
                {"Basil Popcorn Chicken Over Rice", new Tuple<string, string>("Basil Popcorn Chicken Over Rice", "Snack") },
                {"Japanese Style Eel Over Rice", new Tuple<string, string>("Japanese Style Eel Over Rice", "Snack") },
                {"Minced Beef Over Rice", new Tuple<string, string>("Minced Beef w/ House Sauce Over Rice", "Snack") },
                {"Taiwanese Minced Pork Over Rice", new Tuple<string, string>("Taiwanese Minced Pork Over Rice", "Snack") },
                {"Curry Fish Balls Over Rice", new Tuple<string, string>("Curry Fish Balls Over Rice", "Snack") },
                {"Curry Chicken Karaage Over Rice", new Tuple<string, string>("Curry Chicken Karaage Over Rice", "Snack") },
                //Juices
                {"Watermelon Juice", new Tuple<string, string>("Fresh Water Melon Juice", "Drink") }
            };
            
        }
        
    }

    public class Menu : DoorDashMenu {

        //Constructor
        public DoorDashMenu() {
        }
    }

    public class Menu : UberEatsMenu {

        //Constructor
        public UberEatsMenu() {
        }
    }
}


