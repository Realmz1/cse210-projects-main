using System;
using System.Collections.Generic;
using System.Linq;

// Class to represent a comment
public class Comment
{
    public string CommenterName { get; }
    public string CommentText { get; }
    public int Timestamp { get; } // Timestamp in seconds

    public Comment(string commenterName, string commentText, int timestamp)
    {
        CommenterName = commenterName;
        CommentText = commentText;
        Timestamp = timestamp;
    }

    public override string ToString()
    {
        return $"[{Timestamp}s] {CommenterName}: {CommentText}";
    }
}

// Class to represent a video
public class Video
{
    public string Title { get; }
    public string Author { get; }
    public int Length { get; } // Length in seconds
    public int Rating { get; } // Rating from 1 to 5 stars
    private List<Comment> Comments { get; }

    public Video(string title, string author, int length, int rating)
    {
        Title = title;
        Author = author;
        Length = length;
        Rating = rating;
        Comments = new List<Comment>();
    }

    // Add a comment to the video
    public void AddComment(string commenterName, string commentText, int timestamp)
    {
        Comments.Add(new Comment(commenterName, commentText, timestamp));
    }

    // Get the number of comments
    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    // Display video details and comments
    public void DisplayVideoDetails()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {Length} seconds");
        Console.WriteLine($"Rating: {new string('★', Rating)}");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");

        // Sort comments by timestamp
        var sortedComments = Comments.OrderBy(c => c.Timestamp).ToList();

        Console.WriteLine("Comments:");
        foreach (var comment in sortedComments)
        {
            Console.WriteLine($"  {comment}");
        }
        Console.WriteLine();
    }
}

// Main program class
class Program
{
    static void Main(string[] args)
    {
        // Create a list of videos
        List<Video> videos = new List<Video>
        {
            new Video("C# Tutorial for Beginners", "Programming with Mosh", 600, 5),
            new Video("ASP.NET Core Crash Course", "Traversy Media", 1200, 4),
            new Video("Learn Python in 10 Minutes", "Tech With Tim", 600, 3)
        };

        // Add comments to the first video
        videos[0].AddComment("JohnDoe", "Great tutorial!", 120);
        videos[0].AddComment("JaneSmith", "Very helpful, thanks!", 300);
        videos[0].AddComment("CodeMaster", "I learned a lot!", 450);

        // Add comments to the second video
        videos[1].AddComment("DevGuy", "Awesome content!", 200);
        videos[1].AddComment("Coder123", "This was too fast for me.", 800);
        videos[1].AddComment("WebDev", "Loved the examples!", 1000);

        // Add comments to the third video
        videos[2].AddComment("PythonFan", "Short and sweet!", 100);
        videos[2].AddComment("BeginnerCoder", "I wish it was longer.", 300);
        videos[2].AddComment("TechEnthusiast", "Perfect for a quick overview!", 500);

        // Display details for each video
        foreach (var video in videos)
        {
            video.DisplayVideoDetails();
        }
    }
}