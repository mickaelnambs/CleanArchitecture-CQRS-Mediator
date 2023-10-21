using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Todos
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Todo Todo { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Todo).SetValidator(new TodoValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }
            
            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUsername());

                if (user == null) return null;

                var todo = new Todo
                {
                    Description = request.Todo.Description,
                    Status = request.Todo.Status,
                    AppUser = user
                };

                _context.Todos.Add(todo);

                user.Todos.Add(todo);

                var result = await _context.SaveChangesAsync() > 0;

                if (!result)
                {
                    return Result<Unit>.Failure("Failed to create todo");
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}