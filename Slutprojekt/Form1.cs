using System.Windows.Forms;

namespace Slutprojekt
{
    public partial class Form1 : Form
    {
        DeckOfCards deck = new DeckOfCards(8);
        List<Card> playerHand;
        List<Card> dealerHand;

        public Form1()
        {
            InitializeComponent();
        }

        private void RestartGame()
        {
            playerHand = new List<Card>();
            dealerHand = new List<Card>();

            playerHand = new List<Card>
            { //Ger spelaren två kort
                deck.DrawCard(),
                deck.DrawCard()
            };

            dealerHand = new List<Card>
            { //Ger Dealern 1 kort
                deck.DrawCard()
            };

            deck.DisplayHands(playerHand, dealerHand); //Visar båda händer
            deck.UpdateScoreAndReturnValue(playerHand, dealerHand);

            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = false;
            pictureBox8.Image = Slutprojekt.Properties.Resources.back;
            pictureBox9.Visible = false;
            pictureBox10.Visible = false;
            pictureBox11.Visible = false;
            pictureBox12.Visible = false;

            button2.Enabled = true;
            button1.Enabled = true;
        }

        private void label1_Click(object sender, EventArgs e) { }

        private void Form1_Load(object sender, EventArgs e)
        {
            playerHand = new List<Card>
            { //Ger spelaren två kort
                deck.DrawCard(),
                deck.DrawCard()
            };

            dealerHand = new List<Card>
            { //Ger Dealern 1 kort
                deck.DrawCard()
            };

            deck.DisplayHands(playerHand, dealerHand); //Visar båda händer
            deck.UpdateScoreAndReturnValue(playerHand, dealerHand);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            playerHand.Add(deck.DrawCard());
            deck.DisplayHands(playerHand, dealerHand);
            int playerTotal = deck.UpdateScoreAndReturnValue(playerHand, dealerHand)[0];

            if (playerTotal > 21)
            {
                MessageBox.Show("Du bustade! Dealern vann!");
                RestartGame();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            int dealerTotal = deck.UpdateScoreAndReturnValue(playerHand, dealerHand)[1];
            int playerTotal = deck.UpdateScoreAndReturnValue(playerHand, dealerHand)[0];

            while (dealerTotal < 17)
            {
                dealerHand.Add(deck.DrawCard());
                dealerTotal = deck.UpdateScoreAndReturnValue(playerHand, dealerHand)[1];
                deck.DisplayHands(playerHand, dealerHand);
                if (dealerTotal > 21)
                {
                    MessageBox.Show("Dealern bustade! Du vann!");
                }
            }

            if (playerTotal == dealerTotal && playerTotal <= 21)
            {
                MessageBox.Show($"Det blev lika {playerTotal}, ingen vann!");
            }
            else if (dealerTotal > playerTotal && dealerTotal <= 21)
            {
                MessageBox.Show($"Dealern vann med {dealerTotal}!");
            }
            else if (playerTotal > dealerTotal)
            {
                MessageBox.Show($"Du vann med {playerTotal}!");
            }
            RestartGame();
        }

        private void pictureBox6_Click(object sender, EventArgs e) { }

        private void pictureBox7_Click(object sender, EventArgs e) { }

        private void pictureBox3_Click(object sender, EventArgs e) { }
    }
}
