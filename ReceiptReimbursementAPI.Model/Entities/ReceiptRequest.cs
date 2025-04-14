namespace ReceiptReimbursementAPI.Model.Entities
{
    public class ReceiptRequest
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;  // Save file name or full path
    }
}