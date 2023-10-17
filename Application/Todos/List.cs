using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Todos
{
    public class List
    {
        public class Query : IRequest<Result<List<Todo>>> {}

        public class Handler : IRequestHandler<Query, Result<List<Todo>>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<Todo>>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Result<List<Todo>>.Success(await _context.Todos.ToListAsync());
            }
        }
    }
}