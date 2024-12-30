using Application.UserCQ.Commands;
using Application.WorkspaceCQ.Commands;
using Application.WorkspaceCQ.Queries;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Controllers
{
    public static class WorkspaceController
    {
        public static void WorkspacesRoute(this WebApplication app)
        {
            var group = app.MapGroup("Workspaces").WithTags("Workspaces");

            group.MapPost("create-workspace", CreateWorkspace);
            group.MapPut("edit-workspace", EditWorkspace);
            group.MapDelete("delete-workspace/{workspaceId}", DeleteWorkspace);
            group.MapGet("get-all-workspace", GetAllWorkspace);
            group.MapGet("get-workspace/{workspaceId}", GetWorkspace);

            //Delegates
        }

        public static async Task<IResult> CreateWorkspace(
            [FromServices] IMediator _mediator,
            [FromBody] CreateWorkspaceCommand command)
        {
            var result = await _mediator.Send(command);

            if(result.ResponseInfo is null)
            {
                return Results.Ok(result.Value); 
            }
            return Results.BadRequest(result.ResponseInfo);
        }

        public static async Task<IResult> EditWorkspace(
            [FromServices] IMediator _mediator,
            [FromBody] EditWorkspaceCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.ResponseInfo is null)
            {
                return Results.Ok(result.Value);
            }
            return Results.BadRequest(result.ResponseInfo);
        }

        public static async Task<IResult> DeleteWorkspace(
            [FromServices] IMediator _mediator, Guid workspaceId)
        {
            var result = await _mediator.Send(new DeleteWorkspaceCommand
            {
                Id = workspaceId,
            });

            if (result.ResponseInfo is null)
            {
                return Results.NoContent();
            }
            return Results.BadRequest(result.ResponseInfo);
        }

        public static async Task<IResult> GetAllWorkspace(
            [FromServices] IMediator _mediator,
            [FromQuery] Guid userid,
            [FromQuery] int pageSize,
            [FromQuery] int pageIndex)
        {
            var result = await _mediator.Send(new GetAllWorkspacesQuery
            { 
                pageIndex = pageIndex,
                pageSize = pageSize,
                UserId = userid
            
            });

            if (result.ResponseInfo is null)
            {
                return Results.Ok(result.Value);
            }
            return Results.BadRequest(result.ResponseInfo);
        }

        public static async Task<IResult> GetWorkspace(
            [FromServices] IMediator _mediator,
            Guid workspaceId)
        {
            var result = await _mediator.Send(new GetWorkspaceQuery
            {
                Id = workspaceId,
            });

            if (result.ResponseInfo is null)
            {
                return Results.Ok(result.Value);
            }
            return Results.BadRequest(result.ResponseInfo);
        }

    }
}
