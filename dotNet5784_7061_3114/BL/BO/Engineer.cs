


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
    public bool Active { get; set; } = true;
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
        $"Can To Remove: {CanToRemove}\n" +
        $"Task: Id: {Task.Id}  " + $"Alias: {Task.Alias}\n" +
       "===========================\n" +
        $"                                ";
        

    }
}

