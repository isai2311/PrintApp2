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
                args = new string[] { "print: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" };
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
