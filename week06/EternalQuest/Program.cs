using System;
using System.Collections.Generic;
using System.IO;

// Base Goal Class
abstract class Goal
{
    protected string name;
    protected int points;
    
    public Goal(string name, int points)
    {
        this.name = name;
        this.points = points;
    }
    
    public abstract void RecordEvent();
    public abstract string GetStatus();
    public int GetPoints() => points;
}

// Simple Goal Class
class SimpleGoal : Goal
{
    private bool isComplete;

    public SimpleGoal(string name, int points) : base(name, points)
    {
        isComplete = false;
    }

    public override void RecordEvent()
    {
        isComplete = true;
    }

    public override string GetStatus()
    {
        return isComplete ? "[X]" : "[ ]";
    }
}

// Eternal Goal Class
class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name, points) { }

    public override void RecordEvent()
    {
        // Earn points every time without completion
    }

    public override string GetStatus()
    {
        return "[∞]";
    }
}

// Checklist Goal Class
class ChecklistGoal : Goal
{
    private int targetCount;
    private int currentCount;
    private int bonusPoints;

    public ChecklistGoal(string name, int points, int targetCount, int bonusPoints) : base(name, points)
    {
        this.targetCount = targetCount;
        this.bonusPoints = bonusPoints;
        this.currentCount = 0;
    }

    public override void RecordEvent()
    {
        currentCount++;
        if (currentCount == targetCount)
        {
            points += bonusPoints;
        }
    }

    public override string GetStatus()
    {
        return currentCount >= targetCount ? "[X]" : $"[{currentCount}/{targetCount}]";
    }
}

// GoalManager Class
class GoalManager
{
    private List<Goal> goals = new List<Goal>();
    private int totalPoints = 0;
    
    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void RecordGoalEvent(int index)
    {
        if (index >= 0 && index < goals.Count)
        {
            goals[index].RecordEvent();
            totalPoints += goals[index].GetPoints();
        }
    }

    public void ShowGoals()
    {
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].GetStatus()} {goals[i].GetType().Name} - {goals[i].GetPoints()} pts");
        }
    }

    public void ShowScore()
    {
        Console.WriteLine($"Total Score: {totalPoints}");
    }
}

// Program Class
class Program
{
    static void Main()
    {
        GoalManager manager = new GoalManager();
        manager.AddGoal(new SimpleGoal("Run a marathon", 1000));
        manager.AddGoal(new EternalGoal("Read scriptures", 100));
        manager.AddGoal(new ChecklistGoal("Attend temple", 50, 10, 500));
        
        while (true)
        {
            Console.WriteLine("1. Show Goals");
            Console.WriteLine("2. Record Goal Event");
            Console.WriteLine("3. Show Score");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    manager.ShowGoals();
                    break;
                case "2":
                    Console.Write("Enter goal number: ");
                    int index = int.Parse(Console.ReadLine()) - 1;
                    manager.RecordGoalEvent(index);
                    break;
                case "3":
                    manager.ShowScore();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }
}