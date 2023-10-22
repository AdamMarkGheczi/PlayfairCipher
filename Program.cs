namespace Playfair
{
    internal class Program
    {
        static void Main(string[] args)
        {

            KeyMatrix key = Playfair.GenerateKey("zebra", 'j', 'i');

            string plaintext = "This is plaintext";

            string encrypted = Playfair.Encrypt(plaintext, key);

            string decrypted = (Playfair.Decrypt(encrypted, key));

            Console.WriteLine($"Plaintext: {plaintext}");
            Console.WriteLine($"Encrypted: {encrypted}");
            Console.WriteLine($"Decrypted: {decrypted}");

        }
    }
}

