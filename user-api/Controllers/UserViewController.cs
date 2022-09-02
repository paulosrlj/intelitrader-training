using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.Mvc;
using user_api.database;
using user_api.Models;


namespace user_api.Controllers;

public class UserViewController : Controller
{

  private ApplicationDbContext _context;

  public UserViewController(ApplicationDbContext context)
  {
    _context = context;
  }

  [HttpGet]
  public async Task<IActionResult> Index()
  {
    HubConnection hubConnection = new HubConnectionBuilder()
     .WithUrl("http://host.docker.internal:5000/users-hub")
     .Build();
    
    await hubConnection.StartAsync();
    await hubConnection.InvokeAsync("RetrieveHubUsers");
    await hubConnection.DisposeAsync();
    return View();
  }
}