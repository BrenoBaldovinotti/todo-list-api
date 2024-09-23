using Application.Commands;
using Application.DTOs;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize(Policy = "OperatorPolicy")]
[ApiController]
[Route("api/v{version:apiVersion}/issues")]
public class IssuesController(IAuthorizationService authorizationService, IMediator mediator) : BaseController(authorizationService)
{
    [HttpPost]
    public async Task<IActionResult> CreateIssue([FromBody] CreateIssueRequestDto requestDto)
    {
        var issueId = await mediator.Send(new CreateIssueCommand(requestDto.Name));
        return Success(issueId);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllIssues()
    {
        var issues = await mediator.Send(new GetAllIssuesQuery());
        return Success(issues);
    }

    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> MarkIssueAsCompleted(Guid id)
    {
        await mediator.Send(new MarkIssueAsCompletedCommand(id));
        return Success();
    }

    [HttpPut("issues/{id}")]
    public async Task<IActionResult> UpdateIssueName(Guid id, [FromBody] UpdateIssueRequestDto requestDto)
    {
        await mediator.Send(new UpdateIssueNameCommand(id, requestDto.Name));
        return NoContent();
    }

    [Authorize(Policy = "AdminPolicy")]
    [HttpDelete("completed")]
    public async Task<IActionResult> RemoveCompletedIssues()
    {
        await mediator.Send(new RemoveCompletedIssuesCommand());
        return Success();
    }
}
