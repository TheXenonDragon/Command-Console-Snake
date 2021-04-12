using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ClassicSnake_v0._1
{
    class MainMenu
    {
        //Fields
        private static ConsoleColor textColor;
        private static string userChoices;
        private static string tokenSuffixPlural;
        public static int mapSize;
        public static int gameSpeed;

        //The "main method" of this class
        public static void CreateMenu()
        {
            //  Set up window and text
            Console.Title = "Command Console Snake v0.1 Pre-Alpha";
            Console.SetWindowSize(41, 21);
            Console.SetBufferSize(41, 21);
            textColor = ConsoleColor.Red;
            Console.ForegroundColor = textColor;

            TitleCover();
            GameInstructions();
            Settings();
            Console.ReadKey();
            Console.Beep();

            Console.Clear();
            Console.Write("\n\n\n\n\n\tPress any key to Start: ");
            Console.ReadKey();
            Console.ForegroundColor = textColor;
            Console.Beep();
            Console.Clear();
            GameWindowSize();
        }

        //Game Over
        public static void GameOver()
        {
            if(TokenManager.gameScore == 1)
            {
                tokenSuffixPlural = "";
            }
            else
            {
                tokenSuffixPlural = "s";
            }
            Console.SetWindowSize(41, 21);
            Console.SetBufferSize(41, 21);
            Console.WriteLine("\n\n\n\t\tGAME OVER");
            Console.WriteLine("\n\n\tYou collected {0} token{1}!", TokenManager.gameScore, tokenSuffixPlural);
            Console.WriteLine("\n\n\t Your High Score is: {0}", TokenManager.HighScore());
            Console.ReadKey();
            Console.Beep();
            Console.Clear();
        }

        //Pause Screen
        public static void PauseMenu()
        {
            bool correctInput = false;
            ConsoleKey userInput;
            Console.Beep();
            //Change Screen Size
            if (mapSize <= 10)
            {
                Console.SetWindowSize(26, 22);
                Console.SetBufferSize(26, 22);
            }
            else if (mapSize <= 20)
            {
                Console.SetWindowSize(43, 30);
                Console.SetBufferSize(43, 30);
            }
            else if (mapSize <= 30)
            {
                Console.SetWindowSize(63, 40);
                Console.SetBufferSize(63, 40);
            }
            else
            {
                Console.SetWindowSize(83, 44);
                Console.SetBufferSize(83, 44);
            }
            //Ask user whether they want to resume game or quit
            do
            {
                Console.WriteLine("GAME PAUSED:\n\n\nTokens Collected: {0}\n", TokenManager.gameScore);
                Console.WriteLine("-Press the Escape key to quit\n-Press the Spacebar to continue the game");
                userInput = Console.ReadKey().Key;
                if(userInput == ConsoleKey.Escape)
                {
                    GameManager.activeGameplay = false;
                    correctInput = true;
                    Console.Beep();
                }
                else if (userInput == ConsoleKey.Spacebar)
                {
                    correctInput = true;
                    Console.Beep();
                }
                else
                {
                    Console.Write("\n\nPlease enter a valid input.\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (!correctInput);
            //Resize window
            GameWindowSize();
        }

        //Modifier Methods
        public static void ResetUserChoicesString()
        {
            userChoices = "";
        }

        //Additional private methods
        private static void TitleCover()
        {
            Console.Write("\n\tCommand Console Snake");
            Console.ReadKey();
            Console.Beep();
            Console.Clear();
        }
        private static void GameInstructions()
        {
            Console.WriteLine("\t  How to play the game:\n\n");
            Console.WriteLine(
                " -Use the keys W, A, S, and D to move\n\n" + 
                " -You cannot move backwards; \n  The snake only moves forward and turns   left or right (using W A S D)\n\n" +
                " -Press Spacebar to pause the game\n\n" +
                " -Collect tokens \"#\" to get a high score\n\n" + 
                " -Don't run into the borders or you will   get GAME OVER");
            Console.Write("\n\n\n  Command Console Snake v0.1 Pre-Alpha\n\t   Created by: Micah\n\t    Published: 2021");
            Console.ReadKey();
            Console.Beep();
            Console.Clear();
        }
        private static void Settings()
        {
            bool correctInput = false;
            ConsoleKey userInput;
            do
            {
                Console.Write("Would you like to play with \nDefault Settings?\n-Yes (y)\n-No (n)\n");
                userInput = Console.ReadKey().Key;
                if(userInput == ConsoleKey.Y)
                {
                    Console.Clear();
                    Console.Write("\nYou are now playing with Default Settings");
                    Console.Beep();

                    GameManager.PlayWithBorders = true;
                    textColor = ConsoleColor.Green;
                    mapSize = 20;
                    gameSpeed = 300;
                    GameManager.playWithObstacles = true;
                    correctInput = true;
                }
                else if(userInput == ConsoleKey.N)
                {
                    Console.Clear();
                    userChoices += "Custom Settings:\n\n\n";
                    Console.Beep();
                    PlayWithBorders();
                    ChooseTextColor();
                    MapSize();
                    GameSpeed();
                    PlayWithObstacles();
                    correctInput = true;
                }
                else
                {
                    Console.Write("\n\nPlease enter a valid input.\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (!correctInput);
        }
        private static void PlayWithBorders()
        {
            bool correctInput = false;
            ConsoleKey userInput;
            do
            {
                Console.Write(userChoices);
                Console.Write("Play with borders:\n-Yes (y)\n-No (n)\n");
                userInput = Console.ReadKey().Key;

                if (userInput == ConsoleKey.Y)
                {
                    userChoices += "You will play with borders\n\n";
                    Console.Clear();
                    Console.Write(userChoices);
                    GameManager.PlayWithBorders = true;
                    correctInput = true;
                    Console.Beep();
                }
                else if (userInput == ConsoleKey.N)
                {
                    userChoices += "You will play without borders\n\n";
                    Console.Clear();
                    Console.Write(userChoices);
                    GameManager.PlayWithBorders = false;
                    correctInput = true;
                    Console.Beep();
                }
                else
                {
                    Console.Write("\n\nPlease enter a valid input.\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (!correctInput);
        }
        private static void ChooseTextColor()
        {
            Console.Write("Play with:\n-\"g\" for Green Text\n-\"b\" for Blue Text\n-\"r\" for Red Text\n-\"w\" for White Text\n");
            ConsoleKey userInput = Console.ReadKey().Key;
            if (userInput == ConsoleKey.G)
            {
                //Console.WriteLine("\nYou chose the color Green\n");
                userChoices += "You chose the color Green\n\n";
                Console.Clear();
                Console.Write(userChoices);
                textColor = ConsoleColor.Green;
                Console.Beep();
            }
            else if (userInput == ConsoleKey.B)
            {
                //Console.WriteLine("\nYou chose the color Blue\n");
                userChoices += "You chose the color Blue\n\n";
                Console.Clear();
                Console.Write(userChoices);
                textColor = ConsoleColor.Cyan;
                Console.Beep();
            }
            else if (userInput == ConsoleKey.R)
            {
                //Console.WriteLine("\nYou chose the color Red\n");
                userChoices += "You chose the color Red\n\n";
                Console.Clear();
                Console.Write(userChoices);
                textColor = ConsoleColor.DarkRed;
                Console.Beep();
            }
            else if (userInput == ConsoleKey.W)
            {
                //Console.WriteLine("\nYou chose the color White\n");
                userChoices += "You chose the color White\n\n";
                Console.Clear();
                Console.Write(userChoices);
                textColor = ConsoleColor.White;
                Console.Beep();
            }
            else
            {
                Console.Write("\n\nPlease enter a valid input.\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
                Console.Write(userChoices);
                ChooseTextColor();
            }
        }
        private static void MapSize()
        {
            bool correctInput = false;
            ConsoleKey userInput;
            do
            {
                Console.WriteLine("Play on:\n-Tiny Map (t)\n-Small Map (s)\n-Medium Map (m)\n-Large Map (l)\n-Extremely Large Map (x)");
                userInput = Console.ReadKey().Key;
                if (userInput == ConsoleKey.T)
                {
                    userChoices += "You chose to play on a Tiny map\n\n";
                    Console.Clear();
                    Console.Write(userChoices);
                    mapSize = 5;
                    correctInput = true;
                }
                else if (userInput == ConsoleKey.S)
                {
                    userChoices += "You chose to play on a Small map\n\n";
                    Console.Clear();
                    Console.Write(userChoices);
                    mapSize = 10;
                    correctInput = true;
                }
                else if (userInput == ConsoleKey.M)
                {
                    userChoices += "You chose to play on a Medium map\n\n";
                    Console.Clear();
                    Console.Write(userChoices);
                    mapSize = 20;
                    correctInput = true;
                }
                else if (userInput == ConsoleKey.L)
                {
                    userChoices += "You chose to play on a Large map\n\n";
                    Console.Clear();
                    Console.Write(userChoices);
                    mapSize = 30;
                    correctInput = true;

                }
                else if (userInput == ConsoleKey.X)
                {
                    userChoices += "You chose to play on an Extremely Large\nmap\n\n";
                    Console.Clear();
                    Console.Write(userChoices);
                    mapSize = 40;
                    correctInput = true;
                }
                else
                {
                    Console.WriteLine("\n\nPlease enter a valid input.\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    Console.Write(userChoices);
                }
            } while (!correctInput);
            Console.Beep();
        }
        private static void GameSpeed()
        {
            bool correctInput = false;
            ConsoleKey userInput;
            do
            {
                Console.WriteLine("Play Speed:\n-Turtle (1)\n-Slow (2)\n-Moderate (3)\n-Fast (4)\n-Insane (5)");
                userInput = Console.ReadKey().Key;
                if (userInput == ConsoleKey.D1)
                {
                    userChoices += "You chose to play at Turtle speed\n\n";
                    Console.Clear();
                    Console.Write(userChoices);
                    gameSpeed = 700;
                    correctInput = true;
                }
                else if (userInput == ConsoleKey.D2)
                {
                    userChoices += "You chose to play at Slow speed\n\n";
                    Console.Clear();
                    Console.Write(userChoices);
                    gameSpeed = 500;
                    correctInput = true;
                }
                else if (userInput == ConsoleKey.D3)
                {
                    userChoices += "You chose to play at Moderate speed\n\n";
                    Console.Clear();
                    Console.Write(userChoices);
                    gameSpeed = 300;
                    correctInput = true;
                }
                else if (userInput == ConsoleKey.D4)
                {
                    userChoices += "You chose to play at Fast speed\n\n";
                    Console.Clear();
                    Console.Write(userChoices);
                    gameSpeed = 200;
                    correctInput = true;

                }
                else if (userInput == ConsoleKey.D5)
                {
                    userChoices += "You chose to play at Insane speed\n\n";
                    Console.Clear();
                    Console.Write(userChoices);
                    gameSpeed = 120;
                    correctInput = true;
                }
                else
                {
                    Console.WriteLine("\n\nPlease enter a valid input.\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    Console.Write(userChoices);
                }
            } while (!correctInput);
            Console.Beep();
        }
        private static void GameWindowSize()
        {
            if (mapSize <= 10)
            {
                Console.SetWindowSize(24, 13);
                Console.SetBufferSize(24, 13);
            }
            else if (mapSize <= 20)
            {
                Console.SetWindowSize(43, 23);
                Console.SetBufferSize(43, 23);
                //41, 21
                //41, 21
            }
            else if (mapSize <= 30)
            {
                Console.SetWindowSize(63, 33);
                Console.SetBufferSize(63, 33);
            }
            else
            {
                Console.SetWindowSize(83, 43);
                Console.SetBufferSize(83, 43);
            }
        }
        private static void PlayWithObstacles()
        {
            bool correctInput = false;
            ConsoleKey userInput;
            do
            {
                Console.WriteLine("Play With Obstacles:\n-Yes (y)\n-No (n)");
                userInput = Console.ReadKey().Key;
                if (userInput == ConsoleKey.Y)
                {
                    userChoices += "You chose to play with Obstacles\n";
                    Console.Clear();
                    Console.Write(userChoices);
                    GameManager.playWithObstacles = true;
                    correctInput = true;

                }
                else if (userInput == ConsoleKey.N)
                {
                    userChoices += "You chose to play without Obstacles\n";
                    Console.Clear();
                    Console.Write(userChoices);
                    GameManager.playWithObstacles = false;
                    correctInput = true;
                }
                else
                {
                    Console.WriteLine("\n\nPlease enter a valid input.\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    Console.Write(userChoices);
                }
            } while (!correctInput);
            Console.Beep();
        }
    }
}
