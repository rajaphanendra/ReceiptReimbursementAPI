using Microsoft.AspNetCore.Http;

namespace ReceiptReimbursementAPI.Model.DTOs
{
    public class ReceiptUploadDto
    {
        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; } = string.Empty;

        public IFormFile File { get; set; } = null!;
    }
}