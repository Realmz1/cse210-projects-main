using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

abstract class Activity
{
    protected string name;
    protected string description;
    protected int duration;

    public Activity(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public void Start()
    {
        Console.Clear();
        Console.WriteLine($"Starting {name}...");
        Console.WriteLine(description);
        Console.Write("Enter duration (seconds): ");
        duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        ShowSpinner(3);
    }

    public void End()
    {
        Console.WriteLine("Great job! Activity complete.");
        Console.WriteLine($"You spent {duration} seconds on {name}.");
        ShowSpinner(3);
        LogActivity();
    }

    protected void ShowSpinner(int seconds)
    {
        for (int i = 0; i < seconds; i++)
        {
            Console.Write("|");
            Thread.Sleep(500);
            Console.Write("\b/");
            Thread.Sleep(500);
            Console.Write("\b-");
            Thread.Sleep(500);
            Console.Write("\b\\");
            Thread.Sleep(500);
            Console.Write("\b ");
        }
        Console.WriteLine();
    }

    protected void ShowCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000);
            Console.Write("\b\b");
        }
        Console.WriteLine();
    }

    private void LogActivity()
    {
        string filePath = "activity_log.txt";
        Dictionary<string, int> log = new Dictionary<string, int>();

        if (File.Exists(filePath))
        {
            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(':');
                log[parts[0]] = int.Parse(parts[1]);
            }
        }

        if (log.ContainsKey(name))
            log[name]++;
        else
            log[name] = 1;

        File.WriteAllLines(filePath, log.Select(kvp => $"{kvp.Key}:{kvp.Value}"));
    }

    public abstract void Run();
}

class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing Activity", "This activity helps you relax by guiding you through slow breathing.") { }

    public override void Run()
    {
        Start();
        for (int i = 0; i < duration / 6; i++)
        {
            Console.Write("Breathe in... ");
            ShowCountdown(3);
            Console.Write("Hold... ");
            ShowCountdown(2);
            Console.Write("Breathe out... ");
            ShowCountdown(4);
        }
        End();
    }
}

class ReflectionActivity : Activity
{
    private List<string> prompts = new List<string>()
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> questions = new List<string>()
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?"
    };

    public ReflectionActivity() : base("Reflection Activity", "This activity helps you reflect on times in your life when you have shown strength and resilience.") { }

    public override void Run()
    {
        Start();
        Random rand = new Random();
        string prompt = prompts[rand.Next(prompts.Count)];
        Console.WriteLine($"Prompt: {prompt}");
        ShowSpinner(3);

        foreach (var question in questions)
        {
            Console.WriteLine(question);
            ShowSpinner(3);
        }
        End();
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Choose an activity:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Exit");
            Console.Write("Enter choice: ");

            string choice = Console.ReadLine();
            Activity activity = null;

            switch (choice)
            {
                case "1":
                    activity = new BreathingActivity();
                    break;
                case "2":
                    activity = new ReflectionActivity();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    Thread.Sleep(2000);
                    continue;
            }

            activity.Run();
        }
    }
}
