using System.IO;

namespace SecureVaultApp.Converter
{
    public class FileBlobConverter
    {
        private byte[] blobData = null;

        public byte[] FileToBlob(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        this.blobData = br.ReadBytes((int)fs.Length);
                    }
                }
                return this.blobData;
            }
            catch
            {
                throw;
            }
        }

        public void BlobToFile(byte[] blobData, string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(blobData, 0, blobData.Length);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
