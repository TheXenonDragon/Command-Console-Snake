using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicSnake_v0._1
{
    class MapGenerator
    {
        private int MAP_WIDTH = 20;
        private int MAP_HEIGHT = 20;
        private string[,] mapArray;
        private int currentPlayerLocationX;
        private int currentPlayerLocationY;
        private List<int> snakeBodyX = new List<int>();
        private List<int> snakeBodyY = new List<int>();
        private int tokenLocationX;
        private int tokenLocationY;
        private bool Play_With_Obstacles;

        //Temporary Fields
        public enum TemporaryPlayerDirection { PlayerIsHeadingRight, PlayerIsHeadingLeft, PlayerIsHeadingUp, PlayerIsHeadingDown }
        private List<int> tempSnakeBodyX = new List<int>();
        private List<int> tempSnakeBodyY = new List<int>();
        public int tempDirection = -1;
        private int tempObstacleX;
        private int tempObstacleY;

        //Methods
        public void GenerateNewMap(bool playWithObstacles)
        {
            //Sets the values for all fields that need to be set
            MAP_WIDTH = MainMenu.mapSize;
            MAP_HEIGHT = MAP_WIDTH;
            currentPlayerLocationX = MAP_WIDTH / 2;
            currentPlayerLocationY = MAP_HEIGHT / 2;
            snakeBodyX.Add(currentPlayerLocationX);
            snakeBodyY.Add(currentPlayerLocationY);
            Play_With_Obstacles = playWithObstacles;

            mapArray = new string[MAP_HEIGHT, MAP_WIDTH];
            for (int i = 0; i < mapArray.GetLength(0); i++)
            {
                for (int j = 0; j < mapArray.GetLength(1); j++)
                {
                    mapArray[j, i] = ".";
                }
            }

            //Sets up the first token and places it
            TokenManager.GenerateNewToken(MAP_WIDTH, MAP_HEIGHT);
            PlaceToken();   //This method is called in order to make sure that the token is not placed within an obstacle
            TokenManager.gameScore = 0;
            tokenLocationX = TokenManager.tokenPositionX;
            tokenLocationY = TokenManager.tokenPositionY;
            mapArray[tokenLocationX, tokenLocationY] = "#";
        }

        public void OverrideMapWithoutBorders(bool playerIsHeadingRight, bool playerIsHeadingLeft, bool playerIsHeadingUp, bool playerIsHeadingDown)
        {
            //Updates the player's location based on boolean values
            PlayerMovementDirection(playerIsHeadingRight, playerIsHeadingLeft, playerIsHeadingUp, playerIsHeadingDown);

            //Resets the player's location if the player travels "out of bounds"
            if (currentPlayerLocationX > (MAP_WIDTH - 1))
            {
                //  currentPlayerLocationX = currentPlayerLocationX - (MAP_WIDTH * (currentPlayerLocationX / MAP_WIDTH));
                currentPlayerLocationX = 0;
            }
            if (currentPlayerLocationX < 0)
            {
                //  currentPlayerLocationX = currentPlayerLocationX + MAP_WIDTH + (MAP_WIDTH * ((Math.Abs(currentPlayerLocationX) - 1) / MAP_WIDTH));
                currentPlayerLocationX = (MAP_WIDTH - 1);
            }
            if (currentPlayerLocationY > (MAP_HEIGHT - 1))
            {
                //  currentPlayerLocationY = currentPlayerLocationY - (MAP_HEIGHT * (currentPlayerLocationY / MAP_HEIGHT));
                currentPlayerLocationY = 0;
            }
            if (currentPlayerLocationY < 0)
            {
                //  currentPlayerLocationY = currentPlayerLocationY + MAP_HEIGHT + (MAP_HEIGHT * ((Math.Abs(currentPlayerLocationY) - 1) / MAP_HEIGHT));
                currentPlayerLocationY = (MAP_HEIGHT - 1);
            }

            ResetMap();

            if (Play_With_Obstacles)
            {
                AddObstacles();
                CheckForObstacleHit();
            }

            //Handles the Token Position
            PlaceToken();

            //Setting player's current location
            UpdatePlayerLocation();

            //Checks to see if the head of the snake has collided with its body
            CheckForBodyHit();

            //Print the map
            PrintMap();
        }
        public void OverrideMapWithBorders(bool playerIsHeadingRight, bool playerIsHeadingLeft, bool playerIsHeadingUp, bool playerIsHeadingDown)
        {
            //Updates the player's location based on boolean values
            PlayerMovementDirection(playerIsHeadingRight, playerIsHeadingLeft, playerIsHeadingUp, playerIsHeadingDown);

            //Prevents the player from traveling "out of bounds"
            if (currentPlayerLocationX > (MAP_WIDTH - 1))
            {
                currentPlayerLocationX = (MAP_WIDTH - 1);
                GameManager.activeGameplay = false;
                //playerIsHeadingRight = false;
            }
            if (currentPlayerLocationX < 0)
            {
                currentPlayerLocationX = 0;
                GameManager.activeGameplay = false;
                //playerIsHeadingLeft = false;
            }
            if (currentPlayerLocationY > (MAP_HEIGHT - 1))
            {
                currentPlayerLocationY = (MAP_HEIGHT - 1);
                GameManager.activeGameplay = false;
                //playerIsHeadingDown = false;
            }
            if (currentPlayerLocationY < 0)
            {
                currentPlayerLocationY = 0;
                GameManager.activeGameplay = false;
                //playerIsHeadingUp = false;
            }

            ResetMap();

            if (Play_With_Obstacles)
            {
                AddObstacles();
                CheckForObstacleHit();
            }
            
            //Handles the Token Position
            PlaceToken();

            //Setting player's current location
            UpdatePlayerLocation();

            //Checks to see if the head of the snake has collided with its body
            CheckForBodyHit();
            
            //Print the map
            PrintMap();
        }
        private void UpdatePlayerLocation()
        {
            //Setting player's current location
            tempSnakeBodyX.AddRange(snakeBodyX);
            tempSnakeBodyY.AddRange(snakeBodyY);

            for (int i = 0; i < (snakeBodyX.Count - 1); i++)
            {
                snakeBodyX[i + 1] = tempSnakeBodyX[i];
                snakeBodyY[i + 1] = tempSnakeBodyY[i];
                mapArray[snakeBodyX[i + 1], snakeBodyY[i + 1]] = "*";
            }
            tempSnakeBodyX.Clear();
            tempSnakeBodyY.Clear();
            snakeBodyX[0] = currentPlayerLocationX;
            snakeBodyY[0] = currentPlayerLocationY;
            mapArray[snakeBodyX[0], snakeBodyY[0]] = "+";
        }
        private void ResetMap()
        {
            for (int i = 0; i < mapArray.GetLength(0); i++)
            {
                for (int j = 0; j < mapArray.GetLength(1); j++)
                {
                    mapArray[i, j] = " ";
                }
            }
        }
        private void PlaceToken()
        {
            //Handles the Token Position
            if ((TokenManager.tokenPositionX == currentPlayerLocationX) && (TokenManager.tokenPositionY == currentPlayerLocationY))
            {
                while(true) {
                    TokenManager.ResetToken();
                    if (mapArray[TokenManager.tokenPositionX, TokenManager.tokenPositionY] == " ")
                    {
                        break;
                    }
                    TokenManager.gameScore -= 1;
                }
                tokenLocationX = TokenManager.tokenPositionX;
                tokenLocationY = TokenManager.tokenPositionY;
                mapArray[tokenLocationX, tokenLocationY] = "#";
                snakeBodyX.Insert(0, currentPlayerLocationX);
                snakeBodyY.Insert(0, currentPlayerLocationY);
            }
            else
            {
                mapArray[tokenLocationX, tokenLocationY] = "#";
            }
        }
        private void PrintMap()
        {
            //Prints the top border
            for (int i = 0; i < mapArray.GetLength(0) + 1; i++)
            {
                Console.Write("__");
            }
            Console.WriteLine();

            //Print the map
            for (int i = 0; i < mapArray.GetLength(0); i++)
            {
                //Prints the left border
                Console.Write("|");
                for (int j = 0; j < mapArray.GetLength(1); j++)
                {
                    Console.Write(mapArray[j, i] + " ");
                }
                //Prints the right border
                Console.Write("|");
                Console.WriteLine();
            }

            //Prints the bottom border
            for (int i = 0; i < mapArray.GetLength(0) + 1; i++)
            {
                Console.Write("--");
            }
            Console.WriteLine();
        }
        private void CheckForBodyHit()
        {
            //  Checks to see if the head of the snake has collided with its body
            //  (it does not check the first body segment since this part is detected when the player grabs a token)
            for (int i = 2; i < snakeBodyX.Count; i++)
            {
                if ((snakeBodyX[0] == snakeBodyX[i]) && (snakeBodyY[0] == snakeBodyY[i]))
                {
                    GameManager.activeGameplay = false;
                }
            }

        }
        private void PlayerMovementDirection(bool playerIsHeadingRight, bool playerIsHeadingLeft, bool playerIsHeadingUp, bool playerIsHeadingDown)
        {
            //Updates the player's location based on boolean values
            if (playerIsHeadingRight)
            {
                if (tempDirection != (int)TemporaryPlayerDirection.PlayerIsHeadingLeft)
                {
                    currentPlayerLocationX += 1;
                    tempDirection = (int)TemporaryPlayerDirection.PlayerIsHeadingRight;
                }
                else
                {
                    currentPlayerLocationX -= 1;
                }
            }
            else if (playerIsHeadingLeft)
            {
                if (tempDirection != (int)TemporaryPlayerDirection.PlayerIsHeadingRight)
                {
                    currentPlayerLocationX -= 1;
                    tempDirection = (int)TemporaryPlayerDirection.PlayerIsHeadingLeft;
                }
                else
                {
                    currentPlayerLocationX += 1;
                }
            }
            else if (playerIsHeadingUp)
            {
                if (tempDirection != (int)TemporaryPlayerDirection.PlayerIsHeadingDown)
                {
                    currentPlayerLocationY -= 1;
                    tempDirection = (int)TemporaryPlayerDirection.PlayerIsHeadingUp;
                }
                else
                {
                    currentPlayerLocationY += 1;
                }
            }
            else if(playerIsHeadingDown)
            {
                if (tempDirection != (int)TemporaryPlayerDirection.PlayerIsHeadingUp)
                {
                    currentPlayerLocationY += 1;
                    tempDirection = (int)TemporaryPlayerDirection.PlayerIsHeadingDown;
                }
                else
                {
                    currentPlayerLocationY -= 1;
                }
            }
        }
        private void AddObstacles()
        {
            tempObstacleX = (MAP_WIDTH / 5);
            tempObstacleY = (3 * (MAP_WIDTH / 5));
            for (int i = 0; i < (MAP_WIDTH / 5); i++)
            {
                for (int j = 0; j < (MAP_WIDTH / 5); j++)
                {
                    mapArray[tempObstacleX + i, tempObstacleX + j] = "=";
                    mapArray[tempObstacleX + i, tempObstacleY + j] = "=";
                    mapArray[tempObstacleY + i, tempObstacleX + j] = "=";
                    mapArray[tempObstacleY + i, tempObstacleY + j] = "=";
                }
            }
        }
        private void CheckForObstacleHit()
        {
            if (mapArray[currentPlayerLocationX, currentPlayerLocationY] == "=")
            {
                GameManager.activeGameplay = false;
            }
        }
    }
    
}
