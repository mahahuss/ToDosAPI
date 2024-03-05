using Microsoft.AspNetCore.StaticFiles;

namespace Infrastructure.Services
{
    public class FileService
    {

        public string CheckContentType(string filename) {
           new FileExtensionContentTypeProvider().TryGetContentType(filename, out string? contentType);
            return contentType ?? "application/octet-stream";
        
        }
    }
}
