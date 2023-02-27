using System.Xml.Linq;
using XmlDeserializer.FileStreams.Implementations;
using XmlDeserializer.Models;

namespace XmlDeserializer.Deserializer.Implementations;

public class Deserializer
{
    private ReadOnlyStream _readOnlyStream { get; }

    public Deserializer(string fileFullPath)
    {
        _readOnlyStream = new ReadOnlyStream(fileFullPath);
    }

    public Dictionary<string, IStep> GetAllSteps()
    {
        var xmlString = _readOnlyStream.ReadToEnd();

        var tagSteps = new Dictionary<string, IStep>();

        var root = XElement.Parse(xmlString);

        foreach (var stepElement in root.Elements("Step"))
        {
            IStep step = new Step
            {
                Variants = new List<IVariant>()
            };
            SetProperties(stepElement, step);

            if (stepElement.Element("Variants") is XElement variantsElement)
            {
                foreach (var variantElement in variantsElement.Elements("Variant"))
                {
                    IVariant variant = new Variant();
                    SetProperties(variantElement, variant);
                    
                    var linkTagElement = variantElement.Element(nameof(variant.LinkTag));
                    if (linkTagElement != null)
                    {
                        variant.LinkTag = linkTagElement.Value;
                    }

                    step.Variants.Add(variant);
                }
            }

            var tagName = stepElement.Attribute("key").Value;
            tagSteps[tagName] = step;
        }

        return tagSteps;
    }

    private void SetProperties(XElement element, IBaseEnity enity)
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