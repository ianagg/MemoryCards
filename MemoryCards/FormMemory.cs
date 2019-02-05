using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryCards
{
    public partial class FormMemory : Form
    {
        static Random random = new Random();
        int[] cards = new int[16];
        bool[] opens = new bool[16];

        int image_a;
        int image_b;

        enum Status {INIT, FIRST_IMG_OPEN, SECOND_IMG_OPEN, WRONG_ANS};
        Status status = new Status();

        int done;

        public FormMemory()
        {
            InitializeComponent();
            init_game();
        }

         private void FormMemory_Load(object sender, EventArgs e)
         {

         }

        
       ///////// MENU BAR CLICKS
       //exit button closses the game
        private void menu_game_exit_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        //about button shown the info about the author
        private void menu_help_about_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"Author: Iana Gureva", "About");

        }
        
        //starts the new game
        private void menu_game_newGame_Click(object sender, EventArgs e)
        {
            init_game();
        }


        /////////////// GAME FLOW
        // Initiation of the game parameters
        private void init_game()
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
                shuffle_cards();
            }

            //loading the default images
            for (int j = 0; j < length; j++)
            {
                load_image(j, 0);
                hide(j);
            }

            status = Status.INIT;

        }   

        private void shuffle_cards()
        {
            int a = random.Next(0, cards.Length);
            int b = random.Next(0, cards.Length);

            if (a == b) return;
            int temp;
            temp = cards[a];
            cards[a] = cards[b];
            cards[b] = temp;

        }

        private void load_image(int id, int img)
        {
            get_picture_box(id).Image = get_image(img);
        }

        private PictureBox get_picture_box(int id)
        {
            
            switch (id)
            {
                
                case 0: return pictureBox0;
                case 1: return pictureBox1;
                case 2: return pictureBox2;
                case 3: return pictureBox3;
                case 4: return pictureBox4;
                case 5: return pictureBox5;
                case 6: return pictureBox6;
                case 7: return pictureBox7;
                case 8: return pictureBox8;
                case 9: return pictureBox9;
                case 10: return pictureBox10;
                case 11: return pictureBox11;
                case 12: return pictureBox12;
                case 13: return pictureBox13;
                case 14: return pictureBox14;
                case 15: return pictureBox15;

                default: return null;


            }
    
        }

        private Image get_image(int img)
        {
            switch (img)
            {
                case 0: return Properties.Resources._0;
                case 1: return Properties.Resources._1;
                case 2: return Properties.Resources._2;
                case 3: return Properties.Resources._3;
                case 4: return Properties.Resources._4;
                case 5: return Properties.Resources._5;
                case 6: return Properties.Resources._6;
                case 7: return Properties.Resources._7;
                case 8: return Properties.Resources._8;

                default: return null;

            }


        }

        
        //opens the image for the first time
        private void show(int id)
        {
            load_image(id, cards[id]);
            get_picture_box(id).Cursor = Cursors.Arrow;
        }

        //opens the image forever
        private void open(int id)
        {
            opens[id] = true;
            show(id);
        }

        //returns to the default image
        private void hide(int id)
        {
            load_image(id, 0);
            get_picture_box(id).Cursor = Cursors.Hand;

        }


        /// MOUSE CLIKS ON IMGS
        private void pictureBox15_MouseClick(object sender, MouseEventArgs e)
        {
            int id = int.Parse(((PictureBox)sender).Tag.ToString());
            if (opens[id]) return;

            switch (status)
            {
                case Status.INIT:
                    start_sequence(id);
                    break;
                case Status.FIRST_IMG_OPEN:
                    check_if_equals(id);
                    break;
                case Status.SECOND_IMG_OPEN:
                    break;
                case Status.WRONG_ANS:
                    wrong_answer(id);
                    break;

            }


        }

        //if the click opens the first image of the pair
       private void start_sequence(int id)
        {
            image_a = id;
            status = Status.FIRST_IMG_OPEN;
            show(image_a);
        }

       private void check_if_equals(int id)
        {
            image_b = id;
            if (image_a == image_b)
                return;

            status = Status.SECOND_IMG_OPEN;
            show(image_b);

            if(cards[image_b]==cards[image_a])
            {
                open(image_a);
                open(image_b);
                done += 2;
                if (done == 16)
                    win();
                else
                    status = Status.INIT;
            }
            else
            {
                status = Status.WRONG_ANS;
            }
            
        }

        private void win()
        {
            MessageBox.Show("Congratulations!","You win!");
            init_game();
        }

      private void wrong_answer(int id)
        {
            hide(image_a);
            hide(image_b);
            image_a = id;
            start_sequence(id);

        }

    } 
    }
