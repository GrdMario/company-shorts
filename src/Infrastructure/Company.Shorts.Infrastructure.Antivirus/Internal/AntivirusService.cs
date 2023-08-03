namespace Company.Shorts.Infrastructure.Antivirus.Internal
{
    using Company.Shorts.Application.Contracts.Security;
    using Company.Shorts.Domain;
    using nClam;
    using System.Threading.Tasks;

    internal sealed class AntivirusService : IAntivirusService
    {
        private readonly ClamClient client;

        public AntivirusService(ClamClient client)
        {
            this.client = client;
        }

        public async Task<bool> IsVirusAsync(byte[] data)
        {
            var scanResult = await client.SendAndScanFileAsync(data);

            var result = scanResult.Result switch
            {
                ClamScanResults.VirusDetected => true,
                _ => false
            };

            return result;
        }
    }
}