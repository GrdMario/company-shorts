namespace Company.Shorts.Application.Contracts.Security
{
    using Company.Shorts.Domain;
    using System.Threading.Tasks;

    public interface IAntivirusService
    {
        Task<bool> IsVirusAsync(byte[] data);
    }
}
