using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            clsDocs.createProtocol();
            string printerName = "EPSON1";
            string saleInfo = "";
            string jsonText = "";
            //validar información
            if (args.Length == 0 || args == null)
            {
                //System.Environment.Exit(0); //cerrar app
                args = new string[] { "print://fVZtb6M4EP4rEfe1iSBvavhGCVHZa5oIkkq3e6uTY5ycdwHnDOyHdvvfb8xbjDGpqkR5npnx2PPM2B8GPlD8k+QbFlNm2NPZQ4O4MSVpTgzbaqENwnnBkc7MXxt2WsRxi7+y5MTB21iThBk3c8Y5YYrtgcTkzFIV3hD8LyxmWObYWo2n5nR6i+NlOcqLzLBvmRxYjmLDfrQm8xYLi1Ougf3kWpAsZ5Zhmy24JhkuYC9MC+4Zx/CNfhCZ3hCiJB0WuOCZWPF2bvsY5ejMeIIU42fGUUAwu6hRDvTaSePAEf5J04uMwSY4TcBzIR16ri5xPFGMMGWpggdFzzQgV8RzGjGuEM41hiBRJ6OQpph3kD26KDa+v6l+coJyEv2DcqimqOMYSjo1D6Zlz6f2zJqY5d9XKC8n2ZWlGTrFQjt+6PgArsF7I44vJ+UKbQxrBWyIYgFWBS5F4Ke/GMWkwUAEOrgRQQubdSzFrla9WOOM4ow8GFd0YSC9b98fjHPTEh8Grg3lVqqhVq3WDQvBDNMcRaiO+/n5+9vHTXeQcEyaWI8PClH96HRizew5iwosVGzNVU4nztaNYMqUPqk5F6UgCxRp3AYarMlymBrotpIbaM8qF5aINuRUVKCj0trgpbgg7qU5JxdV4M1W0X8FyYk2OIQu9d+n0A+k2b/LMu0WoAqYZEo/KMfZ2Ayk2ZqRiIo4vbWPqaC3gtamBu1yYZyiprTVItWvsjLtPK8KdG2VAzJsZOSyiF4aMTWgKB6n12qsGGvf2e5e1yP32Xv7ayRCwcdSfDyORqsldLplGpK3D/cFb+Iab+F4ZpnQy+N1x6irARWe6uGZHp534fZkqlNrYNCyxCwkpn/SOiYkuEgjVHrXBW1XjFFGz+okblinyNkWzuNcTlneTbZDKI4SJ9TSXxYuYaZHYaeWnhE3HWQ5wAq/4bWAnd6NOsAKv7tRZ3ejDrDC727U+d2oA6zwuxt1cTfqACv8tFH3BQzJrA8jmnWl+MTirK+8PSXvffQJwfSNNTpl0RPiHPXXExrl9L2daFLyVY914UAMaQ20puJ+p+X1rqxQ8n5OEj0jzRzAk76sS6vm2anjxGvLT6k0C7t8QCKWRqTPblBCY01Xw8A4D1DwhqiuqeolI9dpvwu7B1MLQ7x0y5dQb1ZilJyoNCuNz8/vv2FGe/AChAukfiSINWqkfXwH3sY5eH8aNypA7ywNGabiYi551/V3r07gO7LVxhXL+eF2aZnL6eKLu5TY8hBRTLLKRmLexKazTi6uuIvA0J2M9kcvOOxGzsvB2fqBM/rDmluW5O2yGLQhnvzu7mUyOvheAFavR+9Nzs2lRfkWMb4c/y5ME1uB91WixVurfCS6z/7z0YF/2XdfZrKfjGbTxWolMVU/1VWssRd2YSr2tIbDpSnqGVcP4hRapB7gNe7AHYtiAQv98bKO5sw2TVsU8n8=" };
            }
            readPrinter(ref printerName);

            //saleId = args[0].Replace("print://", string.Empty).Replace("/",string.Empty);
            saleInfo = args[0].Substring(8);
            jsonText = GZ.Descomprimir(saleInfo);
            clsDocs.parseData(printerName, jsonText);
        }

        private static void readPrinter(ref string printerName)
        {
            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string textFile = rootFolder + @"\printer.txt";

            if (File.Exists(textFile))
            {
                printerName = File.ReadAllText(textFile);
            }
            else
            {
                printerName = "EPSON1";
            }
        }
    }
}
