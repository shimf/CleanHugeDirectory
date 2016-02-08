using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanHugeDirectory.Main
{
    public class FilesFinder : IEnumerable<FoundFileData>
    {
        readonly string _fileName;
        public FilesFinder(string fileName)
        {
            _fileName = fileName;
        }

        public IEnumerator<FoundFileData> GetEnumerator()
        {
            return new FilesEnumerator(_fileName);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
