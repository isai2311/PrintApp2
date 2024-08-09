using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using System.Collections.Generic;
using static PrintApp2.clsDocs;
using System.Text.RegularExpressions;
using Receipt;

namespace PrintApp2
{
    class FuncionesGenerales
    {
        //Variable para indicar el id de la empresa en uso
        public static int Empresa = 0;

        //Variables para impresion de ticket
        public static System.Drawing.Font LetraImpresion;
        StreamReader ArchivoImprimir;

        //Encriptacion password usando MD5
        public string GetMd5Hash(string input)
        {
            MD5 md5Hash = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        //Verifica si se cuenta con acceso a internet, mediante ping a una pagina
        public bool verificaInternet()
        {
            bool Conexion = false;
            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry("www.google.com");
                Conexion = true;
            }
            catch (Exception es)
            {
                Log(DateTime.Now, "Sin Conexion a internet", es.ToString());
                MessageBox.Show("No se cuenta con Conexion a internet, el sistema se cerrara automaticamente", "ALERT", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Conexion = false;
            }

            return Conexion;
        }

        //Verifica Conexion al servidor mediante ping
        public bool verificaServidor(string Servidor)
        {
            bool Conexion = false;
            try
            {
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(Servidor);
                Conexion = true;
            }
            catch (Exception es)
            {
                Log(DateTime.Now, "Sin Conexion con el servidor", es.ToString());
                MessageBox.Show("No se ha recibido respuesta del servidor", "ALERTA", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Conexion = false;
            }

            return Conexion;
        }

        //Guarda un documento con los errores que se presenten
        public void Log(DateTime fecha, string titulo, string cuerpo)
        {
            try
            {
                string dir = Directory.GetCurrentDirectory() + @"\Log\Errores\";
                string file = dir + fecha.ToString("yyyyMMdd") + ".txt";
                bool vacio = true;

                VerificarDirectorio(dir);

                if (File.Exists(file))
                {
                    StreamReader sr = File.OpenText(file);
                    string line = sr.ReadLine();

                    if (line == null)
                        vacio = true;

                    else
                        vacio = false;

                    sr.Close();
                }

                StreamWriter sw = File.AppendText(file);

                if (!vacio)
                {
                    sw.WriteLine("");
                    sw.WriteLine("");
                }


                sw.WriteLine(fecha.ToString("HH:mm:ss") + "  " + titulo);
                sw.WriteLine(cuerpo);
                sw.Close();

            }
            catch (Exception ex)
            {
                string excepcion = ex.ToString();
            }
        }

        //Inserta una imagen en base 64 guardada desde la DB en un objeto de tipo Image
        public void obtenLogo(string cadena, System.Windows.Controls.Image img)
        {
            try
            {
                if (!String.IsNullOrEmpty(cadena))
                {
                    string[] separa = cadena.Split(',');
                    byte[] binaryData = Convert.FromBase64String(separa[1]);
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    bi.StreamSource = new MemoryStream(binaryData);
                    bi.EndInit();

                    if (bi != null)
                        img.Source = bi;
                }
            }
            catch (Exception ex)
            {
                Log(DateTime.Now, "Error al asignar imagen el el contenedor " + img.Name, ex.ToString());
            }
        }

        // Verfica que el directorio exista y de lo contrario lo crea
        public void VerificarDirectorio(string Directorio)
        {
            if (!System.IO.Directory.Exists(Directorio))
                System.IO.Directory.CreateDirectory(Directorio);
        }

        /*La funcion detecta por tecla y no diferencia entre mayusculas y minusculas
        revisar el correcto funcionamiento*/
        public bool soloPermitidos(object sender, System.Windows.Input.KeyEventArgs e, bool letras,
            bool espacios, bool numeros, bool decimales, bool especiales)
        {
            var caracter = System.Enum.ToObject(e.Key.GetType(), (byte)e.Key).ToString();


            string tipObj = sender.GetType().Name;
            System.Windows.Controls.TextBox control = (System.Windows.Controls.TextBox)sender;

            bool respuesta = true; int ascii = 0;
            //permite el paso directo de 
            if (e.Key == Key.Back)
                respuesta = false;

            else if (e.Key == Key.Back)
                respuesta = false;

            else
            {
                //string caracter = e.Key.ToString();
                ascii = Convert.ToInt32(Convert.ToChar(caracter));
                /*Caracteres Especiales permitidos
                "33-> !", "34-> "", "35-> #", "36-> $", "37-> %", "38-> &", "39-> '", "40-> (", "41-> )", "42-> *", "43-> +", 
                "44-> ,", "45-> -", "46-> .", "47-> /", "58-> :", "59-> ;", "60-> <", "61-> =", "62-> >", "63-> ?", "64-> @", 
                "91-> [", "92-> \", "93-> ]", "94-> ^", "95-> _", "96-> `", "123-> {", "124-> |", "125-> }", "126-> ~", "168-> ¿" */

                int[] permitidos = { 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 58, 59, 60, 61, 62, 63,
                                64, 91, 92, 93, 94, 95, 96, 123, 124, 125, 126, 168 };

                //Verificacion si la tecla es una letra
                if (letras && ((ascii >= 65 && ascii <= 90) || (ascii >= 97 && ascii <= 122) || (ascii >= 160 && ascii <= 165)
                    || ascii == 130))
                    respuesta = false;

                //Verifica si es un espacios
                else if (espacios && ascii == 32)
                    respuesta = true;

                //Verificacion si es un numero
                else if (numeros && (ascii >= 48 && ascii <= 57))
                    respuesta = true;

                //Verifica si es un punto, solo si esta permitido el decimal
                else if (numeros && decimales && ascii == 46)
                {
                    if (!control.Text.Contains("."))
                        respuesta = false;
                }

                //Verifica si es un caracter especial permitidos
                else if (especiales && (Array.IndexOf(permitidos, ascii) >= 0))
                    respuesta = false;

                //Verifica si es un caracter especial permitidos
                else if (especiales && (Array.IndexOf(permitidos, ascii) >= 0))
                    respuesta = false;

                //el enter hace lo mismo que TAB
                else if (especiales && (Array.IndexOf(permitidos, ascii) >= 0))
                    respuesta = false;
            }

            return respuesta;
        }

        //Acomoda el texto en un maximo de columnas y lo alinea, si no es multilinea, se trunca el texto
        public string ajustaTexto(string Entrada, int Columnas, string Alineacion, bool Multilinea)
        {
            string cadena = "";
            char caracter = ' ';

            if (Entrada.Trim().Length > Columnas && Multilinea)
                cadena = Entrada.Substring(0, Columnas)
                    + "\n" + ajustaTexto(Entrada.Substring(Columnas), Columnas, Alineacion, Multilinea);
            else
            {
                if (Entrada.Trim().Length > Columnas)
                    cadena = Entrada.Trim().Substring(0, Columnas - 1);
                else
                    cadena = Entrada.Trim();

                int necesarios = Columnas - cadena.Length;

                switch (Alineacion)
                {
                    case "D":
                        for (int i = 0; i < necesarios; i++)
                            cadena = caracter + cadena;
                        break;
                    case "I":
                        for (int i = 0; i < necesarios; i++)
                            cadena = cadena + caracter;
                        break;
                    case "C":
                        necesarios = necesarios / 2;
                        for (int i = 0; i < necesarios; i++)
                            cadena = caracter + cadena + caracter;
                        break;
                }
            }

            return cadena;
        }

        //Genera el ticket de la operacion
        public string GeneraTicket(Empresas Empresa, List<Detalles> Detalle, Tickets Ticket)
        {
            int columnas = 30;
            string letra = "Lucida Console";
            float size = 10;
            string mensaje1 = "GRACIAS POR SU COMPRA!!";
            string mensaje2 = "";
            string mensaje3 = "";
            FontStyle estilo = FontStyle.Bold;

            string rootFolder = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string sDirectorio = rootFolder + @"\Registros\Tickets\Operacion\"
                + Ticket.created_at.ToString("yyyyMMdd") + @"\";
            VerificarDirectorio(sDirectorio);
            string fileName = sDirectorio + @"Ticket " + Ticket.created_at.ToString("yyyyMMdd") + ".txt";
            FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            LetraImpresion = new System.Drawing.Font(letra, size, estilo);
            StreamWriter writer = new StreamWriter(stream);
            string Separador = "".PadRight(columnas, '=');

            //encabezado
            writer.WriteLine(ajustaTexto("", columnas, "C", true));
            writer.WriteLine(ajustaTexto("", columnas, "C", true));

            writer.WriteLine(ajustaTexto(Empresa.cEmpresaNombre, columnas, "C", true));
            writer.WriteLine(ajustaTexto("", columnas, "I", true));
            writer.WriteLine(ajustaTexto("", columnas, "I", true));

            writer.WriteLine(ajustaTexto(Empresa.cEmpresaRFC, columnas, "C", true));

            writer.WriteLine(ajustaTexto(convierteEspeciales(Empresa.cEmpresaCalle), columnas, "C", true));
            writer.WriteLine(ajustaTexto(convierteEspeciales(Empresa.cEmpresaColonia), columnas, "C", true));
            writer.WriteLine(ajustaTexto(Empresa.cEmpresaCP, columnas, "C", true));
            writer.WriteLine(ajustaTexto(convierteEspeciales(Empresa.cEmpresaCiudad) + " " + Empresa.cEmpresaEstado
                + " " + convierteEspeciales(Empresa.cEmpresaPais), columnas, "C", true));
            writer.WriteLine(ajustaTexto(Separador, columnas, "I", true));
            writer.WriteLine(ajustaTexto("", columnas, "I", true));

            writer.WriteLine(ajustaTexto("TICKET # " + Ticket.consecutivo.ToString(), columnas, "I", true));
            //writer.WriteLine(ajustaTexto("ATENDIO: " + Ticket.responsable, columnas, "I", true));
            writer.WriteLine(ajustaTexto("CLIENTE: " + Ticket.cTicketNombre, columnas, "I", true));
            writer.WriteLine(ajustaTexto("FECHA: " + Ticket.created_at.ToString("dd/MM/yyyy HH:mm:ss"),
                columnas, "I", true));

            writer.WriteLine(ajustaTexto("", columnas, "I", true));
            writer.WriteLine(ajustaTexto(Separador, columnas, "I", true));

            writer.WriteLine(ajustaTexto("", columnas, "I", true));


            for (int i = 0; i < Detalle.Count; i++)
            {
                string codigo = Detalle[i].producto.cProductoInterCodigo;
                string descripcion = Detalle[i].producto.cProductoDescripcion;
                decimal cantidad = Convert.ToDecimal(Detalle[i].cTicketDetalleCantidad);
                decimal precio = Convert.ToDecimal(Detalle[i].cTicketDetallePrecio);
                decimal total = Convert.ToDecimal(Detalle[i].cTicketDetalleTotal);
                decimal subtotal = Convert.ToDecimal(Detalle[i].cTicketDetalleSubtotal);
                writer.WriteLine(ajustaTexto(cantidad.ToString() +" "+ codigo, columnas / 2, "I", true)
                + ajustaTexto(string.Format(MONEY_FORMAT, subtotal), columnas / 2, "D", true));
                writer.WriteLine(ajustaTexto("", columnas, "I", true));

            }
            writer.WriteLine(ajustaTexto("", columnas, "I", true));
            writer.WriteLine(ajustaTexto(Separador, columnas, "I", true));
            writer.WriteLine(ajustaTexto("", columnas, "I", true));

            decimal impuestoTicket = Ticket.cTicketImpuesto1;
            decimal subtotalTicket = Ticket.cTicketSubtotal;

            if (impuestoTicket > 0)
            {
                writer.WriteLine(ajustaTexto("SUBTOTAL", columnas / 2, "I", true)
                + ajustaTexto(string.Format(MONEY_FORMAT, subtotalTicket), columnas / 2, "D", true));
                writer.WriteLine(ajustaTexto("IVA", columnas / 2, "I", true)
                + ajustaTexto(string.Format(MONEY_FORMAT, impuestoTicket), columnas / 2, "D", true));
            }
            writer.WriteLine(ajustaTexto("TOTAL", columnas / 2, "I", true)
                + ajustaTexto(string.Format(MONEY_FORMAT, Ticket.cTicketTotal), columnas / 2, "D", true));
            writer.WriteLine(ajustaTexto("", columnas, "I", true));


            writer.WriteLine(ajustaTexto("", columnas, "I", true));
            
            if(Ticket.cTicketPagado == 0)
            {
                writer.WriteLine(ajustaTexto("CREDITO", columnas, "I", true));
                writer.WriteLine(ajustaTexto("", columnas, "I", true));
            }


            writer.WriteLine(ajustaTexto(mensaje1, columnas, "C", true));
            writer.WriteLine(ajustaTexto(mensaje2, columnas, "C", true));
            writer.WriteLine(ajustaTexto(mensaje3, columnas, "C", true));

            writer.WriteLine(ajustaTexto(".", columnas, "I", true));


            writer.Close();
            return fileName;
        }

        //Imprime el documento
        public void ImprimirArchivo(string Archivo, string Impresora, string Logo, string Descripcion)
        {
            try
            {
                string text = AppDomain.CurrentDomain.BaseDirectory + "logo.png";
                
                ArchivoImprimir = new StreamReader(Archivo);
                PrintDocument pr = new PrintDocument();
                pr.DocumentName = Descripcion;
                pr.PrintController = new StandardPrintController();
                // imprimir logo 
                pr.PrintPage += new PrintPageEventHandler(AddLogo);
                pr.PrintPage += new PrintPageEventHandler(PrintPage);
                pr.PrinterSettings.PrinterName = Impresora;
                pr.Print();
                ArchivoImprimir.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible imprimir el ticket", "ALERTA",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log(DateTime.Now, "Imposible imprimir el ticket", ex.ToString());
            }
        }

        public void AddLogo(object o, PrintPageEventArgs e)
        {
            string text = AppDomain.CurrentDomain.BaseDirectory + "logo.png";
            System.Drawing.Image img = System.Drawing.Image.FromFile(text);
            Point loc = new Point(100, 100);
            e.Graphics.DrawImage(img, loc);
        }

        //Configuracion necesaria para impresion
        public void PrintPage(object sender, PrintPageEventArgs e)
        {
            float yPos = 0f, leftMargin = 0, topMargin = 0;
            int count = 0;
            string line = null;

            SolidBrush myBrush = new SolidBrush(Color.Black);
            float linesPerPage = e.MarginBounds.Height / LetraImpresion.GetHeight(e.Graphics);
            while (count < 100000)
            {
                line = ArchivoImprimir.ReadLine();
                if (line == null)
                    break;

                yPos = topMargin + count * LetraImpresion.GetHeight(e.Graphics);
                e.Graphics.DrawString(line, LetraImpresion, myBrush, leftMargin, yPos, new StringFormat());
                count++;
            }

            if (line != null)
                e.HasMorePages = true;
        }

        public string convierteEspeciales(string Cadena)
        {
            if (Cadena != null)
            {
                return Regex.Replace(Cadena, @"[^a-zA-z0-9 ]+", "");
            }
            return "";
            string textoNormalizado = Cadena.Normalize(NormalizationForm.FormD);
            Regex reg = new Regex("[^a-zA-Z0-9 ]");
            //coincide todo lo que no sean letras y números ascii o espacio
            //y lo reemplazamos por una cadena vacía.Regex reg = new Regex("[^a-zA-Z0-9 ]");
            string textoSinAcentos = reg.Replace(textoNormalizado, "");
            return textoSinAcentos;
            return Cadena.Replace(",", "§").Replace("°", "¶").Replace("\"", "Æ").Replace("'", "~")
                .Replace("¬", " ");
        }

        public string leerEspeciales(string Cadena)
        {
            return Cadena.Replace("§", ",").Replace("¶", "°").Replace("Æ", "\"").Replace("~", "'");
        }

    }
}
