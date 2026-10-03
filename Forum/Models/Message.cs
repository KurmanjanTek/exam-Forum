namespace Forum.Models;

public class Message
{
    public int Id { get; set; }
    public string Text { get; set; }

    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int TopicId { get; set; }
    public Topic Topic { get; set; }
}