namespace TeachersDiary.Data.Ef.Contracts
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
