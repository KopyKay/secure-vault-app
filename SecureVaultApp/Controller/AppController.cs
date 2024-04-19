using SecureVaultApp.Converter;
using Windows.Storage;

namespace SecureVaultApp.Controller
{
    public class AppController
    {
        private FileBlobConverter _blobConverter;

        public AppController()
        {
            _blobConverter = new FileBlobConverter();
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
