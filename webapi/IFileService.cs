namespace webapi
{
    public interface IFileService
    {
        string MimeType { get; }
        string FileName { get; }
        IEnumerable<String> FilesArray { get; }
        Stream GetFile(string name);
    }
}