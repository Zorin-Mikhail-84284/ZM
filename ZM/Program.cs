using System;
using System.Collections.Generic;
using System.Linq;

namespace ZM
{
    class Program
    {
        static void Main(string[] args)
        {
            FortuneMaker ticketGenerator = new FortuneMaker();

            do
            {
                ticketGenerator.GetTicket();
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }

    public class FortuneMaker
    {
        const Int32 MaxNumberValue = 52;
        const Int32 MaxPositionsInTicket = 8;
        private List<Int32> Numbers { get; set; } = new List<int>();
        private List<HashSet<Int32>> PrevTickets = new List<HashSet<int>>();

        private static Random Rnd { get { return new Random((Int32)(DateTime.Now.Ticks % 1_000_000_000));  } }

        public FortuneMaker()
        {
            int currentNumber = 1;
            do
            {
                Numbers.Add(currentNumber++);
            } while (currentNumber <= MaxNumberValue);            
        }

        private void PrintIntList(List<Int32> list, String remarks = "") => Console.WriteLine($"{remarks} {String.Join(", ", list.Select(x => $"{x: 00}"))}");
                     
        
        private List<Int32> Shuffle(List<Int32> initSet)
        {
            List<Int32> oldSet = new List<Int32>(initSet);
            List<Int32> newSet = new List<Int32>(initSet);

            Int32 firstNotShufledItemIndex = FirstNotShufledItemIndex(oldSet, newSet);
            while (firstNotShufledItemIndex >= 0)
            {
                Int32 firstNotShufledItem = newSet[firstNotShufledItemIndex];
                Int32 newPosition = Rnd.Next(0, newSet.Count);
                newSet[firstNotShufledItemIndex] = newSet[newPosition];
                newSet[newPosition] = firstNotShufledItem;
                firstNotShufledItemIndex = FirstNotShufledItemIndex(oldSet, newSet);  
            }

            return newSet;

            Int32 FirstNotShufledItemIndex(List<Int32> oldSet, List<Int32> newSet)
            {
                if (oldSet.Count == 0 || oldSet.Count != newSet.Count) throw new ArgumentException();
                for (Int32 i = 0; i < oldSet.Count; i++) if (oldSet[i] == newSet[i]) return i;
                return -1;
                //
                //
            }
        }

        

        public List<Int32> GetTicket()
        {
            List<Int32> shufledNumbers = Shuffle(Numbers);
            List<HashSet<Int32>> tickets = new List<HashSet<int>>();
            
            //HashSet<Int32>

            while(shufledNumbers.Count > 0)
            {

            }

            
            HashSet<Int32> indexes = new HashSet<int>(MaxPositionsInTicket);


            while (indexes.Count < MaxPositionsInTicket) indexes.Add(Rnd.Next(0, MaxNumberValue));
            PrintIntList(indexes.ToList(), "indexes^ ");
            List<Int32> ticket = new List<Int32>(MaxPositionsInTicket);
            //
            foreach (Int32 index in indexes) ticket.Add(Numbers[index]);
            ticket = ticket.OrderBy(x => x).ToList();
            //
            //PrintIntList(ticket);
            return ticket;
        }
    }
}
