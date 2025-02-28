namespace Domain.Ports
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<List<T>> Get();
        public Task<T> Get(int id);
        public Task<T> Create(T data);
        public Task<T> Edit(T data);
        public Task Delete(int id);
    }
}
