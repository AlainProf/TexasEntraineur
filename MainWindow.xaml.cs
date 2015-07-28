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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TexasEntraineur
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Configurateur cfg;
        public MainWindow()
        {
            InitializeComponent();
            cfg = new Configurateur();
        }

        private void Declenche_Click_1(object sender, RoutedEventArgs e)
        {

            cfg.TempsDonnerCarte = CB_TempsDonnerCarte.Text;
            cfg.TempsPreFlop = CB_TempsPreFlop.Text;
            cfg.TempsPreTurn = CB_TempsPreTurn.Text;
            cfg.TempsPreRiver = CB_TempsPreRiver.Text;
            cfg.TempsPreGagnant = CB_TempsPreGagnant.Text;
            TableDeJeu table = new TableDeJeu(cfg);
            table.Show();
            this.Close();
        }

        //public void ReInitTempsPreFlop(object o, RoutedEventArgs r)
        //{
        //    string Val = CB_TempsPreFlop.Text;
        //    MessageBox.Show(Val);
        //    if (Val.Count() == 0)
        //        Val = "0";
        //    cfg.TempsPreFlop = Convert.ToDouble(Val);
        //}
    }
}
