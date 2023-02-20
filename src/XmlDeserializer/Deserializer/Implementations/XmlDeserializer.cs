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
                Variants = new List<Variant>()
            };
            SetProperties(stepElement, step);

            if (stepElement.Element("Variants") is XElement variantsElement)
            {
                foreach (var variantElement in variantsElement.Elements("Variant"))
                {
                    var variant = new Variant();
                    SetProperties(stepElement, variant);
                    
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

    private void SetProperties(XElement element, BaseEnity enity)
    {
        var questionElement = element.Element("Question");
        if (questionElement != null)
        {
            enity.Question = questionElement.Value;
        }

        var answerElement = element.Element("Answer");
        if (answerElement != null)
        {
            enity.Answer = answerElement.Value;
        }
    }
}