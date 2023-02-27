namespace XmlDeserializer.Models;

public class Step : BaseEnity, IStep
{
    public List<IVariant> Variants { get; set; }
}

public interface IStep:IBaseEnity
{
    public List<IVariant> Variants { get; set; }
}