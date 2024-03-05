namespace DalApi;

public interface IProject 
{
   public void SaveStartProjectDate(DateTime startProject);
   public void SaveChangeOfStatus(string status);
    /// <summary>
    /// Return start projectDate .
    /// </summary>
    /// <returns></returns>
   public DateTime ReturnStartProjectDate();

   public string ReturnStatusProject();

    public void SaveVirtualTimeInDal(DateTime virtualTime);

    public DateTime ReturnVirtualTimeInDal();

}