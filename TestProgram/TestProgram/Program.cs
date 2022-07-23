using System;
using System.Linq;
using System.Xml;
using Domain;
using Domain.Models;
using ValueObjects;

namespace TestProgram
{
    public static class TestClass
    {
        public static void Main()
        {
            new TradeProcessor().ProcessTrades(@"C:\Users\kasozi\Desktop\SRP\Domain\data.txt");

        }
    }
}
