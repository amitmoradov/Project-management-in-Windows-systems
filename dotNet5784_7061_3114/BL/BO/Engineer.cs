


using DO;

namespace BO;

public class Engineer
{
    public int Id {get; init; }
    public double? Cost {get; set; }
    public DO.EngineerExperience? Level { get; set; }
    public BO.TaskInEngineer? Task { get; set; }
    public string? Email { get; set; }
    public string? Name {  get; set; }
    public bool Active {  get; set; }
    public bool CanToRemove {  get; set; }
    public override string ToString() => this.ToStringProperty();

    private string ToStringProperty()
    {
        return $"Engineer details: id:{ Id}, name: {Name}, email:{Email}, cost:{Cost}, active:{Active}, canToRemove:{CanToRemove}, task: {Task}";
    }
}

