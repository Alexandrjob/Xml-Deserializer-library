using System.Xml.Linq;
using XmlDeserializer.FileStreams.Implementations;
using XmlDeserializer.Models;

namespace XmlDeserializer.Deserializer.Implementations;

public class XmlDeserializer
{
    private ReadOnlyStream _readOnlyStream { get; }

    public XmlDeserializer(string fileFullPath)
    {
        _readOnlyStream = new ReadOnlyStream(fileFullPath);
    }

    public Dictionary<string, Step> GetAllSteps()
    {
        var xmlString = _readOnlyStream.ReadToEnd();

        var tagSteps = new Dictionary<string, Step>();

        var root = XElement.Parse(xmlString);

        foreach (var stepElement in root.Elements("Step"))
        {
            var step = new Step
            {
                Text = stepElement.Element("Text").Value,
                Variants = new List<Variant>()
            };

            if (stepElement.Element("Variants") is XElement variantsElement)
            {
                foreach (var variantElement in variantsElement.Elements("Variant"))
                {
                    var variant = new Variant
                    {
                        Text = variantElement.Element("Text").Value
                    };

                    var linkTegElement = variantElement.Element("LinkTeg");
                    if (linkTegElement != null)
                    {
                        variant.LinkTeg = linkTegElement.Value;
                    }

                    step.Variants.Add(variant);
                }
            }

            var tagName = stepElement.Attribute("key").Value;
            tagSteps[tagName] = step;
        }

        return tagSteps;
    }
}