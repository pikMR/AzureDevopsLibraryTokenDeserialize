using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace VisualTokenizacionWConfig.Logic.Model
{
    public partial class BibliotecaTokens
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("value")]
        public GrupoDeVariable[] GruposDeVariables { get; set; }

        public List<string> ObtenerNombresDelosGruposDeVariables()
        {
            return GruposDeVariables.Select(x => x.Name).ToList();
        }
    }

    public partial class GrupoDeVariable
    {
        [JsonProperty("variables")]
        public Dictionary<string, Variable> Variables { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public Dictionary<string, string> GetDicNombreDeVariableYValor()
        {
            return Variables.ToDictionary(x => x.Key, y => y.Value.Value);
        }
    }

    public partial class Variable
    {
        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
