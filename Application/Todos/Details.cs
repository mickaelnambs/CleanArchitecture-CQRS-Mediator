using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Application.Todos
{
    public class Details
    {
        public class Query : IRequest<Result<Todo>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Todo>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;

            }
            public async Task<Result<Todo>> Handle(Query request, CancellationToken cancellationToken)
            {
                var todo = await _context.Todos.FindAsync(request.Id);


                return Result<Todo>.Success(todo);
            }
        }
    }
}