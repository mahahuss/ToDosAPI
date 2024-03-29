﻿namespace Core.Entities;

public class AppUser
{
    public int Id { get; set; }
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string Salt { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public bool Status { get; set; }

}