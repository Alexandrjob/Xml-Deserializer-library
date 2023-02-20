namespace XmlDeserializer.FileStreams.Interfaces;

public interface IReadOnlyStream
{
    public string ReadTag();
    public string ReadToEnd();
    public void Close();
}