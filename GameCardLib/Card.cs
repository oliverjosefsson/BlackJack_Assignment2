using System;
using System.Collections.Generic;
using System.Text;

namespace GameCardLib
{
    public class Card
    {
        private EnumSuit _suite;
        private EnumValue _cardValue;

        /// <summary>
        /// construct of a card, each card has a suit and value as enums
        /// </summary>
        /// <param name="suite"></param>
        /// <param name="cardValue"></param>
        public Card(EnumSuit suite, EnumValue cardValue) 
        {
            this._suite = suite;
            this._cardValue = cardValue;
        }
        /// <summary>
        /// returns the cards value as enum
        /// </summary>
        public EnumValue CardValue { get { return _cardValue; } }

        /// <summary>
        /// returns the cards suite as enum
        /// </summary>
        public EnumSuit Suite { get { return _suite; } }

        /// <summary>
        /// returns the cards value as int.
        /// </summary>
        /// <returns></returns>
        public int CardValueInt()
        {
            if((int)_cardValue > 10)
            {
                return 10;
            } else 
            {
            return (int)_cardValue;
            }
        }

        /// <summary>
        /// returns the card as a string
        /// </summary>
        /// <returns></returns>
        public string CardsToString()
        {
            return this._cardValue + " of " + this._suite;
        }

    }
}
