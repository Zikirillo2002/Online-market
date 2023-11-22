using Bogus;
using Lesson11.Data;
using Lesson11.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson11.Extensions
{
    public static class DatabaseSeeder
    {
        private static Faker _faker = new Faker("ru");

        public static void SeedDatabase(this IServiceCollection _, IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetRequiredService<DbContextOptions<DiyorMarketDbContext>>();
            using var context = new DiyorMarketDbContext(options);

            CreateCategories(context);
            CreateProducts(context);
            CreateInventories(context);
            CreateInventoryItems(context);
            CreateCustomers(context);
            CreateSales(context);
            CreateSaleItems(context);
            CreateSupplier(context);
            CreateSupply(context);
            CreateSupplyItem(context);

        }

        private static void CreateCategories(DiyorMarketDbContext context)
        {
            if (context.Categories.Any()) return;

            List<string> categoryNames = new();
            List<Category> categories = new();

            for (int i = 0; i < 25; i++)
            {
                var categoryName = _faker.Commerce
                    .Categories(1)
                    .First()
                    .FirstLetterToUpper();
                int attempts = 0;

                while (categoryNames.Contains(categoryName) && attempts < 100)
                {
                    categoryName = _faker.Commerce
                        .Categories(1)
                        .First()
                        .FirstLetterToUpper();
                    attempts++;
                }

                categoryNames.Add(categoryName);
                categories.Add(new Category
                {
                    Name = categoryName,
                });
            }

            context.AddRange(categories);
            context.SaveChanges();
        }

        public static void CreateProducts(DiyorMarketDbContext context)
        {
            if (context.Products.Any()) return;

            var categories = context.Categories.ToList();
            var productNames = new List<string>();
            var products = new List<Product>();

            foreach (var category in categories)
            {
                var productsCount = new Random().Next(5, 35);

                for (int i = 0; i < productsCount; i++)
                {
                    var productName = _faker.Commerce.ProductName().FirstLetterToUpper();
                    int attempts = 0;

                    while (productNames.Contains(productName) && attempts < 100)
                    {
                        productName = _faker.Commerce
                            .ProductName()
                            .FirstLetterToUpper();

                        attempts++;
                    }

                    productNames.Add(productName);
                    var supplyPrice = _faker.Random.Decimal(10_000, 2_000_000);
                    var salePrice = _faker.Random.Decimal(supplyPrice, 2_500_000);

                    products.Add(new Product
                    {
                        Name = productName,
                        Description = _faker.Commerce.ProductDescription(),
                        SalePrice = salePrice,
                        SupplyPrice = supplyPrice,
                        CategoryId = category.Id,
                    });
                }


            }

            context.Products.AddRange(products);
            context.SaveChanges();
        }

        public static void CreateInventories(DiyorMarketDbContext context)
        {
            if (context.Inventories.Any()) return;

            List<Inventory> inventories = new();

            for (int i = 0; i < 5; i++)
            {
                inventories.Add(new Inventory
                {
                    Name = _faker.Random.AlphaNumeric(5).ToUpperInvariant()
                });
            }

            context.Inventories.AddRange(inventories);
            context.SaveChanges();
        }

        public static void CreateInventoryItems(DiyorMarketDbContext context)
        {
            if (context.InventoryItems.Any()) return;

            var inventories = context.Inventories.ToList();
            var products = context.Products.ToList();
            var inventoryItems = new List<InventoryItem>();

            foreach (var inventory in inventories)
            {
                var quantity = _faker.Random.Number(400);

                foreach (var product in products)
                {
                    inventoryItems.Add(new InventoryItem
                    {
                        ProductId = product.Id,
                        InventoryId = inventory.Id,
                        QuantityInStock = quantity,
                    });
                }
            }

            context.InventoryItems.AddRange(inventoryItems.Take(1));
            context.SaveChanges();
        }

        private static void CreateCustomers(DiyorMarketDbContext context)
        {
            if (context.Customers.Any()) return;
            List<Customer> customers = new List<Customer>();

            for (int i = 0; i < 125; i++)
            {
                customers.Add(new Customer()
                {
                    FirstName = _faker.Name.FirstName(),
                    LastName = _faker.Name.LastName(),
                    PhoneNumber = _faker.Phone.PhoneNumber("+998-##-###-##-##")
                });
            }

            context.Customers.AddRange(customers);
            context.SaveChanges();
        }

        private static void CreateSales(DiyorMarketDbContext context)
        {
            if (context.Sales.Any()) return;

            var customers = context.Customers.ToList();
            List<Sale> sales = new List<Sale>();

            foreach (var customer in customers)
            {
                int salesCount = new Random().Next(5, 50);
                for (int i = 0; i < salesCount; i++)
                {
                    sales.Add(new Sale()
                    {
                        CustomerId = customer.Id,
                        Date = _faker.Date.Between(DateTime.Now.AddYears(-2), DateTime.Now),
                    });
                }
            }

            context.Sales.AddRange(sales);
            context.SaveChanges();
        }

        private static void CreateSaleItems(DiyorMarketDbContext context)
        {
            if (context.SaleItems.Any()) return;

            var sales = context.Sales.ToList();
            var products = context.Products.ToList();
            List<SaleItem> saleItems = new List<SaleItem>();

            foreach (var sale in sales)
            {
                int saleItemsCount = new Random().Next(1, 20);

                for (int i = 0; i < saleItemsCount; i++)
                {
                    var randomProduct = _faker.PickRandom(products);
                    int attempts = 0;

                    while (saleItems.Any(si => si.Id == randomProduct.Id) && attempts < 100)
                    {
                        randomProduct = _faker.PickRandom(products);
                    }

                    var quantity = new Random().Next(1, 50);

                    saleItems.Add(new SaleItem()
                    {
                        ProductId = randomProduct.Id,
                        SaleId = sale.Id,
                        Quantity = quantity,
                        UnitPrice = randomProduct.SalePrice,
                    });
                }
            }

            context.SaleItems.AddRange(saleItems);
            context.SaveChanges();
        }

        private static void CreateSupplier(DiyorMarketDbContext context)
        {
            if (context.Suppliers.Any()) return;

            List<Supplier> suppliers = new();

            for (int i = 0; i < 50; i++)
            {
                suppliers.Add(new Supplier()
                {
                    FullName = _faker.Name.FullName(),
                    PhoneNumber = _faker.Phone.PhoneNumber("+998-##-###-##-##"),
                    Company = _faker.Company.CompanyName()
                });
            }

            context.Suppliers.AddRange(suppliers);
            context.SaveChanges();
        }

        private static void CreateSupply(DiyorMarketDbContext context)
        {
            if (context.Supplies.Any()) return;

            var suppliers = context.Suppliers.ToList();
            List<Supply> supplies = new List<Supply>();

            foreach (var s in suppliers)
            {
                int suppliersCount = new Random().Next(5, 30);
                for (int i = 0; i < suppliersCount; i++)
                {
                    supplies.Add(new Supply()
                    {
                        SupplierId = s.Id,
                        Date = _faker.Date.Between(DateTime.Now.AddYears(-2), DateTime.Now),
                    });
                }
            }

            context.Supplies.AddRange(supplies);
            context.SaveChanges();
        }

        private static void CreateSupplyItem(DiyorMarketDbContext context)
        {
            if (context.SupplyItems.Any()) return;

            var supplies = context.Supplies.ToList();
            var products = context.Products.ToList();
            List<SupplyItem> supplyItems = new List<SupplyItem>();

            foreach (var supply in supplies)
            {
                int supplyItemsCount = new Random().Next(1, 30);

                for (int i = 0; i < supplyItemsCount; i++)
                {
                    var randomProduct = _faker.PickRandom(products);
                    var quantity = new Random().Next(1, 50);

                    supplyItems.Add(new SupplyItem()
                    {
                        ProductId = randomProduct.Id,
                        SupplyId = supply.Id,
                        Quantity = quantity,
                        UnitPrice = randomProduct.SalePrice,
                    });
                }
            }

            context.SupplyItems.AddRange(supplyItems);
            context.SaveChanges();
        }
    }
}
