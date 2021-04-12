using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassicSnake_v0._1
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager mainGame = new GameManager();
            mainGame.RunGame();

            //Closing Program
            Console.WriteLine("Exiting...");
            Console.ReadKey();
        }
    }
}
