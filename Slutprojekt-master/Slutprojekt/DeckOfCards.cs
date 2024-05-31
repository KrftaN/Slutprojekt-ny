using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slutprojekt
{
    internal class DeckOfCards : Form
    {
        // Listan som håller korten
        protected List<Card> cards;
        static Form1 form = Application.OpenForms.OfType<Form1>().FirstOrDefault(); // Ger mig tillgång till alla kontrollers från form1 ifrån denna klass
        public DeckOfCards(int numberOfDecks) // Konstruktor som skapar och initialiserar en kortlek med det angivna antalet kortlek

        {
            InitializeDeck(numberOfDecks);
        }

        // Metod för att skapa och blanda kortleken
        protected void InitializeDeck(int numberOfDecks)
        {
            cards = new List<Card>();

            // Loopar igenom antalet kortlekar
            for (int i = 0; i < numberOfDecks; i++)
            {
                // Loopar igenom alla färger
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    // Loopar igenom alla valörer
                    foreach (Value value in Enum.GetValues(typeof(Value)))
                    {
                        // Skapar ett kort och lägger till det i kortleken
                        cards.Add(new Card(suit, value));
                    }
                }
            }

            // Blandar kortleken
            Shuffle();
        }

        // Metod för att blanda kortleken
        public void Shuffle()
        {
            // Skapar en slumpgenerator
            Random random = new Random();
            // Använder LINQ för att blanda korten
            cards = cards.OrderBy(c => random.Next()).ToList();
        }

        // Metod för att dra ett kort från kortleken
        public Card DrawCard()
        {
            // Kastar ett undantag om det inte finns fler kort i leken
            if (cards.Count == 0)
            {
                throw new InvalidOperationException("Inga fler kort i leken.");
            }

            // Hämtar det översta kortet i leken, dvs första elementet i cards arrayn 
            Card drawnCard = cards[0];
            // Tar bort det dragna kortet från leken
            cards.RemoveAt(0);

            // Returnerar det dragna kortet
            return drawnCard;
        }

        public int CalculateHandValue(List<Card> hand)
        {
            int totalValue = 0;
            int numberOfAces = 0;

            foreach (Card card in hand)
            {
                int cardValue = card.GetValue();
                totalValue += cardValue;

                // Kontrollera för Ess och justera det totala värdet därefter
                if (cardValue == 11)
                {
                    numberOfAces++;
                }
            }

            // Justera för Ess om det behövs
            while (totalValue > 21 && numberOfAces > 0)
            {
                totalValue -= 10;
                numberOfAces--;
            }

            return totalValue;
        }

        public void DisplayHands(List<Card> playerHand, List<Card> dealerHand)
        {
            int i = 1;

            foreach (Card card in playerHand)
            {
                Image cardImage = GetCardImage(card); // Fetch the correct image for the card
                SetPictureBoxImage(i, cardImage); // Set the image for the PictureBox
                i++;
            }

            i = 7; // Eftersom att dealerns pictureboxes är mellan 6-10 börjar i på 6

            foreach (Card card in dealerHand)
            {
                Image cardImage = GetCardImage(card); // Fetch the correct image for the card
                SetPictureBoxImage(i, cardImage); // Set the image for the PictureBox
                i++;
            }
        }

        public int[] UpdateScoreAndReturnValue(List<Card> playerHand, List<Card> dealerHand)
        {
            int playerTotal = CalculateHandValue(playerHand);
            int dealerTotal = CalculateHandValue(dealerHand);

            form.label2.Text = $"Total: {playerTotal}";
            form.label3.Text = $"Total: {dealerTotal}";

            return [playerTotal, dealerTotal]; //Bara ett sätt att ge 
        }

        private Image GetCardImage(Card card)
        {
            // Får stringen genom att använda min ToString metod i Card klassen
            string resourceName = card.ToString();

            // Tar bilden från resurserna
            Image image = (Image)Slutprojekt.Properties.Resources.ResourceManager.GetObject(resourceName);

            // Returnerar bilden
            return image;
        }

        private void SetPictureBoxImage(int index, Image image)
        {
            string pictureBoxName = "pictureBox" + index;
            PictureBox pictureBox = form.Controls.Find(pictureBoxName, true).FirstOrDefault() as PictureBox; //Tar picturboxen
            if (pictureBox != null)
            {
                pictureBox.Visible = true;
                pictureBox.Image = image;  //Sätter pictureboxen som rätt bild
            }
        }
    }
}
