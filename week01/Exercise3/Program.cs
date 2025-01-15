using System;

class Program
{
    static void Main(string[] args)
    {
        string playAgain;

        do
        {
            // Generate a random magic number between 1 and 100
            Random random = new Random();
            int magicNumber = random.Next(1, 101);
            int guess = 0;
            int guessCount = 0;

            Console.WriteLine("Welcome to the 'Guess My Number' game!");
            Console.WriteLine("The computer has chosen a number between 1 and 100.");
            Console.WriteLine("Try to guess it!");

            // Loop until the user guesses the magic number
            while (guess != magicNumber)
            {
                Console.Write("Enter your guess: ");
                // Validate user input
                while (!int.TryParse(Console.ReadLine(), out guess))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }

                guessCount++; // Increment the guess counter

                if (guess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine($"Congratulations! You guessed it in {guessCount} attempts.");
                }
            }

            // Ask the user if they want to play again
            Console.Write("Do you want to play again? (yes/no): ");
            playAgain = Console.ReadLine().ToLower();

        } while (playAgain == "yes");

        Console.WriteLine("Thanks for playing 'Guess My Number'! Goodbye!");
    }
}
