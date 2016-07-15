// <copyright file="ComPlayer.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SampleJanken
{
    using System;

    internal class ComPlayer : PlayerBase, IPlayer
    {
        private static Random rand = new Random();

        public ComPlayer(string name = "")
        {
            this.Name = string.IsNullOrEmpty(name) ? "ＣＯＭ" : name;
        }

        public JankenHand Janken()
        {
            return (JankenHand)(1 << rand.Next(3));
        }
    }
}
