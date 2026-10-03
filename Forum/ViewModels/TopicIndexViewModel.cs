using Forum.Models;

namespace Forum.ViewModels;

public class TopicIndexViewModel
{
     public List<Topic> Topics { get; set; }
     public PageViewModel PageViewModel { get; set; }
}