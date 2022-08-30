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

        public void Adicionar(Tarefa tarefa)
        {
            _tarefas.InsertOne(tarefa);
        }

        public void Atualizar(string id, Tarefa tarefaAtualizada)
        {
            _tarefas.ReplaceOne(tarefa => tarefa.Id == id, tarefaAtualizada);
        }

        public IEnumerable<Tarefa> Buscar()
        {
            return _tarefas.Find(tarefa => true).ToList();
        }

        public Tarefa Buscar(string id)
        {
            return _tarefas.Find(tarefa => tarefa.Id == id).FirstOrDefault();
        }

        public void Remover(string id)
        {
            _tarefas.DeleteOne(tarefa => tarefa.Id == id);
        }
    }
}