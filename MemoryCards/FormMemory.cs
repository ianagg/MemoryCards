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
    public partial class FormMemory : Form, IPlayable
    {
        LogicMemory logic;

        public FormMemory()
        {

            InitializeComponent();
            logic = new LogicMemory(this);
            logic.CreateNewGame();

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
            logic.CreateNewGame();
        }

        //image loader  
        private void LoadImage(int id, int img)
        {
            getPictureBox(id).Image = getImage(img);
        }

        private PictureBox getPictureBox(int id)
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

        private Image getImage(int img)
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


        /// MOUSE CLICKS ON IMGS
        private void pictureBox15_MouseClick(object sender, MouseEventArgs e)
        {
            int id = int.Parse(((PictureBox)sender).Tag.ToString());
            logic.ClickOnImage(id);

        }

        ////// INTERFACE REALIZATION
        public void ShowCard(int id, int card)
        {
            LoadImage(id, card);
            getPictureBox(id).Cursor = Cursors.Arrow;
        }

        public void HideCard(int id)
        {
            LoadImage(id, 0);
            getPictureBox(id).Cursor = Cursors.Hand;

        }

        public void Win()
        {
            MessageBox.Show("Congratulations!", "You win!");
            logic.CreateNewGame();
        }



    }
}
