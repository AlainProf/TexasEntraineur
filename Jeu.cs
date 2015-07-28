/****************************************************/
/*   Auteur:   Alain Martel 
/*   Projet:   PokerTexas, 
/*   Desc:     Un moteur de poker Texas Hold'Hem
/*   Création: 2013-juin 
/*
/*   Fichier:	Jeu.cs
/*   Desc:		Définition de la classe Jeu
/***************************************************/

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace TexasEntraineur
{
    class cJeu
    {
        
        public cPaquet lePaquet;
        static int NbJoueurs = 6;
        public uneMain[] MainDesJoueurs;
        public Carte[] Flop;
        public Carte Turn;
        public Carte River;
        private Configurateur cfg;

        public event EventHandler PreFlopTermine;
        public event EventHandler FlopTermine;
        public event EventHandler TurnTermine;
        public event EventHandler RiverTermine;
        public event EventHandler EstimationTermine;

        protected virtual void OnVerificationPreFlop(EventArgs e)
        {
            if (PreFlopTermine != null)
                PreFlopTermine(this, e);
        }

        protected virtual void OnVerificationFlop(EventArgs e)
        {
            if (FlopTermine != null)
                FlopTermine(this, e);
        }

        protected virtual void OnVerificationTurn(EventArgs e)
        {
            if (TurnTermine != null)
                TurnTermine(this, e);
        }
        protected virtual void OnVerificationRiver(EventArgs e)
        {
            if (RiverTermine != null)
                RiverTermine(this, e);
        }
        protected virtual void OnVerificationEstimation(EventArgs e)
        {
            if (EstimationTermine != null)
                EstimationTermine(this, e);
        }
        /// ///////////////////////////////////////////////
/// </summary>

        public bool animationEnCours;

        private Storyboard[] scenarioPreFlop = new Storyboard[12];
        private Storyboard[] scenarioFlop = new Storyboard[3];
        private Storyboard scenarioTurn = new Storyboard();
        private Storyboard scenarioRiver = new Storyboard();

        public cEvaluateur Eval;
        public cJeu(Configurateur cfg_param)
        {
            MainDesJoueurs = new uneMain[NbJoueurs]; 
            Flop = new Carte[3];
            lePaquet = new cPaquet();
            Turn = new Carte();
            River = new Carte();
            lePaquet.brasse();
            Eval = new cEvaluateur();
            cfg = cfg_param;
       }

  
        public void ConstruitNomsPhysiques()
        {
            for (int i = 0; i < 4; i++)
            {
                MainDesJoueurs[i].mainOrigine[0].NomPhysique = Configurateur.PathImage + MainDesJoueurs[i].mainOrigine[0].GetNomTextuel();
                MainDesJoueurs[i].mainOrigine[1].NomPhysique = Configurateur.PathImage + MainDesJoueurs[i].mainOrigine[1].GetNomTextuel();
            }
            Flop[0].NomPhysique = Configurateur.PathImage + Flop[0].GetNomTextuel();
            Flop[1].NomPhysique = Configurateur.PathImage + Flop[1].GetNomTextuel();
            Flop[2].NomPhysique = Configurateur.PathImage + Flop[2].GetNomTextuel();
            Turn.NomPhysique = Configurateur.PathImage + Turn.GetNomTextuel();
            River.NomPhysique = Configurateur.PathImage + River.GetNomTextuel();
        }

        public void animationPreFlop()
        {
            int i_scn = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    scenarioPreFlop[i_scn] = new Storyboard();
                    Storyboard.SetTarget(MainDesJoueurs[i].mainOrigine[j].posLeft, MainDesJoueurs[i].mainOrigine[j].imgCarte);
                    Storyboard.SetTargetProperty(MainDesJoueurs[i].mainOrigine[j].posLeft, new PropertyPath("(Canvas.Left)"));
                    scenarioPreFlop[i_scn].Children.Add(MainDesJoueurs[i].mainOrigine[j].posLeft);

                    Storyboard.SetTarget(MainDesJoueurs[i].mainOrigine[j].posTop, MainDesJoueurs[i].mainOrigine[j].imgCarte);
                    Storyboard.SetTargetProperty(MainDesJoueurs[i].mainOrigine[j].posTop, new PropertyPath("(Canvas.Top)"));
                    scenarioPreFlop[i_scn].Children.Add(MainDesJoueurs[i].mainOrigine[j].posTop);


                
                    scenarioPreFlop[i_scn].Duration = TimeSpan.FromSeconds(Convert.ToDouble(cfg.TempsDonnerCarte));
                    i_scn++;
                }
            }
            scenarioPreFlop[0].Completed += scenarioPreFlop0_Completed;
            scenarioPreFlop[1].Completed += scenarioPreFlop1_Completed;
            scenarioPreFlop[2].Completed += scenarioPreFlop2_Completed;
            scenarioPreFlop[3].Completed += scenarioPreFlop3_Completed;
            scenarioPreFlop[4].Completed += scenarioPreFlop4_Completed;
            scenarioPreFlop[5].Completed += scenarioPreFlop5_Completed;
            scenarioPreFlop[6].Completed += scenarioPreFlop6_Completed;
            scenarioPreFlop[7].Completed += scenarioPreFlop7_Completed;
            scenarioPreFlop[8].Completed += scenarioPreFlop8_Completed;
            scenarioPreFlop[9].Completed += scenarioPreFlop9_Completed;
            scenarioPreFlop[10].Completed += scenarioPreFlop10_Completed;
            scenarioPreFlop[11].Completed += scenarioPreFlop11_Completed;
            
            scenarioPreFlop[0].Begin();
        }

        public void animationFlop()
        {
            string Val = cfg.TempsDonnerCarte;
            double TDC = Convert.ToDouble(Val);

            int i_scn = 0;
            for (int i = 0; i < 3; i++)
            {
                scenarioFlop[i_scn] = new Storyboard();
                Storyboard.SetTarget(Flop[i].posLeft, Flop[i].imgCarte);
                Storyboard.SetTargetProperty(Flop[i].posLeft, new PropertyPath("(Canvas.Left)"));
                scenarioFlop[i_scn].Children.Add(Flop[i].posLeft);

                Storyboard.SetTarget(Flop[i].posTop, Flop[i].imgCarte);
                Storyboard.SetTargetProperty(Flop[i].posTop, new PropertyPath("(Canvas.Top)"));
                scenarioFlop[i_scn].Children.Add(Flop[i].posTop);

                scenarioFlop[i_scn].Duration = TimeSpan.FromSeconds(TDC);
                i_scn++;
            }
            scenarioFlop[0].Completed += scenarioFlop0_Completed;
            scenarioFlop[1].Completed += scenarioFlop1_Completed;
            scenarioFlop[2].Completed += scenarioFlop2_Completed;
            scenarioFlop[0].Begin();
        }

        public void animationTurn()
        {
            string Val = cfg.TempsDonnerCarte;
            double TDC = Convert.ToDouble(Val);

            scenarioTurn = new Storyboard();
            Storyboard.SetTarget(Turn.posLeft, Turn.imgCarte);
            Storyboard.SetTargetProperty(Turn.posLeft, new PropertyPath("(Canvas.Left)"));
            scenarioTurn.Children.Add(Turn.posLeft);

            Storyboard.SetTarget(Turn.posTop, Turn.imgCarte);
            Storyboard.SetTargetProperty(Turn.posTop, new PropertyPath("(Canvas.Top)"));
            scenarioTurn.Children.Add(Turn.posTop);
            scenarioTurn.Duration = TimeSpan.FromSeconds(TDC);
            scenarioTurn.Completed += scenarioTurn_Completed;
            scenarioTurn.Begin();
        }

        public void animationRiver()
        {
            string Val = cfg.TempsDonnerCarte;
            double TDC = Convert.ToDouble(Val);

            scenarioRiver = new Storyboard();
            Storyboard.SetTarget(River.posLeft, River.imgCarte);
            Storyboard.SetTargetProperty(River.posLeft, new PropertyPath("(Canvas.Left)"));
            scenarioRiver.Children.Add(River.posLeft);

            Storyboard.SetTarget(River.posTop, River.imgCarte);
            Storyboard.SetTargetProperty(River.posTop, new PropertyPath("(Canvas.Top)"));
            scenarioRiver.Children.Add(River.posTop);
            scenarioRiver.Duration = TimeSpan.FromSeconds(TDC);
            scenarioRiver.Completed += scenarioRiver_Completed;
            scenarioRiver.Begin();
        }

        private void scenarioFlop0_Completed(object o, EventArgs e)
        {
            scenarioFlop[1].Begin();
        }
        private void scenarioFlop1_Completed(object o, EventArgs e)
        {
            scenarioFlop[2].Begin();
        }
        private void scenarioFlop2_Completed(object o, EventArgs e)
        {
            EventArgs p = new EventArgs();
            OnVerificationFlop(p);
        }
        private void scenarioTurn_Completed(object o, EventArgs e)
        {
            EventArgs p = new EventArgs();
            OnVerificationTurn(p);
        }
        private void scenarioRiver_Completed(object o, EventArgs e)
        {
            EventArgs p = new EventArgs();
            OnVerificationRiver(p);
        }

        private void scenarioPreFlop0_Completed(object o, EventArgs e)
        {
            scenarioPreFlop[2].Begin();
        }
        private void scenarioPreFlop2_Completed(object o, EventArgs e)
        {
            scenarioPreFlop[4].Begin();
        }
        private void scenarioPreFlop4_Completed(object o, EventArgs e)
        {
            scenarioPreFlop[6].Begin();
        }
        private void scenarioPreFlop6_Completed(object o, EventArgs e)
        {
            scenarioPreFlop[8].Begin();
        }
        private void scenarioPreFlop8_Completed(object o, EventArgs e)
        {
            scenarioPreFlop[10].Begin();
        }
        private void scenarioPreFlop10_Completed(object o, EventArgs e)
        {
            scenarioPreFlop[1].Begin();
        }
        private void scenarioPreFlop1_Completed(object o, EventArgs e)
        {
            scenarioPreFlop[3].Begin();
        }
        private void scenarioPreFlop3_Completed(object o, EventArgs e)
        {
            scenarioPreFlop[5].Begin();
        }
        private void scenarioPreFlop5_Completed(object o, EventArgs e)
        {
            scenarioPreFlop[7].Begin();
        }
        private void scenarioPreFlop7_Completed(object o, EventArgs e)
        {
            scenarioPreFlop[9].Begin();
        }
        private void scenarioPreFlop9_Completed(object o, EventArgs e)
        {
            scenarioPreFlop[11].Begin();
        }
        private void scenarioPreFlop11_Completed(object o, EventArgs e)
        {
            EventArgs p = new EventArgs();
            OnVerificationPreFlop(p);
        }
        ///////////////////////////////////////


        public string afficheFlop()
        {
           string infoFlop = " FLOP:\n";
           infoFlop += Flop[0].afficheTexte();
           infoFlop += " ";
           infoFlop += Flop[1].afficheTexte();
           infoFlop += " ";
           infoFlop += Flop[2].afficheTexte();
           infoFlop += "\n";
           return infoFlop;
        }

        public string afficheTurn()
        {
            string infoTurn = " TURN:\n";
            infoTurn += Turn.afficheTexte();
            infoTurn += "\n";
            return infoTurn;
        }

        public string afficheRiver()
        {
            string infoRiver = " RIVER:\n";
            infoRiver += River.afficheTexte();
            infoRiver += "\n";
            return infoRiver;
        }
 
        public void distribueMains()
        {
            for (int j = 0; j < NbJoueurs; j++)
            {
                MainDesJoueurs[j] = new uneMain();
                MainDesJoueurs[j].mainOrigine[0] = lePaquet.donneProchaineCarte();
                MainDesJoueurs[j].mainOrigine[0].InitAnimation(j, 0);
            }
            for (int j = 0; j < NbJoueurs; j++)
            {
                MainDesJoueurs[j].mainOrigine[1] = lePaquet.donneProchaineCarte();
                MainDesJoueurs[j].mainOrigine[1].InitAnimation(j, 1);
            }
        }
 
        public void distribueFlop()
        {
            for (int i=0; i<3; i++)
            {
                Flop[i] = new Carte();
                Flop[i] = lePaquet.donneProchaineCarte();
                Flop[i].InitAnimation("FLOP", i);
            }
        }

        public void distribueFlop(int vf0, int sf0, int vf1, int sf1, int vf2, int sf2)
        {
        Carte flop0 = new Carte(vf0,sf0);
        Carte flop1 = new Carte(vf1,sf1);
        Carte flop2 = new Carte(vf2, sf2);

            Flop[0] = flop0;
            Flop[1] = flop1;
            Flop[2] = flop2;
            for (int i=0; i<3; i++)
            {
              Flop[i].GetNomTextuel();
            }           
        }

        public void distribueTurn()
        {
            Turn = new Carte();
            Turn = lePaquet.donneProchaineCarte();
            Turn.InitAnimation("TURN");
        }

        public void distribueTurn(int vt, int st)
        {
            Carte TurnTmp = new Carte(vt, st);
           Turn = TurnTmp;
           Turn.GetNomTextuel();
        }

        public void distribueRiver()
        {
            River = new Carte();
            River = lePaquet.donneProchaineCarte();
            River.InitAnimation("RIVER");
        }

        public void distribueRiver(int vr, int sr)
        {
            Carte RiverTmp = new Carte(vr, sr);
           River = RiverTmp;
           River.GetNomTextuel();
        }


        public string determineGagnant()
        {
           string infoGagnant = "Le gagnant est:";
           uneMain[] LesMains = new uneMain[6];
           LesMains[0] = MainDesJoueurs[0];
           LesMains[1] = MainDesJoueurs[1];
           LesMains[2] = MainDesJoueurs[2];
           LesMains[3] = MainDesJoueurs[3];
           LesMains[4] = MainDesJoueurs[4];
           LesMains[5] = MainDesJoueurs[5];
         

           LesMains[0].mainOrigine[2] = Flop[0];
           LesMains[0].mainOrigine[3] = Flop[1];
           LesMains[0].mainOrigine[4] = Flop[2];
           LesMains[1].mainOrigine[2] = Flop[0];
           LesMains[1].mainOrigine[3] = Flop[1];
           LesMains[1].mainOrigine[4] = Flop[2];
           LesMains[2].mainOrigine[2] = Flop[0];
           LesMains[2].mainOrigine[3] = Flop[1];
           LesMains[2].mainOrigine[4] = Flop[2];
           LesMains[3].mainOrigine[2] = Flop[0];
           LesMains[3].mainOrigine[3] = Flop[1];
           LesMains[3].mainOrigine[4] = Flop[2];
           LesMains[4].mainOrigine[2] = Flop[0];
           LesMains[4].mainOrigine[3] = Flop[1];
           LesMains[4].mainOrigine[4] = Flop[2];
           LesMains[5].mainOrigine[2] = Flop[0];
           LesMains[5].mainOrigine[3] = Flop[1];
           LesMains[5].mainOrigine[4] = Flop[2];

           LesMains[0].mainOrigine[5] = Turn;
           LesMains[1].mainOrigine[5] = Turn;
           LesMains[2].mainOrigine[5] = Turn;
           LesMains[3].mainOrigine[5] = Turn;
           LesMains[4].mainOrigine[5] = Turn;
           LesMains[5].mainOrigine[5] = Turn;

           LesMains[0].mainOrigine[6] = River;
           LesMains[1].mainOrigine[6] = River;
           LesMains[2].mainOrigine[6] = River;
           LesMains[3].mainOrigine[6] = River;
           LesMains[4].mainOrigine[6] = River;
           LesMains[5].mainOrigine[6] = River;
   
           int valeurJ0 = Eval.CalculeValeurPostRiver(LesMains[0].mainOrigine);
           int valeurJ1 = Eval.CalculeValeurPostRiver(LesMains[1].mainOrigine);
           int valeurJ2 = Eval.CalculeValeurPostRiver(LesMains[2].mainOrigine);
           int valeurJ3 = Eval.CalculeValeurPostRiver(LesMains[3].mainOrigine);
           int valeurJ4 = Eval.CalculeValeurPostRiver(LesMains[4].mainOrigine);
           int valeurJ5 = Eval.CalculeValeurPostRiver(LesMains[5].mainOrigine);
           int Gagnant = Eval.TrouveGagnant(valeurJ0,
                                            valeurJ1,
                                            valeurJ2,
                                            valeurJ3,
                                            valeurJ4,
                                            valeurJ5);
           infoGagnant += ConvertGagnant(Gagnant);
           infoGagnant += "\nJ0:" + Eval.ConvertEvalEnFrancais(valeurJ0) + "(" + valeurJ0 + ")";   
           infoGagnant += "\nJ1:" + Eval.ConvertEvalEnFrancais(valeurJ1) + "(" + valeurJ1 + ")";   
           infoGagnant += "\nJ2:" + Eval.ConvertEvalEnFrancais(valeurJ2) + "(" + valeurJ2 + ")";
           infoGagnant += "\nJ3:" + Eval.ConvertEvalEnFrancais(valeurJ3) + "(" + valeurJ3 + ")";
           infoGagnant += "\nJ4:" + Eval.ConvertEvalEnFrancais(valeurJ4) + "(" + valeurJ4 + ")";
           infoGagnant += "\nJ5:" + Eval.ConvertEvalEnFrancais(valeurJ5) + "(" + valeurJ5 + ")";
           return infoGagnant;
        }

        public string ConvertGagnant(int codeGagnant)
        {
            string G = "";
            byte codeGagnantBinaire = Convert.ToByte(codeGagnant);

            int r;
            r = codeGagnantBinaire & 1;
            if (r == 1)
                G += " J0";
            r = codeGagnantBinaire & 2;
            if (r == 2)
                G += " J1";
            r = codeGagnantBinaire & 4;
            if (r == 4)
                G += " J2";
            r = codeGagnantBinaire & 8;
            if (r == 8)
                G += " J3";
            r = codeGagnantBinaire & 16;
            if (r == 16)
                G += " J4";
            r = codeGagnantBinaire & 32;
            if (r == 32)
                G += " J5";
           return G;
        }
    }
}

