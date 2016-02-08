using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CleanHugeDirectory.Main
{
    public class FilesEnumerator : IEnumerator<FoundFileData>
    {
        #region Interop imports

        private const int ERROR_FILE_NOT_FOUND = 2;
        private const int ERROR_NO_MORE_FILES = 18;

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr FindFirstFile(string lpFileName, out WIN32_FIND_DATA lpFindFileData);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool FindNextFile(SafeHandle hFindFile, out WIN32_FIND_DATA lpFindFileData);

        #endregion

        #region Data Members

        private readonly string _fileName;
        private SafeHandle _findHandle;
        private WIN32_FIND_DATA _win32FindData;

        #endregion

        public FilesEnumerator(string fileName)
        {
            _fileName = fileName;
            _findHandle = null;
            _win32FindData = new WIN32_FIND_DATA();
        }

        #region IEnumerator<FoundFileData> Members

        public FoundFileData Current
        {
            get
            {
                if (_findHandle == null)
                    throw new InvalidOperationException("MoveNext() must be called first");

                return new FoundFileData(ref _win32FindData);
            }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            if (_findHandle == null)
            {
                _findHandle = new SafeFileHandle(FindFirstFile(_fileName, out _win32FindData), true);
                if (_findHandle.IsInvalid)
                {
                    int lastError = Marshal.GetLastWin32Error();
                    if (lastError == ERROR_FILE_NOT_FOUND)
                        return false;

                    throw new Win32Exception(lastError);
                }
            }
            else
            {
                if (!FindNextFile(_findHandle, out _win32FindData))
                {
                    int lastError = Marshal.GetLastWin32Error();
                    if (lastError == ERROR_NO_MORE_FILES)
                        return false;

                    throw new Win32Exception(lastError);
                }
            }

            return true;
        }

        public void Reset()
        {
            if (_findHandle.IsInvalid)
                return;

            _findHandle.Close();
            _findHandle.SetHandleAsInvalid();
        }

        public void Dispose()
        {
            _findHandle.Dispose();
        }

        #endregion
    }
}
