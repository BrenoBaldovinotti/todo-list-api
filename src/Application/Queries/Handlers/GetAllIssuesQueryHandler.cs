using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Queries.Handlers;

public class GetAllIssuesQueryHandler(IIssueRepository orderRepository) : IRequestHandler<GetAllIssuesQuery, IEnumerable<Issue>?>
{
    private readonly IIssueRepository _orderRepository = orderRepository;

    public async Task<IEnumerable<Issue>?> Handle(GetAllIssuesQuery request, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetAllIssuesAsync();
    }
}
