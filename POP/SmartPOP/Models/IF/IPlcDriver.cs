using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.IF
{
    public interface IPlcDriver
    {
        void Start(string ip, int port, string ctCode);
        void Stop();
    }
}
