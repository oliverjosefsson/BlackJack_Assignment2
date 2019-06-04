using GameCardLib;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using UtilitiesLib;
using System.Linq;

namespace Blackjack
{
    public partial class Form1 : Form
    {
        private GameManager gameManager;
        private Player currentPlayer;
        private Player dealer;



        public Form1()
        {
            InitializeComponent();
            InitializeComponents();
            
        }

  
        /// <summary>
        /// buttons to be false when game starts up.
        /// </summary>
        private void InitializeComponents()
        {
            btn_dealNewRound.Visible = false;
            btn_player1_hit.Visible = false;
            btn_playerStand.Visible = false;
            btn_shuffle.Visible = false;
            lbl_cardsLeft.Visible = false;
            lbl_dealerCards.Visible = false;
            lbl_playerCards.Visible = false;
            lbl_valueOfCards_dealer.Visible = false;
            lbl_valueOfCards_player.Visible = false;
            lbl_staticCardLeft.Visible = false;
            lbl_playerShowValue.Visible = false;
            lbl_dealerShowValue.Visible = false;
        }

        private void showComponents()
        {
            btn_shuffle.Visible = true;
            lbl_cardsLeft.Visible = true;
            lbl_dealerCards.Visible = true;
            lbl_playerCards.Visible = true;
            lbl_valueOfCards_dealer.Visible = true;
            lbl_valueOfCards_player.Visible = true;
            lbl_staticCardLeft.Visible = true;
          //  lbl_playerShowValue.Visible = true;
        }
       
        /// <summary>
        /// when pressed, the deck is shuffled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_shuffle_Click(object sender, EventArgs e)
        {
            ShuffleDeckHandler shuffleDeck = new ShuffleDeckHandler(gameManager.ShuffleDeck);
            shuffleDeck();

            checkCardsLeft();
        }

        /// <summary>
        /// recieves the next player 
        /// also checks if all players are finnished -> dealers turn.
        /// </summary>
        private void GetNextPlayer()
        {
            if (gameManager.AllPlayersIsFinnished())
            {
                MessageBox.Show("Dealer will show cards");
                DealerHandfinnish();
            }

            NextPlayerHandler nextplayer = new NextPlayerHandler(gameManager.NextPlayer);
            currentPlayer = nextplayer(currentPlayer);

            if (currentPlayer != null)
            {
                updatePlayer();
                checkPlayerScore();
                checkCardsLeft();
            }
            else
            {
                btn_dealNewRound.Visible = true;
                btn_player1_hit.Visible = false;
                btn_playerStand.Visible = false;
            }
        }

        /// <summary>
        /// method to check the number of cards left in the deck.
        /// user gets asked to shuffle if there is 15 cards or less
        /// </summary>
        private void checkCardsLeft()
        {
            lbl_cardsLeft.Text = gameManager.cardsLeftInDeck().ToString();
            if (gameManager.cardsLeftInDeck() < 30)
            {
                lbl_cardsLeft.ForeColor = System.Drawing.Color.Red;
                if (gameManager.cardsLeftInDeck() < 15)
                {
                    lbl_cardsLeft.ForeColor = System.Drawing.Color.Red;
                    DialogResult dr = MessageBox.Show(gameManager.cardsLeftInDeck().ToString() + " cards left in deck, do you want to shuffle?", "Warning!", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        ShuffleDeckHandler shuffleDeck = new ShuffleDeckHandler(gameManager.ShuffleDeck);
                        shuffleDeck();
                        checkCardsLeft();
                    }
                }
            } else
            {
            lbl_cardsLeft.ForeColor = new System.Drawing.Color();
            }
        }

        /// <summary>
        /// stand button, if pressed, player status is finnished, and next player will be available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_playerStand_Click(object sender, EventArgs e)
        {
            currentPlayer.IsFinished = true;
            GetNextPlayer();
        }
        
        /// <summary>
        /// checks the players bools.
        /// </summary>
        private void checkPlayerScore()
        {
            Thread.Sleep(1000);
            if (currentPlayer.HasBlackJack)
            {
                lbl_playerShowValue.Visible = true;
                lbl_playerShowValue.Text = "BLACKJACK!!!";
                MessageBox.Show("BlackJack!!!! \nPress ok for next player");
                GetNextPlayer(); 
            }
            else if(currentPlayer.IsThick)
            {
                lbl_playerShowValue.Visible = true;
                lbl_playerShowValue.Text = "BUSTED...!";
                MessageBox.Show("Busted..\nPress ok for next player");
                GetNextPlayer();
            }
            else
            {
                lbl_playerShowValue.Visible = true;
                lbl_playerShowValue.Text = currentPlayer.HandValue().ToString();
            }
        }

        /// <summary>
        /// updates the player-labels.
        /// </summary>
        private void updatePlayer()
        {
            lbl_playerName.Text = currentPlayer.Name;
            lbl_playerCards.Text = currentPlayer.HandsToString();
            lbl_valueOfCards_player.Text = currentPlayer.HandValue().ToString();
        }

        /// <summary>
        /// hit button, if pressed, player recieves another card.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_player_hit_Click(object sender, EventArgs e)
        {
            PlayerHitHandler playerHit = new PlayerHitHandler(gameManager.PlayerHits);
            playerHit(currentPlayer);

            updatePlayer();
            checkPlayerScore();
            checkCardsLeft();
        }

        /// <summary>
        /// newgame button. when pressed the game initializes, asks for amount of players,
        /// and decks.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_newGame_Click(object sender, EventArgs e)
        {
            int[] newGame = newGameDialog("New Game");
            if(newGame != null && newGame[0] > 0 && newGame[1] > 0)
            {
                gameManager = new GameManager(newGame[0],newGame[1]);

                SendDealerHandler sendDealer = new SendDealerHandler(gameManager.sendDealer);
                dealer = sendDealer();

                ReturnFirstPlayerHandler sendFirstPlayer = new ReturnFirstPlayerHandler(gameManager.sendFirstPlayer);
                currentPlayer = sendFirstPlayer();


                btn_playerStand.Visible = true; 
                btn_player1_hit.Visible = true;

                checkCardsLeft();
                updatePlayer();
                updateDealerFirstCard();

                showComponents();
                checkPlayerScore();
            }
            else
            {
                MessageBox.Show("Please insert numbers for decks and players");
            }
        }

        /// <summary>
        /// shows the dealers first card, other is face down.
        /// </summary>
        private void updateDealerFirstCard()
        {
            lbl_dealerCards.Text = dealer.LastCard.CardsToString() + "\nsecond card face down";
            lbl_valueOfCards_dealer.Text = dealer.LastCard.CardValueInt().ToString();
        }

        /// <summary>
        /// shows the dealers first and second card,
        /// </summary>
        private void DealerHandfinnish()
        {
            lbl_dealerCards.Text = dealer.HandsToString();
            lbl_valueOfCards_dealer.Text = dealer.HandValue().ToString();
            
            while (!dealer.IsFinished)
            {
                gameManager.DealersTurn();
                lbl_dealerCards.Text = dealer.HandsToString();
                lbl_valueOfCards_dealer.Text = dealer.HandValue().ToString();
                if (dealer.IsThick)
                {
                    lbl_dealerShowValue.Visible = true;
                    lbl_dealerShowValue.Text = "Dealer Busted";
                } else if(dealer.HasBlackJack)
                {
                    lbl_dealerShowValue.Visible = true;
                    lbl_dealerShowValue.Text = "Dealer has blackjack!";
                }
                else
                {
                    lbl_dealerShowValue.Visible = true;
                    lbl_dealerShowValue.Text = dealer.HandValue().ToString();
                }
            }
            MessageBox.Show(gameManager.Results(),"Results");
            btn_dealNewRound.Visible = true;
            btn_player1_hit.Visible = false;
            btn_playerStand.Visible = false;
        }
        
        /// <summary>
        /// a new Form to show a dialog with inputs for # of decks to be used 
        /// and # of players to participate when the new game starts
        /// </summary>
        /// <param name="caption"></param>
        /// <returns></returns>
        private int[] newGameDialog( string caption)
        {
            int[] info;
            int deckCount = 0;
            int playerCount = 0; 

            var prompt = new Form { Width = 300, Height = 180,FormBorderStyle = FormBorderStyle.FixedSingle, Text = caption, StartPosition = FormStartPosition.CenterScreen,};
            var lbldecks = new Label {Left = 110, Top = 5, Text = "number of decks",};
            var tbDecks = new TextBox {Left = 100, Top = 20, Width = 100,};
            var lblplayers = new Label {Left = 105, Top = 60, Text = "number of players",};
            var tbPlayers = new TextBox {Left = 100, Top = 75, Width = 100,};
            var confirmationButton = new Button {Text = @"Start new game", DialogResult = DialogResult.OK, Dock = DockStyle.Bottom,};
   
            confirmationButton.Click += (sender, e) =>
            {
                prompt.Close();
            };

            prompt.Controls.Add(tbDecks);
            prompt.Controls.Add(tbPlayers);
            prompt.Controls.Add(confirmationButton);
            prompt.Controls.Add(lblplayers);
            prompt.Controls.Add(lbldecks);

            //checks the texboxes values, if it is parsable to int 
            if (prompt.ShowDialog() == DialogResult.OK )
            {
                if (int.TryParse(tbDecks.Text.Trim(), out deckCount) && int.TryParse(tbPlayers.Text.Trim(), out playerCount))
                {
                    return info = new int[] { deckCount, playerCount };
                }
            }
            return null;
        }

        /// <summary>
        /// btn new round method, resets the players and dealer, and deals  new cards to each. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_dealNewRound_Click(object sender, EventArgs e)
        {
            lbl_dealerShowValue.Visible = false;
            checkCardsLeft();
            gameManager.startNewDeal();
            currentPlayer = gameManager.sendFirstPlayer();
            updatePlayer();
            updateDealerFirstCard();
            btn_dealNewRound.Visible = false;
            btn_player1_hit.Visible = true;
            btn_playerStand.Visible = true;
            checkPlayerScore();
        }
    }
}
