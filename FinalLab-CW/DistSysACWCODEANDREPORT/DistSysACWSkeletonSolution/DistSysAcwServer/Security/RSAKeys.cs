using System.Security.Cryptography;

//one keypair for the entire server 

namespace DistSysAcwServer.Security
{
    public static class RSAKeys
    {
        public static RSACryptoServiceProvider Provider { get; } =
            new RSACryptoServiceProvider(2048);
    }
}