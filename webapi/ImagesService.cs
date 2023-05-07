namespace webapi
{
    public class ImagesService : IFileService
    {
        private string? _mimeType;
        private string? _fileName;
        private IEnumerable<String>? _files;
        public string MimeType => _mimeType ?? "";

        public string FileName => _fileName ?? "";

        public IEnumerable<String> FilesArray => _files ?? Enumerable.Empty<String>();

        public ImagesService()
        {
            _files = new List<String>();
            foreach (string file in Directory.GetFiles(".\\Assets\\Images"))
            {
                _files.Append(file);
            }
        }

        public Stream GetFile(string name)
        {
            if(File.Exists(".\\Assets\\Images\\" + name))
            {
                Stream? image = File.OpenRead(".\\Assets\\Images\\" + name);                

                if(image != null)
                {
                    switch (name.Substring(name.IndexOf('.') + 1))
                    {
                        case "png":
                            _mimeType = "image/png";
                            break;
                        case "jpeg":
                            _mimeType = "image/jpeg";
                            break;
                        case "jpg":
                            _mimeType = "image/jpeg";
                            break;
                        case "bmp":
                            _mimeType = "image/bmp";
                            break;
                        default:
                            break;
                    }
                    _fileName = name.Substring((".\\Assets\\Images\\" + name).LastIndexOf("/") + 1);
                    return image;
                }
            }
            
            throw new NotImplementedException();
        }
    }
}
