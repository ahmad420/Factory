using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory_Project
{
    internal class Product
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public int Count { get; set; }
        public DateTime DateAdded { get; set; }
        public decimal Price { get; set; }

        public Product(string name, string barcode, int count, DateTime dateAdded, decimal price)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Product name cannot be empty.");
            if (string.IsNullOrEmpty(barcode))
                throw new ArgumentException("Product barcode cannot be empty.");
            if (count < 0)
                throw new ArgumentException("Product count cannot be negative.");
            if (price < 0)
                throw new ArgumentException("Product price cannot be negative.");

            Name = name;
            Barcode = barcode;
            Count = count;
            DateAdded = dateAdded;
            Price = price;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Barcode: {Barcode}, Count: {Count}, Date Added: {DateAdded}, Price: {Price:C}";
        }
    
    }
}
