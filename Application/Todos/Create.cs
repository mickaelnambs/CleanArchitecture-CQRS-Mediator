using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Todos
{
    public class Create
    {
        public class Command : IRequest
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

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Todos.Add(request.Todo);

                await _context.SaveChangesAsync();
            }
        }
    }
}