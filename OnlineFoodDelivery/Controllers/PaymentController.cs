using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineFoodDelivery.Services.Interface;
using System.Security.Claims;

namespace OnlineFoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayment _paymentService;

        public PaymentController(IPayment paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpPost("make-payment")]
        public async Task<IActionResult> MakePayment([FromBody] Dtos.Requests.MakePaymentDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }
            dto.CreatedBy = userId;
            dto.CreatedAt = DateTime.UtcNow;
            return Ok(await _paymentService.MakePaymentAsync(dto));
        }

        [HttpPut("{paymentId}/status")]
        public async Task<IActionResult> UpdatePaymentStatus(int paymentId, [FromQuery] string status)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }
            return Ok(await _paymentService.UpdatePaymentStatusAsync(paymentId, status, userId));
        }

        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetPaymentByOrderIdAsync(int paymentId)
        {
            return Ok(await _paymentService.GetPaymentByOrderIdAsync(paymentId));
        }

        [HttpGet("getallpayments")]
        public async Task<IActionResult> GetAllPaymentsAsync()
        {
            return Ok(await _paymentService.GetAllPaymentsAsync());
        }


    }
}
