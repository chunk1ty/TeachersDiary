namespace TeachersDiary.Services.Contracts
{
    public interface INumberConvertorService
    {
        string FromDoubleToFractionNumber(double number);

        double FromFractionToDoubleNumber(string fraction);
    }
}