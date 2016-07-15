// <copyright file="IPlayer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SampleJanken
{
    using System;

    [Flags]
    internal enum JankenHand
    {
        Unknown = 0,    // 未定
        Goo = 1,    // グー
        Choki = 2,    // チョキ
        Paa = 4,    // パー
    }

    internal interface IPlayer
    {
        int WinCount { get; set; }

        int LossCount { get; set; }

        string Name { get; set; }

        void Win();

        void Loss();

        float Average();

        JankenHand Janken();
    }

    internal class PlayerBase
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
            if (totalCount == 0)
            {
                return 0;
            }

            return (float)this.WinCount / totalCount * 100;
        }
    }
}
