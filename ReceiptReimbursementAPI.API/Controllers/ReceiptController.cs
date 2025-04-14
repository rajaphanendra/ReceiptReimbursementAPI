using Microsoft.AspNetCore.Mvc;
using ReceiptReimbursementAPI.Application.Interfaces;
using ReceiptReimbursementAPI.Model.DTOs;

namespace ReceiptReimbursementAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptService _receiptService;

        public ReceiptController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadReceipt([FromForm] ReceiptUploadDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _receiptService.UploadReceiptAsync(dto);

            return Ok(new
            {
                message = "Receipt uploaded successfully.",
                receiptId = result.Id,
                file = result.FilePath
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetReceipts()
        {
            var receipts = await _receiptService.GetAllReceiptsAsync();
            return Ok(receipts);
        }
    }
}