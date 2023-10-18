using Application.Todos;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TodosController : BaseApiController
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(Guid id)
        {
            return HandleResult(await Mediator.Send(new Details.Query{Id = id}));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] Todo todo)
        {
            return HandleResult(await Mediator.Send(new Create.Command {Todo = todo}));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTodo(Guid id, Todo todo)
        {
            todo.Id = id;

            return HandleResult(await Mediator.Send(new Edit.Command {Todo = todo}));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command {Id = id}));
        }
    }
}