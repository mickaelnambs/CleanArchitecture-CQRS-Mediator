using Application.Core;
using Application.Interfaces;
using AutoMapper;
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
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _mapper = mapper;
                _context = context;
            }

            public async Task<Result<List<TodoDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var todos = await _context.Todos
                    .Where(t => t.AppUser.UserName == _userAccessor.GetUsername())
                    .ToListAsync();

                var todosToReturn = _mapper.Map<List<TodoDto>>(todos);

                return Result<List<TodoDto>>.Success(todosToReturn);
            }
        }
    }
}