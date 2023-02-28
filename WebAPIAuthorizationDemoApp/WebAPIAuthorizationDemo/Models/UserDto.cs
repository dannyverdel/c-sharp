using System;
namespace WebAPIAuthorizationDemo.Models;

public class UserDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required List<string> Roles { get; set; }
}

