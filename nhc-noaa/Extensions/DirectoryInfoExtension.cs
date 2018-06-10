using System;
using System.IO;
using System.Linq;

namespace nhc_noaa
{
    public static class DirectoryInfoExtension
    {
        public static FileInfo[] GetOldestFiles(this DirectoryInfo value, int count)
        {
            return value.GetFiles().OrderBy(p => p.CreationTime).Take(count).ToArray();
        }

        public static FileInfo[] GetLatestFiles(this DirectoryInfo value, int count)
        {
            return value.GetFiles().OrderByDescending(p => p.CreationTime).Take(count).ToArray();
        }

        public static FileInfo[] GetLatestFiles(this DirectoryInfo value, int count, DateTime? min, DateTime? max)
        {
            return value.GetLatestFiles(count)
                 .Where(x => (
                        (x.CreationTime > (min ?? DateTime.MinValue)) &&
                        (x.CreationTime < (max ?? DateTime.MaxValue)))
                    ).ToArray();
        }
    }
}