using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisualTokenizacionWConfig.Logic
{
    public class HelperJsonNet
    {
        public static Dictionary<string, string> ObtieneClavesValorDelGrupoDeVariable(JObject json, string grupoDeVariable)
        {
            return json["value"]
                .Where(x => x["name"].ToString() == grupoDeVariable)
                .Select(x => (JObject)x["variables"])
                .Properties()
                .Select(x => new { NombreVariable = x.Name, ValorVariable = x.Value["value"].ToString() })
                .ToDictionary(t => t.NombreVariable, t => t.ValorVariable);
        }

        public static List<string> ObtieneTodosLosTokens(JObject json)
        {
            return json["value"].Select(x => (JObject)x["variables"]).Properties().Select(x => x.Name).ToList();
        }

        public static List<string> ObtenerNombresDelListadoDelGrupoDeVariables(JObject json)
        {
            return json["value"].Select(x => x["name"].ToString()).ToList();
        }
    }
}
