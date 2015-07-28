using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TexasEntraineur
{
    public class Configurateur  //: INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged;
        //public void OnPropertyChanged(PropertyChangedEventArgs e)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, e);
        //}
        //public static string PathImage = "C:\\Users\\Amartel\\Dropbox\\Aut2014\\5B6\\CS\\Exemples applications WPF\\TexAsynch\\images\\cartes";
        public static string PathImage = "pack://application:,,,/images/cartes";
        public string TempsDonnerCarte;
        public string TempsPreFlop;
        public string TempsPreTurn;
        public string TempsPreRiver;
        public string TempsPreGagnant;

        //public string TempsPreFlop
        
        //{
        //    get { return tempsPreFlop; }
        //    set { tempsPreFlop = value; }
        //}

        public Configurateur()
        {
           
            TempsDonnerCarte = "0,3";
            TempsPreFlop = "6";
            TempsPreTurn = "3";
            TempsPreRiver = "1";
            TempsPreGagnant = "10";
        }
    }
}
