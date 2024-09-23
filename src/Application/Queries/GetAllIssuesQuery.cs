using Domain.Entities;
using MediatR;

namespace Application.Queries;

public class GetAllIssuesQuery : IRequest<IEnumerable<Issue>?> { }
