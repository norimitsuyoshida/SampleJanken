using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleJanken
{
    class Program
    {
        static void Main(string[] args)
        {
            // 人数
            int playersCount = 0;
            do
            {
                Console.WriteLine("何人でじゃんけんしますか？");
                Console.Write(">>> ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out playersCount))
                {
                    Console.WriteLine("数値化できない入力でしたのでやり直します");
                }
            } while (playersCount < 2);

            // じゃんけん
            Janken janken = new Janken(playersCount);
            while (janken.Play());
            janken.Result();
            Console.WriteLine("Press any keys.");
            Console.Read();
        }
    }
}
