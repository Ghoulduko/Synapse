namespace Synapse.Core.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public string TokenHash { get; set; }
    public int UserId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime Expires { get; set; }
}