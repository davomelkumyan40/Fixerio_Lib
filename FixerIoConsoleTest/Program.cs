using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNet_FixerIo;
using DotNet_FixerIo.Manage;

namespace FixerIoConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            FixerIo fixer = new FixerIo("b3dfdaf429146cbaff9074aed1db7789");
            Fluctuate info = fixer.GetFluctuation(new DateTime(2017, 5, 12), new DateTime(2017, 9, 12));
            //info.Rates.TryGetValue("AMD", out decimal value);
            RateInfo inf = fixer.GetRatesBy("EUR", new DateTime(2017, 5, 25), "AMD", "RUB");
            //Console.WriteLine(value);
            TimeSeries s = new TimeSeries();
        }
    }
}
