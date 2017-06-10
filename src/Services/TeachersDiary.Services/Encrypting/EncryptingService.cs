using System;
using System.Text;

namespace TeachersDiary.Services.Encrypting
{
    public class EncryptingService : IEncryptingService
    {
        private const string Salt = "ankSallt";

        public int DecodeId(string id)
        {
            var base64EncodedBytes = Convert.FromBase64String(id);
            var bytesAsString = Encoding.UTF8.GetString(base64EncodedBytes);
            bytesAsString = bytesAsString.Replace(Salt, string.Empty);

            return int.Parse(bytesAsString);
        }

        public string EncodeId(int id)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(id + Salt);

            return Convert.ToBase64String(plainTextBytes);
        }
    }
}
