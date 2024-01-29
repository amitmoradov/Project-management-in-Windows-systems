


using DO;

namespace BO;

public class Engineer
{
    public int _id {get; init; }
    double? _cost {get; set; }
    DO.EngineerExperience? _level { get; set; }
    string? _email { get; set; }
    string? _name {  get; set; }
    bool _active {  get; set; }
    bool _canToRemove {  get; set; }
    public override string ToString() => this.ToStringProperty();

    public string ToStringProperty()
    {
        return $"Engineer details: id:{ _id}, name: {_name}, email:{_email}, cost:{_cost}, active:{_active}, canToRemove:{_canToRemove}";
    }
}

