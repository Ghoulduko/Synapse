using System.Runtime.Serialization;

namespace Synapse.Core.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string? message) : base(message)
    {
        
    }
}