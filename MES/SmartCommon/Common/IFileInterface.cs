using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCommon.Common
{
    public interface IFileInterface
    {
        void SetData(string Filename, long FileCurrent, long fileSize, int CurrentRate, long TotalCurrent, long TotalSize, int TotalRate);
        void InsertDB(int fileSeq, string title, string oriFileName, string fileName, long capacity, string fileExt);
    }
}
