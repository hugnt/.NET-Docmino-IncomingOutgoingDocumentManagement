﻿namespace Docmino.Application.Models.External;
public class EmailRequest
{
    public List<string> ToEmails { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public string FileSource { get; set; }
    public string FileName { get; set; }
    public byte[] ImageSourceByte { get; set; }
}