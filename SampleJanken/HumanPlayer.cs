// <copyright file="HumanPlayer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SampleJanken
{
    using System;

    internal class HumanPlayer : PlayerBase, IPlayer
    {
        public HumanPlayer(string name = "")
        {
            Name = string.IsNullOrEmpty(name) ? "あなた" : name;
        }

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
    }
}
