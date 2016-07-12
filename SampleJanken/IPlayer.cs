using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleJanken
{
    [Flags()]
    enum JankenHand {
        Unknown = 0,    // 未定
        Goo     = 1,    // グー
        Choki   = 2,    // チョキ
        Paa     = 4,    // パー
    }

    interface IPlayer
    {
        JankenHand Janken();
        int WinCount { get; set; }
        int LossCount { get; set; }
        string Name { get; set; }
        void Win();
        void Loss();
        float Average();
    }

    class PlayerBase
    {
        public int WinCount { get; set; }
        public int LossCount { get; set; }
        public string Name { get; set; }
        public void Win()
        {
            WinCount++;
        }
        public void Loss()
        {
            LossCount++;
        }
        public float Average()
        {
            int totalCount = WinCount + LossCount;
            if (totalCount == 0) return 0;
            return (float)WinCount / totalCount * 100;
        }

    }

    class HumanPlayer : PlayerBase, IPlayer
    {
        public JankenHand Janken()
        {
            Console.WriteLine("出す手の数字を入力してください");
            JankenHand hand = JankenHand.Unknown;
            do
            {
                Console.WriteLine("グー = 1");
                Console.WriteLine("チョキ = 2");
                Console.WriteLine("パー = 3");
                Console.Write(">>> ");
                string input = Console.ReadLine();
                if (input == "1" || input == "2" || input == "3")
                {
                    hand = (JankenHand)(1 << (int.Parse(input) - 1));
                }
                else
                {
                    Console.WriteLine("手に応じた数字を入力し直してください");
                }
            }
            while (hand == JankenHand.Unknown);
            return hand;
        }

        public HumanPlayer(string name="")
        {
            Name = string.IsNullOrEmpty(name) ? "あなた" : name;
        }
    }

    class ComPlayer : PlayerBase, IPlayer
    {
        private static Random Rand = new System.Random();

        public JankenHand Janken()
        {
            return (JankenHand)(1 << (Rand.Next(3)));
        }

        public ComPlayer(string name="")
        {
            Name = string.IsNullOrEmpty(name) ? "ＣＯＭ" : name;
        }
    }
}
