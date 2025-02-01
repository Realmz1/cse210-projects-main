using System;
using System.Collections.Generic;

// Class to represent a product
public class Product
{
    public string Name { get; }
    public string ProductId { get; }
    public double Price { get; }
    public int Quantity { get; }
    public string Category { get; } // Extended feature: Product category

    public Product(string name, string productId, double price, int quantity, string category)
    {
        Name = name;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
        Category = category;
    }

    // Calculate the total cost of the product
    public double CalculateTotalCost()
    {
        return Price * Quantity;
    }
}

// Class to represent an address
public class Address
{
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string Country { get; }

    public Address(string street, string city, string state, string country)
    {
        Street = street;
        City = city;
        State = state;
        Country = country;
    }

    // Check if the address is in the USA
    public bool IsInUSA()
    {
        return Country.ToLower() == "usa";
    }

    // Format the address as a string
    public string GetFullAddress()
    {
        return $"{Street}\n{City}, {State}\n{Country}";
    }
}

// Class to represent a customer
public class Customer
{
    public string Name { get; }
    public Address Address { get; }

    public Customer(string name, Address address)
    {
        Name = name;
        Address = address;
    }

    // Check if the customer lives in the USA
    public bool IsInUSA()
    {
        return Address.IsInUSA();
    }
}

// Class to represent an order
public class Order
{
    public List<Product> Products { get; }
    public Customer Customer { get; }
    public DateTime OrderDate { get; } // Extended feature: Order date

    public Order(List<Product> products, Customer customer)
    {
        Products = products;
        Customer = customer;
        OrderDate = DateTime.Now; // Set the order date to the current date and time
    }

    // Calculate the total cost of the order
    public double CalculateTotalCost()
    {
        double totalProductCost = 0;
        foreach (var product in Products)
        {
            totalProductCost += product.CalculateTotalCost();
        }

        // Apply a 10% discount if the total product cost exceeds $100 (Extended feature)
        if (totalProductCost > 100)
        {
            totalProductCost *= 0.9;
        }

        // Add shipping cost based on the customer's location
        double shippingCost = Customer.IsInUSA() ? 5 : 35;
        return totalProductCost + shippingCost;
    }

    // Generate the packing label
    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (var product in Products)
        {
            label += $"{product.Name} (ID: {product.ProductId}, Category: {product.Category})\n";
        }
        return label;
    }

    // Generate the shipping label
    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{Customer.Name}\n{Customer.Address.GetFullAddress()}\nOrder Date: {OrderDate.ToShortDateString()}\n";
    }
}

// Main program class
class Program
{
    static void Main(string[] args)
    {
        // Create addresses
        Address address1 = new Address("123 Main St", "Springfield", "IL", "USA");
        Address address2 = new Address("456 Elm St", "Toronto", "ON", "Canada");

        // Create customers
        Customer customer1 = new Customer("John Doe", address1);
        Customer customer2 = new Customer("Jane Smith", address2);

        // Create products
        Product product1 = new Product("Laptop", "P001", 1200, 1, "Electronics");
        Product product2 = new Product("T-Shirt", "P002", 20, 3, "Clothing");
        Product product3 = new Product("Book", "P003", 15, 2, "Books");
        Product product4 = new Product("Headphones", "P004", 50, 1, "Electronics");

        // Create orders
        Order order1 = new Order(new List<Product> { product1, product2 }, customer1);
        Order order2 = new Order(new List<Product> { product3, product4 }, customer2);

        // Display order details
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order1.CalculateTotalCost():0.00}\n");

        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order2.CalculateTotalCost():0.00}\n");
    }
}