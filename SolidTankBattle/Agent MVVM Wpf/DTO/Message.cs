using System;

namespace Agent_MVVM_Wpf.DTO;

public class Message
{
    public string? ObjectId { get; set; } = String.Empty;

    public string? Action { get; set; } = String.Empty;

    public string? Direction { get; set; } = String.Empty;

    public string? TypeObject { get; set; } = String.Empty;

    public string? GameId { get; set; } = String.Empty;
}