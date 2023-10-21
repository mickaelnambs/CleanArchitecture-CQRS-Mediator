using Application.Core;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Todos
{
    public class Details
    {
        public class Query : IRequest<Result<TodoDto>>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<TodoDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }
            public async Task<Result<TodoDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var todo = await _context.Todos.FindAsync(request.Id);

                var todoToReturn = _mapper.Map<TodoDto>(todo);

                return Result<TodoDto>.Success(todoToReturn);
            }
        }
    }
}