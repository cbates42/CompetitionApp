using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Services.Model
{
    public class RecordModel
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("playerName")]
        public string playerName { get; set; } = string.Empty;

        [JsonPropertyName("hp")]
        public int hp { get; set; }

        [JsonPropertyName("atk")]
        public int atk { get; set; }
    }
}
