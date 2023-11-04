namespace AuthService.Entities
{
    public class AuditBase
    {
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public string? ModifiedBy { get; set; }

        public AuditBase()
        {
            this.CreatedAt = DateTime.Now;
            this.CreatedBy = "system";
        }
    }
}
