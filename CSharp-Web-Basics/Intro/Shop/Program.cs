
namespace Shop
{
    using System;
    using System.Linq;
    using Shop.Database;
    using Shop.Models;

    public class Program
    {
        public static void Main()
        {
            using (ShopDbContext db = new ShopDbContext())
            {
                ClearDatabase(db);
                FillSalesMan(db);
                FillItems(db);
                ParseCommands(db);
                //PrintSalesmanWithCustomersCount(db);
                //PrintCustomersWithTheirOrdersAndReviews(db);

                // 7. Shop Hierarchy – Complex Query -->
                //PrintCustomersNumbersOfItemforEachOrder(db);

                // 8. Explicit Data Loading
                //ExplicitDataLoading(db);

                // 9. Query Loaded Data
                PrintNumberOfOrdersWithMoreThanOneItem(db);
            }
        }

        private static void PrintNumberOfOrdersWithMoreThanOneItem(ShopDbContext db)
        {
            var customerId = int.Parse(Console.ReadLine());
            var customerOrders = db.Orders
                .Where(c => c.CustomerId == customerId)
                .Where(i => i.Items.Count > 1)
                .Count();

            Console.WriteLine($"Orders: {customerOrders}");
        }

        private static void ExplicitDataLoading(ShopDbContext db)
        {
            var customerId = int.Parse(Console.ReadLine());
            var customer = db.Customers
                .Where(e => e.Id == customerId)
                .Select(c => new
                {
                    c.Name,
                    OrdersCount = c.Orders.Count,
                    ReviewCount = c.Reviews.Count,
                    Salesman = c.Salesman.Name
                }).FirstOrDefault();

            Console.WriteLine($"Customer: {customer.Name}");
            Console.WriteLine($"Orders count: {customer.OrdersCount}");
            Console.WriteLine($"Reviews count: {customer.ReviewCount}");
            Console.WriteLine($"Salesman: {customer.Salesman}");
        }

        private static void PrintCustomersNumbersOfItemforEachOrder(ShopDbContext db)
        {
            var customerId = int.Parse(Console.ReadLine());
            var customer = db.Customers
                .Where(e => e.Id == customerId)
                .Select(c => new
                {
                    Orders = c
                         .Orders
                         .Select(o => new
                        {
                            o.Id,
                            ItemsCount = o.Items.Count
                        })
                        .OrderBy(a => a.Id),
                    ReviewsCount = c.Reviews.Count
                })
                .FirstOrDefault();
                
            foreach (var order in customer.Orders)
            {
                Console.WriteLine($"order {order.Id}: {order.ItemsCount} items");
            }
            Console.WriteLine($"reviews: {customer.ReviewsCount}");
        }

        private static void FillItems(ShopDbContext db)
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "END")
                {
                    break;
                }
                var splited = input.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries);
                var itemName = splited[0];
                var itemPrice = decimal.Parse(splited[1]);

                Item item = new Item
                {
                    Name = itemName,
                    Price = itemPrice
                };

                db.Items.Add(item);
            }
            db.SaveChanges();
        }

        private static void PrintCustomersWithTheirOrdersAndReviews(ShopDbContext db)
        {
            var customers = db.Customers
                .Select(c => new
                {
                    c.Name,
                    Orders = c.Orders.Count,
                    Reviews = c.Reviews.Count
                })
                .OrderByDescending(c => c.Orders)
                .ThenByDescending(c => c.Reviews);

            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Name}");
                Console.WriteLine($"Orders: {customer.Orders}");
                Console.WriteLine($"Reviews: {customer.Reviews}");
            }
        }

        private static void PrintSalesmanWithCustomersCount(ShopDbContext db)
        {
            var salesmen = db.Salesmen
                .Select(s => new
                {
                    s.Name,
                    Customers = s.Customers.Count
                })
                .OrderByDescending(s => s.Customers)
                .ThenBy(s => s.Name);

            foreach (var salesman in salesmen)
            {
                Console.WriteLine($"{salesman.Name} - {salesman.Customers} customers");
            }
        }

        private static void ParseCommands(ShopDbContext db)
        {
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "END")
                {
                    break;
                }
                var splited = input.Split(new[] {'-', ';'}, StringSplitOptions.RemoveEmptyEntries);
                var command = splited[0];
                var cmdArgs = splited.Skip(1).ToArray();
                switch (command)
                {
                    case "register":
                        RegisterCommand(db, cmdArgs);
                        break;
                    case "order":
                        OrderCommand(db, cmdArgs);
                        break;
                    case "review":
                        ReviewCommand(db, cmdArgs);
                        break;
                }
            }
        }

        private static void ReviewCommand(ShopDbContext db, string[] cmdArgs)
        {
            var customerId = int.Parse(cmdArgs[0]);
            var itemId = int.Parse(cmdArgs[1]);
            
            Review review = new Review
            {
                CustomerId = customerId,
                ItemId = itemId
            };
            
            db.Reviews.Add(review);
            db.SaveChanges();
        }

        private static void OrderCommand(ShopDbContext db, string[] cmdArgs)
        {
            var customerId = int.Parse(cmdArgs[0]);

            Order order = new Order
            {
                CustomerId = customerId
            };

            for (int i = 1; i < cmdArgs.Length; i++)
            {
                var itemId = int.Parse(cmdArgs[i]);
                order.Items.Add(new ItemOrder
                {
                    ItemId = itemId
                });            
            }
            
            db.Add(order);
            db.SaveChanges();
        }

        private static void RegisterCommand(ShopDbContext db, string[] cmdArgs)
        {
            var customerName = cmdArgs[0];
            var salesmanId = int.Parse(cmdArgs[1]);
            var salesman = db.Salesmen.FirstOrDefault(s => s.Id == salesmanId);
            Customer customer = new Customer
            {
                Name = customerName,
                Salesman = salesman
            };
            salesman.Customers.Add(customer);
            db.Customers.Add(customer);
            db.SaveChanges();
        }

        private static void FillSalesMan(ShopDbContext db)
        {
            string[] salesManNames = Console.ReadLine().Split(new []{';'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var name in salesManNames)
            {
                Salesman salesman = new Salesman {Name = name};
                db.Salesmen.Add(salesman);
            }

            db.SaveChanges();
        }

        private static void ClearDatabase(ShopDbContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}
