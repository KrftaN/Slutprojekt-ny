using System.Windows.Forms;

namespace Slutprojekt
{
    public partial class Form1 : Form
    {
        DeckOfCards deck = new DeckOfCards(6);
        List<Card> playerHand;
        List<Card> dealerHand;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            deck = new DeckOfCards(8); //Skapar en "blackjack shoe" med 8 kortlekar

            playerHand = new List<Card>{ //Ger spelaren två kort
                deck.DrawCard(),
                deck.DrawCard()
            };

            dealerHand = new List<Card>{ //Ger Dealern 1 kort
                deck.DrawCard()
            };

            deck.DisplayHands(playerHand, dealerHand); //Visar båda händer
            deck.UpdateScoreAndReturnValue(playerHand, dealerHand);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            playerHand.Add(deck.DrawCard());
            deck.DisplayHands(playerHand, dealerHand);
            deck.UpdateScoreAndReturnValue(playerHand, dealerHand);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            int dealerTotal = deck.UpdateScoreAndReturnValue(playerHand, dealerHand)[1];

            while (dealerTotal < 17)
            {
                dealerHand.Add(deck.DrawCard());
                dealerTotal = deck.UpdateScoreAndReturnValue(playerHand, dealerHand)[1];
                deck.DisplayHands(playerHand, dealerHand);
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }
    }
}
