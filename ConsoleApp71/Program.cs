using System;
using System.IO;
using System.Collections.Generic;

class GuessTheNumberGame
{
    static void Main()
    {
        bool playAgain = true;
        List<int> attemptsHistory = new List<int>();
        List<Player> singlePlayerScores = new List<Player>();
        List<Player> multiplayerScores = new List<Player>();

        Console.WriteLine("Welcome to Guess the Number!");

        while (playAgain)
        {
            Console.WriteLine("\nSelect the game mode:");
            Console.WriteLine("1. Single Player");
            Console.WriteLine("2. Multiplayer");

            int mode;
            while (true)
            {
                Console.Write("Enter the mode number: ");
                string modeInput = Console.ReadLine();

                if (int.TryParse(modeInput, out mode) && (mode == 1 || mode == 2))
                    break;

                Console.WriteLine("Invalid input. Please enter a valid mode number.");
            }

            if (mode == 1)
            {
                PlaySinglePlayerMode(attemptsHistory, singlePlayerScores);
            }
            else
            {
                PlayMultiplayerMode(attemptsHistory, multiplayerScores);
            }

            Console.WriteLine("\nDo you want to play again? (Y/N)");
            string playAgainInput = Console.ReadLine();

            playAgain = (playAgainInput.ToUpper() == "Y");

            if (!playAgain)
            {
                Console.WriteLine("\nSingle Player High Scores:");
                PrintScores(singlePlayerScores);

                Console.WriteLine("\nMultiplayer High Scores:");
                PrintScores(multiplayerScores);
            }
        }

        Console.WriteLine("\nThank you for playing Guess the Number!");
        Console.ReadLine();
    }

    static void PlaySinglePlayerMode(List<int> attemptsHistory, List<Player> singlePlayerScores)
    {
        Console.WriteLine("\nSingle Player Mode");

        int maxNumber = 100;
        int attempts = 5;
        attemptsHistory.Clear();

        Console.WriteLine($"\nNew game started! Guess a number between 1 and {maxNumber}.");
        Console.WriteLine("You have 5 attempts to guess it.");

        int numberToGuess = GenerateRandomNumber(1, maxNumber);

        while (attempts > 0)
        {
            Console.Write("Enter your guess: ");
            string input = Console.ReadLine();

            int guess;
            if (int.TryParse(input, out guess))
            {
                if (guess < 1 || guess > maxNumber)
                {
                    Console.WriteLine($"Please enter a number between 1 and {maxNumber}.");
                    continue;
                }

                attemptsHistory.Add(guess);

                if (guess == numberToGuess)
                {
                    Console.WriteLine("Congratulations! You guessed the number!");
                    break;
                }
                else if (guess < numberToGuess)
                {
                    Console.WriteLine("Too low! Try again.");
                }
                else
                {
                    Console.WriteLine("Too high! Try again.");
                }

                if (attempts > 1)
                {
                    int difference = Math.Abs(numberToGuess - guess);
                    if (difference <= 5)
                        Console.WriteLine("You're very close!");
                    else if (difference <= 10)
                        Console.WriteLine("You're getting closer!");
                    else
                        Console.WriteLine("You're far!");
                }

                attempts--;
                Console.WriteLine("Attempts left: " + attempts);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        if (attempts == 0)
        {
            Console.WriteLine("Game over! You ran out of attempts.");
            Console.WriteLine("The number was: " + numberToGuess);
        }

        Console.WriteLine("\nAttempts history:");
        foreach (int attempt in attemptsHistory)
        {
            Console.Write(attempt + " ");
        }

        if (attempts > 0)
        {
            Console.WriteLine("\nEnter your name to save your score: ");
            string playerName = Console.ReadLine();

            Player player = new Player(playerName, attempts);
            singlePlayerScores.Add(player);
        }
    }

    static void PlayMultiplayerMode(List<int> attemptsHistory, List<Player> multiplayerScores)
    {
        Console.WriteLine("\nMultiplayer Mode");

        int maxNumber = 100;
        int attempts = 5;
        attemptsHistory.Clear();

        Console.WriteLine($"\nNew game started! Player 1, guess a number between 1 and {maxNumber}.");
        Console.WriteLine("Player 2, look away!");

        int numberToGuess = GenerateRandomNumber(1, maxNumber);

        while (attempts > 0)
        {
            Console.Write("Player 1, enter your guess: ");
            string input = Console.ReadLine();

            int guess;
            if (int.TryParse(input, out guess))
            {
                if (guess < 1 || guess > maxNumber)
                {
                    Console.WriteLine($"Please enter a number between 1 and {maxNumber}.");
                    continue;
                }

                attemptsHistory.Add(guess);

                if (guess == numberToGuess)
                {
                    Console.WriteLine("Player 1 wins! You guessed the number!");
                    break;
                }
                else if (guess < numberToGuess)
                {
                    Console.WriteLine("Too low! Player 2, it's your turn.");
                }
                else
                {
                    Console.WriteLine("Too high! Player 2, it's your turn.");
                }

                if (attempts > 1)
                {
                    int difference = Math.Abs(numberToGuess - guess);
                    if (difference <= 5)
                        Console.WriteLine("Player 1 is very close!");
                    else if (difference <= 10)
                        Console.WriteLine("Player 1 is getting closer!");
                    else
                        Console.WriteLine("Player 1 is far!");
                }

                attempts--;
                Console.WriteLine("Attempts left: " + attempts);

                Console.WriteLine("Player 2, press enter when ready.");
                Console.ReadLine();

                Console.Clear();

                Console.WriteLine($"Player 2, guess a number between 1 and {maxNumber}.");

                Console.Write("Player 2, enter your guess: ");
                input = Console.ReadLine();

                if (int.TryParse(input, out guess))
                {
                    if (guess < 1 || guess > maxNumber)
                    {
                        Console.WriteLine($"Please enter a number between 1 and {maxNumber}.");
                        continue;
                    }

                    attemptsHistory.Add(guess);

                    if (guess == numberToGuess)
                    {
                        Console.WriteLine("Player 2 wins! You guessed the number!");
                        break;
                    }
                    else if (guess < numberToGuess)
                    {
                        Console.WriteLine("Too low! Player 1, it's your turn.");
                    }
                    else
                    {
                        Console.WriteLine("Too high! Player 1, it's your turn.");
                    }

                    if (attempts > 1)
                    {
                        int difference = Math.Abs(numberToGuess - guess);
                        if (difference <= 5)
                            Console.WriteLine("Player 2 is very close!");
                        else if (difference <= 10)
                            Console.WriteLine("Player 2 is getting closer!");
                        else
                            Console.WriteLine("Player 2 is far!");
                    }

                    attempts--;
                    Console.WriteLine("Attempts left: " + attempts);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        if (attempts == 0)
        {
            Console.WriteLine("Game over! Both players ran out of attempts.");
            Console.WriteLine("The number was: " + numberToGuess);
        }

        Console.WriteLine("\nAttempts history:");
        foreach (int attempt in attemptsHistory)
        {
            Console.Write(attempt + " ");
        }

        if (attempts > 0)
        {
            Console.WriteLine("\nEnter the names of the players to save their scores: ");
            Console.Write("Player 1: ");
            string player1Name = Console.ReadLine();

            Console.Write("Player 2: ");
            string player2Name = Console.ReadLine();

            Player player1 = new Player(player1Name, attempts);
            Player player2 = new Player(player2Name, attempts);
            multiplayerScores.Add(player1);
            multiplayerScores.Add(player2);
        }
    }

    static void PrintScores(List<Player> scores)
    {
        if (scores.Count > 0)
        {
            scores.Sort();
            foreach (Player player in scores)
            {
                Console.WriteLine($"{player.Name}: {player.Score} attempts");
            }
        }
        else
        {
            Console.WriteLine("No scores available.");
        }
    }

    static int GenerateRandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max + 1);
    }
}

class Player : IComparable<Player>
{
    public string Name { get; set; }
    public int Score { get; set; }

    public Player(string name, int score)
    {
        Name = name;
        Score = score;
    }

    public int CompareTo(Player other)
    {
        return Score.CompareTo(other.Score);
    }
}
