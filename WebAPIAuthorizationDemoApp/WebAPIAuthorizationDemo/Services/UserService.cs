namespace WebAPIAuthorizationDemo.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _http_context_accessor;
    public UserService(IHttpContextAccessor http_context_accessor) => _http_context_accessor = http_context_accessor;
    public string GetMyName() {
        string? result = String.Empty;

        if ( _http_context_accessor is not null )
            result = _http_context_accessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);

        return result ?? "";
    }
}

