using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
/********************************/

namespace TexasEntraineur
{
    /// <summary>
    /// Logique d'interaction pour TableDeJeu.xaml
    /// </summary>
    public partial class TableDeJeu : Window
    {
        private cJeu leJeu;
        public Configurateur cfg;

        private DispatcherTimer delaiPreFlop = new DispatcherTimer();
        private DispatcherTimer delaiPreTurn = new DispatcherTimer();
        private DispatcherTimer delaiPreRiver = new DispatcherTimer();
        private DispatcherTimer delaiPreGagnant = new DispatcherTimer();
        string CompteAReboursPourTrouverGagnant = "NEUTRE";

        bool[] JoueurSoumission;
    
        public TableDeJeu(Configurateur cfg_param)
        {
            InitializeComponent();
            //bout_PasserFlop.IsEnabled = false;
            //bout_PasserTurn.IsEnabled = false;
           // bout_PasserRiver.IsEnabled = false;
            //bout_Gagnant.IsEnabled = false;
            bout_Rejouer.IsEnabled = false;
            JoueurSoumission = new bool[6];
            for (int i = 0; i < 6; i++)
                JoueurSoumission[i] = false;

            leJeu = new cJeu(cfg_param);
            leJeu.PreFlopTermine += ActiverFlop;
            leJeu.FlopTermine += ActiverTurn;
            leJeu.TurnTermine += ActiverRiver;
            leJeu.RiverTermine += ActiverGagnant;
            

            leJeu.distribueMains();
            leJeu.distribueFlop();
            leJeu.distribueTurn();
            leJeu.distribueRiver();
            InstancieCartes();
            cfg = cfg_param;
            leJeu.animationPreFlop();
            bout_Rejouer.IsEnabled = true;
           
        }

        public void ActiverFlop(object sender, EventArgs e)
        {
            delaiPreFlop.Tick += AnimationFlop;
            delaiPreFlop.Interval = TimeSpan.FromSeconds(Convert.ToDouble(cfg.TempsPreFlop));
            delaiPreFlop.Start();   
        }

        private void AnimationFlop(object sender, EventArgs e)
        {
            delaiPreFlop.Stop();
            leJeu.animationFlop();
        }

        private void attend(string DelaiString)
        {
            double delai = Convert.ToDouble(DelaiString);
            attend((int)(1000 * delai));
        }

        public void attend(int delai_param)
        {
            System.Threading.Thread.Sleep(delai_param);
            //DateTime start = DateTime.Now;
            //Int64 start_tick = start.Ticks;
            //Int64 courant_tick = start_tick;
            //Int64 delai = start_tick + delai_param;
            ////MessageBox.Show("start_tick:" + start_tick);
            //while (courant_tick < delai)
            //{
            //    DateTime courant = DateTime.Now;
            //    courant_tick = courant.Ticks;
            //}
        }

        public void ActiverTurn(object sender, EventArgs e)
        {
            delaiPreTurn.Tick += AnimationTurn;
            delaiPreTurn.Interval = TimeSpan.FromSeconds(Convert.ToDouble(cfg.TempsPreTurn));
            delaiPreTurn.Start();
        }

        private void AnimationTurn(object sender, EventArgs e)
        {
            delaiPreTurn.Stop();
            leJeu.animationTurn();
        }

        public void ActiverRiver(object sender, EventArgs e)
        {
            delaiPreRiver.Tick += AnimationRiver;
            delaiPreRiver.Interval = TimeSpan.FromSeconds(Convert.ToDouble(cfg.TempsPreRiver));
            delaiPreRiver.Start();
        }

        private void AnimationRiver(object sender, EventArgs e)
        {
            delaiPreRiver.Stop();
            leJeu.animationRiver();
        }
        public void ActiverGagnant(object sender, EventArgs e)
        {
            delaiPreGagnant.Tick += DetermineGagnant;
            delaiPreGagnant.Interval = TimeSpan.FromSeconds(Convert.ToDouble(cfg.TempsPreGagnant));
            CompteAReboursPourTrouverGagnant = "EN COURS";
            delaiPreGagnant.Start();
        }

        private void DetermineGagnant(object sender, EventArgs e)
        {
            
            delaiPreGagnant.Stop();
            if (CompteAReboursPourTrouverGagnant != "SOUMISSION TRAITÉE")
               MessageBox.Show("ERREUR... Temps dépassé");
            Info.Text = leJeu.determineGagnant();
        }

        public void EvaluerSoumission(object o, RoutedEventArgs r)
        {
            if (CompteAReboursPourTrouverGagnant == "EN COURS")
            {
                CompteAReboursPourTrouverGagnant = "SOUMISSION TRAITÉE";
                if (ChoixCorrect())
                    MessageBox.Show("Bravo! Vous avez deviné juste");
                else
                    MessageBox.Show("ERREUR... Vous vous êtes trompé");
                
            }
        }

        private bool ChoixCorrect()
        {
          string TexteGagnant = leJeu.determineGagnant();
          int PositionFinLigne = TexteGagnant.IndexOf("\n");
          string PremiereLigne = TexteGagnant.Substring(0, 18);
          //MessageBox.Show(PremiereLigne);
          bool J0_SOUMISSION_OK = false;
          bool J1_SOUMISSION_OK = false;
          bool J2_SOUMISSION_OK = false;
          bool J3_SOUMISSION_OK = false;
          bool J4_SOUMISSION_OK = false;         
          bool J5_SOUMISSION_OK = false;

          if (JoueurSoumission[0])
          {
              if (PremiereLigne.Contains("J0"))
                  J0_SOUMISSION_OK = true;
          }
          else
          {
              if (!PremiereLigne.Contains("J0"))
                  J0_SOUMISSION_OK = true;
          }
          if (JoueurSoumission[1])
          {
              if (PremiereLigne.Contains("J1"))
                  J1_SOUMISSION_OK = true;
          }
          else
          {
              if (!PremiereLigne.Contains("J1"))
                  J1_SOUMISSION_OK = true;
          }
          if (JoueurSoumission[2])
          {
              if (PremiereLigne.Contains("J2"))
                  J2_SOUMISSION_OK = true;
          }
          else
          {
              if (!PremiereLigne.Contains("J2"))
                  J2_SOUMISSION_OK = true;
          }
          if (JoueurSoumission[3])
          {
              if (PremiereLigne.Contains("J3"))
                  J3_SOUMISSION_OK = true;
          }
          else
          {
              if (!PremiereLigne.Contains("J3"))
                  J3_SOUMISSION_OK = true;
          }
          if (JoueurSoumission[4])
          {
              if (PremiereLigne.Contains("J4"))
                  J4_SOUMISSION_OK = true;
          }
          else
          {
              if (!PremiereLigne.Contains("J4"))
                  J4_SOUMISSION_OK = true;
          }
          if (JoueurSoumission[5])
          {
              if (PremiereLigne.Contains("J5"))
                  J5_SOUMISSION_OK = true;
          }
          else
          {
              if (!PremiereLigne.Contains("J5"))
                  J5_SOUMISSION_OK = true;
          }

          if (J0_SOUMISSION_OK && J1_SOUMISSION_OK && J2_SOUMISSION_OK &&
              J3_SOUMISSION_OK && J4_SOUMISSION_OK && J5_SOUMISSION_OK)
            return true;
          return false;
        }

        public void ReConfigurer(object o, RoutedEventArgs r)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        public void Rejouer(object o, RoutedEventArgs r)
        {
            TableDeJeu tj = new TableDeJeu(cfg);
            tj.Show();
            this.Close();
        }

        private void InstancieCartes()
        {
            Carte CarteDessusPaquet;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    int NoJoueur = i;
                    int NoCarte = j;
                    MasterCanvas.Children.Add(leJeu.MainDesJoueurs[NoJoueur].mainOrigine[NoCarte].imgCarte);
                    Canvas.SetTop(leJeu.MainDesJoueurs[NoJoueur].mainOrigine[NoCarte].imgCarte, 100);
                    Canvas.SetLeft(leJeu.MainDesJoueurs[NoJoueur].mainOrigine[NoCarte].imgCarte, 10);
                }
            }

            for (int i = 0; i < 3; i++)
            {
               MasterCanvas.Children.Add(leJeu.Flop[i].imgCarte);
               Canvas.SetTop(leJeu.Flop[i].imgCarte, 100);
               Canvas.SetLeft(leJeu.Flop[i].imgCarte, 10);
            }

            MasterCanvas.Children.Add(leJeu.Turn.imgCarte);
            Canvas.SetTop(leJeu.Turn.imgCarte, 100);
            Canvas.SetLeft(leJeu.Turn.imgCarte, 10);

            MasterCanvas.Children.Add(leJeu.River.imgCarte);
            Canvas.SetTop(leJeu.River.imgCarte, 100);
            Canvas.SetLeft(leJeu.River.imgCarte, 10);

            CarteDessusPaquet = new Carte();
            MasterCanvas.Children.Add(CarteDessusPaquet.imgCarte);
            Canvas.SetTop(CarteDessusPaquet.imgCarte, 100);
            Canvas.SetLeft(CarteDessusPaquet.imgCarte, 10);
        }

        private void CB0_Checked(object sender, RoutedEventArgs e)
        {
            JoueurSoumission[0] = true;
        }
        private void CB1_Checked(object sender, RoutedEventArgs e)
        {
            JoueurSoumission[1] = true;
        }
        private void CB2_Checked(object sender, RoutedEventArgs e)
        {
            JoueurSoumission[2] = true;
        }
        private void CB3_Checked(object sender, RoutedEventArgs e)
        {
            JoueurSoumission[3] = true;
        }
        private void CB4_Checked(object sender, RoutedEventArgs e)
        {
            JoueurSoumission[4] = true;
        }
        private void CB5_Checked(object sender, RoutedEventArgs e)
        {
            JoueurSoumission[5] = true;
        }


        private void CB0_UnChecked(object sender, RoutedEventArgs e)
        {
            JoueurSoumission[0] = false;
        }
        private void CB1_UnChecked(object sender, RoutedEventArgs e)
        {
            JoueurSoumission[1] = false;
        }
        private void CB2_UnChecked(object sender, RoutedEventArgs e)
        {
            JoueurSoumission[2] = false;
        }
        private void CB3_UnChecked(object sender, RoutedEventArgs e)
        {
            JoueurSoumission[3] = false;
        }
        private void CB4_UnChecked(object sender, RoutedEventArgs e)
        {
            JoueurSoumission[4] = false;
        }
        private void CB5_UnChecked(object sender, RoutedEventArgs e)
        {
            JoueurSoumission[5] = true;
        }
    }
}
