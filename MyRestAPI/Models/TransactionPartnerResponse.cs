using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyRestAPI.Models
{
    public class TransactionPartnerResponse
    {
        /// <summary>
        /// Whether the operation was successful or not. 
        /// •	1 is successful, 
        /// •	0 if errors encountered.
        /// </summary>
        [Range(0, 1)]
        public int result { get; set; }

        /// <summary>
        /// Total amount of payment include discount in the MYR only.
        /// </summary>
        [Range(0, long.MaxValue)]
        public long totalAmount { get; set; }

        /// <summary>
        /// Total discount of payment in the MYR only.
        /// </summary>
        [Range(0, long.MaxValue)]
        public long totalDiscount { get; set; }

        /// <summary>
        /// Final amount that customer paid in the MYR only. 
        /// </summary>
        [Range(0, long.MaxValue)]
        public long finalAmount { get; set; }

        /// <summary>
        /// Result message if the operation was Failure
        /// </summary>
        public string resultMessage { get; set; }
    }
}
