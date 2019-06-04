using System;
using System.Collections.Generic;
using System.Text;

namespace GameCardLib
{
    public class Hand
    {
        private List<Card> cardsOnHand;

        /// <summary>
        /// constructs the hand with a list of cards.
        /// zero cards in the beginning.
        /// </summary>
        public Hand()
        {
            cardsOnHand = new List<Card>();
        }

        /// <summary>
        /// returns the last card in the list.
        /// </summary>
        public Card LastCard { get => cardsOnHand[cardsOnHand.Count -1]; }

        /// <summary>
        /// adds a card to the list.
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(Card card)
        {
            cardsOnHand.Add(card);
        }

        /// <summary>
        /// clears the list of cards.
        /// </summary>
        public void ClearHand()
        {
            cardsOnHand.Clear();
        }
           
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public List<Card> OnHand()
        //{
        //    return cardsOnHand;
        //}

        /// <summary>
        /// returns a string with all cards from the list.
        /// </summary>
        /// <returns></returns>
        public string HandsToString()
        {
            string s = "";
            for (int i = 0; i < cardsOnHand.Count; i++)
            {
                s += cardsOnHand[i].CardsToString() + "\n";
            }
            return s;
        }

        /// <summary>
        /// returns a value from all cards added together 
        /// </summary>
        /// <returns></returns>
        public int HandValue()
        {
            int count = 0;
            for (int i = 0; i < cardsOnHand.Count; i++)
            {
                if((int)cardsOnHand[i].CardValueInt() == 1 && count<= 10)
                {
                    count += 11;
                }
                else if((int)cardsOnHand[i].CardValueInt() == 1 && count >= 11)
                {
                count += 1;
                }
                else {

                count += (int)cardsOnHand[i].CardValueInt();
                }
            }
            return count;
        }
    }
}

