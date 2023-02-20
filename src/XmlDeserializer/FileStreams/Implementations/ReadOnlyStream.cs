using XmlDeserializer.FileStreams.Interfaces;

namespace XmlDeserializer.FileStreams.Implementations;

public class ReadOnlyStream : IReadOnlyStream
{
    private readonly StreamReader _reader;

    public ReadOnlyStream(string fileFullPath)
    {
        _reader = new StreamReader(fileFullPath);
    }

    public string ReadTag()
    {
        throw new NotImplementedException();
    }

    public string ReadToEnd()
    {
        try
        {
            var result = _reader.ReadToEnd();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            Close();
        }
    }

    public void Close()
    {
        _reader.Dispose();
    }
}