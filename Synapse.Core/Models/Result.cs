namespace Synapse.Core.Models;

public class Result<T> where T : class
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
}