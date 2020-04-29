
namespace LivaNovaAnimalsApi.Helper
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using LivaNovaAnimalsApi.Models;

    public interface ICosmosDbService
    {
        Task<Animal> GetAnimalAsync(string id);
        Task AddAnimalAsync(Animal animal);
        Task UpdateAnimalAsync(string id, Animal animal);
        Task DeleteAnimalAsync(string id);
        IEnumerable<Animal> GetAnimals(string queryString);
    }
}
