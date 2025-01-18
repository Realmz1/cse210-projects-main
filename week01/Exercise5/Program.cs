using System;

class Program
{
    // Function to display a welcome message
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the Program!");
    }

    // Function to prompt the user for their name and return it
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        return Console.ReadLine();
    }

    // Function to prompt the user for their favorite number and return it as an integer
    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        return int.Parse(Console.ReadLine());
    }

    // Function to square a number
    static int SquareNumber(int number)
    {
        return number * number;
    }

    // Function to display the result
    static void DisplayResult(string userName, int squaredNumber)
    {
        Console.WriteLine($"{userName}, the square of your number is {squaredNumber}");
    }

    // Main function
    static void Main(string[] args)
    {
        // Call the DisplayWelcome function
        DisplayWelcome();

        // Get the user's name
        string userName = PromptUserName();

        // Get the user's favorite number
        int favoriteNumber = PromptUserNumber();

        // Calculate the square of the number
        int squaredNumber = SquareNumber(favoriteNumber);

        // Display the result
        DisplayResult(userName, squaredNumber);
    }
}
