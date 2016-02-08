using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CleanHugeDirectory.Main
{
    /// <summary>
    /// Safely wraps handles that need to be closed via FindClose() WIN32 method (obtained by FindFirstFile())
    /// </summary>
    public class SafeFindFileHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FindClose(SafeHandle hFindFile);

        public SafeFindFileHandle(bool ownsHandle)
            : base(ownsHandle)
        {
        }

        protected override bool ReleaseHandle()
        {
            return FindClose(this);
        }
    }
}
