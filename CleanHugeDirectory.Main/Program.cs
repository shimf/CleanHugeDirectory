using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanHugeDirectory.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            for(char A1 = 'A'; A1 <= 'Z'; A1++)
                for(char A2 = 'A'; A2 <= 'Z'; A2++)
                {
                    var prefix = string.Format("{0}{1}*.HTM", A1, A2);
                    Console.WriteLine("Prefix:{0}", prefix);
                    foreach (var file in GetFilesUnmanaged(@"C:\Users\Administrator\AppData\Local\Microsoft\Windows\Temporary Internet Files\Content.IE5", prefix))
                        File.Delete(file);
                }

            for (char A1 = '0'; A1 <= '9'; A1++)
                for (char A2 = '0'; A2 <= '9'; A2++)
                {
                    var prefix = string.Format("{0}{1}*.HTM", A1, A2);
                    Console.WriteLine("Prefix:{0}", prefix);
                    foreach (var file in GetFilesUnmanaged(@"C:\Users\Administrator\AppData\Local\Microsoft\Windows\Temporary Internet Files\Content.IE5", prefix))
                        File.Delete(file);
                }
        }

        public static IEnumerable<string> GetFilesUnmanaged(string directory, string filter)
        {
            return new FilesFinder(Path.Combine(directory, filter))
                .Where(f => (f.Attributes & FileAttributes.Normal) == FileAttributes.Normal
                    || (f.Attributes & FileAttributes.Archive) == FileAttributes.Archive)
                .Select(s => s.FileName);
        }
    }
}

