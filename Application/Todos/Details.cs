using Domain;
using MediatR;
using Persistence;

namespace Application.Todos
{
    public class Details
    {
        public class Query : IRequest<Todo>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Todo>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;

            }
            public async Task<Todo> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Todos.FindAsync(request.Id);
            }
        }
    }
}