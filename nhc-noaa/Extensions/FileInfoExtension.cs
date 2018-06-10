using Microsoft.WindowsAzure.Storage;
using System.IO;
using System.Threading.Tasks;

namespace nhc_noaa
{
    public static class FileInfoExtension
    {
        public static string FirstAndLast(this FileInfo[] files, string oldValue, string newValue)
        {
            return files[0].Name.Replace(oldValue, newValue) + "_" +
                   files[files.Length - 1].Name.Replace(oldValue, newValue);
        }

        public static async Task Upload(this FileInfo file, string connectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var fileClient = storageAccount.CreateCloudFileClient();
            var share = fileClient.GetShareReference("images");
            if (share.Exists())
            {
                var dir = share.GetRootDirectoryReference();
                var sourceFile = dir.GetFileReference(file.Name);
                await sourceFile.UploadFromFileAsync(file.FullName);
                file.Delete();
            }
        }
    }
}