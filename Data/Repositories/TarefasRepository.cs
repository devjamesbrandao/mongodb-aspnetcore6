using MongoDb.Data.Configurations;
using MongoDb.Models;
using MongoDB.Driver;

namespace MongoDb.Data.Repositories
{
    public class TarefasRepository : ITarefasRepository
    {
        private readonly IMongoCollection<Tarefa> _tarefas;

        public TarefasRepository(IDatabaseConfig databaseConfig)
        {
            var client = new MongoClient(databaseConfig.ConnectionString);
            
            var database = client.GetDatabase(databaseConfig.DatabaseName);

            _tarefas = database.GetCollection<Tarefa>("todos");
        }

        public async Task Adicionar(Tarefa tarefa)
        {
            await _tarefas.InsertOneAsync(tarefa);
        }

        public async Task Atualizar(string id, Tarefa tarefaAtualizada)
        {
            await _tarefas.ReplaceOneAsync(tarefa => tarefa.Id == id, tarefaAtualizada);
        }

        public async Task<IEnumerable<Tarefa>> Buscar()
        {
            return await _tarefas.Find(tarefa => true).ToListAsync();
        }

        public async Task<Tarefa> Buscar(string id)
        {
            return await _tarefas.Find(tarefa => tarefa.Id == id).FirstOrDefaultAsync();
        }

        public async Task Remover(string id)
        {
            await _tarefas.DeleteOneAsync(tarefa => tarefa.Id == id);
        }
    }
}