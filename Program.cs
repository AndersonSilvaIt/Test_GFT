using Console_GFT.Entities;
using Console_GFT.Extensions;
using Console_GFT.Interfaces;
using Console_GFT.Rules;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Console_GFT
{
    internal class Program
    {
        static DateTime referenceDate;
        static int tradesNumber;

        static void Main(string[] args)
        {
            try
            {
                ReadStarInformations();

                var trades = new List<ITrade>();

                ReadTrades(ref trades);

                // First Example
                CategorizeWithInterfaceRules(trades);

                // Second Example
                //CategorizeWithOutInterfaceRules(trades);
            }
            catch (ErrorMessageException eme)
            {
                Console.WriteLine(eme.Message);
            }
            catch
            {
                Console.WriteLine("Internal Error");
            }
            Console.ReadKey();
        }

        private static void ReadStarInformations()
        {
            if (!DateTime.TryParseExact(Console.ReadLine(), new[] { "MM/dd/yyyy", "M/d/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out referenceDate))
            {
                throw new ErrorMessageException("Invalid Date");
            }

            if (!int.TryParse(Console.ReadLine(), out tradesNumber))
            {
                throw new ErrorMessageException("Invalid trades number");
            }
        }

        private static void ReadTrades(ref List<ITrade> trades)
        {
            double tradeAmount;
            string clientSector;
            DateTime nextPaymentDate;
            
            for (int i = 0; i < tradesNumber; i++)
            {
                var input = CleanSpacesAndSplit(Console.ReadLine());

                double.TryParse(input[0], out tradeAmount);
                clientSector = input[1];
                DateTime.TryParseExact(input[2], new[] { "MM/dd/yyyy", "M/d/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out nextPaymentDate);

                ValidateInputs(tradeAmount, clientSector, nextPaymentDate);

                trades.Add(new Trade(tradeAmount, clientSector, nextPaymentDate));
            }
        }

        private static void CategorizeWithInterfaceRules(List<ITrade> trades)
        {
            var rules = new List<ICategoryRule>
                {
                    new ExpiredRule(),
                    new HighRiskRule(),
                    new MediumRiskRule()
                };

            var categorizer = new TradeCategorizer(rules);

            foreach (var trade in trades)
            {
                Console.WriteLine(categorizer.Categorize(trade, referenceDate));
            }
        }

        private static void CategorizeWithOutInterfaceRules(List<ITrade> trades)
        {
            var categorizer = new TradeCategorizer();

            foreach (var trade in trades)
            {
                Console.WriteLine(categorizer.CategorizeWithoutInterface(trade, referenceDate));
            }
        }

        static string[] CleanSpacesAndSplit(string input)
        {
            var result = new StringBuilder();

            bool lastWasSpace = false;

            foreach (char c in input.Trim())
            {
                if (c.Equals(' ') && !lastWasSpace)
                {
                    result.Append(' ');
                    lastWasSpace = true;
                }
                else if (!c.Equals(' '))
                {
                    result.Append(c);
                    lastWasSpace = false;
                }
            }

            return result.ToString().Split(' ');
        }

        private static void ValidateInputs(double tradeAmount, string clientSector, DateTime paymentDate)
        {
            if (tradeAmount <= 0)
                throw new ErrorMessageException("Trade amount is invalid.");

            if (!clientSector.Equals("Public", StringComparison.InvariantCultureIgnoreCase) &&
                !clientSector.Equals("Private", StringComparison.InvariantCultureIgnoreCase))
                throw new ErrorMessageException("Invalid Client Sector");

            if (paymentDate == DateTime.MinValue)
                throw new ErrorMessageException("Invalid Payment Date");
        }
    }
}