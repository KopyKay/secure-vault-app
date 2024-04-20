using SecureVaultApp.Converter;
using System.Collections.Generic;
using Windows.Storage;

namespace SecureVaultApp.Controller
{
    public class AppController
    {
        private FileBlobConverter _blobConverter;

        public List<string> sortByOptions { get; private set; }

        public AppController()
        {
            _blobConverter = new FileBlobConverter();
            sortByOptions = new() { "Date", "Size", "Type", "Name" };
        }

        public byte[] ConvertFileToBlob(StorageFile file)
        {
            var blobData = _blobConverter.SaveFileAsBlob(file);

            return blobData;
        }

        public void ConvertBlobToFile(byte[] blobData, string destinationPath)
        {
            _blobConverter.SaveBlobAsFile(blobData, destinationPath);
        }
    }
}
