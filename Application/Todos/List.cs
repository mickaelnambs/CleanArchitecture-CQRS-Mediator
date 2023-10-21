using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Todos
{
    public class List
    {
        public class Query : IRequest<Result<List<TodoDto>>> {}

        public class Handler : IRequestHandler<Query, Result<List<TodoDto>>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<TodoDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var todos = await _context.Todos.ToListAsync();

                var todosToReturn = _mapper.Map<List<TodoDto>>(todos);

                return Result<List<TodoDto>>.Success(todosToReturn);
            }
        }
    }
}