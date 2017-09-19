using System.IO;

namespace nhc_noaa
{
    public static class FileInfoExtension
    {
        public static string FirstAndLast(this FileInfo[] files, string oldValue, string newValue)
        {
            return files[0].Name.Replace(oldValue, newValue) + "_" +
                   files[files.Length - 1].Name.Replace(oldValue, newValue);
        }
    }
}