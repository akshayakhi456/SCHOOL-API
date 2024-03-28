namespace School.API.Core.Interfaces
{
    public interface IPdfGenerator
    {
        Task<byte[]> GeneratePdf(string htmlContent);
    }
}
