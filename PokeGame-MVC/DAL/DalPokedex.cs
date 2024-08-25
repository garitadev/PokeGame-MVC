using Newtonsoft.Json;
using PokeGame_MVC.Entities;

namespace PokeGame_MVC.DAL
{
    public class DalPokedex
    {
        private readonly HttpClient _client;

        public DalPokedex(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<Pokemon>> GetFirst200PokemonsAsync()
        {

            try
            {
                var tasks = new List<Task<Pokemon>>();

                for (int i = 1; i <= 20; i++)
                {
                    tasks.Add(GetPokemonDataAsync(i));
                }

                var pokemons = await Task.WhenAll(tasks);
                var ts = pokemons.ToList();

                return ts;
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        private async Task<Pokemon> GetPokemonDataAsync(int id)
        {
            var response = await _client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{id}");
            var pokemon = JsonConvert.DeserializeObject<Pokemon>(response);
            pokemon.Image = $"https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/{id}.png";
            return pokemon;
        }



        
    }
}
