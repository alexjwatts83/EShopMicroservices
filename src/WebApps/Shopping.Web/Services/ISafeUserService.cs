namespace Shopping.Web.Services;

public interface ISafeUserService
{
    string GetUserName();
}

public class SafeUserService : ISafeUserService
{
    // TODO inject http context and use that instead
    public string GetUserName()
    {
        return "swn05";
    }
}