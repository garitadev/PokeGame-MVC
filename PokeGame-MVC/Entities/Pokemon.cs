namespace PokeGame_MVC.Entities
{

    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
    public class PokeApiResponse
    {
        public List<PokemonResult> Results { get; set; }
    }

    public class PokemonResult
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

}
