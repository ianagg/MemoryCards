using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCards
{
    class LogicMemory
    {
        IPlayable play;
        static Random random = new Random();
        //number of cards on the field
        static int numberOfCards = 16;

        int[] cards = new int[numberOfCards];
        bool[] opens = new bool[numberOfCards];

        int image_a;
        int image_b;

        enum Status { INIT, IMG_OPEN, WRONG_ANS };
        Status status = new Status();

        int done;

        /////////////// GAME FLOW
        public LogicMemory(IPlayable play)
        {
            this.play = play;
        }

        // Initiation of the game parameters
        public void CreateNewGame()
        {
            done = 0;
            int length = cards.Length;
            for (int i = 0; i < length; i++)
            {
                cards[i] = i % (length / 2) + 1;
                opens[i] = false;
            }

            //randomazing the images positions
            for (int i = 0; i < 100; i++)
            {
                ShuffleCards();
            }

            //hiding cards behind the default images
            for (int j = 0; j < length; j++)
            {
                play.HideCard(j);
            }

            status = Status.INIT;

        }

        public void ClickOnImage(int id)
        {
            if (opens[id]) return;
            switch (status)
            {
                case Status.INIT:
                    StartSequence(id);
                    break;
                case Status.IMG_OPEN:
                    CheckIfEquals(id);
                    break;
                case Status.WRONG_ANS:
                    WrongAnswer(id);
                    break;

            }

        }

        //randomazing the images positions
        //by switching their places
        private void ShuffleCards()
        {
            int a = random.Next(0, cards.Length);
            int b = random.Next(0, cards.Length);

            if (a == b) return;
            int temp;
            temp = cards[a];
            cards[a] = cards[b];
            cards[b] = temp;

        }

        //opens the image forever
        private void Open(int id)
        {
            opens[id] = true;
            play.ShowCard(id, cards[id]);
        }


        //if the click opens the first image of the pair
        private void StartSequence(int id)
        {
            image_a = id;
            status = Status.IMG_OPEN;
            play.ShowCard(image_a, cards[image_a]);
        }

        //if the click opens the second image of the pair
        //checking if the images in the opened pair are equal
        private void CheckIfEquals(int id)
        {
            image_b = id;
            //cannot be the same card 
            if (image_a == image_b)
                return;

            play.ShowCard(image_b, cards[image_b]);

            if (cards[image_b] == cards[image_a])
            {
                Open(image_a);
                Open(image_b);
                done += 2;
                if (done == numberOfCards)
                    play.Win();
                else
                    status = Status.INIT;
            }
            else
            {
                status = Status.WRONG_ANS;
            }

        }

        private void WrongAnswer(int id)
        {
            play.HideCard(image_a);
            play.HideCard(image_b);
            image_a = id;
            StartSequence(id);

        }
    }
}
