


using DO;

namespace BO;

public class Engineer
{
    public int Id {get; init; }
    public double? Cost {get; set; }
    public BO.EngineerExperience? Level { get; set; }
    public BO.TaskInEngineer? Task { get; set; }
    public string? Email { get; set; }
    public string? Name {  get; set; }
    public bool Active { get; set; } = false;
    public bool CanToRemove { get; set; } = true;
    public override string ToString() => this.ToStringProperty();

    private string ToStringProperty()
    {
        return $"Id: {Id}\n" +
        $"Name: {Name}\n" +
        $"Email: {Email}\n" +
        $"Level: {Level}\n"+
        $"Cost: {Cost}\n" +
        $"Active: {Active}\n" +
        $"Task: Id: {Task.Id}\n  "+
        $"Alias: {Task.Alias}\n" +
       "===========================\n" +
        $"                                ";
        

    }
}

