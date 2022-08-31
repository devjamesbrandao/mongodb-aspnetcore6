using Microsoft.AspNetCore.Mvc;
using MongoDb.Data.Repositories;
using MongoDb.Models;
using MongoDb.Models.InputModels;

namespace MongoDb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private ITarefasRepository _tarefasRepository;

        public TarefasController(ITarefasRepository tarefasRepository)
        {
            _tarefasRepository = tarefasRepository;
        }


        // GET: api/tarefas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> Get()
        {
            var tarefas = await _tarefasRepository.Buscar();
            
            return Ok(tarefas);
        }

        // GET api/tarefas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> Get(string id)
        {
            var tarefa = await _tarefasRepository.Buscar(id);

            if (tarefa == null) return NotFound();

            return Ok(tarefa);
        }

        //POST api/tarefas
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TarefaInputModel novaTarefa)
        {
            var tarefa = new Tarefa(novaTarefa.Nome, novaTarefa.Detalhes);

            await _tarefasRepository.Adicionar(tarefa);

            return Created("", tarefa);
        }

        //PUT api/tarefas/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Tarefa>> Put(string id, [FromBody] TarefaInputModel tarefaAtualizada)
        {
            var tarefa = await _tarefasRepository.Buscar(id);

            if (tarefa == null) return NotFound();

            tarefa.AtualizarTarefa(tarefaAtualizada.Nome, tarefaAtualizada.Detalhes, tarefaAtualizada.Concluido);

            await _tarefasRepository.Atualizar(id, tarefa);

            return Ok(tarefa);
        }

        // DELETE api/tarefas/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var tarefa = await _tarefasRepository.Buscar(id);

            if (tarefa == null) return NotFound();

            await _tarefasRepository.Remover(id);

            return NoContent();
        }
    }
}