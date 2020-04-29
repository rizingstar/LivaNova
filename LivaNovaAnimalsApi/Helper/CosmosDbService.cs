using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LivaNovaAnimalsApi.Models;
using Microsoft.Azure.Cosmos;

namespace LivaNovaAnimalsApi.Helper
{


    public class CosmosDbService : ICosmosDbService
    {
        private Container _container;

        public CosmosDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            _container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddAnimalAsync(Animal Animal)
        {
            await _container.CreateItemAsync<Animal>(Animal, new PartitionKey(Animal.Id));
        }

        public async Task DeleteAnimalAsync(string id)
        {
            await this._container.DeleteItemAsync<Animal>(id, new PartitionKey(id));
        }

        public async Task<Animal> GetAnimalAsync(string id)
        {
            try
            {
                ItemResponse<Animal> response = await this._container.ReadItemAsync<Animal>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public IEnumerable<Animal> GetAnimals(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Animal>(new QueryDefinition(queryString));
            List<Animal> results = new List<Animal>();
            while (query.HasMoreResults)
            {
                var response = query.ReadNextAsync();

                results.AddRange(response.Result);
            }

            return results;
        }

        public async Task UpdateAnimalAsync(string id, Animal Animal)
        {
            await this._container.UpsertItemAsync<Animal>(Animal, new PartitionKey(id));
        }
    }
}
