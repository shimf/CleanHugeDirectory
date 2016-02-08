using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanHugeDirectory.Main
{
    public class FoundFileData
    {
        public string AlternateFileName;
        public FileAttributes Attributes;
        public DateTime CreationTime;
        public string FileName;
        public DateTime LastAccessTime;
        public DateTime LastWriteTime;
        public UInt64 Size;

        internal FoundFileData(ref WIN32_FIND_DATA win32FindData)
        {
            Attributes = (FileAttributes)win32FindData.dwFileAttributes;
            CreationTime = DateTime.FromFileTime((long)
                    (((UInt64)win32FindData.ftCreationTime.dwHighDateTime << 32) +
                     (UInt64)win32FindData.ftCreationTime.dwLowDateTime));

            LastAccessTime = DateTime.FromFileTime((long)
                    (((UInt64)win32FindData.ftLastAccessTime.dwHighDateTime << 32) +
                     (UInt64)win32FindData.ftLastAccessTime.dwLowDateTime));

            LastWriteTime = DateTime.FromFileTime((long)
                    (((UInt64)win32FindData.ftLastWriteTime.dwHighDateTime << 32) +
                     (UInt64)win32FindData.ftLastWriteTime.dwLowDateTime));

            Size = ((UInt64)win32FindData.nFileSizeHigh << 32) + win32FindData.nFileSizeLow;
            FileName = win32FindData.cFileName;
            AlternateFileName = win32FindData.cAlternateFileName;
        }
    }
}
