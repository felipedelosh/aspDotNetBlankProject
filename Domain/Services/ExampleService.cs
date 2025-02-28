using Domain.Ports;
using Domain.Entities;

namespace Domain.Services
{
    public class ExampleService
    {

        private readonly IGenericRepository<Example> _repository;
        public ExampleService(IGenericRepository<Example> reposittory)
        {
            _repository = reposittory;
        }

        public async Task<List<Example>> Get()
        {
            return await _repository.Get();
        }

        public async Task<Example> Get(int id)
        {
            return await _repository.Get(id);
        }


        public async Task<Example> Create(Example person)
        {
            return await _repository.Create(person);
        }


        public async Task<Example> Edit(Example person)
        {
            return await _repository.Edit(person);
        }


        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }
    }
}
