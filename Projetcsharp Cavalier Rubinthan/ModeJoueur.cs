/*
 Rubinthan Jegatheeswaran
 INSTRUMENTATION 2
 Projet Cavalier
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Projetcsharp_Cavalier_Rubinthan
{
    public partial class ModeJoueur : Form
    {
        const int chessboardSize = 8; // la taille de l'échequier
        const int taillebouton = 50; // taille du bouton
        int xbouton = 0;
        int ybouton = 0;
      
        int nbButtonClicked = 0;
        int bName;
        
        Image cavalier;
        Image epee;
        Image gameover;
        Button[,] grille = new Button[8, 8];        //permet de cree une matrice de bouton de taille 8 
        int[] bClicked = new int[64];                  //vecteur qui stocke les boutons sur lesquels on a cliqué 
        int[] btnHelp = new int[8];                      //vecteur permettant de simuler les deplacements du cavalier 
        //string[] tempArray = new string[2];
        bool endGame = true;
        
   


        private void Form1_Load(object sender, EventArgs e)    //fonction load qui ne s'éxécute qu'une seule fois 
        {
            
            cavalier = Image.FromFile(@"images\cavalier.jpg");         //on charge l'image dans cavalier on le fait dans le load 
            epee = Image.FromFile(@"images\epee.jpg");         //on charge l'image dans cavalier on le fait dans le load 
            gameover= Image.FromFile(@"images\gameover.jpg");
            this.BackColor = Color.Beige;
            damier(chessboardSize);                   //permet de créer l'échéquier
            Colorchess(chessboardSize);
            this.Text = "Mode Joueur";
            label2.Text = "";
            pictureBox1.Visible = false;
            disableButton();
            button1.Text = "JOUER";
            button2.Visible = false;
            button3.Visible = false;
            button2.Text = "A toi de choisir";
            button3.Text = "Au hasard";
            label1.Visible = false;
            button4.Text = "Rejouer ?";
            button4.Visible = false;
        }

        public ModeJoueur()
        {
            InitializeComponent();
        }

        private void btn_clicked(object sender, EventArgs e)
        {
            Button b = sender as Button;
            bName = Convert.ToInt32(b.Name);               //convertit le nom de btn en entier de 32 bits  permettant de localiser le bouton
            bClicked[nbButtonClicked] = bName;             //on le met dans le tableaux qui repertorie les boutons déjà appuyé
            nbButtonClicked += 1;                              //à chaque fois qu'on clique on fait +1 sur la variable nbbuttonclicked 
            b.Text = nbButtonClicked.ToString();                                          //convertit 2 en text pour l'afficher 
            //b.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);        //affiche en blanc le texte qui est au premier plan 
            disableButton();

            //permet de gérer les déplacement du cavalier avec un tableau 
            btnHelp[0] = bName - 21;    //3                    
            btnHelp[1] = bName - 19;    //2
            btnHelp[2] = bName - 12;    //4
            btnHelp[3] = bName - 8;     //1
            btnHelp[4] = bName + 8;     //5
            btnHelp[5] = bName + 12;    //8
            btnHelp[6] = bName + 19;    //6
            btnHelp[7] = bName + 21;    //7

            // Reset the color
            Colorchess(chessboardSize);
            colorButton(bClicked);
            b.Image =cavalier;

            endGame = true;
            
            foreach (int helpButton in btnHelp)
            {
                int isValid = checkIfIsInArray(bClicked, helpButton);                  
                if (isValid == 0)                          //le cavalier peut se déplacer encore 
                {
                    
                    foreach (Button buttonToEnable in grille)      //active le bouton et change la couleur des boutons sur lequel peut appuyer l'utilisateur
                    {
                        if (helpButton == Convert.ToInt32(buttonToEnable.Name))
                        {
                            buttonToEnable.Enabled = true;
                            buttonToEnable.BackColor = Color.Red;
                            endGame = false;
                        }
                    }
                }
            }

            if (endGame) 
            { 
                endTheGame(nbButtonClicked);
            }
        }

        private int checkIfIsInArray(int[] array, int value)     //fonction qui regarde si dans le tableau il y a value 
        {
           
            foreach (int isInArray in array) 
            {
                if (value == isInArray)                     //si value est dans le tableau on retourne 1 sinon 0
                {
                    return 1;             
                }
            }
            return 0;
        }

     
        private void damier(int chesboardSize)      //création damier avec des bouton et paramétrage des boutons 
        {
            
            for (int i = 0; i < chesboardSize; i++)
            {

                xbouton = 0;

                for (int j = 0; j < chesboardSize; j++)
                {
                    grille[i, j] = new Button();
                    ((Button)grille[i, j]).Name = "" + (i + 1) + j;         //le nom du bouton fait reference à son positionnement
                    ((Button)grille[i, j]).Size = new Size(taillebouton, taillebouton);
                    ((Button)grille[i, j]).Location = new Point(xbouton, ybouton);
                    ((Button)grille[i, j]).Click += new EventHandler(btn_clicked);   //on peut desormais clique sur les boutons et il y a une fonction qui est associé au clic.
                    panel1.Controls.Add((Button)grille[i, j]);
                    xbouton += taillebouton;
                }
                ybouton += taillebouton;
            }
        }
    

        private void Colorchess(int chesboardSize)     //Damier noir et blanc
        {
            for (int l = 0; l < chesboardSize; l++)
            {
                for (int c = 0; c < chesboardSize; c++)
                {
                    if (l % 2 == 0)                                     
                    {
                        if (c % 2 == 0)
                            ((Button)grille[l, c]).BackColor = Color.White;
                        else
                            ((Button)grille[l, c]).BackColor = Color.Black;
                    }
                    else if (l % 2 != 0)
                    {
                        if (c % 2 == 0)
                            ((Button)grille[l, c]).BackColor = Color.Black;
                        else
                            ((Button)grille[l, c]).BackColor = Color.White;
                    }
                    
                }

            }
        }



        private void colorButton(int[] buttonUsed)
        {
            foreach (Button colorButton in grille)
            {
                for (int i = 0; i < nbButtonClicked; i++)
                {
                    if (buttonUsed[i] == Convert.ToInt32(colorButton.Name))
                    {
                        colorButton.Image =epee;
                    }
                }
            }

        }

 
     
        private void endTheGame(int nbButtonClicked)
        {
            label1.Text = ""; // Not show the rules
            disableButton();

            if (nbButtonClicked == 64)
            {
                label2.Text = " Bravo !! Vous avez gagné !";
                button4.Visible = true;
                label2.Visible = true;
            }
            else
            {
                pictureBox1.Visible = true;
                pictureBox1.Image = gameover;
                label2.Text = "Vous avez " + nbButtonClicked + " points";
                button4.Visible = true;
                label2.Visible = true;
            }

        }
        private void disableButton()
        {
            foreach (Button buttonDisabled in grille) //Disables all buttons
            {
                buttonDisabled.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)        //le bouton jouer 
        {
            
            button1.Enabled = false;
            button2.Visible = true;
            button3.Visible = true;
        }
        private void enableButton()   //activer tous les boutons 
        {
            foreach (Button buttonDisabled in grille) 
            {
                buttonDisabled.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)     //le bouton a toi de choisir 
        {
            enableButton();
            button2.Enabled = false;
            button3.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)         //le bouton au hasard 
        {
            
            button2.Visible = false;
            button3.Enabled= false;
            Array.Clear(bClicked, 0, bClicked.Length);
            Array.Clear(btnHelp, 0, btnHelp.Length);
            Random random = new Random();
            int lh = random.Next(0, 7);               //Génère un entier compris entre 0 et 7
            int ch = random.Next(0, 7);
            bName = Convert.ToInt32(grille[lh,ch].Name);               //convertit le nom de btn en nombre permettant de localiser le bouton
            bClicked[nbButtonClicked] = bName;
            nbButtonClicked += 1;
            //grille[lh, ch].Click -= new EventHandler(this.btn_clicked);
            grille[lh, ch].Enabled = false;
            btnHelp[0] = bName - 21;    //3                    
            btnHelp[1] = bName - 19;    //2
            btnHelp[2] = bName - 12;    //4
            btnHelp[3] = bName - 8;     //1
            btnHelp[4] = bName + 8;     //5
            btnHelp[5] = bName + 12;    //8
            btnHelp[6] = bName + 19;    //6
            btnHelp[7] = bName + 21;    //7
            endGame = true;

            foreach (int helpButton in btnHelp)
            {
                int isValid = checkIfIsInArray(bClicked, helpButton);
                if (isValid == 0)                          //le cavalier peut se déplacer encore 
                {

                    foreach (Button buttonToEnable in grille)      //active le bouton et change la couleur des boutons sur lequel peut appuyer l'utilisateur
                    {
                        if (helpButton == Convert.ToInt32(buttonToEnable.Name))
                        {
                            buttonToEnable.Enabled = true;
                            buttonToEnable.BackColor = Color.Red;
                            endGame = false;
                        }
                    }
                }
                
            }
            if(endGame) 
            {
                endTheGame(nbButtonClicked); 
            }
                grille[lh, ch].Image = cavalier;
            
            
        }

        private void button4_Click(object sender, EventArgs e) // button recommencer
        {
            clearboard(chessboardSize);
            pictureBox1.Visible = false;
            Array.Clear(btnHelp, 0, btnHelp.Length);
            Array.Clear(bClicked, 0, bClicked.Length);                        //reinitialiser le tableau
            label2.Visible = false;
            disableButton();
            nbButtonClicked = 0;

            button4.Visible = false;
            button3.Visible = true;
            button2.Visible = true;
            button2.Enabled = true;
            button3.Enabled = true;
            
        }
        
        private void clearboard(int chesboardSize)
        {
            
            for (int l = 0; l < chessboardSize; l++)
            {
                for (int c = 0; c < chessboardSize; c++)
                {
                    if (l % 2 == 0)
                    {
                        if (c % 2 == 0)
                            ((Button)grille[l, c]).BackColor = Color.White;
                        else
                            ((Button)grille[l, c]).BackColor = Color.Black;
                    }
                    else if (l % 2 != 0)
                    {
                        if (c % 2 == 0)
                            ((Button)grille[l, c]).BackColor = Color.Black;
                        else
                            ((Button)grille[l, c]).BackColor = Color.White;
                    }
                    grille[l, c].Image = null;
                    grille[l, c].Text = "";

                }

            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)         //fermer la fenêtre affichage message Box
        {
            DialogResult reponse = MessageBox.Show(
            "Voulez vous vraiment fermer cette fenetre ?",
            "Fermeture fenetre",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button3,
            MessageBoxOptions.RightAlign);
            if (reponse == DialogResult.Yes)
                ;
            else if (reponse == DialogResult.No)
                e.Cancel = true;
            else 
                e.Cancel = true;
        }

        private void règleDeJeuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string regles = "Le but de ce jeu est de faire parcourir un cavalier sur l'ensemble des cases d'un échiquier sans passer deux fois sur la même case.";
            MessageBox.Show(regles);
        }

        private void aProposToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string about = "Jeu du cavalier développé par Rubinthan Jegatheeswaran, Ingénieur Instrumentation à Sup Galilée";
            MessageBox.Show(about);
        }

        private void modeSimulationToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Close();   //fermer la fenêtre Mode Joueur
            if (this.DialogResult == DialogResult.Yes);
            {
                
                ModeSimulation form2 = new ModeSimulation();
                form2.Show();        //ouvrir la fenêtre 2 
            }
            
            
            
        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }



}
