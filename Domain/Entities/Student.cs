namespace Domain.Entities;
public partial class Student
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;    

    public int ProgramId { get; set; }

    public string PasswordHash { get; set; } = null!;
    public string PasswordKey { get; set; } = null!;
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Program Program { get; set; } = null!;
}
