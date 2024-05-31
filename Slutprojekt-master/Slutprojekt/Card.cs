using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slutprojekt
{
    public enum Suit
    {
        H,
        D,
        C,
        S
    }

    // Enum för olika valörer på korten
    public enum Value
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }
    internal class Card : DeckOfCards
    {
        // Kortets färg och valör
        public Suit Suit { get; }
        public Value Value { get; }

        // Konstruktor som skapar ett kort och kallar på basklassens konstruktor
        public Card(Suit suit, Value value) : base(0)
        {
            Suit = suit;
            Value = value;
        }

        // Överskuggar ToString-metoden för att skriva ut kortet på ett läsbart sätt
        public override string ToString()
        {
            return $"{Value}{Suit}";
        }

        public int GetValue()
        {
            // För numrerade kort (2-10) är värdet detsamma som valören
            if (Value >= Value.Two & Value <= Value.Ten)
            {
                return (int)Value;
            }
            // För klädda kort (Knekt, Dam, Kung) är värdet 10
            else if (Value >= Value.Jack && Value <= Value.King)
            {
                return 10;
            }
            // För Ess, returnera 11 (kan ändras till 1 senare om det behövs)
            else if (Value == Value.Ace)
            {
                return 11;
            }

            return 0; // Standardvärde om något går fel
        }
    }
}
