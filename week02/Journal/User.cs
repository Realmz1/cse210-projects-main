using System;
using System.Collections.Generic;

public class User
{
    public List<string> Prompts { get; private set; } = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "What am I grateful for today?",
        "What challenged me the most today?",
        "If I could relive one moment today, what would it be?"
    };

    public string GetRandomPrompt()
    {
        Random random = new Random();
        return Prompts[random.Next(Prompts.Count)];
    }
}