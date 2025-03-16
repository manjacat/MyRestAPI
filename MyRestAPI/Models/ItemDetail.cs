using System.ComponentModel.DataAnnotations;

namespace MyRestAPI.Models
{
    /// <summary>
    /// This object is used to describe the items information
    /// </summary>
    public class ItemDetail
    {
        /// <summary>
        /// Reference ID the partner uses for this item
        /// </summary>
        public required string partnerItemRef { get; set; }

        /// <summary>
        /// Name of the item
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Quantity of the item bought 
        /// </summary>
        [Range(2, 5)]
        public int Qty { get; set; }

        /// <summary>
        /// Price of one unit of the item in the currency of the transaction
        /// value in cents
        /// </summary>
        [Range (0, int.MaxValue)]
        public long unitPrice { get; set; }
    }
}
