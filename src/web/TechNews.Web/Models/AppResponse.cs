namespace TechNews.Web.Models;

public class AppResponseModel
{
    public bool Succeeded { get; init; }
    public object? Data { get; init; }
    public List<string>? Errors { get; init; }

    public AppResponseModel()
    {
    }

    public AppResponseModel(object? data)
    {
        Succeeded = true;
        Data = data;
    }

    public AppResponseModel(string error)
    {
        Succeeded = false;
        Errors ??= new List<string>();
        Errors.Add(error);
    }

    public AppResponseModel(List<string> errors)
    {
        Succeeded = false;
        Errors = errors;
    }
}