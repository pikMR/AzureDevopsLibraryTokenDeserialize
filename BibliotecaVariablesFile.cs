using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VisualTokenizacionWConfig.Logic
{
    public class BibliotecaVariablesFile
    {
        private string PathAndFile;
        public BibliotecaVariablesFile(string PathAndFile)
        {
            if (!File.Exists(PathAndFile))
            {
                throw new FileNotFoundException("El fichero correspondiente al json api no se encuentra, debería estar en : "+ PathAndFile);
            }

            this.PathAndFile = PathAndFile;
        }
        public JObject ObtenerJSONDeVariables()
        {
            return JObject.Parse(File.ReadAllText(PathAndFile));
        }
    }
}
