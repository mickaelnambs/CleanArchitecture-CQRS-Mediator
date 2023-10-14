using Application.Todos;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TodosController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetTodos()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(Guid id)
        {
            return await Mediator.Send(new Details.Query{Id = id});
        }

        [HttpPost]
        public async Task<ActionResult> CreateTodo([FromBody] Todo todo)
        {
            await Mediator.Send(new Create.Command {Todo = todo});

            return Ok();
        }
    }
}