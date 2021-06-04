using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dodge_game
{
    public partial class Form1 : Form
    {
        //global variables 

        int playerSpeed = 0;
        int blocker1Speed = -8;
        int blocker2Speed = -8;
        int blocker1counter = 0;
        int blocker2counter = 0;
        string gameState = "waiting";
        

        //keys
        bool wDown = false;
        bool sDown = false;
        bool aDown = false;
        bool dDown = false;

        // paint
        Rectangle player = new Rectangle(50, 200, 10, 10);
        // Rectangle blocker = new Rectangle(200, 300, 20, 60);

        List<Rectangle> blocker1 = new List<Rectangle>();
        List<Rectangle> blocker2 = new List<Rectangle>();

        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush redBrush = new SolidBrush(Color.Red);
       

        public Form1()
        {
            InitializeComponent();

            
        }
        public void GameInitialize()
        {
            outputLabel.Text = "";
            subTitleLabel.Text = "";

            gameTimer.Enabled = true;
            gameState = "running";
            player.X = 50;
            player.Y = 200;

                
            playerSpeed = 8;


        }


        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //move player

            if (wDown == true && player.Y > 0)
            {
                player.Y -= playerSpeed;
            }
            if (sDown == true && player.Y < 350)
            {
                player.Y += playerSpeed;
            }
            if (aDown == true && player.X > 0)
            {
                player.X -= playerSpeed;
            }
            if (dDown == true && player.X < 570)
            {
                player.X += playerSpeed;
            }

            // add bloker1
             if (blocker1counter == 18)
             {
                blocker1.Add(new Rectangle(200, 300, 10, 20));
                blocker1counter = 0;
             }

            //add blocker 2

            if (blocker2counter == 18)
            {
                blocker2.Add(new Rectangle(400, 300, 10, 20));
                blocker2counter = 0;
            }

            // move blockers
            for (int i = 0; i < blocker1.Count(); i++)
            {
                //find the new postion of y based on speed 
                int y = blocker1[i].Y + blocker1Speed;

                //replace the rectangle in the list with updated one using new y 
                blocker1[i] = new Rectangle(blocker1[i].X, y, 20, 60);
            }

            for (int i = 0; i < blocker2.Count(); i++)
            {
                //find the new postion of y based on speed 
                int y = blocker2[i].Y + blocker2Speed;

                //replace the rectangle in the list with updated one using new y 
                blocker2[i] = new Rectangle(blocker2[i].X, y, 20, 60);
            }


            //remove blokers

            for (int i = 0; i < blocker1.Count(); i++)
            {

                if (blocker1[i].Y > this.Height - 60)
                {
                    blocker1.RemoveAt(i);
                  
                  
                }
            }

            for (int i = 0; i < blocker2.Count(); i++)
            {

                if (blocker2[i].Y > this.Height - 60)
                {
                    blocker2.RemoveAt(i);


                }
            }

            //collsions
            for (int i = 0; i < blocker1.Count; i++)
            {
                if (player.IntersectsWith(blocker1[i]))
                {
                    gameState = "over";
                    outputLabel.Text = "They Got You";
                    subTitleLabel.Text = "Press Space To PLay Agian Or Escape To Exit";
                    playerSpeed = 0;
                }
            }

            for (int i = 0; i < blocker2.Count; i++)
            {
                if (player.IntersectsWith(blocker2[i]))
                {
                    gameState = "over";
                    outputLabel.Text = "They Got You";
                    subTitleLabel.Text = "Press Space To PLay Agian Or Escape To Exit";
                    playerSpeed = 0;
                }
            }
            
            
                if (player.X > 500 && gameState == "running")
                {
                    gameState = "over";
                    outputLabel.Text = "You Win";
                    subTitleLabel.Text = "Press Space To PLay Agian Or Escape To Exit";
                    playerSpeed = 0;

                }
            


            // counter 
            blocker1counter++;
            blocker2counter++;
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(whiteBrush, player);
            for (int i = 0; i < blocker1.Count(); i++)
            {
                e.Graphics.FillRectangle(redBrush, blocker1[i]);
            }

            e.Graphics.FillRectangle(whiteBrush, player);
            for (int i = 0; i < blocker2.Count(); i++)
            {
                e.Graphics.FillRectangle(redBrush, blocker2[i]);
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.A:
                    aDown = false;
                    break;
                case Keys.D:
                    dDown = false;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.A:
                    aDown = true;
                    break;
                case Keys.D:
                    dDown = true;
                    break;

                case Keys.Space:

                    if (gameState == "waiting" || gameState == "over")

                    {

                        GameInitialize();

                    }

                    break;

                case Keys.Escape:

                    if (gameState == "waiting" || gameState == "over")

                    {

                        Application.Exit();

                    }

                    break;
            }
            
        }
    }
}
