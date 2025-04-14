using ReceiptReimbursementAPI.Model.DTOs;
using ReceiptReimbursementAPI.Model.Entities;

namespace ReceiptReimbursementAPI.Application.Interfaces
{
    public interface IReceiptService
    {
        Task<ReceiptRequest> UploadReceiptAsync(ReceiptUploadDto dto);
        Task<IEnumerable<ReceiptRequest>> GetAllReceiptsAsync();
    }
}