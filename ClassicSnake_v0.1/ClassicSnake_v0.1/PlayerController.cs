using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicSnake_v0._1
{
    class PlayerController
    {
        private static ConsoleKey playerResponse;

        public static void PlayerInput()
        {
            playerResponse = Console.ReadKey().Key;

            if (playerResponse == ConsoleKey.Escape)
            {
                GameManager.activeGameplay = false;
            }
            else if (playerResponse == ConsoleKey.W)
            {
                //Sets the player's direction upwards
                GameManager.playerIsHeadingRight = false;
                GameManager.playerIsHeadingLeft = false;
                GameManager.playerIsHeadingUp = true;
                GameManager.playerIsHeadingDown = false;
            }
            else if (playerResponse == ConsoleKey.S)
            {
                //Sets the player's direction downwards
                GameManager.playerIsHeadingRight = false;
                GameManager.playerIsHeadingLeft = false;
                GameManager.playerIsHeadingUp = false;
                GameManager.playerIsHeadingDown = true;
            }
            else if (playerResponse == ConsoleKey.A)
            {
                //Sets the player's direction to the left
                GameManager.playerIsHeadingRight = false;
                GameManager.playerIsHeadingLeft = true;
                GameManager.playerIsHeadingUp = false;
                GameManager.playerIsHeadingDown = false;
            }
            else if (playerResponse == ConsoleKey.D)
            {
                //Sets the player's direction to the right
                GameManager.playerIsHeadingRight = true;
                GameManager.playerIsHeadingLeft = false;
                GameManager.playerIsHeadingUp = false;
                GameManager.playerIsHeadingDown = false;
            }
            else if(playerResponse == ConsoleKey.Spacebar)
            {
                //Pauses game
                MainMenu.PauseMenu();
            }
            Console.Clear();
        }
    }
}
