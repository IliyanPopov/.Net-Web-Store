﻿namespace SportsStore.Domain.Entities
{
    using Contracts;

    public class Product : IProduct
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Categoty { get; set; }
    }
}