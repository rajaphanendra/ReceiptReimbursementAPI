using ReceiptReimbursementAPI.Application.Interfaces;
using ReceiptReimbursementAPI.Model.DTOs;
using ReceiptReimbursementAPI.Model.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ReceiptReimbursementAPI.Data;

namespace ReceiptReimbursementAPI.Application.Services
{
    public class ReceiptService : IReceiptService
    {
        private readonly ReimbursementDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ReceiptService(ReimbursementDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<ReceiptRequest> UploadReceiptAsync(ReceiptUploadDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                throw new ArgumentException("File is required");

            var uploadsFolder = Path.Combine(_env.ContentRootPath, "Uploads");
            Directory.CreateDirectory(uploadsFolder); // Ensure folder exists

            var uniqueFileName = $"{Guid.NewGuid()}_{dto.File.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.File.CopyToAsync(stream);
            }

            var receipt = new ReceiptRequest
            {
                Date = dto.Date,
                Amount = dto.Amount,
                Description = dto.Description,
                FilePath = uniqueFileName
            };

            _context.ReceiptRequests.Add(receipt);
            await _context.SaveChangesAsync();

            return receipt;
        }

        public async Task<IEnumerable<ReceiptRequest>> GetAllReceiptsAsync()
        {
            return await _context.ReceiptRequests.ToListAsync();
        }

    }
}