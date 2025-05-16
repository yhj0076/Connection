using UnityEngine;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System;

public class SecurityPlayerPrefs
{
    // http://www.codeproject.com/Articles/769741/Csharp-AES-bits-Encryption-Library-with-Salt
    // http://ikpil.com/1342

    private static string _saltForKey;

    private static byte[] _keys;
    private static byte[] _iv;
    private static int keySize = 256;
    private static int blockSize = 128;
    private static int _hashLen = 32;

    static SecurityPlayerPrefs()
    {
        // 8 ����Ʈ�� �ϰ�, �����ؼ� ����
        byte[] saltBytes = new byte[] { 24, 99, 36, 34, 14, 56, 11, 20 };

        // ���� ��� ����, Ű�� ����� ���� �뵵�� ��
        string randomSeedForKey = "s5h12ljkh5l12h5l0zs7f071f278450fv9865b9f";

        // ���� ��� ����, aes�� �� key �� iv �� ���� �뵵
        string randomSeedForValue = "6x896v986f9s69fva6srf9awseb34m5b32m5b2m";

        {
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(randomSeedForKey, saltBytes, 1000);
            _saltForKey = System.Convert.ToBase64String(key.GetBytes(blockSize / 8));
        }

        {
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(randomSeedForValue, saltBytes, 1000);
            _keys = key.GetBytes(keySize / 8);
            _iv = key.GetBytes(blockSize / 8);
        }
    }

    public static string MakeHash(string original)
    {
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(original);
            byte[] hashBytes = md5.ComputeHash(bytes);

            string hashToString = "";
            for (int i = 0; i < hashBytes.Length; ++i)
                hashToString += hashBytes[i].ToString("x2");

            return hashToString;
        }
    }

    public static byte[] Encrypt(byte[] bytesToBeEncrypted)
    {
        using (RijndaelManaged aes = new RijndaelManaged())
        {
            aes.KeySize = keySize;
            aes.BlockSize = blockSize;

            aes.Key = _keys;
            aes.IV = _iv;

            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (ICryptoTransform ct = aes.CreateEncryptor())
            {
                return ct.TransformFinalBlock(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
            }
        }
    }

    public static byte[] Decrypt(byte[] bytesToBeDecrypted)
    {
        using (RijndaelManaged aes = new RijndaelManaged())
        {
            aes.KeySize = keySize;
            aes.BlockSize = blockSize;

            aes.Key = _keys;
            aes.IV = _iv;

            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (ICryptoTransform ct = aes.CreateDecryptor())
            {
                return ct.TransformFinalBlock(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
            }
        }
    }

    public static string Encrypt(string input)
    {
        byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
        byte[] bytesEncrypted = Encrypt(bytesToBeEncrypted);

        return System.Convert.ToBase64String(bytesEncrypted);
    }

    public static string Decrypt(string input)
    {
        byte[] bytesToBeDecrypted = System.Convert.FromBase64String(input);
        byte[] bytesDecrypted = Decrypt(bytesToBeDecrypted);

        return Encoding.UTF8.GetString(bytesDecrypted);
    }

    private static void SetSecurityValue(string key, string value)
    {
        string hideKey = MakeHash(key + _saltForKey);
        string encryptValue = Encrypt(value + MakeHash(value));

        PlayerPrefs.SetString(hideKey, encryptValue);
    }

    private static string GetSecurityValue(string key)
    {
        string hideKey = MakeHash(key + _saltForKey);

        string encryptValue = PlayerPrefs.GetString(hideKey);
        if (true == string.IsNullOrEmpty(encryptValue))
            return string.Empty;

        string valueAndHash = Decrypt(encryptValue);
        if (_hashLen > valueAndHash.Length)
            return string.Empty;

        string savedValue = valueAndHash.Substring(0, valueAndHash.Length - _hashLen);
        string savedHash = valueAndHash.Substring(valueAndHash.Length - _hashLen);

        if (MakeHash(savedValue) != savedHash)
            return string.Empty;

        return savedValue;
    }

    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(MakeHash(key + _saltForKey));
    }

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void Save()
    {
        PlayerPrefs.Save();
    }

    public static void SetInt(string key, int value)
    {
        SetSecurityValue(key, value.ToString());
    }

    public static void SetLong(string key, long value)
    {
        SetSecurityValue(key, value.ToString());
    }

    public static void SetFloat(string key, float value)
    {
        SetSecurityValue(key, value.ToString());
    }

    public static void SetString(string key, string value)
    {
        SetSecurityValue(key, value);
    }

    public static int GetInt(string key, int defaultValue)
    {
        string originalValue = GetSecurityValue(key);
        if (true == string.IsNullOrEmpty(originalValue))
            return defaultValue;

        int result = defaultValue;
        if (false == int.TryParse(originalValue, out result))
            return defaultValue;

        return result;
    }

    public static long GetLong(string key, long defaultValue)
    {
        string originalValue = GetSecurityValue(key);
        if (true == string.IsNullOrEmpty(originalValue))
            return defaultValue;

        long result = defaultValue;
        if (false == long.TryParse(originalValue, out result))
            return defaultValue;

        return result;
    }

    public static float GetFloat(string key, float defaultValue)
    {
        string originalValue = GetSecurityValue(key);
        if (true == string.IsNullOrEmpty(originalValue))
            return defaultValue;

        float result = defaultValue;
        if (false == float.TryParse(originalValue, out result))
            return defaultValue;

        return result;
    }

    public static string GetString(string key, string defaultValue)
    {
        string originalValue = GetSecurityValue(key);
        if (true == string.IsNullOrEmpty(originalValue))
            return defaultValue;

        return originalValue;
    }
}