using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Projetcsharp_Cavalier_Rubinthan
{
    public partial class ModeSimulation : Form
    {

       /* Echiquier graphique */
            Button[,] echiquier;
            Image cavalier,cavalier2;
            bool pause = false;
            int gardeI = 0, gardeJ = 0;
            int pas, durée;
            bool enCours;

        /* Echiqiuer console */
        static int[,] echec = new int[12, 12];
        static int[] depi = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
        static int[] depj = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
        int nb_fuite, min_fuite, lmin_fuite = 0;
        int i, j, k, l, ii, jj;


        public ModeSimulation()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            enCours = false;
            label2.Visible = false;
            button1.Text = "Au hasard ";
            cavalier = Image.FromFile(@"images\cavalier.jpg");
            cavalier2 = Image.FromFile(@"images\cavalier3.jpg");
            this.Text = "Mode Simulation";
            this.echiquier = new Button[12, 12];
            this.BackColor = Color.Beige;


            // initialisation des boutton de l'échiquier
            for (int l = 0; l < 12; l++)
            {
                for (int c = 0; c < 12; c++)
                {
                    Button b;
                    b = new Button();
                    b.Location = new Point(l * 50, c * 50);
                    b.Size = new Size(50, 50);
                    b.Click += new System.EventHandler(this.Mon_Bouton_Click);
                    if (c < 2 | c > 9 || l < 2 | l > 9)
                        b.Visible = false;
                    this.echiquier[l, c] = b;
                    this.Controls.Add(b); // ??

                }
            }
            Colorchess(12);
        }

        
        private void Mon_Bouton_Click(object sender, EventArgs e)      //choisir la première case 
        {
            button1.Visible = false;
            if (!enCours)        //avant la simulation 
            {
                effacerEchiquier();

                
                gardeI = trouverI(sender, echiquier);
                gardeJ = trouverJ(sender, echiquier);

                jouer(gardeI, gardeJ, 2000, 1);
            }
            else            //pendant la simulation 
            {
                label2.Text = "Simulation en cours impossible de selectionner un autre boutton";
                label2.Visible = true;
            }
        }


        private void button1_Click(object sender, EventArgs e)     //bouton au hasard 
        {
            
            effacerEchiquier();
            button1.Enabled = false;

            Random random = new Random();

            gardeI = random.Next(1, 8) + 1;                         //premier jeux au hasard 
            gardeJ = random.Next(1, 8) + 1;                         //avoir un nombre au hasard entre 2 et 9
            

            jouer(gardeI, gardeJ, 1000, 1);                         //chaque pas a une durée de 2s.
            label1.Visible = false;
        }

       
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)       //avant la fermeture messagebox
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


        // trouve la valeur de i dans un tableau
        static int trouverI(object o, Button[,] b)
        {

            for (int i = 2; i < 10; i++)
            {
                for (int j = 2; j < 10; j++)
                {
                    if (o == b[i, j])
                        return i;
                }
            }
            return 0;
        }

        // trouve la valeur de j dans un tableau 
        static int trouverJ(object o, Button[,] b)
        {

            for (int i = 2; i < 10; i++)
            {
                for (int j = 2; j < 10; j++)
                {
                    if (o == b[i, j])
                        return j;
                }
            }
            return 0;
        }

        //effacer toutes les cases de l'échiquier
        public void effacerEchiquier()
        {
            for (int i = 2; i < 10; i++)

            {
                for (int j = 2; j < 10; j++)
                {
                    echiquier[i, j].Text = "";
                    echiquier[i, j].BackgroundImage = null;
                }

            }
        }

        static int fuite(int i, int j)
        {
            int n, l;

            for (l = 0, n = 8; l < 8; l++)
                if (echec[i + depi[l], j + depj[l]] != 0) n--;

            return (n == 0) ? 9 : n;
        }


        public async void jouer(int ip, int jp, int duree, int pas)
        {
            enCours = true;
            for (i = 0; i < 12; i++)
                for (j = 0; j < 12; j++)
                    echec[i, j] = ((i < 2 | i > 9 | j < 2 | j > 9) ? -1 : 0);

            echec[ip, jp] = 1;
            echiquier[ip, jp].BackgroundImage = cavalier2;
            await Task.Delay(250);            //le temps pour jouer le deuxième coup 

            for (k = 2; k <= 64; k++)
            {
               for (l = 0, min_fuite = 11; l < 8; l++)
                {
                    ii = ip + depi[l]; jj = jp + depj[l];

                    nb_fuite = ((echec[ii, jj] != 0) ? 10 : fuite(ii, jj));

                    if (nb_fuite < min_fuite)
                    {
                        min_fuite = nb_fuite; lmin_fuite = l;
                    }
                }
                if (min_fuite == 9 & k != 64)
                {
                    break;
                }
                ip += depi[lmin_fuite]; jp += depj[lmin_fuite];
                echec[ip, jp] = k;
                if (k % 1 == 0 || k == 64)
                {
                    echiquier[ip, jp].BackgroundImage = cavalier2;
                    echiquier[ip, jp].Text = "" + k ;
                    await Task.Delay(250);                    //0.5 s pour chaque coup 
                }
                
            }
            label1.Text = "Trop fort Euler";
            label1.Visible = true;
            button1.Enabled = true;
            button1.Visible = true;
            button1.Text = "Recommencer la simulation ? ";

            
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
                            ((Button)echiquier[l, c]).BackColor = Color.White;
                        else
                            ((Button)echiquier[l, c]).BackColor = Color.Black;
                    }
                    else if (l % 2 != 0)
                    {
                        if (c % 2 == 0)
                            ((Button)echiquier[l, c]).BackColor = Color.Black;
                        else
                            ((Button)echiquier[l, c]).BackColor = Color.White;
                    }

                }

            }
        }


    }
}
