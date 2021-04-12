using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClassicSnake_v0._1
{
    class GameManager
    {
        //Fields
        public static bool activeGameplay = true;
        private static bool playWithBorders = false;
        public static bool playWithObstacles = false;
        public static bool playerIsHeadingRight = false;
        public static bool playerIsHeadingLeft = false;
        public static bool playerIsHeadingUp = false;
        public static bool playerIsHeadingDown = false;
        private ConsoleKey userInput;
        private bool correctInput;

        //Properties
        public static bool PlayWithBorders { set { playWithBorders = value; } }

        //Methods
        public void RunGame()
        {
            StartGame:

            //Initializing Objects/Stucts
            MapGenerator newMap = new MapGenerator();

            //Sets up all variables and initializes the map
            MainMenu.CreateMenu();
            newMap.GenerateNewMap(playWithObstacles);

            //Runs the game
            if (playWithBorders == false)
            {
                //Runs game without borders
                do
                {
                    //Updates the map
                    newMap.OverrideMapWithoutBorders(playerIsHeadingRight, playerIsHeadingLeft, playerIsHeadingUp, playerIsHeadingDown);

                    //Determines the speed of the game by having the current thread wait a certain amount of time
                    Thread.Sleep(MainMenu.gameSpeed);
                    if (Console.KeyAvailable)
                    {
                        PlayerController.PlayerInput();
                    }
                    //Clears the screen so that the map can be freshly printed on the next loop
                    Console.Clear();
                } while (activeGameplay);
            }
            else
            {
                //Runs game with borders
                do
                {
                    //Updates the map
                    newMap.OverrideMapWithBorders(playerIsHeadingRight, playerIsHeadingLeft, playerIsHeadingUp, playerIsHeadingDown);
                    
                    //Determines the speed of the game by having the current thread wait a certain amount of time
                    Thread.Sleep(MainMenu.gameSpeed);
                    if (Console.KeyAvailable)
                    {
                        PlayerController.PlayerInput();
                    }
                    //Clears the screen so that the map can be freshly printed on the next loop
                    Console.Clear();
                } while (activeGameplay);
            }
            
            MainMenu.GameOver();
            
            //Checks if the user would like to replay the game
            correctInput = false;
            do
            {
                Console.WriteLine("Would you like to play again?\n -Yes (y)\n- No (n)");
                userInput = Console.ReadKey().Key;
                if (userInput == ConsoleKey.Y)
                {
                    Console.Clear();

                    //End Game Management
                    newMap = null;
                    MainMenu.ResetUserChoicesString();
                    playerIsHeadingRight = false;
                    playerIsHeadingLeft = false;
                    playerIsHeadingUp = false;
                    playerIsHeadingDown = false;
                    activeGameplay = true;
                    
                    correctInput = true;
                    Console.Beep();
                    goto StartGame;
                }
                else if (userInput == ConsoleKey.N)
                {
                    Console.Clear();
                    correctInput = true;
                    Console.Beep();
                }
                else
                {
                    Console.WriteLine("\n\nPlease enter a valid input.\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (!correctInput);
        }
    }
}
