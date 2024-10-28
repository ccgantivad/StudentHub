namespace Domain.Entities;

public partial class Subject
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Credits { get; set; }

    public int TeacherId { get; set; }

    public int ProgramId { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Program Program { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;
}
