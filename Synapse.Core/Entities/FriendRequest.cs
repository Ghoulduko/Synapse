using Synapse.Core.Enums;

namespace Synapse.Core.Entities;

public class FriendRequest
{
    public int Id { get; set; }
    public int ReceiverId { get; set; }
    public int SenderId { get; set; }
    public FriendRequestStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public User Receiver { get; set; }
    public User Sender { get; set; }
    
    private FriendRequest() { }
    
    public FriendRequest(int senderId, int receiverId)
    {
        if (senderId == receiverId)
            throw new InvalidOperationException("Cannot send a friend request to yourself.");

        SenderId = senderId;
        ReceiverId = receiverId;
        Status = FriendRequestStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }
}