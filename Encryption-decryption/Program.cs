using System;
using System.Security.Cryptography;
using System.Text;

public class AesEncryptionExample
{
    public static void Main()
    {
        string originalText = "Hello, world!";
        byte[] key;
        byte[] encryptedBytes;
        byte[] decryptedBytes;

        // Generate a new AES key and IV
        using (Aes aes = Aes.Create())
        {
            key = aes.Key;
            aes.GenerateIV(); // Generate a new IV for each encryption
            byte[] iv = aes.IV;

            // Encrypt the text
            aes.Mode = CipherMode.CBC; // Use appropriate mode
            aes.Padding = PaddingMode.PKCS7; // Use appropriate padding
            ICryptoTransform encryptor = aes.CreateEncryptor(key, iv);
            byte[] originalBytes = Encoding.UTF8.GetBytes(originalText);
            encryptedBytes = encryptor.TransformFinalBlock(originalBytes, 0, originalBytes.Length);

            // Decrypt the encrypted bytes
            aes.IV = iv; // Set the same IV for decryption
            ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);
            decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
        }

        // Display the results
        string decryptedText = Encoding.UTF8.GetString(decryptedBytes);
        Console.WriteLine("Original: " + originalText);
        Console.WriteLine("Encrypted: " + Convert.ToBase64String(encryptedBytes));
        Console.WriteLine("Decrypted: " + decryptedText);
    }
}
