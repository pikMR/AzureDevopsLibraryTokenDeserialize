using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace VisualTokenizacionWConfig.Logic
{
    public class FileConfiguration
    {
        private string NAME_FILE_CONFIG;
        private string PATH_CONFIG;
        public string FILE_CONFIG;

        public FileConfiguration(string PATH_CONFIG, string NAME_FILE_CONFIG)
        {
            this.NAME_FILE_CONFIG = NAME_FILE_CONFIG;
            this.PATH_CONFIG = PATH_CONFIG;
            this.FILE_CONFIG = PATH_CONFIG + NAME_FILE_CONFIG;
        }

        public string Get()
        {
            if (!File.Exists(FILE_CONFIG))
            {
                throw new FileNotFoundException();
            }

            string readText = File.ReadAllText(FILE_CONFIG);
            return readText;
        }

        public void ReplaceAppSettings(IDictionary<string, string> dic, string nombreFichero = "")
        {
            if (!File.Exists(FILE_CONFIG))
            {
                throw new FileNotFoundException($"Se necesita establecer un fichero {NAME_FILE_CONFIG} en la ruta {PATH_CONFIG}");
            }

            XDocument xdoc = XDocument.Load(FILE_CONFIG);
            var ElementsOfAppsettings = xdoc.Descendants("appSettings").Elements();

            foreach(XElement ele in ElementsOfAppsettings)
            {
                var claveNodo = ele.Attribute("key");
                string dicToken = $"TOKEN_{claveNodo.Value}";
                
                if (dic.ContainsKey(dicToken))
                {
                    string nuevoValor;
                    dic.TryGetValue(dicToken, out nuevoValor);
                    ele.SetAttributeValue("value", nuevoValor);
                }
            }
            xdoc.Save(PATH_CONFIG + "\\" + nombreFichero);
        }

        /// <summary>
        /// Metodo de Prueba que remplaza una palabra concreta {FWK4} por un valor concreto {FWK5}
        /// </summary>
        /// <returns></returns>
        public string Replace()
        {
            // Open a stream for the source file
            string nuevoNombre = string.Empty;
            using (var sourceFile = File.OpenText(FILE_CONFIG))
            {
                var diff = DateTime.Now.ToString("MMddmmss");
                nuevoNombre = $"{diff}{NAME_FILE_CONFIG}";
                // Create a temporary file path where we can write modify lines
                string tempFile = Path.Combine(Path.GetDirectoryName(FILE_CONFIG),nuevoNombre);
                // Open a stream for the temporary file
                using (var tempFileStream = new StreamWriter(tempFile))
                {
                    string line;
                    // read lines while the file has them
                    while ((line = sourceFile.ReadLine()) != null)
                    {
                        // Do the word replacement
                        line = line.Replace("X", "Y");
                        // Write the modified line to the new file
                        tempFileStream.WriteLine(line);
                    }
                }
            }
            var nuevo = PATH_CONFIG + "\\" + nuevoNombre;
            File.Replace(nuevo, FILE_CONFIG, FILE_CONFIG + ".old");
            return nuevoNombre;
        }
    }
}
