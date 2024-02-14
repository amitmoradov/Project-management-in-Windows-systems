using BO;
namespace PL;


[Serializable]
public class BlReadNotFoundException : Exception
{
    public BlReadNotFoundException(string msg, BO.BlReadNotFoundException ex) : base(msg, ex) { }
    public BlReadNotFoundException(string msg) : base(msg) { }
}
