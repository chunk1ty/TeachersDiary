namespace TeachersDiary.Data.Ef.Contracts
{
    public interface IUnitOfWork
    {
        int SaveChanges();
    }
}
