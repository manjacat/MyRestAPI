using Microsoft.AspNetCore.Mvc;
using MyRestAPI.Models;
using MyRestAPI.Utility;
using static MyRestAPI.Utility.EnumList;
using System.Text.Json;

namespace MyRestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubmitTRXMessageController : Controller
    {
        private readonly ILogger<SubmitTRXMessageController> _logger;

        public SubmitTRXMessageController(ILogger<SubmitTRXMessageController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            _logger.LogTrace("Log trace from trace method");
            _logger.LogDebug("Log trace from debug method");
            _logger.LogInformation("Log trace from information method");
            _logger.LogWarning("Log trace from warning method");
            _logger.LogError("Log trace from error method");
            _logger.LogCritical("Log trace from critical method");

            return Ok("Hello World");
        }


        /// <summary>
        /// HTTP Post To receive transaction from partner for further processing
        /// </summary>
        /// <returns>TransactionPartnerResponse</returns>
        [HttpPost]
        public IActionResult Post(TransactionPartnerRequest request)
        {
            _logger.LogInformation(string.Format("REQUEST: {0}", JsonSerializer.Serialize(request)));

            try
            {
                if (!ModelState.IsValid)
                {
                    TransactionPartnerResponse failResponse = new TransactionPartnerResponse
                    {
                        result = 0,
                        resultMessage = EnumList.GetDescription(ErrorMessage.MissingParameter),
                    };
                    _logger.LogInformation(string.Format("RESPONSE: {0}", JsonSerializer.Serialize(failResponse)));
                    return BadRequest(failResponse);
                }

                TransactionPartnerResponse response = ValidateRequest(request);
                if (response != null)
                {
                    _logger.LogInformation(string.Format("RESPONSE: {0}", JsonSerializer.Serialize(response)));
                    return BadRequest(response);
                }

                long totalDiscount = CalculateDiscount(request.totalAmount);
                TransactionPartnerResponse succResponse = new TransactionPartnerResponse
                {
                    result = 1,
                    totalAmount = request.totalAmount,
                    totalDiscount = totalDiscount,
                    finalAmount = request.totalAmount - totalDiscount,
                };
                _logger.LogInformation(string.Format("RESPONSE: {0}", JsonSerializer.Serialize(succResponse)));
                return Ok(succResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                TransactionPartnerResponse response = new TransactionPartnerResponse
                {
                    result = 0,
                    resultMessage = EnumList.GetDescription(ErrorMessage.InternalServerError),
                };
                _logger.LogInformation(string.Format("RESPONSE: {0}", JsonSerializer.Serialize(response)));
                return BadRequest(response);
            }            
        }

        #region private methods
        private TransactionPartnerResponse ValidateRequest(TransactionPartnerRequest request) 
        {
            // check for unauthorized partner
            List<Partner> partners = new AllowedPartner().Partners;
            var password = request.partnerPassword.DecodeBase64();
            Partner? allowedParner = partners.FirstOrDefault(x =>
                x.partnerNo == request.partnerRefNo &&
                x.allowedPartner == request.partnerKey &&
                x.partnerPassword == password);
            if (allowedParner == null)
            {
                TransactionPartnerResponse failResponse = new TransactionPartnerResponse
                {
                    result = 0,
                    resultMessage = EnumList.GetDescription(ErrorMessage.AccessDenied),
                };
                return failResponse;
            }

            // check for Expired request
            DateTime serverTime = DateTime.Now;
            DateTime requestTime = DateTime.Parse(request.timeStamp);
            TimeSpan ts = requestTime.Subtract(serverTime);
            if (ts.TotalMinutes > 5 || ts.TotalMinutes < -5)
            {
               TransactionPartnerResponse failResponse = new TransactionPartnerResponse
               {
                   result = 0,
                   resultMessage = EnumList.GetDescription(ErrorMessage.Expired),
               };
               return failResponse;
            }

            // check for invalid total amount
            long itemsTotal = request.items
                .Select(x => x.Qty * x.unitPrice).Sum();
            if (itemsTotal != request.totalAmount)
            {
                TransactionPartnerResponse failResponse = new TransactionPartnerResponse
                {
                    result = 0,
                    resultMessage = EnumList.GetDescription(ErrorMessage.InvalidTotalAmount),
                };
                return failResponse;
            }

            return null;
        }

        private long CalculateDiscount(long totalAmount)
        {
            double percentage = 0;
            
            if (20000 <= totalAmount && totalAmount <= 50000)
            {
                percentage = 0.05;
            }
            else if (50000 < totalAmount && totalAmount <= 80000)
            {
                percentage = 0.07;
            }
            else if (80000 < totalAmount && totalAmount <= 120000)
            {
                percentage = 0.07;
            }
            else if (120000 < totalAmount)
            {
                percentage = 0.15;
            }
            else
            {
                percentage = 0;
            }

            // If totalAmount is a prime number above MYR 500:
            // Apply an additional 8% discount.
            if (totalAmount > 50000 && NumbersUtility.IsPrimeNumber(totalAmount)) 
            { 
                percentage += (long)0.8; 
            }

            // If totalAmount ends in the digit 5 and is above MYR 900:
            // Apply an additional 10% discount.
            if (totalAmount > 90000 && totalAmount.ToString().Last() == '5') 
            {
                percentage += (long)0.1;
            }

            // Cap on Maximum Discount: cannot exceed 20%
            if (percentage > 0.2) 
            {
                percentage = 0.2;
            }

            long totalDiscount = (long)(totalAmount * percentage);

            return totalDiscount;
        }
        #endregion
    }
}
