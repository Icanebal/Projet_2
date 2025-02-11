using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        /// <summary>
        /// Read-only property for display only
        /// </summary>
        public IEnumerable<CartLine> Lines => _cartLines;
        private readonly List<CartLine> _cartLines = new ();


        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            var line = _cartLines.FirstOrDefault(l => l.Product.Id == product.Id);
            if (line != null)
            {
                line.Quantity += quantity;
            }
            else
            {
                _cartLines.Add(new CartLine { Product = product, Quantity = quantity });
            }            
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            _cartLines.RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            if (_cartLines.Count == 0)
            {
                return 0.0;
            }
            return _cartLines.Sum(l => l.Product.Price * l.Quantity);     
        }


        public double GetAverageValue()
        {
            if (_cartLines.Count == 0)
            {
                return 0.0;
            }
            return GetTotalValue() / _cartLines.Sum(l => l.Quantity);
        }


        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            return _cartLines.FirstOrDefault(l => l.Product.Id == productId)?.Product;
        }

        /// <summary>
        /// Useless :
        /// Get a specific cartline by its index
        /// </summary>
        //public CartLine GetCartLineByIndex(int index)
        //{
        //    return Lines.ToArray()[index];
        //}

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            _cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
