using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleJanken
{
    class Janken
    {
        private List<IPlayer> Players { get; set; }

        public Janken(int playerCount)
        {
            Players = new List<IPlayer>(playerCount);
            Players.Add(new HumanPlayer()); // 自分はいります
            for (int i = 1; i < playerCount; i++)
            {
                Players.Add(new ComPlayer(string.Format("ＣＯＭ{0}", i)));
            }
            IsAiko = false;
        }

        private bool IsAiko { get; set; }

        /// <summary>
        /// じゃんけんをプレイする。
        /// </summary>
        /// <returns>true=続いている, false=終わり</returns>
        public bool Play()
        {
            if (!IsAiko)
            {
                Console.Clear();
            }
            else
            {
                Console.WriteLine("");
            }
            Console.WriteLine(!IsAiko ? "じゃんけん！！！" : "あいこで！！！");
            IsAiko = !Match();  // 勝負！
            if (IsAiko) return true;

            Console.Write("終わりますか？（y[es]|[n[o]]） >>> ");
            string input = Console.ReadLine();
            if (0 == string.Compare(input, "yes", true) || 0 == string.Compare(input, "y", true))
                return false;
            return true;
        }

        /// <summary>
        /// 成績発表
        /// </summary>
        public void Result()
        {
            Console.WriteLine("");
            Console.WriteLine("成績");
            foreach (var player in Players)
            {
                Console.WriteLine(string.Format("{0} = {1} Win, {2} Loss, {3:0.00}%",
                    player.Name, player.WinCount, player.LossCount, player.Average()));
            }
        }

        /// <summary>
        /// 勝負！
        /// </summary>
        /// <returns>true=決着, false=未決（あいこ）</returns>
        private bool Match()
        {
            var hands = new JankenHand();
            var playerHands = new Dictionary<IPlayer, JankenHand>();

            // じゃんけんプレイヤーの手を出す
            foreach (var player in Players)
            {
                var hand = player.Janken();
                hands |= hand;
                playerHands.Add(player, hand);
            }

            // 一覧
            Console.WriteLine(!IsAiko ? "ぽん！！！" : "しょ！！！");
            foreach (var playerHand in playerHands)
            {
                Console.WriteLine(string.Format("{0}->{1}", playerHand.Key.Name, Enum.GetName(playerHand.Value.GetType(), playerHand.Value)));
            }

            // 勝ち手判定
            JankenHand winHand = GetWinHand(hands);
            if (winHand == JankenHand.Unknown) return false;    // あいこなのでfalseを返す

            // Win Loss 付けと表示
            foreach (var playerHand in playerHands)
            {
                if (playerHand.Value == winHand)
                {
                    playerHand.Key.Win();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(string.Format("{0} Ｗｉｎ！", playerHand.Key.Name));
                }
                else
                {
                    playerHand.Key.Loss();
                    Console.WriteLine(string.Format("{0} Ｌｏｓｓ！", playerHand.Key.Name));
                }
                Console.ResetColor();
            }
            return true;
        }

        /// <summary>
        /// フラグのビットオン数を返す
        /// </summary>
        /// <typeparam name="T">フラグ型</typeparam>
        /// <param name="target">数えたいフラグ</param>
        /// <returns></returns>
        private UInt32 BitOnCount<T>(T target)
            where T : IConvertible
        {
            if (default(T) == null) return 0; // 数値型以外は０を返す
            UInt32 flg = Convert.ToUInt32(target);

            var x = flg - ((flg >> 1) & 0x55555555);
            x = ((x >> 2) & 0x33333333) + (x & 0x33333333);
            x = (x >> 4) + x & 0x0f0f0f0f;
            x += x >> 8;
            return (x >> 16) + x & 0xff;
        }

        /// <summary>
        /// 勝ち手を返す。
        /// </summary>
        /// <param name="hands"></param>
        /// <returns>Unknownの場合勝ち手無し＝あいこ</returns>
        private JankenHand GetWinHand(JankenHand hands)
        {
            var handTypeCount = BitOnCount((int)hands);
            if (handTypeCount == 3 || handTypeCount == 1)   // ３種または１種の手
            {
                return JankenHand.Unknown;   // あいこなので勝ち手無し
            }

            if (hands.HasFlag(JankenHand.Goo))
            {
                if (hands.HasFlag(JankenHand.Choki))
                {
                    return JankenHand.Goo;
                }
                else
                {
                    return JankenHand.Paa;
                }
            }
            return JankenHand.Choki;
        }
    }
}
