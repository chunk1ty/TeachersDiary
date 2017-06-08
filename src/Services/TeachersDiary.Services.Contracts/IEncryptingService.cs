namespace TeachersDiary.Services.Contracts
{
    public interface IEncryptingService
    {
        int DecodeId(string id);

        string EncodeId(int id);
    }
}