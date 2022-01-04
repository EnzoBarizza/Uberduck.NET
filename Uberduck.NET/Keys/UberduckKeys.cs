namespace Uberduck.NET.Keys
{
    /// <summary>
    /// The class that represents the Uberduck keys
    /// </summary>
    public class UberduckKeys
    {
        /// <summary>
        /// Your API Public Key
        /// </summary>
        public string PublicKey { get; private set; }
        internal string SecretKey { get; private set; }

        /// <summary>
        /// Instantiate a new keys object for Uberduck.NET
        /// </summary>
        /// <param name="publicKey">Your API Public Key</param>
        /// <param name="secretKey">Your API Secret Key</param>
        public UberduckKeys(string publicKey, string secretKey)
        {
            PublicKey = publicKey;
            SecretKey = secretKey;
        }
    }
}
