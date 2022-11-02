using System.IO.Compression;
using System.IO;
using System.Text;
using System;

namespace PrintApp2
{
    internal class GZ
    {
        public static string Descomprimir(string Texto)
        {
            byte[] finalBytes;
            var base64 = Convert.FromBase64String(Texto);

            // creamos un buffer de memoria
            var bufferInicial = new MemoryStream(base64);

            using (var dfStream = new DeflateStream(bufferInicial, CompressionMode.Decompress))
            {
                // creamos un nuevo bufer de memoria
                using (var bufferFinal = new MemoryStream())
                {
                    dfStream.CopyTo(bufferFinal);

                    finalBytes = bufferFinal.ToArray();
                }

                var data = Encoding.UTF8.GetString(finalBytes);
                return data;
            }
        }
    }
}