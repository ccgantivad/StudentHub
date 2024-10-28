namespace Domain.Entities;
public partial class Teacher
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
