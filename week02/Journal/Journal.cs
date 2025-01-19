using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> Entries = new List<Entry>();

    public void AddEntry(string prompt, string response, string date)
    {
        Entries.Add(new Entry(prompt, response, date));
    }

    public void DisplayEntries()
    {
        foreach (var entry in Entries)
        {
            Console.WriteLine(entry);
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in Entries)
            {
                writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        Entries.Clear();
        foreach (var line in File.ReadAllLines(filename))
        {
            var parts = line.Split('|');
            if (parts.Length == 3)
            {
                Entries.Add(new Entry(parts[1], parts[2], parts[0]));
            }
        }
    }
}