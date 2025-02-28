using Dapper;
using Microsoft.EntityFrameworkCore;
using MyFirstAPI.Infraestructure;
using Domain.Ports;
using System.Data;
using System.Text;

namespace Infrastructure.Adapters.BASE
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly PersitenceContext _context;
        private readonly IDbConnection _dapperSource;
        public GenericRepository(PersitenceContext context, IDbConnection dapperSource)
        {
            _context = context;
            _dapperSource = dapperSource;
        }

        public async Task<List<T>> Get()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> Get(int id)
        {
            var data = await _context.Set<T>().FindAsync(id);

            if (data == null)
            {
                throw new Exception($"No find: {id}");
            }

            return data;
        }

        public async Task<T> Create(T data)
        {
            using (IDbConnection dbConnection = _dapperSource)
            {
                string sqlQuery = GenerateInsertQuery(data) + "; SELECT CAST(SCOPE_IDENTITY() as int);";
                var parameters = new DynamicParameters();

                foreach (var property in typeof(T).GetProperties())
                {
                    if (property.Name != "Id")
                    {
                        parameters.Add($"@{property.Name}", property.GetValue(data));
                    }
                }

                int newId = await dbConnection.QuerySingleAsync<int>(sqlQuery, parameters);

                var idProperty = typeof(T).GetProperty("Id");
                if (idProperty != null)
                {
                    idProperty.SetValue(data, newId);
                }

                return data;
            }
        }


        public async Task<T> Edit(T data)
        {
            using (IDbConnection dbConnection = _dapperSource)
            {
                string sqlQuery = GenerateUpdateQuery(data);
                var parameters = new DynamicParameters();

                foreach (var property in typeof(T).GetProperties())
                {
                    if (property.Name != "Id")
                    {
                        parameters.Add($"@{property.Name}", property.GetValue(data));
                    }
                    else
                    {
                        parameters.Add($"@{property.Name}", property.GetValue(data));
                    }
                }

                await dbConnection.ExecuteAsync(sqlQuery, parameters);
                return data;
            }
        }

        public async Task Delete(int id)
        {
            var data = await _context.Set<T>().FindAsync(id);

            if (data == null)
            {
                throw new Exception($"No find: {id}");
            }

            _context.Set<T>().Remove(data);
            await _context.SaveChangesAsync();
        }

        private string GenerateInsertQuery(T data)
        {
            var insertQuery = new StringBuilder($"INSERT INTO {typeof(T).Name}s (");

            var properties = typeof(T).GetProperties()
                                      .Where(p => p.Name != "Id")
                                      .Select(p => p.Name);

            insertQuery.Append(string.Join(", ", properties));
            insertQuery.Append(") VALUES (");
            insertQuery.Append(string.Join(", ", properties.Select(p => $"@{p}")));
            insertQuery.Append(")");

            return insertQuery.ToString();
        }

        private string GenerateUpdateQuery(T data)
        {
            var updateQuery = new StringBuilder($"UPDATE {typeof(T).Name}s SET ");

            var properties = typeof(T).GetProperties()
                                      .Where(p => p.Name != "Id")
                                      .Select(p => p.Name);

            updateQuery.Append(string.Join(", ", properties.Select(p => $"{p} = @{p}")));
            updateQuery.Append(" WHERE Id = @Id");

            return updateQuery.ToString();
        }
    }
}
