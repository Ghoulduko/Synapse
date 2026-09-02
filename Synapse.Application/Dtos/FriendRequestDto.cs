using Synapse.Core.Enums;

namespace Synapse.Application.Dtos;

public class FriendRequestDto
{
    public int Id { get; set; }
    public int ReceiverId { get; set; }
    public int SenderId { get; set; }
    public FriendRequestStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}