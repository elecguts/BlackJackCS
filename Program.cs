using System;
using System.Collections.Generic;

namespace BlackJackCS
{
    class Card
    {
        public string Suit { get; set; }
        public string Rank { get; set; }
        public Card(string newSuit, string newRank) { Suit = newSuit; Rank = newRank; }
        public int CardValue()
        {
            var value = 0;
            if (Rank == "Ace")
            {
                value = 11;
            }
            else if (Rank == "Two")
            {
                value = 2;
            }
            else if (Rank == "Three")
            {
                value = 3;
            }
            else if (Rank == "Four")
            {
                value = 4;
            }
            else if (Rank == "Five")
            {
                value = 5;
            }
            else if (Rank == "Six")
            {
                value = 6;
            }
            else if (Rank == "Seven")
            {
                value = 7;
            }
            else if (Rank == "Eight")
            {
                value = 8;
            }
            else if (Rank == "Nine")
            {
                value = 9;
            }
            else { value = 10; }
            return value;
        }
        public string CardDisplay()
        {
            var cardString = $"{Rank} of {Suit}";
            return cardString;
        }
    }
    class Hand
    {
        public List<Card> Cards = new List<Card>();
        public int HandValue()
        {
            var value = 0;
            foreach (var card in Cards)
            {
                value = value + card.CardValue();
            }
            return value;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Creates two lists of suits and ranks
            var suits = new List<string>() { "Clubs", "Diamonds", "Hearts", "Spades" };
            var ranks = new List<string>() { "Ace", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King" };
            //Creates an empty list to store the completed deck of cards
            var fullDeck = new List<Card>();
            var player1Hand = new Hand();
            var dealerHand = new Hand();

            //Uses nested loops to combine and store the strings
            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    fullDeck.Add(new Card(suit, rank));
                }

            }

            //This code implements the Fisher-Yates shuffling algorithm
            for (var rightIndex = fullDeck.Count - 1; rightIndex > 0; rightIndex--)
            {
                var randomNumberGenerator = new Random();
                var randomNumber = randomNumberGenerator.Next(rightIndex);
                var leftIndex = randomNumber;
                var leftCard = fullDeck[leftIndex];
                var rightCard = fullDeck[rightIndex];
                fullDeck[leftIndex] = rightCard;
                fullDeck[rightIndex] = leftCard;
            }
            //foreach (var card in fullDeck)
            //{
            //    Console.WriteLine($"{card.Rank} of {card.Suit} is worth {card.CardValue()} points");
            //}
            Console.WriteLine("Press any key to start the deal.");
            Console.ReadLine();
            // Deals first the dealer, then the player, two cards
            dealerHand.Cards.Add(fullDeck[0]);
            fullDeck.RemoveAt(0);
            dealerHand.Cards.Add(fullDeck[0]);
            fullDeck.RemoveAt(0);
            Console.WriteLine($"Dealer drew two cards.");

            player1Hand.Cards.Add(fullDeck[0]);
            fullDeck.RemoveAt(0);
            player1Hand.Cards.Add(fullDeck[0]);
            fullDeck.RemoveAt(0);
            foreach (var card in player1Hand.Cards)
            {
                Console.WriteLine($"You drew the {card.CardDisplay()}");
            }
            Console.WriteLine($"Your hand is worth {player1Hand.HandValue()}");

            //loop for player choices
            var keepGoing = true;
            while (keepGoing)
            {
                Console.WriteLine("Would you like to (s)tay or (h)it?");
                var stayOrHit = Console.ReadLine();
                if (stayOrHit == "h")
                {
                    player1Hand.Cards.Add(fullDeck[0]);
                    Console.WriteLine($"You drew the {fullDeck[0].CardDisplay()}");
                    fullDeck.RemoveAt(0);
                    Console.WriteLine($"Your hand is now worth {player1Hand.HandValue()}.");
                    if (player1Hand.HandValue() > 21)
                    { keepGoing = false; }
                }
                else if (stayOrHit == "s")
                {
                    keepGoing = false;
                }
                else
                {
                    Console.WriteLine("Please choose to stay or hit.");
                }
            }
            //check for bust
            var playerBust = false;
            var dealerBust = false;
            if (player1Hand.HandValue() > 21)
            {
                Console.WriteLine("You busted!");
                playerBust = true;
            }

            //loop for dealer choices
            else
            {
                if (dealerHand.HandValue() > 21)
                {
                    Console.WriteLine($"The dealer has {dealerHand.HandValue()} points and busted!");
                    dealerBust = true;
                }
                else if (dealerHand.HandValue() < player1Hand.HandValue())
                {
                    while (dealerHand.HandValue() < 17)
                    {
                        Console.WriteLine($"The dealer's hand is at {dealerHand.HandValue()} points.");
                        Console.WriteLine($"The dealer draws the {fullDeck[0].CardDisplay()}");
                        dealerHand.Cards.Add(fullDeck[0]);
                        fullDeck.RemoveAt(0);
                    }
                }
            }
            Console.WriteLine($"The dealer's hand is at {dealerHand.HandValue()} points.");

            //compare totals and check for winner
            if (playerBust)
            {
                Console.WriteLine($"Your hand had {player1Hand.HandValue()} points and is a bust. You lose!");
            }
            else if (dealerBust)
            {
                Console.WriteLine($"The dealer hand had {dealerHand.HandValue()} points and was a bust. You win!");
            }
            else if (player1Hand.HandValue() > dealerHand.HandValue())
            {
                Console.WriteLine("Your hand beat the dealer's hand. You win!");
            }
            else if (player1Hand.HandValue() <= dealerHand.HandValue())
            {
                Console.WriteLine("The dealer hand beats yours. You lose!");
            }
        }
    }
}
