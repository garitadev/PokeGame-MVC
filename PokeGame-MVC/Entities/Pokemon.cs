using Newtonsoft.Json;

namespace PokeGame_MVC.Entities
{
    public class Pokemon
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        public string Image { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }

        [JsonProperty("sprites")]
        public Sprites Sprites { get; set; }

        [JsonProperty("types")]
        public List<PokemonType> Types { get; set; }

        [JsonProperty("evolves_from_species")]
        public Evolution EvolvesFromSpecies { get; set; }
    }

    public class Sprites
    {
        [JsonProperty("front_default")]
        public string FrontDefault { get; set; }
    }

    public class PokemonType
    {
        [JsonProperty("type")]
        public Type Type { get; set; }
    }

    public class Type
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Evolution
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class PokemonSpecies
    {
        [JsonProperty("evolution_chain")]
        public EvolutionChain EvolutionChain { get; set; }
    }

    public class EvolutionChain
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }


}
