using MongoDb.Models;

namespace MongoDb.Data.Repositories
{
    public interface ITarefasRepository
    {
        Task Adicionar(Tarefa tarefa);

        Task Atualizar(string id, Tarefa tarefaAtualizada);

        Task<IEnumerable<Tarefa>> Buscar();

        Task<Tarefa> Buscar(string id);

        Task Remover(string id);
    }
}