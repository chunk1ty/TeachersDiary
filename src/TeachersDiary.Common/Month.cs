namespace TeachersDiary.Common
{
    public class Month
    {
        public Month(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
