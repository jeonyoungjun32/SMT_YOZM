using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPOP.Models.IF
{
    public enum PLCType
    {
        LS,
        Mitsubishi
    }

    public class PLCCollector
    {
        public string CTCode { get; set; }

        public string IP { get; set; }

        public int Port { get; set; }

        public PLCType Type { get; set; }

        private IPlcDriver driver;

        public void Start(string ip, int port, PLCType type, string ctCode)
        {
            switch (type)
            {
                case PLCType.LS:
                    driver = new LSPlcDriver();
                    break;

                case PLCType.Mitsubishi:
                    driver = new MitsubishiPlcDriver();
                    break;
            }

            driver.Start(ip, port, ctCode);
        }

        public void Stop()
        {
            driver?.Stop();
        }
    }
}
