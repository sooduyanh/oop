using System;
using System.Collections.Generic;

public class Employee
{
    public string Name { get; set; }
}

public class Item
{
    public double Price { get; set; }
    public double Discount { get; set; }
}

public class BillLine
{
    public Item Item { get; set; }
    public int Quantity { get; set; }
}

public class GroceryBill
{
    private Employee clerk;
    private List<BillLine> items;

    public GroceryBill(Employee clerk)
    {
        this.clerk = clerk;
        items = new List<BillLine>();
    }

    public void Add(BillLine item)
    {
        items.Add(item);
    }

    public double GetTotal()
    {
        double total = 0;
        foreach (var item in items)
        {
            total += item.Item.Price * item.Quantity;
        }
        return total;
    }

    public void PrintReceipt()
    {
        Console.WriteLine("Receipt:");
        foreach (var item in items)
        {
            Console.WriteLine($"{item.Item.Name} - Quantity: {item.Quantity} - Price: {item.Item.Price * item.Quantity}");
        }
        Console.WriteLine($"Total: {GetTotal()}");
    }
}

public class DiscountBill : GroceryBill
{
    private bool preferredCustomer;

    public DiscountBill(Employee clerk, bool preferredCustomer) : base(clerk)
    {
        this.preferredCustomer = preferredCustomer;
    }

    public int GetDiscountCount()
    {
        int count = 0;
        foreach (var item in items)
        {
            if (item.Item.Discount > 0)
            {
                count++;
            }
        }
        return count;
    }

    public double GetDiscountAmount()
    {
        double amount = 0;
        foreach (var item in items)
        {
            amount += item.Item.Discount * item.Quantity;
        }
        return amount;
    }

    public double GetDiscountPercent()
    {
        double total = GetTotal();
        double discount = GetDiscountAmount();
        if (total > 0)
        {
            return (discount / total) * 100;
        }
        return 0;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Employee clerk = new Employee { Name = "John Doe" };

        Item item1 = new Item { Name = "Candy", Price = 1.35, Discount = 0.25 };
        Item item2 = new Item { Name = "Bread", Price = 2.5, Discount = 0 };
        Item item3 = new Item { Name = "Milk", Price = 3.0, Discount = 0.5 };

        BillLine billLine1 = new BillLine { Item = item1, Quantity = 2 };
        BillLine billLine2 = new BillLine { Item = item2, Quantity = 1 };
        BillLine billLine3 = new BillLine { Item = item3, Quantity = 3 };

        GroceryBill groceryBill = new GroceryBill(clerk);
        groceryBill.Add(billLine1);
        groceryBill.Add(billLine2);
        groceryBill.Add(billLine3);

        groceryBill.PrintReceipt();

        DiscountBill discountBill = new DiscountBill(clerk, true);
        discountBill.Add(billLine1);
        discountBill.Add(billLine2);
        discountBill.Add(billLine3);

        Console.WriteLine($"Discount Count: {discountBill.GetDiscountCount()}");
        Console.WriteLine($"Discount Amount: {discountBill.GetDiscountAmount()}");
        Console.WriteLine($"Discount Percent: {discountBill.GetDiscountPercent()}");

        discountBill.PrintReceipt();
    }
}