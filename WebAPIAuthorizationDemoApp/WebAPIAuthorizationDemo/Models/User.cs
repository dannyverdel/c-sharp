﻿using System;
namespace WebAPIAuthorizationDemo.Models;

public class User
{
    public string Username { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = new List<string>();
    public string PasswordHash { get; set; } = string.Empty;
}

