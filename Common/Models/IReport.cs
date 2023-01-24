namespace Common.Models
{
    public interface IReport
    {
        string Id { get; }

        string Email { get; }

        string MedicalReport { get; }
    }
}
