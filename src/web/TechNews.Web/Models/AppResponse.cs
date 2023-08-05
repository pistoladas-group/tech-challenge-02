namespace TechNews.Web.Models;

//TODO: Talvez fazer uma classe só?
// ApiResponse x AppResponse lá no shared
public class AppResponse
{
    public bool Succeeded { get; private set; }
    public object? Data { get; init; }
    public List<string>? Errors { get; init; }

    public AppResponse()
    {
        Succeeded = true;
    }

    public AppResponse(object? data)
    {
        Succeeded = true;
        Data = data;
    }

    public AppResponse(string error)
    {
        Succeeded = false;
        Errors ??= new List<string>();
        Errors.Add(error);
    }

    public AppResponse(List<string> errors)
    {
        Succeeded = false;
        Errors = errors;
    }
}