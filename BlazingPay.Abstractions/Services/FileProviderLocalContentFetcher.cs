using System.IO;
using System.Threading.Tasks;
using BlazingPay.Abstractions.Contracts;
using Microsoft.Extensions.FileProviders;

namespace BlazingPay.Abstractions.Services
{
    public class FileProviderLocalContentFetcher : ILocalContentFetcher
    {
        private readonly IFileProvider _fileProvider;

        public FileProviderLocalContentFetcher(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }

        public Task<Stream> Fetch(string path)
        {
            var fileInfo = _fileProvider.GetFileInfo(path);

            return fileInfo is {Exists: true} ? Task.FromResult(fileInfo.CreateReadStream()) : Task.FromResult<Stream>(null);
        }
    }
}