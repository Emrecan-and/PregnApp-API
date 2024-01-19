using System.Security.Cryptography;
using System.Text;

public class PasswordHasher
{
    public static string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Şifre ve salt'ı birleştir
            string combinedString = password;

            // Birleştirilmiş metni SHA-256 ile hash'e dönüştür
            byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(combinedString));

            // Hash'i hexadecimal formata dönüştür
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}