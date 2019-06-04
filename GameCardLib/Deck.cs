using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameCardLib
{
    public class Deck
    {
        private int _numberOfDecks;
        private int _numberOfCards;

        private List<int> cardAsInt;
        private Queue<Card> deckOfCards;
        private Random rand = new Random();


        /// <summary>
        /// Construct with number of decks from parameter
        /// </summary>
        /// <param name="numberOfDecks"></param>
        public Deck(int numberOfDecks)
        {
            if(numberOfDecks > 1 && numberOfDecks <= 6){
                Console.WriteLine(numberOfDecks);
                this._numberOfDecks = numberOfDecks;
                this._numberOfCards = numberOfDecks * 52;
            } else {
                Console.WriteLine(numberOfDecks);
                this._numberOfDecks = 1;
                this._numberOfCards = _numberOfDecks * 52;
            }
            Shuffle();
        }

        /// <summary>
        /// initialize the deck and shuffles it
        /// </summary>
        public void Shuffle()
        {
            deckOfCards = new Queue<Card>();
            generateCardAsInt();
            for(int i = _numberOfCards - 1; i >= 0; i--)
            {
                int index = rand.Next(0, i);
                int temp = cardAsInt[i];
                cardAsInt[i] = cardAsInt[index];
                cardAsInt[index] = temp;
            }
            fillDeck();
        }

        /// <summary>
        /// generates a int-list with the size of _numberOfCards
        /// </summary>
        private void generateCardAsInt()
        {
            cardAsInt = new List<int>();
            for (int i = 0; i < _numberOfCards; i++)
            {
                cardAsInt.Add(i);
            }
        }

        /// <summary>
        /// Fills the DeckOfCards with cards
        /// </summary>
        private void fillDeck()
        {
            for (int i = 0; i < cardAsInt.Count; i++)
            {
                EnumSuit suit = (EnumSuit)(cardAsInt[i] % 4);
                EnumValue value = (EnumValue)(cardAsInt[i] % 13 + 1);
                deckOfCards.Enqueue(new Card(suit, value));
            }
        }

        /// <summary>
        /// deals the first card in the deck
        /// if the deck is empty, reshuffles.
        /// </summary>
        /// <returns></returns>
        public Card DealCard()
        {
            if(counter() == 0)
            {
                Shuffle();
            }
            return deckOfCards.Dequeue();
        }

        /// <summary>
        /// returns the number of cards left in the deck
        /// </summary>
        /// <returns></returns>
        public int counter()
        {
            return deckOfCards.Count();
        }
    }
}
