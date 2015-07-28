/****************************************************/
/*   Auteur:   Alain Martel 
/*   Projet:   PokerTexas, 
/*   Desc:     Un moteur de poker Texas Hold'Hem (conversion c++ à c#)
/*   Création: 2013-juin 
/*
/*   Fichier:	Paquet.cs
/*   Desc:		Définition des classes Carte, Paquet et uneMain 
/***************************************************/
using System;
//using System.Windows;
using System.Windows.Controls;
//using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
//using System.Windows.Threading;

namespace TexasEntraineur
{
    class Carte
    {
        public int Valeur;
        public int Sorte;
        public string NomPhysique;
        public DoubleAnimation posLeft;
        public DoubleAnimation posTop;
        public Image imgCarte;

        

        public Carte()
        {
            Valeur = -1;
            Sorte = -1;
             BitmapImage bmi = new BitmapImage();
            bmi.BeginInit();
            bmi.UriSource = new Uri(Configurateur.PathImage + "\\endos.gif");
            bmi.EndInit();

            imgCarte = new Image();
            imgCarte.Stretch = Stretch.Fill;
            imgCarte.Source = bmi;
        }
        
        public Carte(int v, int s)
        {
            Valeur = v;
            Sorte = s;
        }

        public void InitAnimation(int joueur, int posCarte)
        {
            posLeft = new DoubleAnimation();
            if (joueur < 3)
                posLeft.To = 100 + (joueur * 250) + (posCarte * 25);
            else
                posLeft.To = 900 - (joueur - 3) * 150 - (joueur * 100) + (posCarte * 25);
            posLeft.Duration = TimeSpan.FromSeconds(0.1);

            posTop = new DoubleAnimation();
            if (joueur < 3)
            {
                posTop.To = 100;
                if (joueur == 1)
                    posTop.To = 10;
            }
            else
            {
                posTop.To = 300;
                if (joueur == 4)
                    posTop.To = 375;
            }
            posTop.Duration = TimeSpan.FromSeconds(0.1);

            BitmapImage bmi = new BitmapImage();
            bmi.BeginInit();
            bmi.UriSource = new Uri(Configurateur.PathImage + "\\" + GetNomTextuel());
            bmi.EndInit();

            imgCarte = new Image();
            imgCarte.Stretch = Stretch.Fill;
            imgCarte.Source = bmi;
        }

        public void InitAnimation(string Etape, int noCarte)
        {
            if (Etape == "FLOP")
            {
                posLeft = new DoubleAnimation();
                posLeft.To = 175 + (noCarte * 55);
                posLeft.Duration = TimeSpan.FromSeconds(0.25);

                posTop = new DoubleAnimation();
                posTop.To = 200;
                posTop.Duration = TimeSpan.FromSeconds(0.25);

                BitmapImage bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.UriSource = new Uri(Configurateur.PathImage + "\\" + GetNomTextuel());
                bmi.EndInit();

                imgCarte = new Image();
                imgCarte.Stretch = Stretch.Fill;
                imgCarte.Source = bmi;
            }
        }
        public void InitAnimation(string Etape)
        {
            int offset;
            offset = 500;
            if (Etape == "TURN")
                offset = 400;

            posLeft = new DoubleAnimation();
            posLeft.To = offset;
            posLeft.Duration = TimeSpan.FromSeconds(0.25);

            posTop = new DoubleAnimation();
            posTop.To = 200;
            posTop.Duration = TimeSpan.FromSeconds(0.25);

            BitmapImage bmi = new BitmapImage();
            bmi.BeginInit();
            bmi.UriSource = new Uri(Configurateur.PathImage + "\\" + GetNomTextuel());
            bmi.EndInit();

            imgCarte = new Image();
            imgCarte.Stretch = Stretch.Fill;
            imgCarte.Source = bmi;
        }

        public string GetNomTextuel()
        {
            string s="", v="";
            switch (Sorte)
            {
                case -1: v = ""; break;
                case 0: s = "Pique"; break;
                case 1: s = "Trefle"; break;
                case 2: s = "Carreau"; break;
                case 3: s = "Coeur"; break;
                default: Console.WriteLine("couleur de carte inexistante: Sorte");
                    break;
            }
            switch (Valeur)
            {
                case -1: v = ""; break;
                case 0: v = "Deux"; break;
                case 1: v = "Trois"; break;
                case 2: v = "Quatre"; break;
                case 3: v = "Cinq"; break;
                case 4: v = "Six"; break;
                case 5: v = "Sept"; break;
                case 6: v = "Huit"; break;
                case 7: v = "Neuf"; break;
                case 8: v = "Dix"; break;
                case 9: v = "Valet"; break;
                case 10: v = "Reine"; break;
                case 11: v = "Roi"; break;
                case 12: v = "As"; break;
                default: Console.WriteLine("valeur de carte inexistante:" + Valeur);
                    break;
            }
            return s + "_" + v + ".gif";
        }

        public string afficheTexte()
        {
            string CarteTexte = "";
            switch (Valeur)
            {
                case -1: break;
                case 0: CarteTexte ="2"; break;
                case 1: CarteTexte ="3"; break;
                case 2: CarteTexte ="4"; break;
                case 3: CarteTexte ="5"; break;
                case 4: CarteTexte ="6"; break;
                case 5: CarteTexte ="7"; break;
                case 6: CarteTexte ="8"; break;
                case 7: CarteTexte ="9"; break;
                case 8: CarteTexte ="10"; break;
                case 9: CarteTexte ="J"; break;
                case 10: CarteTexte ="Q"; break;
                case 11: CarteTexte ="K"; break;
                case 12: CarteTexte ="A"; break;
                default: CarteTexte ="valeur de carte inexistante:" + Valeur; break;
            }
             switch (Sorte)
            {
                case -1: break;
                case 0: CarteTexte += " de pique"; break;
                case 1: CarteTexte += " de trèfle"; break;
                case 2: CarteTexte += " de carreau"; break;
                case 3: CarteTexte += " de coeur"; break;
                default: CarteTexte += " couleur de carte inexistante:" + Sorte;break;
            }
            return CarteTexte;
        }
    }

    class cPaquet
    {
        Carte[] cartes         = new Carte[52];
        Carte[] cartesBrassees = new Carte[52];
        int indiceCarteCourante;
        
        public cPaquet()
        {
            int i = 0;
            for (int sor = 0; sor < 4; sor++)
            {
                for (int val = 0; val < 13; val++)
                {
                    cartes[i] = new Carte(val, sor);
                    i++;
                }
            }
            indiceCarteCourante = 0;
        }

        public void affiche()
        {
            for (int i = 0; i < cartes.Length; i++)
            {
                cartes[i].afficheTexte();
            }
        }

        public void brasse()
        {
            int[] TabValChoisie = new int[52];
            int AleaTmp = -1;
            int AleaTmpTmp = 0;
            int indice=-1;
            string etat=null;

            for (int i = 0 ; i < 52; i++) {TabValChoisie[i] = -1;}
            for (int i = 0 ; i < 52; i++) 
            {
                bool TrouveProchaine = false;
                while (!TrouveProchaine)
                {
                    DateTime DTCourant = DateTime.Now; 
                    int monTick = (int) DTCourant.Ticks;
                    Random r = new Random(monTick);
                    AleaTmpTmp = r.Next(0,52);
                    if (AleaTmp != AleaTmpTmp)
                        AleaTmp = AleaTmpTmp;
                    else
                        continue;
                   // Console.Write(" " + AleaTmp + " ");

                    for (int j=0; j < 52; j++)
                    {
                        if (TabValChoisie[j] == -1)
                        {
                            indice = j;
                            etat = "OK";      
                            break;                        
                        }    
                        else
                        {
                            if (TabValChoisie[j] == AleaTmp)
                            {
                                etat = "DEJA_PRIS";
                                break;
                            }
                        }
                    }
                    if (etat == "OK")
                    {
                        TabValChoisie[indice] = AleaTmp;
                        TrouveProchaine = true;
                    }  
                }
            }
            for (int i = 0;  i < 52; i++) 
            {    
                    cartesBrassees[i] = cartes[TabValChoisie[i]];
            }
          }

            public Carte donneProchaineCarte()
            {
                indiceCarteCourante++;
                return cartesBrassees[indiceCarteCourante - 1];
            }
        }

        class uneMain
        {
            public Carte[] mainOrigine;
            public Carte[] mainTriee;
            public cEvaluateur Eval;

            public uneMain(Carte[] lamain)
            {
                mainTriee = new Carte[7];
                for (int i = 0; i < 7; i++)
                {
                    mainOrigine[i] = lamain[i];
                }
                Trier();
                Eval = new cEvaluateur();
            }

            public uneMain()
            {
                mainOrigine = new Carte[7];
                for (int i = 0; i < 7; i++)
                {
                    mainOrigine[i] = new Carte();
                }
                Eval = new cEvaluateur();
            }

            uneMain(Carte c0, Carte c1, Carte c2, Carte c3, Carte c4)
            {
                mainOrigine[0] = c0;
                mainOrigine[1] = c1;
                mainOrigine[2] = c2;
                mainOrigine[3] = c3;
                mainOrigine[4] = c4;
            }

            public int DonneValeur()
            {      
               int[] v = new int[5];
               int[] s = new int[5];
               int  res=-1;
               for (int i=0; i < 5; i++)
               {
                   v[i] = mainTriee[i].Valeur;
                   s[i] = mainTriee[i].Sorte;
               }  
               res = Eval.Eval_Seq_Flush(v,s) ; if (res>0){return res;}
               res = Eval.Eval_Carre(v, s); if (res > 0) { return res; }
               res = Eval.Eval_Full(v, s); if (res > 0) { return res; }
               res = Eval.Eval_Flush(v, s); if (res > 0) { return res; }
               res = Eval.Eval_Seq(v, s); if (res > 0) { return res; }
               res = Eval.Eval_Brelan(v, s); if (res > 0) { return res; }
               res = Eval.Eval_Deux_Paires(v, s); if (res > 0) { return res; }
               res = Eval.Eval_Paire(v, s); if (res > 0) { return res; }
               res = Eval.Eval_Rien(v, s); if (res > 0) { return res; }
               return res;
            }

            public void Trier()
            {
               mainTriee = new Carte[5];
               int LaPlusGrosse = -2;
               int IndicePlusGrosse = -1;
               int[] ValeurUtilisée = new int[52];
   
               for (int i=0; i<5; i++) {ValeurUtilisée[i] = -1;}
   
               for (int i=0; i<5; i++)
               {    
                   for (int j=0; j<5; j++)
                   {
                       int val = mainOrigine[j].Valeur;            
                       if ( mainOrigine[j].Valeur > LaPlusGrosse)
                       {  
                           if ( ValeurUtilisée[j] == -1)
                           {
                               LaPlusGrosse = mainOrigine[j].Valeur;
                               IndicePlusGrosse = j;
                           }    
                       }    
                   }
                   ValeurUtilisée[IndicePlusGrosse] = 0;
                   mainTriee[i] = mainOrigine[IndicePlusGrosse];
                   LaPlusGrosse = -2;
               }
            }

        }
    }


