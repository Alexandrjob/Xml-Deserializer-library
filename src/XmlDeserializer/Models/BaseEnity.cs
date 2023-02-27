namespace XmlDeserializer.Models;

public class BaseEnity:IBaseEnity
{
    public string Question { get; set; }
    public string Answer { get; set; }
}

public interface IBaseEnity
{
    public string Question { get; set; }
    public string Answer { get; set; }
}