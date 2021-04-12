using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClassicSnake_v0._1
{
    class TokenManager
    {
        //Fields
        public static Random rngPosition = new Random();
        public static int tokenPositionX;
        public static int tokenPositionY;
        public static int gameScore = 0;
        private static int highScore = 0;
        private static int mapWidth_;
        private static int mapHeight_;

        //Sets up the first token
        public static void GenerateNewToken(int mapWidth, int mapHeight)
        {
            mapWidth_ = mapWidth;
            mapHeight_ = mapHeight;
            tokenPositionX = rngPosition.Next(0, mapWidth_);
            tokenPositionY = rngPosition.Next(0, mapHeight_);
        }
        //Resets all tokens following the spawn of the first token
        public static void ResetToken()
        {
            gameScore++;
            GenerateNewToken(mapWidth_, mapHeight_);
        }
        //Updates and Returns the value of highScore
        public static int HighScore()
        {
            if(gameScore > highScore)
            {
                highScore = gameScore;
            }
            return highScore;
        }
    }
}
