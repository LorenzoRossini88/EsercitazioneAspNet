using System.Text.Json.Serialization;

namespace EsercitazioneAspNet.Dto
{
    public class UtentiDto
    {

        [JsonPropertyName("NomeUtente")]
        public string NomeUtente { get; set; }
        [JsonPropertyName("Password")]
        public string Password { get; set; }
        [JsonPropertyName("IdBanca")]
        public int IdBanca { get; set; }
    }
}
