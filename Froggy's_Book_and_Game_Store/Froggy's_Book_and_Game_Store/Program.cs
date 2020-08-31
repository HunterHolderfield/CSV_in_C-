using System;
using System.Collections.Generic;
using System.IO;


namespace Froggy_s_Book_and_Game_Store
{
    class Program
    {
        public static double itemPrice { get; private set; }
        public class CartItem
        {
            public string itemPurchased;
            public double Qty;
            public CartItem (string itemName, double qty, double ItemPrice)
            {
                itemPurchased = itemName;
                qty = Qty;
                ItemPrice = itemPrice;

            }

            public string ItemPurchased { get => itemPurchased; set => itemPurchased = value; }
            public double ItemPrice { get; private set; }
            public double qty { get => Qty; set => Qty = value; }
        }

        public class Customer
        {
            //Grabbing Users Name
            private string _name;
            public List<CartItem> cartItems = new List<CartItem>();
            public Customer(string name)
            {
                Name = name;

            }

            string user = Console.ReadLine();
            public string Name
            {
                get => _name;
                set => _name = value;
            }
        }

        public class Products
        {
            private string itemCode, itemName, description;
            private double price;
            
            public string ItemCode { get => itemCode; set => itemCode = value; }
            public string ItemName { get => itemName; set => itemName = value; }
            public string Description { get => description; set => description = value; }
            public double Price { get => price; set => price = value; }

            public Products(string itemCode, string itemName, string description, double price)
            {
                ItemCode = itemCode;
                ItemName = itemName;
                Description = description;
                Price = price;


            }


        }

        public class Books : Products
         {
            private string author;
            public string Author { get => author; set => author = value; }
            public Books(string itemCode, string itemName, string description, double price, string author ) :base (itemCode, itemName, description, price)
            {
                Author = author;
            }

         }
        public class Games : Products
            {
            private string rating;
            public string Rating { get => rating; set => rating = value; }
            public Games(string itemCode, string itemName, string description, double price, string rating) : base(itemCode, itemName, description, price)
            {
                Rating = rating;
            }
        }
           static void Main(string[] args)
            {
                
            Console.WriteLine("WELCOME TO FROGGY'S BOOOK & GAME STORE!");

            //Getting user's name
            Console.WriteLine("Please enter your name... ");
            string userName = Console.ReadLine();
            Customer user = new Customer(userName);

            Console.WriteLine("Here's everything that we have in our store: ");
            Console.WriteLine("\tBooks Available: ");

            List<Books> Books = new List<Books>();
            using (StreamReader scan = new StreamReader(@"Books.txt"))
                  {
                    while (scan.Peek() >= 0)
                     {
                        string book;
                        string[] books;
                        book = scan.ReadLine();
                        books = book.Split(",");
                        Books thisBook = new Books(books[0], books[1], books[2], Convert.ToDouble(books[3]), books[4]);
                        Books.Add(thisBook);


                     }
                    scan.Close();
                  }
            foreach (Books book in Books)
            {
                Console.WriteLine(book.ItemCode + " " + book.ItemName + "\n");
            }

            Console.WriteLine("\tGames Available: ");
            List<Games> Games = new List<Games>();
            using (StreamReader scan = new StreamReader(@"Games.txt"))
            {
                while (scan.Peek() >= 0)
                {
                    string game;
                    string[] games;
                    game = scan.ReadLine();
                    games = game.Split(",");
                    Games thisGame = new Games(games[0], games[1], games[2], Convert.ToDouble(games[3]), games[4]);
                    Games.Add(thisGame);
                }
                scan.Close();
            }
            foreach (Games game in Games)
            {
                Console.WriteLine(game.ItemCode + " " + game.ItemName + "\n");
            }

            Choose(Games, Books, user);
            Console.ReadLine();

            }
        public static void Choose(List<Games> Games, List<Books> Books, Customer user)
        {
            Console.WriteLine("Which item would you like to select? ");
            string choose = Console.ReadLine();
            double choosePrice = 0;
            foreach (Games game in Games)
            {
                if (choose.Equals(game.ItemCode))
                {
                    choose = game.ItemName;
                    choosePrice = game.Price;
                }
                else
                {
                    foreach (Books book in Books)
                    {
                        if (choose.Equals(book.ItemCode))
                        {
                            choose = book.ItemName;
                            choosePrice = book.Price;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

            }
            Console.WriteLine("You picked {0}. How many would you like? ", choose);
            string qty = Console.ReadLine();

            Console.WriteLine("Qty: {0}. " +
                "            \nItem: {1}" +
                "            \nPrice: {2}" +
                "            \nSubTotal: {3}\n\n", qty, choose, choosePrice, choosePrice * Convert.ToDouble(qty));

            CartItem cart = new CartItem(choose, Convert.ToDouble(qty), itemPrice);
            user.cartItems.Add(cart);



            Console.WriteLine("Would you like something else? Y or N");
            string choice = Console.ReadLine();
            if (choice.Equals("Y"))
            {
                Choose(Games, Books, user);
            }
            else
            {
                Console.WriteLine("Thank you for your purchase, your reciept is printing...");

               string reciept = @"Reciept" + user.Name + ".txt";


                try
                {

                    if (File.Exists(reciept))
                    {
                        File.Delete(reciept);
                    }
                    using (StreamWriter write = File.CreateText(reciept))
                    {
                        write.WriteLine("*****************" +
                                "\nThank you {0} for your purchase!" +
                                "\n*****************", user.Name);
                        write.WriteLine("Qty:\tItem\tPrice\tTotal");
                        foreach (CartItem item in user.cartItems)
                        {
                            double total = itemPrice * item.Qty;
                            write.WriteLine(item.Qty + "\t" + item.itemPurchased + "\t" + itemPrice + "\t" + total);
                        }
                    }
                }
                catch (Exception oop)
                {
                    Console.WriteLine(oop.ToString());
                }
                    

            }

        }
      }
   }

