namespace SportsStore.Models.Entities
{
    using System.Collections.Generic;
    using System.Linq;

    public class Cart
    {
        private readonly List<CartLine> _lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines
        {
            get { return this._lineCollection; }
        }

        public void AddItem(Product product, int quantity)
        {
            CartLine line = this._lineCollection
                .FirstOrDefault(p => p.Product.ProductId == product.ProductId);
            if (line == null)
            {
                this._lineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product)
        {
            this._lineCollection.RemoveAll(l => l.Product.ProductId == product.ProductId);
        }

        public decimal ComputeTotalValue()
        {
            return this._lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear()
        {
            this._lineCollection.Clear();
        }
    }
}