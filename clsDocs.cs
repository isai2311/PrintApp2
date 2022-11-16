using Microsoft.Win32;
using System.Collections.Generic;
using System;
using System.Data;
using Newtonsoft.Json;
using Receipt;
using static PrintApp2.clsDocs;

namespace PrintApp2
{
    internal class clsDocs
    {
        public const string MONEY_FORMAT = "{0:C2}";

        public static void parseData(string printerName, string jsonText)
        {
            try
            {
                string valor = printerName;
                string valor2 = jsonText;
                string[] parts = jsonText.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                Tickets ticket = JsonConvert.DeserializeObject<Tickets>(parts[0]);
                List<Detalles> detalle = JsonConvert.DeserializeObject<List<Detalles>>(parts[1]);
                Empresas empresa = JsonConvert.DeserializeObject<Empresas>(parts[2]);
                FuncionesGenerales fn1 = new FuncionesGenerales();
                fn1.ImprimirArchivo(fn1.GeneraTicket(empresa, detalle, ticket), printerName, empresa.cEmpresaLogo,
                    "Ticket " + ticket.cTicketFactura.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void createProtocol()
        {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"print");
            if (key == null)
            {
                //crear protocolo
                RegistryKey subkeyPrint = Registry.ClassesRoot.CreateSubKey("print");
                subkeyPrint.SetValue(null, "URL:print");
                subkeyPrint.SetValue("EditFlags", 2);
                subkeyPrint.SetValue("URL Protocol", "");

                //crear shell
                RegistryKey subkeyShell = subkeyPrint.CreateSubKey("shell");
                subkeyShell.SetValue(null, "open");

                RegistryKey subkeyOpen = subkeyShell.CreateSubKey("open");
                subkeyOpen.SetValue(null, "");

                RegistryKey subkeyCommand = subkeyOpen.CreateSubKey("command");
                subkeyCommand.SetValue(null, "\"C:\\Printer\\PrintApp2.exe\" \"%1\"");
                //windowmanager.Show("PROTOCOLO REGISTRADO EN EL SO", "PrinterApp", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}