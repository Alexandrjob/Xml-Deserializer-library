using XmlDeserializer.Models;

namespace XmlDeserializer.Deserializer.Interfaces;

public interface IXmlDeserializer
{
    public Dictionary<string, Step> GetAllSteps { get; set; }
}