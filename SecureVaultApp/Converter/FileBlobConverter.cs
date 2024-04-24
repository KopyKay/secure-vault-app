using System.IO;
using Windows.Storage;

namespace SecureVaultApp.Converter
{
    public class FileBlobConverter
    {
        public byte[] SaveFileAsBlob(StorageFile file)
        {
            byte[] blobData;

            using (var stream = file.OpenStreamForReadAsync().Result)
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    blobData = memoryStream.ToArray();
                }
            }

            return blobData;
        }

        public async void SaveBlobAsFileAsync(byte[] blobData, string destinationPath)
        {
            using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
            {
                await fileStream.WriteAsync(blobData, 0, blobData.Length);
            }
        }
    }
}
