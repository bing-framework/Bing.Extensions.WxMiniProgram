using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Bing.WxMiniProgram.Helpers
{
    /// <summary>
    /// 加密操作辅助类
    /// </summary>
    public class EncryptHelper
    {
        /// <summary>
        /// 解密已加密的数据
        /// </summary>
        /// <param name="sessionKey">会话密钥</param>
        /// <param name="encryptedData">加密数据</param>
        /// <param name="iv">偏移量</param>
        public static string DecodeEncryptData(string sessionKey, string encryptedData, string iv)
        {
            var aesCipher = Convert.FromBase64String(encryptedData);
            var aesKey = Convert.FromBase64String(sessionKey);
            var aesIv = Convert.FromBase64String(iv);

            var result = AesDecrypt(encryptedData, aesIv, aesKey);
            var resultStr = Encoding.UTF8.GetString(result);
            return resultStr;
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="input">加密数据</param>
        /// <param name="iv">偏移量</param>
        /// <param name="key">加密密钥</param>
        private static byte[] AesDecrypt(string input, byte[] iv, byte[] key)
        {
            var aes = Aes.Create();
            aes.KeySize = 128;// 原始: 256
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key;
            aes.IV = iv;

            var decrypt = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] xBuff = null;
            try
            {
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                    {
                        var xXml = Convert.FromBase64String(input);
                        var msg = new byte[xXml.Length + 32 - xXml.Length % 32];
                        Array.Copy(xXml, msg, xXml.Length);
                        cs.Write(xXml, 0, xXml.Length);
                    }
                    xBuff = Decode2(ms.ToArray());
                }
            }
            catch (CryptographicException e)
            {
                using (var ms = new MemoryStream())
                {
                    // cs 不自动释放，用于避免“Padding is invalid and cannot be removed”的错误
                    var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write);
                    {
                        var xXml = Convert.FromBase64String(input);
                        var msg = new byte[xXml.Length + 32 - xXml.Length % 32];
                        Array.Copy(xXml, msg, xXml.Length);
                        cs.Write(xXml, 0, xXml.Length);
                    }
                    xBuff = Decode2(ms.ToArray());
                }
            }

            return xBuff;
        }

        /// <summary>
        /// 二次反编码
        /// </summary>
        /// <param name="decrypted">已解密的字节数组</param>
        private static byte[] Decode2(byte[] decrypted)
        {
            var pad = (int)decrypted[decrypted.Length - 1];
            if (pad < 1 || pad > 32)
                pad = 0;
            var res = new byte[decrypted.Length - pad];
            Array.Copy(decrypted, 0, res, 0, decrypted.Length - pad);
            return res;
        }
    }
}
