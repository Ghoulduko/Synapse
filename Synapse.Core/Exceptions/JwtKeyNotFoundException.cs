namespace Synapse.Core.Exceptions;

public class JwtKeyNotFoundException : Exception
{
    public JwtKeyNotFoundException(string? message) : base(message)
    {
    }
}