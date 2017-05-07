namespace TeachersDiary.Services.Encrypting
{
    public interface IIdentifierProvider
    {
        int DecodeId(string id);

        string EncodeId(int id);
    }
}