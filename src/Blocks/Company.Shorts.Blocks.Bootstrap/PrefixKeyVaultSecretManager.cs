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
            return properties.Name.StartsWith(this.prefix);
        }

        public override string GetKey(KeyVaultSecret secret)
        {
            return secret.Name[this.prefix.Length..].Replace("--", ConfigurationPath.KeyDelimiter);
        }
    }
}