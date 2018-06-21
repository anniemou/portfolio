using System;
using System.Collections.Generic;
using DeckOfCards;
using Players;

namespace BlackJack
{
    class Program
    {
        // StartDeal begins the first draws of game.
        // static void StartDeal(){
        //     Player Javier = new Player("Javier");
        //     Dealer D = new Dealer();
        //     Deck NewDeck = new Deck();
        //     NewDeck.Deal();
        //     Javier.Hit(NewDeck);
        //     D.Hit(NewDeck);
        //     Javier.Hit(NewDeck);
        //     D.Hit(NewDeck);
        //     D.Showupcard();
        //     if(Javier.handvalue == 21){
        //         Javier.money += Javier.bet * 2;
        //         Console.WriteLine("BLACKJACK! WINNER WINNER CHICKEN DINNER!");
        //     }
        // }
        static void Main(string[] args)
        {
            Player Javier = new Player("Javier");
            Dealer D = new Dealer();
            int TotalDecks = 2;
            Deck NewDeck = new Deck(TotalDecks);
            NewDeck.Deal();
            // Javier.Hit(NewDeck);
            // D.Hit(NewDeck);
            // Javier.Hit(NewDeck);
            // D.Hit(NewDeck);
            // D.Showupcard();
            // if(Javier.handvalue == 21){
            //     Javier.money += Javier.bet * 2;
            //     Console.WriteLine("BLACKJACK! WINNER WINNER CHICKEN DINNER!");
            // }
            // Javier.Hit(NewDeck);
            // Javier.Hit(NewDeck);
            // Javier.Hit(NewDeck);
            // Javier.Hit(NewDeck);
            // Javier.Hit(NewDeck);

            // Class Constructor will not tansfer over. Need to user database.

            // Console.WriteLine(p1.hand);
            // p1.Draw(NewDeck);
            // p1.Discard();
            // foreach (var card in p1.hand){
            //     Console.WriteLine("p1 hand: " + card.stringVal + card.suit);
            // }
            int newcount = 0;
            foreach (var card in NewDeck.cards){
                newcount++;
                Console.WriteLine(card.stringVal + card.suit);
            }
            // Console.WriteLine(newcount);
        }
    }
}
