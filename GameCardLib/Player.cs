using System;

namespace GameCardLib
{
    public class Player : Hand
    {
        private bool _isFinished;
        private string _name;
        private int _id;
        private Hand _hand;
        private bool _hasBlackJack;
        private bool _isThick;
        
        /// <summary>
        /// constructs a player with id and name.
        /// </summary>
        /// <param name="playerID"></param>
        /// <param name="name"></param>
        public Player(int playerID, string name)
        {
            _id = playerID;
            _name = name;
            _hand = new Hand();
            _isFinished = false;
            _isThick = false;
            _hasBlackJack = false;
        }
        /// <summary>
        /// set and getter for bool value hasblackjack. false from start
        /// </summary>
        public bool HasBlackJack { get => _hasBlackJack; set => _hasBlackJack = value; }

        /// <summary>
        /// set and getter for bool value isfinnished. false from start
        /// </summary>
        public bool IsFinished { get => _isFinished; set => _isFinished = value; }

        /// <summary>
        /// set and getter for bool value isthick. false from start
        /// e.g value of cards is over 21 -> isthick = true.
        /// </summary>
        public bool IsThick { get => _isThick; set => _isThick = value; }

        /// <summary>
        /// returns the hand of the player
        /// </summary>
        public Hand Hand { get => _hand; set => _hand = value; }

        /// <summary>
        /// returns the name of the player
        /// </summary>
        public string Name { get => _name; set => _name = value; }

        /// <summary>
        /// returns the players id.
        /// </summary>
        public int PlayerID { get => _id; set => _id = value; }

        /// <summary>
        /// sets all player values to false. used in new deal/new game
        /// </summary>
        public void Reset()
        {
            _isFinished = false;
            _isThick = false;
            _hasBlackJack = false;
        }

  
    }
}
