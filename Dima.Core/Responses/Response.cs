using System.Text.Json.Serialization;

namespace Dima.Core.Responses;

public class Response <TData>
{
   
    private readonly int _code;

    [JsonConstructor]
    public Response() => _code = Configuration.StatusCode;
    
    
    public Response(TData? data, int code = Configuration.StatusCode, string? message = null)
    {
        Data = data;
        Message = message; 
        _code = code;
    }    
    
    public TData? Data { get; set; }
    public string? Message { get; set; } 
    
    [JsonIgnore]
    public bool IsSuccess => _code >= 200 && _code <= 299;
}