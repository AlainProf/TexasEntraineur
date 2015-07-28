/****************************************************/
/*   Auteur:   Alain Martel 
/*   Projet:   PokerTexas, 
/*   Desc:     Un moteur de poker Texas Hold'Hem en C#
/*   Création: 2013-juin 
/*
/*   Fichier:	Evaluateur.cs
/*   Desc:		Déclaration de la classe Evaluateur
/*              Déclaration de constantes importante
/*              dont les niveaux des mains
/***************************************************/
using System;

namespace TexasEntraineur
{
    class cEvaluateur
    {

        public static int SEQ_FLUSH = 1000000000;
        public static int CARRE = 900000000;
        public static int FULL = 800000000;
        public static int FLUSH = 700000000;
        public static int SEQ = 600000000;
        public static int BRELAN = 500000000;
        public static int DEUX_PAIRES = 300000000;
        public static int PAIRE = 150000000;
        public static int RIEN = 0;

        public cEvaluateur()
        {
        }

        public int Eval_Seq_Flush(int[] v, int[] s)
        {
            if ((v[0] != v[1]) &&
                (v[1] != v[2]) &&
                (v[2] != v[3]) &&
                (v[3] != v[4]))
            {
                if ((s[0] == s[1]) &&
                     (s[0] == s[2]) &&
                     (s[0] == s[3]) &&
                     (s[0] == s[4]))
                {
                    if (v[0] - 4 == v[4])
                    {
                        // faut ajouter +1 pour différencier 6-5-4-3-2 de A-5-4-3-2
                        int Score = SEQ_FLUSH + v[4] * 100 + 2;
                        return Score;
                    }
                }
            }

            //## SEQ FLUSH en partant de l"AS
            if ((v[0] == 12) &&
             (v[1] == 3) &&
             (v[2] == 2) &&
             (v[3] == 1) &&
             (v[4] == 0))
            {
                if ((s[0] == s[1]) &&
                   (s[0] == s[2]) &&
                   (s[0] == s[3]) &&
                   (s[0] == s[4]))
                {
                    int Score = SEQ_FLUSH + 1;
                    return Score;
                }
            }
            return 0;
        }


        /*##---------------------------------------
        #
        # Eval_Carre()  
        #
        ##---------------------------------------*/
        public int Eval_Carre(int[] v, int[] s)
        {
            int Score = 0;
            if (((v[0] == v[1]) &&
                 (v[1] == v[2]) &&
                 (v[2] == v[3])
                ) || (
                 (v[1] == v[2]) &&
                 (v[2] == v[3]) &&
                 (v[3] == v[4])))
            {
                if (v[0] == v[1])
                {
                    Score = CARRE + v[3] * 100 + v[4];
                }
                else
                {
                    Score = CARRE + v[3] * 100 + v[0];
                }
                return Score;
            }
            return 0;
        }


        public int Eval_Full(int[] v, int[] s)
        {
            if (v[2] == -1)
            {
                // cas d'une main de deux cartes
                return 0;
            }
            if ((v[0] == v[1]) &&
                 (v[1] == v[2]) &&
                 (v[3] == v[4]) &&
                 (v[2] != v[3]))
            {
                int Score = FULL + v[2] * 100 + v[4];
                return Score;
            }
            else if ((v[0] == v[1]) &&
                     (v[2] == v[3]) &&
                     (v[3] == v[4]) &&
                     (v[1] != v[2]))
            {
                int Score = FULL + v[2] * 100 + v[0];
                return Score;
            }
            return 0;
        }


        public int Eval_Flush(int[] v, int[] s)
        {
            if ((s[0] == s[1]) &&
                 (s[0] == s[2]) &&
                 (s[0] == s[3]) &&
                 (s[0] == s[4]))
            {
                if (v[0] - 4 > v[4])
                {
                    int Score = FLUSH + v[0] * 10000 + v[1] * 1000 + v[2] * 100 + v[3] * 10 + v[4];
                    return Score;
                }
            }
            return 0;
        }


        public int Eval_Seq(int[] v, int[] s)
        {
            if ((v[0] != v[1]) &&
                (v[1] != v[2]) &&
                (v[2] != v[3]) &&
                (v[3] != v[4]))
            {
                if ((s[0] != s[1]) ||
                     (s[0] != s[2]) ||
                     (s[0] != s[3]) ||
                     (s[0] != s[4]))
                {
                    if (v[0] - 4 == v[4])
                    {
                        int Score = SEQ + v[4] * 100 + 2;
                        return Score;
                    }
                }
            }

            if ((v[0] == 12) &&
                (v[1] == 3) &&
                (v[2] == 2) &&
                (v[3] == 1) &&
                (v[4] == 0))
            {
                int Score = SEQ + 1;
                return Score;
            }
            return 0;
        }


        public int Eval_Brelan(int[] v, int[] s)
        {
            int Score = 0;
            if ((v[0] == v[1]) && (v[1] == v[2]))
            {
                Score = BRELAN + v[2] * 10000 + v[3] * 100 + v[4];
                return Score;
            }
            if ((v[1] == v[2]) && (v[2] == v[3]))
            {
                Score = BRELAN + v[2] * 10000 + v[0] * 100 + v[4];
                return Score;
            }
            if ((v[2] == v[3]) && (v[3] == v[4]))
            {
                if (v[2] == -1)
                {
                    // cas d'une carte à -1 (ex. main de deux cartes)	
                    return 0;
                }
                else
                {
                    Score = BRELAN + v[2] * 10000 + v[0] * 100 + v[1];
                    return Score;
                }
            }
            return 0;
        }


        public int Eval_Deux_Paires(int[] v, int[] s)
        {
            int Score = 0;
            if (v[2] == -1)
            {
                return Score;
            }
            if ((v[0] == v[1]) &&
                 (v[2] == v[3]) &&
                 (v[1] != v[2]))
            {
                Score = DEUX_PAIRES + v[0] * 10000 + v[2] * 100 + v[4];
                return Score;
            }
            if ((v[0] == v[1]) &&
                 (v[3] == v[4]) &&
                 (v[1] != v[3]))
            {
                Score = DEUX_PAIRES + v[0] * 10000 + v[3] * 100 + v[2];
                return Score;
            }
            if ((v[1] == v[2]) &&
                 (v[3] == v[4]) &&
                 (v[2] != v[3]))
            {
                Score = DEUX_PAIRES + v[1] * 10000 + v[3] * 100 + v[0];
                return Score;
            }
            return 0;
        }


        public int Eval_Paire(int[] v, int[] s)
        {
            int Score = 0;
            if ((v[0] == v[1]) && (v[2] == -1))
            {
                Score = PAIRE + v[1] * 1000000;
                return Score;
            }
            if ((v[0] == v[1]) &&
                 (v[1] != v[2]) &&
                 (v[1] != v[3]) &&
                 (v[1] != v[4]))
            {
                Score = PAIRE + v[1] * 1000000 + v[2] * 10000 + v[3] * 100 + v[4];
                return Score;
            }
            if ((v[1] == v[2]) &&
                     (v[1] != v[0]) &&
                     (v[1] != v[3]) &&
                     (v[1] != v[4]))
            {
                Score = PAIRE + v[2] * 1000000 + v[0] * 10000 + v[3] * 100 + v[4];
                return Score;
            }
            else if ((v[2] == v[3]) &&
                     (v[2] != v[0]) &&
                     (v[2] != v[1]) &&
                     (v[2] != v[4]))
            {
                Score = PAIRE + v[3] * 1000000 + v[0] * 10000 + v[1] * 100 + v[4];
                return Score;
            }
            else if ((v[3] == v[4]) &&
                     (v[3] != v[0]) &&
                     (v[3] != v[1]) &&
                     (v[3] != v[2]))
            {
                Score = PAIRE + v[4] * 1000000 + v[0] * 10000 + v[1] * 100 + v[2];
                return Score;
            }
            return 0;
        }

        public int Eval_Rien(int[] v, int[] s)
        {
            int Score;
            if (v[2] == -1)
            {
                Score = v[0] * 10000000 + v[1] * 100000;
            }
            else
            {
                Score = v[0] * 10000000 + v[1] * 100000 + v[2] * 1000 + v[3] * 10 + v[4];
            }
            return Score;
        }

        public int CalculeValeurPostRiver(Carte[] desCartes)
        {
            int[] val = new int[21];
            uneMain mainTmp = new uneMain();
            // 21 mains possibles :
            /* 
            0				J0	J1	F	F1	F3		
            1               J0	J1	F	F1		T	
            2               J0	J1	F	F1			R
            3               J0	J1	F		F3	T	
            4               J0	J1	F		F3		R
            5               J0	J1	F			T	R
            6               J0	J1		F1	F3	T	
            7               J0	J1		F1	F3		R
            8               J0	J1		F1		T	R
            9               J0	J1			F3	T	R
            10               J0		F	F1	F3	T	
            11               J0		F	F1	F3		R
            12               J0		F	F1		T	R
            13               J0		F		F3	T	R
            14               J0			F1	F3	T	R
            15                  J1	F	F1	F3	T	
            16                  J1	F	F1	F3		R
            17                  J1	F	F1		T	R
            18                  J1	F		F3	T	R
            19                  J1		F1	F3	T	R
            20                     F	F1	F3	T	R
            //////////////////////////////////////////////////*/

            //cas 1:    J0	J1	F	F1	F3	

            mainTmp.mainOrigine[0] = desCartes[0];
            mainTmp.mainOrigine[1] = desCartes[1];
            mainTmp.mainOrigine[2] = desCartes[2];
            mainTmp.mainOrigine[3] = desCartes[3];
            mainTmp.mainOrigine[4] = desCartes[4];
            mainTmp.Trier();
            val[0] = mainTmp.DonneValeur();

            //cas 2:  J0	J1	F	F1		T	
            mainTmp.mainOrigine[0] = desCartes[0];
            mainTmp.mainOrigine[1] = desCartes[1];
            mainTmp.mainOrigine[2] = desCartes[2];
            mainTmp.mainOrigine[3] = desCartes[3];
            mainTmp.mainOrigine[4] = desCartes[5];
            mainTmp.Trier();
            val[1] = mainTmp.DonneValeur();

            //cas 3:  J0	J1	F	F1			R
            mainTmp.mainOrigine[0] = desCartes[0];
            mainTmp.mainOrigine[1] = desCartes[1];
            mainTmp.mainOrigine[2] = desCartes[2];
            mainTmp.mainOrigine[3] = desCartes[3];
            mainTmp.mainOrigine[4] = desCartes[6];
            mainTmp.Trier();
            val[2] = mainTmp.DonneValeur();

            //      J0	J1	F		F3	T	
            mainTmp.mainOrigine[0] = desCartes[0];
            mainTmp.mainOrigine[1] = desCartes[1];
            mainTmp.mainOrigine[2] = desCartes[2];
            mainTmp.mainOrigine[3] = desCartes[4];
            mainTmp.mainOrigine[4] = desCartes[5];
            mainTmp.Trier();
            val[3] = mainTmp.DonneValeur();

            //      J0	J1	F		F3		R
            mainTmp.mainOrigine[0] = desCartes[0];
            mainTmp.mainOrigine[1] = desCartes[1];
            mainTmp.mainOrigine[2] = desCartes[2];
            mainTmp.mainOrigine[3] = desCartes[4];
            mainTmp.mainOrigine[4] = desCartes[6];
            mainTmp.Trier();
            val[4] = mainTmp.DonneValeur();

            //      J0	J1	F			T	R
            mainTmp.mainOrigine[0] = desCartes[0];
            mainTmp.mainOrigine[1] = desCartes[1];
            mainTmp.mainOrigine[2] = desCartes[2];
            mainTmp.mainOrigine[3] = desCartes[5];
            mainTmp.mainOrigine[4] = desCartes[6];
            mainTmp.Trier();
            val[5] = mainTmp.DonneValeur();

            //      J0	J1		F1	F3	T	
            mainTmp.mainOrigine[0] = desCartes[0];
            mainTmp.mainOrigine[1] = desCartes[1];
            mainTmp.mainOrigine[2] = desCartes[3];
            mainTmp.mainOrigine[3] = desCartes[4];
            mainTmp.mainOrigine[4] = desCartes[5];
            mainTmp.Trier();
            val[6] = mainTmp.DonneValeur();

            //      J0	J1		F1	F3		R
            mainTmp.mainOrigine[0] = desCartes[0];
            mainTmp.mainOrigine[1] = desCartes[1];
            mainTmp.mainOrigine[2] = desCartes[3];
            mainTmp.mainOrigine[3] = desCartes[4];
            mainTmp.mainOrigine[4] = desCartes[6];
            mainTmp.Trier();
            val[7] = mainTmp.DonneValeur();

            //      J0	J1		F1		T	R
            mainTmp.mainOrigine[0] = desCartes[0];
            mainTmp.mainOrigine[1] = desCartes[1];
            mainTmp.mainOrigine[2] = desCartes[3];
            mainTmp.mainOrigine[3] = desCartes[5];
            mainTmp.mainOrigine[4] = desCartes[6];
            mainTmp.Trier();
            val[8] = mainTmp.DonneValeur();

            //      J0	J1			F3	T	R
            mainTmp.mainOrigine[0] = desCartes[0];
            mainTmp.mainOrigine[1] = desCartes[1];
            mainTmp.mainOrigine[2] = desCartes[4];
            mainTmp.mainOrigine[3] = desCartes[5];
            mainTmp.mainOrigine[4] = desCartes[6];
            mainTmp.Trier();
            val[9] = mainTmp.DonneValeur();

            //      J0		F	F1	F3	T	
            mainTmp.mainOrigine[0] = desCartes[0];
            mainTmp.mainOrigine[1] = desCartes[2];
            mainTmp.mainOrigine[2] = desCartes[3];
            mainTmp.mainOrigine[3] = desCartes[4];
            mainTmp.mainOrigine[4] = desCartes[5];
            mainTmp.Trier();
            val[10] = mainTmp.DonneValeur();

            //      J0		F	F1	F3		R
            mainTmp.mainOrigine[0] = desCartes[0];
            mainTmp.mainOrigine[1] = desCartes[2];
            mainTmp.mainOrigine[2] = desCartes[3];
            mainTmp.mainOrigine[3] = desCartes[4];
            mainTmp.mainOrigine[4] = desCartes[6];
            mainTmp.Trier();
            val[11] = mainTmp.DonneValeur();

            //      J0		F	F1		T	R
            mainTmp.mainOrigine[0] = desCartes[0];
            mainTmp.mainOrigine[1] = desCartes[2];
            mainTmp.mainOrigine[2] = desCartes[3];
            mainTmp.mainOrigine[3] = desCartes[5];
            mainTmp.mainOrigine[4] = desCartes[6];
            mainTmp.Trier();
            val[12] = mainTmp.DonneValeur();

            //      J0		F		F3	T	R
            mainTmp.mainOrigine[0] = desCartes[0];
            mainTmp.mainOrigine[1] = desCartes[2];
            mainTmp.mainOrigine[2] = desCartes[4];
            mainTmp.mainOrigine[3] = desCartes[5];
            mainTmp.mainOrigine[4] = desCartes[6];
            mainTmp.Trier();
            val[13] = mainTmp.DonneValeur();

            //      J0			F1	F3	T	R
            mainTmp.mainOrigine[0] = desCartes[0];
            mainTmp.mainOrigine[1] = desCartes[3];
            mainTmp.mainOrigine[2] = desCartes[4];
            mainTmp.mainOrigine[3] = desCartes[5];
            mainTmp.mainOrigine[4] = desCartes[6];
            mainTmp.Trier();
            val[14] = mainTmp.DonneValeur();

            //      J1	F	F1	F3	T	
            mainTmp.mainOrigine[0] = desCartes[1];
            mainTmp.mainOrigine[1] = desCartes[2];
            mainTmp.mainOrigine[2] = desCartes[3];
            mainTmp.mainOrigine[3] = desCartes[4];
            mainTmp.mainOrigine[4] = desCartes[5];
            mainTmp.Trier();
            val[15] = mainTmp.DonneValeur();

            //      J1	F	F1	F3		R
            mainTmp.mainOrigine[0] = desCartes[1];
            mainTmp.mainOrigine[1] = desCartes[2];
            mainTmp.mainOrigine[2] = desCartes[3];
            mainTmp.mainOrigine[3] = desCartes[4];
            mainTmp.mainOrigine[4] = desCartes[6];
            mainTmp.Trier();
            val[16] = mainTmp.DonneValeur();

            //      J1	F	F1		T	R
            mainTmp.mainOrigine[0] = desCartes[1];
            mainTmp.mainOrigine[1] = desCartes[2];
            mainTmp.mainOrigine[2] = desCartes[3];
            mainTmp.mainOrigine[3] = desCartes[5];
            mainTmp.mainOrigine[4] = desCartes[6];
            mainTmp.Trier();
            val[17] = mainTmp.DonneValeur();

            //      J1	F		F3	T	R
            mainTmp.mainOrigine[0] = desCartes[1];
            mainTmp.mainOrigine[1] = desCartes[2];
            mainTmp.mainOrigine[2] = desCartes[4];
            mainTmp.mainOrigine[3] = desCartes[5];
            mainTmp.mainOrigine[4] = desCartes[6];
            mainTmp.Trier();
            val[18] = mainTmp.DonneValeur();

            //      J1		F1	F3	T	R
            mainTmp.mainOrigine[0] = desCartes[1];
            mainTmp.mainOrigine[1] = desCartes[3];
            mainTmp.mainOrigine[2] = desCartes[4];
            mainTmp.mainOrigine[3] = desCartes[5];
            mainTmp.mainOrigine[4] = desCartes[6];
            mainTmp.Trier();
            val[19] = mainTmp.DonneValeur();

            //      F	F1	F3	T	R
            mainTmp.mainOrigine[0] = desCartes[2];
            mainTmp.mainOrigine[1] = desCartes[3];
            mainTmp.mainOrigine[2] = desCartes[4];
            mainTmp.mainOrigine[3] = desCartes[5];
            mainTmp.mainOrigine[4] = desCartes[6];
            mainTmp.Trier();
            val[20] = mainTmp.DonneValeur();

            int plusGrosseMain = 0;
            int indicePlusGrosseMain = -1;
            for (int i = 0; i < 21; i++)
            {
                if (val[i] > plusGrosseMain)
                {
                    plusGrosseMain = val[i];
                    indicePlusGrosseMain = i;
                }
            }
            return plusGrosseMain;
        }

        public string ConvertEvalEnFrancais(int Eval)
        {
            string valF = "";

            if (Eval < PAIRE) { valF = DecodeRIEN(Eval); return valF; }
            if (Eval > SEQ_FLUSH) { valF = DecodeSEQ_FLUSH(Eval); return valF; }
            if (Eval > CARRE) { valF = DecodeCARRE(Eval); return valF; }
            if (Eval > FULL) { valF = DecodeFULL(Eval); return valF; }
            if (Eval > FLUSH) { valF = DecodeFLUSH(Eval); return valF; }
            if (Eval > SEQ) { valF = DecodeSEQ(Eval); return valF; }
            if (Eval > BRELAN) { valF = DecodeBRELAN(Eval); return valF; }
            if (Eval > DEUX_PAIRES) { valF = DecodeDEUX_PAIRES(Eval); return valF; }
            if (Eval > PAIRE) { valF = DecodePAIRE(Eval); return valF; }
            if (Eval == PAIRE) { valF = "Paire de deux"; return valF; }
            else { valF = "erreur 234 ??"; return valF; }
        }

        public int TrouveGagnant(int j0, int j1, int j2, int j3, int j4, int j5)
        {
            int[] TabValeur = new int[6];
            TabValeur[0] = j0;
            TabValeur[1] = j1;
            TabValeur[2] = j2;
            TabValeur[3] = j3;
            TabValeur[4] = j4;
            TabValeur[5] = j5;

            int Max = TabValeur[0];
            for (int i = 1; i < 6; i++)
            {
                if (TabValeur[i] > Max)
                    Max = TabValeur[i];
            }
            
            int CodeGagnant = 0;
            for (int i = 0; i < 6; i++)
            {
                if (TabValeur[i] == Max)
                    CodeGagnant += Convert.ToInt32(Math.Pow(2,Convert.ToDouble(i)));    
            }
            return CodeGagnant;
        }

 string DecodeRIEN(int score)
{
	string valRetour = " rien:";
    int[] reste=new int[5];

    reste[0] = score / 10000000;
  	switch (reste[0]){
		case 12 : valRetour += " as"; break; 
		case 11 : valRetour += " roi"; break; 
		case 10 : valRetour += " dame"; break; 
		case 9 : valRetour += " valet"; break; 
		case 8 : valRetour += " dix"; break; 
		case 7 : valRetour += " neuf"; break; 
		case 6 : valRetour += " huit"; break; 
		case 5 : valRetour += " sept"; break; 
		case 4 : valRetour += " six"; break; 
		case 3 : valRetour += " cinq"; break; 
		case 2 : valRetour += " quatre"; break; 
		case 1 : valRetour += " trois"; break; 
		case 0 : valRetour += " deux"; break;
        default: valRetour = "Erreur 51"; break;
	}
	reste[1] = score - (reste[0] *10000000);
    reste[1] = reste[1] / 100000;
  	switch (reste[1]){
		case 11 : valRetour += ", roi"; break; 
		case 10 : valRetour += ", dame"; break; 
		case 9 : valRetour += ", valet"; break; 
		case 8 : valRetour += ", dix"; break; 
		case 7 : valRetour += ", neuf"; break; 
		case 6 : valRetour += ", huit"; break; 
		case 5 : valRetour += ", sept"; break; 
		case 4 : valRetour += ", six"; break; 
		case 3 : valRetour += ", cinq"; break; 
		case 2 : valRetour += ", quatre"; break; 
		case 1 : valRetour += ", trois"; break; 
		case 0 : valRetour += ", deux"; break;
        default: valRetour = "Erreur 51"; break;
	}

	if (score % 100000 == 0)
	{
		// main de deux cartes, faut pas vérifier les 3 autres
		return valRetour;
	}
	reste[2] = score - (reste[0] *10000000) - (reste[1] * 100000);
    reste[2] = reste[2] / 1000;
  	switch (reste[2]){
		case 11 : valRetour += ", roi"; break; 
		case 10 : valRetour += ", dame"; break; 
		case 9 : valRetour += ", valet"; break; 
		case 8 : valRetour += ", dix"; break; 
		case 7 : valRetour += ", neuf"; break; 
		case 6 : valRetour += ", huit"; break; 
		case 5 : valRetour += ", sept"; break; 
		case 4 : valRetour += ", six"; break;
		case 3 : valRetour += ", cinq"; break;
		case 2 : valRetour += ", quatre"; break;
		case 1 : valRetour += ", trois"; break;
		case 0 : valRetour += ", deux"; break;
        default: valRetour = "Erreur 15"; break;
	}
	reste[3] = (score - (reste[0] *10000000) - (reste[1] * 100000) - (reste[2] * 1000));
    reste[3] = reste[3] / 10;
  	switch (reste[3]){
		case 10 : valRetour += ", dame"; break; 
		case 9 : valRetour += ", valet"; break; 
		case 8 : valRetour += ", dix"; break; 
		case 7 : valRetour += ", neuf"; break; 
		case 6 : valRetour += ", huit"; break; 
		case 5 : valRetour += ", sept"; break; 
		case 4 : valRetour += ", six"; break;
		case 3 : valRetour += ", cinq"; break;
		case 2 : valRetour += ", quatre"; break;
		case 1 : valRetour += ", trois"; break;
		case 0 : valRetour += ", deux"; break;
        default: valRetour = "Erreur 16"; break;
	}
	reste[4] = (score - (reste[0] *10000000) - (reste[1] * 100000) - (reste[2] * 1000)- (reste[3] * 10));
  	switch (reste[4]){
		case 8 : valRetour += ", dix."; break; 
		case 7 : valRetour += ", neuf."; break; 
		case 6 : valRetour += ", huit."; break; 
		case 5 : valRetour += ", sept."; break; 
		case 4 : valRetour += ", six."; break;
		case 3 : valRetour += ", cinq."; break;
		case 2 : valRetour += ", quatre."; break;
		case 1 : valRetour += ", trois."; break;
		case 0 : valRetour += ", deux."; break;
        default: valRetour = "Erreur 17a"; break;
	}
return valRetour;
}

        string DecodePAIRE(int score)
{
string valRetour = " paire";
int[]reste = new int[5];

    reste[0] = score - PAIRE;
	reste[0] = reste[0] / 1000000; 

  	switch (reste[0]){
		case 12 : valRetour += " d'as"; break; 
		case 11 : valRetour += " de roi"; break; 
		case 10 : valRetour += " de dame"; break; 
		case 9 : valRetour += " de valet"; break; 
		case 8 : valRetour += " de dix"; break; 
		case 7 : valRetour += " de neuf"; break; 
		case 6 : valRetour += " de huit"; break; 
		case 5 : valRetour += " de sept"; break; 
		case 4 : valRetour += " de six"; break; 
		case 3 : valRetour += " de cinq"; break; 
		case 2 : valRetour += " de quatre"; break; 
		case 1 : valRetour += " de trois"; break; 
		case 0 : valRetour += " de deux"; break;
        default: valRetour += "+Erreur 51"; break;
	}
	if (score % 1000000 == 0)
	{
		// cas de la main à deux cartes, faut retourner
		return valRetour;
	}
	reste[1] = (score - PAIRE - (reste[0] * 1000000));
    reste[1] = reste[1] / 10000;
  	switch (reste[1]){
		case 12 : valRetour += ", à l'as"; break;
		case 11 : valRetour += ", au roi"; break; 
		case 10 : valRetour += ", à la dame"; break; 
		case 9 : valRetour += ", au valet"; break; 
		case 8 : valRetour += ", au dix"; break; 
		case 7 : valRetour += ", au neuf"; break; 
		case 6 : valRetour += ", au huit"; break; 
		case 5 : valRetour += ", au sept"; break; 
		case 4 : valRetour += ", au six"; break;
		case 3 : valRetour += ", au cinq"; break;
		case 2 : valRetour += " au quatre"; break; 
		case 1 : valRetour += " au trois"; break; 
		case 0 : valRetour += " au deux"; break;
        default: valRetour += "Erreur 52a"; break;
	}

	reste[2] = score - PAIRE - (reste[0] * 1000000) - (reste[1] * 10000);
    reste[2] = reste[2] / 100;
  	switch (reste[2]){
		case 12 : valRetour += ", à l'as"; break;
		case 11 : valRetour += ", au roi"; break; 
		case 10 : valRetour += ", à la dame"; break; 
		case 9 : valRetour += ", au valet"; break; 
		case 8 : valRetour += ", au dix"; break; 
		case 7 : valRetour += ", au neuf"; break; 
		case 6 : valRetour += ", au huit"; break; 
		case 5 : valRetour += ", au sept"; break; 
		case 4 : valRetour += ", au six"; break;
		case 3 : valRetour += ", au cinq"; break;
		case 2 : valRetour += " au quatre"; break; 
		case 1 : valRetour += " au trois"; break; 
		case 0 : valRetour += " au deux"; break;
        default: valRetour += "Erreur 52b"; break;
	}
	reste[3] = score - PAIRE - (reste[0] * 1000000) - (reste[1] * 10000) - (reste[2] * 100);
  	switch (reste[3]){
		case 12 : valRetour += ", à l'as"; break;
		case 11 : valRetour += ", au roi"; break; 
		case 10 : valRetour += ", à la dame"; break; 
		case 9 : valRetour += ", au valet"; break; 
		case 8 : valRetour += ", au dix"; break; 
		case 7 : valRetour += ", au neuf"; break; 
		case 6 : valRetour += ", au huit"; break; 
		case 5 : valRetour += ", au sept"; break; 
		case 4 : valRetour += ", au six"; break;
		case 3 : valRetour += ", au cinq"; break;
		case 2 : valRetour += " au quatre"; break; 
		case 1 : valRetour += " au trois"; break; 
		case 0 : valRetour += " au deux"; break;
        default: valRetour += "Erreur 52c"; break;
	}
return valRetour;
}

        string DecodeDEUX_PAIRES(int score)
{
string valRetour = " deux paires:  paire ";
int[] reste = new int[5];

    reste[0] = score - DEUX_PAIRES;
	reste[0] = reste[0] / 10000; 

  	switch (reste[0]){
		case 12 : valRetour += " d'as"; break; 
		case 11 : valRetour += " de roi"; break; 
		case 10 : valRetour += " de dame"; break; 
		case 9 : valRetour += " de valet"; break; 
		case 8 : valRetour += " de dix"; break; 
		case 7 : valRetour += " de neuf"; break; 
		case 6 : valRetour += " de huit"; break; 
		case 5 : valRetour += " de sept"; break; 
		case 4 : valRetour += " de six"; break; 
		case 3 : valRetour += " de cinq"; break; 
		case 2 : valRetour += " de quatre"; break; 
		case 1 : valRetour += " de trois"; break; 
		case 0 : valRetour += " de deux"; break;
        default: valRetour = "Erreur 62"; break;
	}
	reste[1] = (score - DEUX_PAIRES - (reste[0] * 10000));
    reste[1] = reste[1] / 100;
	valRetour += " et paire";
  	switch (reste[1]){
		case 12 : valRetour += " d'as"; break; 
		case 11 : valRetour += " de roi"; break; 
		case 10 : valRetour += " de dame"; break; 
		case 9 : valRetour += " de valet"; break; 
		case 8 : valRetour += " de dix"; break; 
		case 7 : valRetour += " de neuf"; break; 
		case 6 : valRetour += " de huit"; break; 
		case 5 : valRetour += " de sept"; break; 
		case 4 : valRetour += " de six"; break; 
		case 3 : valRetour += " de cinq"; break; 
		case 2 : valRetour += " de quatre"; break; 
		case 1 : valRetour += " de trois"; break; 
		case 0 : valRetour += " de deux"; break;
        default: valRetour = "Erreur 63"; break;
	}
	reste[2] = score - DEUX_PAIRES - (reste[0] * 10000) - (reste[1] * 100);
    switch (reste[2]){
		case 12 : valRetour += ", à l'as"; break;
		case 11 : valRetour += ", au roi"; break; 
		case 10 : valRetour += ", à la dame"; break; 
		case 9 : valRetour += ", au valet"; break; 
		case 8 : valRetour += ", au dix"; break; 
		case 7 : valRetour += ", au neuf"; break; 
		case 6 : valRetour += ", au huit"; break; 
		case 5 : valRetour += ", au sept"; break; 
		case 4 : valRetour += ", au six"; break;
		case 3 : valRetour += ", au cinq"; break;
		case 2 : valRetour += " au quatre"; break; 
		case 1 : valRetour += " au trois"; break; 
		case 0 : valRetour += " au deux"; break;
        default: valRetour = "Erreur 52"; break;
	}
return valRetour;
}

        string DecodeBRELAN(int score)
{
string valRetour = " brelan ";
int[] reste = new int[5];

    reste[0] = score - BRELAN;
	reste[0] = reste[0] / 10000; 

  	switch (reste[0]){
		case 12 : valRetour += " d'as"; break; 
		case 11 : valRetour += " de roi"; break; 
		case 10 : valRetour += " de dame"; break; 
		case 9 : valRetour += " de valet"; break; 
		case 8 : valRetour += " de dix"; break; 
		case 7 : valRetour += " de neuf"; break; 
		case 6 : valRetour += " de huit"; break; 
		case 5 : valRetour += " de sept"; break; 
		case 4 : valRetour += " de six"; break; 
		case 3 : valRetour += " de cinq"; break; 
		case 2 : valRetour += " de quatre"; break; 
		case 1 : valRetour += " de trois"; break; 
		case 0 : valRetour += " de deux"; break;
        default: valRetour = "Erreur 70"; break;
	}
return valRetour;
}

        string DecodeSEQ(int score)
        {
            string valRetour = " sequence";
            int reste;

            reste = score - SEQ;
            reste = reste - 1;
            if (reste == 0)
            {
                valRetour += " au cinq.";
            }
            else
            {
                reste = reste / 100;
                switch (reste)
                {
                    case 0: valRetour += " au six"; break;
                    case 1: valRetour += " au sept"; break;
                    case 2: valRetour += " au huit"; break;
                    case 3: valRetour += " au neuf"; break;
                    case 4: valRetour += " au dix"; break;
                    case 5: valRetour += " au valet"; break;
                    case 6: valRetour += " à la dame"; break;
                    case 7: valRetour += " au roi"; break;
                    case 8: valRetour += " à l'as"; break;
                    default: valRetour = "Erreur 1"; break;
                }
            }
            return valRetour;
        }

        string DecodeFLUSH(int score)
        {
          string valRetour = " couleur ";
          int[] reste = new int[6];

          reste[0] = score - FLUSH;
          reste[1] = reste[0] / 10000;
  	      switch (reste[1]){
              case 13: reste[1] = 12; goto case 12;
		    case 12 : valRetour += " à l'as"; break; 
		    case 11 : valRetour += " au roi"; break; 
		    case 10 : valRetour += " à la dame"; break; 
		    case 9 : valRetour += " au valet"; break; 
		    case 8 : valRetour += " au dix"; break; 
		    case 7 : valRetour += " au neuf"; break; 
		    case 6 : valRetour += " au huit"; break; 
		    case 5 : valRetour += " au sept"; break; 
		    case 4 : valRetour += " au six"; break;
		    case 3 : valRetour += " au cinq"; break;
		    case 2 : valRetour += " au quatre"; break;
		    case 1 : valRetour += " au trois"; break;
		    case 0 : valRetour += " au deux"; break;
            default: valRetour = "Erreur 4"; break;
	     }
	     reste[2] = ((score - FLUSH) - (reste[1] *10000));
         reste[2] = reste[2] / 1000;
  	     switch (reste[2]){
             case 12: reste[2] = 11; goto case 11;
		case 11 : valRetour += ", roi"; break; 
		case 10 : valRetour += ", dame"; break; 
		case 9 : valRetour += ", valet"; break; 
		case 8 : valRetour += ", dix"; break; 
		case 7 : valRetour += ", neuf"; break; 
		case 6 : valRetour += ", huit"; break; 
		case 5 : valRetour += ", sept"; break; 
		case 4 : valRetour += ", six"; break;
		case 3 : valRetour += ", cinq"; break;
		case 2 : valRetour += ", quatre"; break;
		case 1 : valRetour += ", trois"; break;
		case 0 : valRetour += ", deux"; break;
        default: valRetour = "Erreur 14"; break;
	}
	reste[3] = ((score - FLUSH) - (reste[1] *10000) - (reste[2] * 1000));
    reste[3] = reste[3] / 100;
  	switch (reste[3]){
        case 11: reste[3] = 10; goto case 10;
		case 10 : valRetour += ", dame"; break; 
		case 9 : valRetour += ", valet"; break; 
		case 8 : valRetour += ", dix"; break; 
		case 7 : valRetour += ", neuf"; break; 
		case 6 : valRetour += ", huit"; break; 
		case 5 : valRetour += ", sept"; break; 
		case 4 : valRetour += ", six"; break;
		case 3 : valRetour += ", cinq"; break;
		case 2 : valRetour += ", quatre"; break;
		case 1 : valRetour += ", trois"; break;
		case 0 : valRetour += ", deux"; break;
        default: valRetour = "Erreur 15"; break;
	}
	reste[4] = ((score - FLUSH) - (reste[1] *10000) - (reste[2] * 1000) - (reste[3] * 100));
    reste[4] = reste[4] / 10;
  	switch (reste[4]){
		case 12 : 
		case 11 :
        case 10: reste[4] = 9; goto case 9;
		case 9 : valRetour += ", valet"; break; 
		case 8 : valRetour += ", dix"; break; 
		case 7 : valRetour += ", neuf"; break; 
		case 6 : valRetour += ", huit"; break; 
		case 5 : valRetour += ", sept"; break; 
		case 4 : valRetour += ", six"; break;
		case 3 : valRetour += ", cinq"; break;
		case 2 : valRetour += ", quatre"; break;
		case 1 : valRetour += ", trois"; break;
		case 0 : valRetour += ", deux"; break;
        default: valRetour = "Erreur 16"; break;
	}
	reste[5] = ((score - FLUSH) - (reste[1] *10000) - (reste[2] * 1000) - (reste[3] * 100)- (reste[4] * 10));
  	switch (reste[5]){
		case 8 : valRetour += ", dix."; break; 
		case 7 : valRetour += ", neuf."; break; 
		case 6 : valRetour += ", huit."; break; 
		case 5 : valRetour += ", sept."; break; 
		case 4 : valRetour += ", six."; break;
		case 3 : valRetour += ", cinq."; break;
		case 2 : valRetour += ", quatre."; break;
		case 1 : valRetour += ", trois."; break;
		case 0 : valRetour += ", deux."; break;
        default: valRetour = "Erreur 17c"; break;
	}
	return valRetour;
}

        string DecodeFULL(int score)
        {
            string valRetour = " main pleine ";
            int reste;

            reste = score - FULL;
            reste = reste / 100;
            switch (reste)
            {
                case 12: valRetour += " d'as"; break;
                case 11: valRetour += " de roi"; break;
                case 10: valRetour += " de dame"; break;
                case 9: valRetour += " de valet"; break;
                case 8: valRetour += " de dix"; break;
                case 7: valRetour += " de neuf"; break;
                case 6: valRetour += " de huit"; break;
                case 5: valRetour += " de sept"; break;
                case 4: valRetour += " de six"; break;
                case 3: valRetour += " de cinq"; break;
                case 2: valRetour += " de quatre"; break;
                case 1: valRetour += " de trois"; break;
                case 0: valRetour += " de deux"; break;
                default: valRetour = "Erreur 2"; break;
            }
            reste = ((score - FULL) % 100);
            switch (reste)
            {
                case 12: valRetour += " à l'as"; break;
                case 11: valRetour += " au roi"; break;
                case 10: valRetour += " à la dame"; break;
                case 9: valRetour += " au valet"; break;
                case 8: valRetour += " au dix"; break;
                case 7: valRetour += " au neuf"; break;
                case 6: valRetour += " au huit"; break;
                case 5: valRetour += " au sept"; break;
                case 4: valRetour += " au six"; break;
                case 3: valRetour += " au cinq"; break;
                case 2: valRetour += " au quatre"; break;
                case 1: valRetour += " au trois"; break;
                case 0: valRetour += " au deux"; break;
                default: valRetour = "Erreur 3"; break;
            }
            return valRetour;
        }

        string DecodeCARRE(int score)
        {
            string valRetour = " carré ";
            int reste;

            reste = score - CARRE;
            reste = reste / 100;
            switch (reste)
            {
                case 12: valRetour += " d'as"; break;
                case 11: valRetour += " de roi"; break;
                case 10: valRetour += " de dame"; break;
                case 9: valRetour += " de valet"; break;
                case 8: valRetour += " de dix"; break;
                case 7: valRetour += " de neuf"; break;
                case 6: valRetour += " de huit"; break;
                case 5: valRetour += " de sept"; break;
                case 4: valRetour += " de six"; break;
                case 3: valRetour += " de cinq"; break;
                case 2: valRetour += " de quatre"; break;
                case 1: valRetour += " de trois"; break;
                case 0: valRetour += " de deux"; break;
                default: valRetour = "Erreur 2"; break;
            }
            reste = ((score - CARRE) % 100);
            switch (reste)
            {
                case 12: valRetour += " à l'as"; break;
                case 11: valRetour += " au roi"; break;
                case 10: valRetour += " à la dame"; break;
                case 9: valRetour += " au valet"; break;
                case 8: valRetour += " au dix"; break;
                case 7: valRetour += " au neuf"; break;
                case 6: valRetour += " au huit"; break;
                case 5: valRetour += " au sept"; break;
                case 4: valRetour += " au six"; break;
                case 3: valRetour += " au cinq"; break;
                case 2: valRetour += " au quatre"; break;
                case 1: valRetour += " au trois"; break;
                case 0: valRetour += " au deux"; break;
                default: valRetour = "Erreur 3"; break;
            }
            return valRetour;
        }

        string DecodeSEQ_FLUSH(int score)
        {
            string valRetour = "Séquence couleur";
            int reste;

            reste = score - SEQ_FLUSH;
            reste = reste - 1;
            if (reste == 0)
            {
                valRetour += " au cinq.";
            }
            else
            {
                reste = reste / 100;
                switch (reste)
                {
                    case 0: valRetour += " au six"; break;
                    case 1: valRetour += " au sept"; break;
                    case 2: valRetour += " au huit"; break;
                    case 3: valRetour += " au neuf"; break;
                    case 4: valRetour += " au dix"; break;
                    case 5: valRetour += " au valet"; break;
                    case 6: valRetour += " à la dame"; break;
                    case 7: valRetour += " au roi"; break;
                    case 8: valRetour += " à l'as"; break;
                    default: valRetour = "Erreur 1"; break;
                }
            }
            return valRetour;
        }
    }
}
