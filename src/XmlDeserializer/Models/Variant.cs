namespace XmlDeserializer.Models;

public class Variant : BaseEnity, IVariant
{
    public string? LinkTag { get; set; }
}

public interface IVariant: IBaseEnity
{
    public string? LinkTag { get; set; }
}