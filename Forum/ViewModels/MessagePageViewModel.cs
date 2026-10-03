using Forum.Models;

namespace Forum.ViewModels;

public class MessagePageViewModel
{
    public Topic Topic { get; set; }
    public List<Message> Messages { get; set; }
    public PageViewModel PageViewModel { get; set; }
}