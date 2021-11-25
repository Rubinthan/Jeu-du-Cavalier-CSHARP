using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Projetcsharp_Cavalier_Rubinthan
{
    public partial class Euler : Form
    {
        Button[,] echiquier;
        Image cavalier;
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

        private void button5_Click(object sender, EventArgs e)   //simulation
        {

            //effacerEchiquier();

            Random random = new Random();

            gardeI = random.Next(1, 8) + 1;
            gardeJ = random.Next(1, 8) + 1;
            // iR et jR evoluent de 2 à 9 !

            //jouer(gardeI, gardeJ, durée, pas);

        }
        public Euler()
        {
            InitializeComponent();
        }


    }
}
