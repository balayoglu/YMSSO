using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace YMDiscourseSSO.Services
{
    public class HmacSHA256 : IHmacSHA256
    {
        public string CreateHexToken(string message, string secret)
        {
            secret = secret ?? "";
            var encoding = new ASCIIEncoding();
            var keyByte = encoding.GetBytes(secret);
            var messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                var hashmessage = hmacsha256.ComputeHash(messageBytes);
                return HashEncode(hashmessage);
            }
        }

        public bool VerifyHexToken(string message, string secret, string hexToken)
        {
            var expectedHexToken = CreateHexToken(message, secret);
            return hexToken == expectedHexToken;
        }

        private string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        private byte[] HexDecode(string hex)
        {
            var bytes = new byte[hex.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = byte.Parse(hex.Substring(i * 2, 2), NumberStyles.HexNumber);
            }
            return bytes;
        }
    }
}
