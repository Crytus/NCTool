using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace NCTool
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NCTool());

            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                Console.WriteLine(port);
            }
            Console.ReadLine();

        }
    }
}
