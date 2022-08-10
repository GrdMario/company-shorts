namespace Company.Shorts.Blocks.Bootstrap
{
    using Azure.Extensions.AspNetCore.Configuration.Secrets;
    using Azure.Security.KeyVault.Secrets;
    using Microsoft.Extensions.Configuration;

    internal class PrefixKeyVaultSecretManager : KeyVaultSecretManager
    {
        private readonly string prefix;

        public PrefixKeyVaultSecretManager(string environment, string aplicationName)
        {
            var parsedApplicationName = aplicationName.Replace(".", string.Empty);

            this.prefix = $"{environment}-{parsedApplicationName}-";
        }

        public override bool Load(SecretProperties properties)
        {
            bool isThere = properties.Name.StartsWith(this.prefix);

            return isThere;
        }

        public override string GetKey(KeyVaultSecret secret)
        {
            var value = secret.Name[this.prefix.Length..].Replace("--", ConfigurationPath.KeyDelimiter);

            return value;
        }
    }
}