using Forum.Models;
using Forum.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum.Controllers;

public class TopicController :Controller
{
    private readonly ForumContext _context;
    private readonly UserManager<User> _userManager;
    
    public TopicController(ForumContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager=userManager;
    }
    
    public async Task<IActionResult> Index(int page=1)
    {
        var topics = await _context.Topics .Include(t => t.User)
            .Include(t => t.Messages) 
            .OrderByDescending(t => t.CreatedAt) .ToListAsync();
        int pageSize = 5;
        var count=topics.Count();
        var items=topics.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        PageViewModel pvm = new PageViewModel(count, page, pageSize);
        TopicIndexViewModel tivm = new TopicIndexViewModel()
        {
            Topics = items,
            PageViewModel = pvm
        };
        return View(tivm);
        
    }
    
    [Authorize]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost] 
    [Authorize] 
    public async Task<IActionResult> Create(Topic topic) 
    { 
        topic.CreatedAt = DateTime.UtcNow;
        topic.UserId=int.Parse(_userManager.GetUserId(User));
        _context.Topics.Add(topic);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
}