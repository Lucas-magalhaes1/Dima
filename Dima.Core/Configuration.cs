namespace Dima.Core;

public static class Configuration
{
    public const int StatusCode = 200;
    public const int PageSize = 25;
    public const int PageNumber = 1;
    
    public static string ConnectionString { get; set; } = string.Empty;
    
    public static string BackendUrl { get; set; } = string.Empty;

    public static string FrontendUrl { get; set; } = string.Empty;

}