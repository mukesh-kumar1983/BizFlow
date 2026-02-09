namespace EmployeeService.Application.Common;

public interface IAuditLogger
{
    Task LogAsync(string action, string entity, object entityId);
}
