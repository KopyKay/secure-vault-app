using System.IO;

namespace SecureVaultApp.Converter
{
    public class FileBlobConverter
    {
        private byte[] _blobData = null;

        public byte[] FileToBlob(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    _blobData = br.ReadBytes((int)fs.Length);
                }
            }
            return _blobData;
        }

        public void BlobToFile(byte[] blobData, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                fs.Write(blobData, 0, blobData.Length);
            }
        }
    }
}
