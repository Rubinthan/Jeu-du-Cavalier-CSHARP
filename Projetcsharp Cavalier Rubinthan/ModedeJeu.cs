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
    public partial class ModedeJeu : Form
    {
        Image echequier;
        public ModedeJeu()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
           echequier= Image.FromFile(@"images\echiquier.jpg");
            this.Text = "Mode de Jeu";
            this.BackgroundImage = echequier;
            button1.Text = "Mode Joueur";
            button2.Text = "Mode Simulation";
            //button1.BackColor = Color.Transparent;
            //button2.BackColor = Color.Transparent;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ModeJoueur form1 = new ModeJoueur();
            form1.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ModeSimulation form2 = new ModeSimulation();
            form2.Show();
        }
    }
}
