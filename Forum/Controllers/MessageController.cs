using Forum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers;

public class MessageController: Controller
{
    private readonly ForumContext _context;
    private readonly UserManager<User> _userManager;

    public MessageController(ForumContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Message message)
    {
        message.CreatedAt = DateTime.UtcNow;
        message.UserId = int.Parse(_userManager.GetUserId(User));
        
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        
        var user =await _userManager.GetUserAsync(User); 
        return Json(new
        {
            success = true,
            userName=user.UserName,
            text=message.Text,
            createdAt=message.CreatedAt.ToString("dd.MM.yyyy HH:mm")
        });
    }
}