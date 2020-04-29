namespace LivaNovaAnimalsApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using LivaNovaAnimalsApi.Data;
    using LivaNovaAnimalsApi.Helper;
    using LivaNovaAnimalsApi.Models;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class AnimalController : ControllerBase
    {
        private readonly ICosmosDbService _cosmosDbService;
        int id = 1;
        public AnimalController(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
            initializeWithData();
        }

        void initializeWithData()
        {
            AddAnimal(BearData.Bears, "Wild", "Bear");
            AddAnimal(CatData.Cats, "Domestic", "Cat");
            AddAnimal(DogData.Dogs, "Domestic", "Dog");
            AddAnimal(MonkeyData.Monkeys, "Wild", "Monkey");
            AddAnimal(ElephantData.Elephants, "Wild", "Elephant");
        }

        void AddAnimal(IList<Animal> animals, string animalCategory, string animalType)
        {
            foreach (var animal in animals)
            {
                animal.Id = (id++).ToString();
                animal.AnimalCategory = animalCategory;
                animal.AnimalType = animalType;
                _cosmosDbService.AddAnimalAsync(animal);
            }
        }


        [HttpGet]
       public IEnumerable<Animal> GetAnimals()
       {
          return _cosmosDbService.GetAnimals("SELECT * FROM c");
       }

        // GET: api/Animal/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(long id)
        {
            var animal = await _cosmosDbService.GetAnimalAsync(id.ToString());

            if (animal == null)
            {
                return NotFound();
            }

            return animal;
        }

        // POST: api/Animal
        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
        {
            await _cosmosDbService.AddAnimalAsync(animal);

            return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
        }

        // PUT: api/Animal/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(long id, Animal animal)
        {
            if (ModelState.IsValid)
            {
                await _cosmosDbService.UpdateAnimalAsync(animal.Id, animal);
                return RedirectToAction("Index");
            }

            return NoContent();
        }

        // DELETE: api/Animal/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Animal>> DeleteAsync(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            Animal animal = await _cosmosDbService.GetAnimalAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            await _cosmosDbService.DeleteAnimalAsync(id);

            return animal;
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmedAsync([Bind("Id")] string id)
        {
            await _cosmosDbService.DeleteAnimalAsync(id);
            return RedirectToAction("Index");
        }
    }
}
