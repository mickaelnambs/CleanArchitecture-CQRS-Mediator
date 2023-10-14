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
        public async Task<IActionResult> CreateTodo([FromBody] Todo todo)
        {
            await Mediator.Send(new Create.Command {Todo = todo});

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTodo(Guid id, Todo todo)
        {
            todo.Id = id;

            await Mediator.Send(new Edit.Command {Todo = todo});

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            await Mediator.Send(new Delete.Command {Id = id});

            return Ok();
        }
    }
}