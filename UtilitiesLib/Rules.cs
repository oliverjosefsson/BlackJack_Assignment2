using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameCardLib;

namespace UtilitiesLib
{
    class Rules 
    {
        private readonly int blackJack = 21;

        /// <summary>
        /// rules of blackjack, checks the recieved players hand, 
        /// sets values in player if bools reached.
        /// </summary>
        /// <param name="player"></param>
        public void CheckHand(Player player)
        {
            if (player.HandValue() <= blackJack && player.HandValue() >= 0)
            {
                player.IsThick = false;
                if (player.HandValue() == blackJack)
                {
                    player.HasBlackJack = true;
                    player.IsFinished = true;
                }
            }
            else
            {
                player.IsThick = true;
                player.IsFinished = true;
            }
        }

        /// <summary>
        /// rules for the dealer in blackjack, 
        /// sets values for the dealer if bools reached.
        /// </summary>
        /// <param name="dealer"></param>
        public void dealerHand(Player dealer)
        {
            if (dealer.HandValue() < 17)
            {
                return;
            }
            else if (dealer.HandValue() >= 17 && dealer.HandValue() <= 21)
            {
                dealer.IsFinished = true;
                if (dealer.HandValue() == 21)
                {
                    dealer.HasBlackJack = true;
                }
            }
            else
            {
                dealer.IsThick = true;
                dealer.IsFinished = true;
            }
        }

        public string Results(Player player, Player dealer)
        {
            if (!dealer.IsThick && !player.IsThick && !dealer.HasBlackJack && !player.HasBlackJack)
            {
                if(dealer.HandValue() > player.HandValue())
                {
                    return player.Name + " - loses, dealer scores higher!";
                } else if( dealer.HandValue() == player.HandValue())
                {
                    return player.Name + " - draw, same score as dealer";
                }
                else
                {
                    return player.Name + " - won, higer score than dealer!";
                }
            }
            else if (dealer.IsThick && !player.IsThick)
            {
                if (player.HasBlackJack)
                {
                    return player.PlayerID + " - won with blackjack!";
                } else if(!player.HasBlackJack)
                {
                    return player.Name + " - won, dealer busted!";
                }
            }
            else if(player.IsThick && !dealer.IsThick)
            {
                if(dealer.HasBlackJack)
                {
                    return player.Name + " - busted, dealer has blackjack!";
                } 
                else
                {
                    return player.Name + " - busted, dealer wins!";
                }
            }
            else if(!player.IsThick && !dealer.IsThick)
            {
                if(player.HasBlackJack && !dealer.HasBlackJack)
                {
                    return player.Name + " - won with blackjack!";
                }
                else if(player.HasBlackJack && dealer.HasBlackJack)
                {
                    return player.Name + " - draw, both have blackjack!";
                }
                {
                    return player.Name + " - loses, dealer has blackjack!";
                }
            }
            else if (dealer.HasBlackJack)
            {
                if (player.HasBlackJack)
                {
                    return player.Name + " - draw, both dealer and player has blackjack!";
                } 
                else if(!player.HasBlackJack)
                {
                    return player.Name + " - loses, dealer has blackjack!";
                }
            }
            else
            {
                return player.Name + " - loses, busted before dealer!";
            }
            return "Something went wrong with " + player.Name + ", playervalue: "  + player.HandValue() + " dealervalue: " + dealer.HandValue();
        }
    }
}
