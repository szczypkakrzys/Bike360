﻿namespace Bike360.Application.Models;

public class EmailSettings
{
    public string SenderEmail { get; set; }
    public string SenderName { get; set; }
    public string Password { get; set; }
    public string SmtpServer { get; set; }
    public int Port { get; set; }
}