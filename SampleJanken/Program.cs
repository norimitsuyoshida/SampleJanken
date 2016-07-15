// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SampleJanken
{
    using System;

    internal class Program
    {
        private static void Main(string[] args)
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
                    Console.WriteLine("え？何人だって？？？");
                }
            }
            while (playersCount < 2);

            // じゃんけん
            Janken janken = new Janken(playersCount);
            while (janken.Play())
            {
            }

            janken.Result();
            Console.WriteLine("Press any keys.");
            Console.Read();
        }
    }
}
