namespace ReportSystem.Domain.Interfaces
{
    public class IAuditable
    {
        long? CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        long? UpdatedBy { get; set; }
        DateTime? UpdatedOn { get; set; }
    }
}
