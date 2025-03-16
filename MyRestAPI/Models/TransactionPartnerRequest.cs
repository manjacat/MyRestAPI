using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyRestAPI.Models
{
    /// <summary>
    /// This object is used to describe the transaction details of the vertical partner
    /// </summary>
    public class TransactionPartnerRequest
    {
        /// <summary>
        /// The allowed partner's key
        /// </summary>
        [MaxLength(50)]
        public string partnerKey { get; set; }

        /// <summary>
        /// Partner's reference number for this transaction
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string partnerRefNo { get; set; }

        /// <summary>
        /// Encode to Base64 format. 
        /// </summary>        
        [MaxLength(50)]
        [Required]
        public string partnerPassword { get; set; }

        /// <summary>
        /// Total amount of payment include discount in the MYR only.
        /// -	Only allow positive value
        /// -	value in cents
        /// </summary>
        [Range(0, long.MaxValue)]
        [Required]
        public long totalAmount { get; set; }

        /// <summary>
        /// Array of items purchased through this transaction
        /// </summary>
        public ItemDetail[] items { get; set; }

        /// <summary>
        /// String representation of the UTC time of the operation in ISO 8601 format 
        /// ie. 2024-08-15T02:11:22.0000000Z
        /// </summary>
        [Required]
        public string timeStamp { get; set; }

        /// <summary>
        /// Message Signature. 
        /// Message Signature Parameter Order 
        /// timestamp + partnerkey + partnerrefno + totalamount  + partnerpassword(encoded)
        /// Example: 20240815021122FAKEGOOGLEFG-000011000RkFLRVBBU1NXT1JEMTIzNA==
        /// </summary>
        [Required]
        public string sig { get; set; }
    }
}
