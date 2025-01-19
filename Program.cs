
using System.Net;

namespace SharesCalculator
{
    internal class Program
    {
        private static decimal? SellBuyFee = null;
        public static decimal? BuyFee_ = null;
        public static decimal? SellFee_ = null;

        static void Main(string[] args)
        {
            //Init
            if (File.Exists("./data.txt"))
            {
                foreach (var line in File.ReadLines("./data.txt"))
                {
                    //not set value
                    if (line[2] == '0')
                    {
                        continue;
                    }

                    if (line[0] == '0')
                    {
                        SellBuyFee = decimal.Parse(line.Split("=")[1]);
                    }
                    if (line[0] == '1')
                    {
                        BuyFee_ = decimal.Parse(line.Split("=")[1]);
                    }
                    if (line[0] == '2')
                    {
                        SellFee_ = decimal.Parse(line.Split("=")[1]);
                    }

                }
            }


            while (true)
            {


                Console.WriteLine("0. Options");
                Console.WriteLine("1. Exit");
                Console.WriteLine("2. Sell Price for Max Loss");
                Console.WriteLine("3. Sell price for Profit");
                Console.WriteLine("4. How many shares to buy for profit");

                switch (Console.ReadKey().KeyChar)
                {
                    case '0':
                        Console.Clear();
                        Options();
                        Console.Clear();
                        break;
                    case '1':
                        return;
                    case '2':
                        Console.Clear();
                        PriceMaxStopLoss();
                        Console.Clear();
                        break;
                    case '3':
                        Console.Clear();
                        SellPriceProfit();
                        Console.Clear();
                        break;
                    case '4':
                        Console.Clear();
                        SahresForProfitBuy();
                        Console.Clear();
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Command not found");

                        break;


                }
            }

        }

        private static void Options()
        {
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. Use Sell Buy Fee as one");
            Console.WriteLine("2. Buy fee set");
            Console.WriteLine("3. Sell fee set");

            while (true)
            {
                switch (Console.ReadKey().KeyChar)
                {
                    case '0':
                        List<string> lines = new List<string>();

                        if (SellBuyFee == null)
                        {
                            lines.Add("0_0=");
                        }
                        else
                        {
                            lines.Add("0_1=" + SellBuyFee.ToString()!);
                        }

                        if (BuyFee_ == null)
                        {
                            lines.Add("1_0=");
                        }
                        else
                        {
                            lines.Add("1_1=" + BuyFee_.ToString()!);
                        }

                        if (SellFee_ == null)
                        {
                            lines.Add("2_0=");
                        }
                        else
                        {
                            lines.Add("2_1=" + SellFee_.ToString()!);
                        }

                        File.WriteAllLines("./data.txt", lines);
                        return;
                    case '1':
                        Console.WriteLine("Your fees combined");
                        SellBuyFee = ReadDecimal();
                        break;
                    case '2':
                        Console.WriteLine("Your Buy fee");
                        BuyFee_ = ReadDecimal();
                        break;
                    case '3':
                        Console.WriteLine("Your Sell fee");
                        SellFee_ = ReadDecimal();
                        break;



                }


            }
        }

        private static void SahresForProfitBuy()
        {
            decimal BuyPrice, SellPrice, BuyFee, SellFee, Profit;

            Console.WriteLine("Buy Price");
            BuyPrice = ReadDecimal();

            if (SellFee_ is not null)
            {
                SellFee = (decimal)SellFee_;
            }
            else
            {
                Console.WriteLine("Sell Fee");
                SellFee = ReadDecimal();
            }

            if (BuyFee_ is not null)
            {
                BuyFee = (decimal)BuyFee_;
            }
            else
            {
                Console.WriteLine("Buy Fee");
                BuyFee = ReadDecimal();
            }

            BuyFee = BuyFee + SellFee;

            Console.WriteLine("Profit");
            Profit = ReadDecimal();
            Console.WriteLine("Sell price");
            SellPrice = ReadDecimal();

            //((Buy*Shares) +fee+Profit)/Shares=Sell => This is the normal
            decimal Shares = (-BuyFee - Profit) / (BuyPrice - SellPrice);

            if (Shares < 1)
            {
                Shares = 1;
            }

            Shares = Math.Round(Shares, 0);

            Console.WriteLine($"You have to buy {Shares} Shares");
            Console.WriteLine($"This will cost you {BuyPrice * Shares + BuyFee}");

            Console.ReadKey();
        }

        private static void SellPriceProfit()
        {
            decimal Shares, BuyPrice, BuyFee, SellFee, Profit;

            Console.WriteLine("How many shares do you have?");
            Shares = ReadDecimal();
            Console.WriteLine("Buy Price");
            BuyPrice = ReadDecimal();
            if (SellFee_ is not null)
            {
                SellFee = (decimal)SellFee_;
            }
            else
            {
                Console.WriteLine("Sell Fee");
                SellFee = ReadDecimal();
            }

            if (BuyFee_ is not null)
            {
                BuyFee = (decimal)BuyFee_;
            }
            else
            {
                Console.WriteLine("Buy Fee");
                BuyFee = ReadDecimal();
            }
            Console.WriteLine("Profit");
            Profit = ReadDecimal();

            decimal Price = (BuyPrice * Shares) + BuyFee + SellFee;

            Price += Profit;

            decimal sellPrice = Math.Round((Price / Shares), 2);

            Console.WriteLine($"You have to sell it on {sellPrice}");
            Console.ReadKey();

        }

        private static void PriceMaxStopLoss()
        {
            decimal Shares, BuyPrice, BuyFee, SellFee, MaxLoss;

            Console.WriteLine("How many shares do you have?");
            Shares = ReadDecimal();
            Console.WriteLine("Buy Price");
            BuyPrice = ReadDecimal();
            if (SellFee_ is not null)
            {
                SellFee = (decimal)SellFee_;
            }
            else
            {
                Console.WriteLine("Sell Fee");
                SellFee = ReadDecimal();
            }

            if (BuyFee_ is not null)
            {
                BuyFee = (decimal)BuyFee_;
            }
            else
            {
                Console.WriteLine("Buy Fee");
                BuyFee = ReadDecimal();
            }
            Console.WriteLine("Max loss");
            MaxLoss = ReadDecimal();

            //Buy Price with Fess
            decimal Price = (BuyPrice * Shares) + BuyFee + SellFee;

            Price -= MaxLoss;

            decimal sellPrice = Math.Round((Price / Shares), 2);

            Console.WriteLine($"You have to sell it on {sellPrice}");
            Console.ReadKey();
        }



        private static decimal ReadDecimal()
        {
            while (true)
            {
                try
                {
                    return decimal.Parse(Console.ReadLine() ?? "w");
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
