
using System.Net;

namespace SharesCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            while (true)
            {
            Console.WriteLine("1. Exit");
            Console.WriteLine("2. Sell Price for Max Loss");
            Console.WriteLine("3. Sell price for Profit");
            Console.WriteLine("4. How many shares to buy for profit");
                switch (Console.ReadKey().KeyChar)
                {
                    
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

        private static void SahresForProfitBuy()
        {
            decimal BuyPrice,SellPrice, BuyFee, SellFee, Profit;

            Console.WriteLine("Buy Price");
            BuyPrice = ReadInt();
            Console.WriteLine("Sell Fee");
            SellFee = ReadInt();
            Console.WriteLine("Buy Fee");
            BuyFee = ReadInt();
            Console.WriteLine("Profit");
            Profit = ReadInt();
            Console.WriteLine("Sell price");
            SellPrice = ReadInt();

            //((Buy*Shares) +fee+Profit)/Shares=Sell => This is the normal
            decimal Shares = (-BuyFee - Profit) / (BuyPrice-SellPrice);

            if(Shares < 1)
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
            Shares = ReadInt();
            Console.WriteLine("Buy Price");
            BuyPrice = ReadInt();
            Console.WriteLine("Sell Fee");
            SellFee = ReadInt();
            Console.WriteLine("Buy Fee");
            BuyFee = ReadInt();
            Console.WriteLine("Profit");
            Profit = ReadInt();

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
            Shares = ReadInt();
            Console.WriteLine("Buy Price");
            BuyPrice = ReadInt();
            Console.WriteLine("Sell Fee");
            SellFee = ReadInt();
            Console.WriteLine("Buy Fee");
            BuyFee = ReadInt();
            Console.WriteLine("Max loss");
            MaxLoss = ReadInt();

            //Buy Price with Fess
            decimal Price = (BuyPrice * Shares) + BuyFee + SellFee;

            Price -= MaxLoss;

            decimal sellPrice = Math.Round((Price / Shares), 2);

            Console.WriteLine($"You have to sell it on {sellPrice}");
            Console.ReadKey();
        }



        private static decimal ReadInt()
        {
            while (true)
            {
                try
                {
                    return decimal.Parse(Console.ReadLine()??"w");
                }
                catch (Exception e)
                {

                }
            }
        }
    }
}
