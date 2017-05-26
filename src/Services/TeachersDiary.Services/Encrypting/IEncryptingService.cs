namespace TeachersDiary.Services.Encrypting
{
    public interface IEncryptingService
    {
        int DecodeId(string id);

        string EncodeId(int id);
    }
}