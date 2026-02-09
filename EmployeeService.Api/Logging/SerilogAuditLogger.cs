using EmployeeService.Application.Common;
using Serilog;

namespace EmployeeService.Api.Logging;

public sealed class SerilogAuditLogger : IAuditLogger
{
    public Task LogAsync(string action, string entity, object entityId)
    {
        Log.Information(
            "AUDIT | Action: {Action} | Entity: {Entity} | EntityId: {EntityId}",
            action, entity, entityId);

        return Task.CompletedTask;
    }
}
