using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// Class to represent a scripture reference (e.g., "John 3:16" or "Proverbs 3:5-6")
public class Reference
{
    private string _book;
    private int _chapter;
    private int _verse;
    private int _endVerse;

    // Constructor for single verse
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
    }

    // Constructor for verse range
    public Reference(string book, int chapter, int startVerse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _verse = startVerse;
        _endVerse = endVerse;
    }

    // Get the display text for the reference
    public string GetDisplayText()
    {
        if (_endVerse == 0)
            return $"{_book} {_chapter}:{_verse}";
        else
            return $"{_book} {_chapter}:{_verse}-{_endVerse}";
    }
}

// Class to represent a single word in the scripture
public class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
    }

    // Hide the word
    public void Hide()
    {
        _isHidden = true;
    }

    // Check if the word is hidden
    public bool IsHidden()
    {
        return _isHidden;
    }

    // Get the display text for the word (either the word or underscores)
    public string GetDisplayText()
    {
        return _isHidden ? new string('_', _text.Length) : _text;
    }
}

// Class to represent a scripture, including the reference and text
public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    // Hide a specified number of random words
    public void HideRandomWords(int numberToHide)
    {
        Random random = new Random();
        int wordsHidden = 0;

        while (wordsHidden < numberToHide)
        {
            int index = random.Next(_words.Count);
            if (!_words[index].IsHidden())
            {
                _words[index].Hide();
                wordsHidden++;
            }
        }
    }

    // Check if all words are hidden
    public bool IsCompletelyHidden()
    {
        return _words.All(word => word.IsHidden());
    }

    // Display the scripture reference and text
    public void Display()
    {
        Console.Clear();
        Console.WriteLine(_reference.GetDisplayText());
        foreach (var word in _words)
        {
            Console.Write(word.GetDisplayText() + " ");
        }
        Console.WriteLine();
    }

    // Get the percentage of words hidden
    public double GetHiddenPercentage()
    {
        int hiddenCount = _words.Count(word => word.IsHidden());
        return (double)hiddenCount / _words.Count * 100;
    }
}

// Main program class
class Program
{
    static void Main(string[] args)
    {
        // Load a random scripture from a file
        Scripture scripture = LoadRandomScriptureFromFile("scriptures.txt");

        // Ask the user for difficulty level
        Console.WriteLine("Choose difficulty (easy, medium, hard):");
        string difficulty = Console.ReadLine().ToLower();
        int wordsToHide = difficulty switch
        {
            "easy" => 1,
            "medium" => 3,
            "hard" => 5,
            _ => 3 // Default to medium
        };

        // Main loop to hide words and display scripture
        while (!scripture.IsCompletelyHidden())
        {
            scripture.Display();
            Console.WriteLine($"\n{scripture.GetHiddenPercentage():0}% hidden.");
            Console.WriteLine("Press Enter to continue or type 'quit' to exit.");
            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
                break;

            scripture.HideRandomWords(wordsToHide);
        }

        // Final display when all words are hidden
        if (scripture.IsCompletelyHidden())
        {
            scripture.Display();
            Console.WriteLine("\nAll words are hidden. Program ending.");
        }
    }

    // Method to load a random scripture from a file
    static Scripture LoadRandomScriptureFromFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        Random random = new Random();
        string[] parts = lines[random.Next(lines.Length)].Split('|');

        // Parse the reference and text
        string[] referenceParts = parts[0].Split(' ');
        string book = referenceParts[0];
        int chapter = int.Parse(referenceParts[1].Split(':')[0]);
        string[] verseParts = referenceParts[1].Split(':')[1].Split('-');
        int startVerse = int.Parse(verseParts[0]);
        int endVerse = verseParts.Length > 1 ? int.Parse(verseParts[1]) : 0;

        Reference reference = endVerse == 0
            ? new Reference(book, chapter, startVerse)
            : new Reference(book, chapter, startVerse, endVerse);

        return new Scripture(reference, parts[1]);
    }
}