namespace TeachersDiary.Services.ExcelParser
{
    public interface IExelParser
    {
        void CreateClassForUser(string filePath, string userId);
    }
}