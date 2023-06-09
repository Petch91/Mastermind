namespace Mastermind;

public struct Mastermind
{
    private Couleurs[] _combinaison = new Couleurs[4];
    private Couleurs[] _combiUser = new Couleurs[4];

    public Mastermind()
    {

    }

    private void newCombi((int, int) minMax)
    {
        Random random = new Random();
        for(int j=0 ; j < _combinaison.Length ; j++)
        {
            int i = random.Next(minMax.Item1, minMax.Item2);
            _combinaison[j] = (Couleurs)i;
        }
    }

    private void showCombi(Couleurs[] combi)
    {
        foreach (Couleurs i in combi)
        {
            switch (i)
            {
                case Couleurs.Bleu:
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("B ");
                    break;
                }
                case Couleurs.Rouge:
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("R ");
                    break;
                }
                case Couleurs.Vert:
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("V ");
                    break;
                }
                case Couleurs.Jaune:
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("J ");
                    break;
                }
                case Couleurs.Mauve:
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write("M ");
                    break;
                }
                default: ;
                    break;
            }

        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
    }

    private void AskCombiUser()
    {
        bool ok = false;
        string inputUser;
        Console.WriteLine("Entre ta combinaison (ex: V R G R) : ");
        ColorsAvailable();
        while (!ok)
        {
            inputUser = Console.ReadLine() ?? "";
            Console.ForegroundColor = ConsoleColor.Gray;
            try
            {
                GetCombiUser(inputUser);
                ok = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Rentrez votre combinaison en respectant l'exemple svp (ex : V R G R) ");
                ColorsAvailable();
            } 
        }
    }

    private void GetCombiUser(string combi)
    {

        string[] s = new string[combi.Length];

        try
        {
            s = System.Text.RegularExpressions.Regex.Replace(combi.ToUpper(),(@"\s+")," ").Split(' ');
        }
        catch (Exception e)
        {
            throw new Exception();
        }
  
        
        for (int i = 0; i < _combiUser.Length; i++)
        {
            switch (s[i])
            {
                case "R": _combiUser[i] = Couleurs.Rouge;
                    break;
                case "B": _combiUser[i] = Couleurs.Bleu;
                    break;
                case "V": _combiUser[i] = Couleurs.Vert;
                    break;
                case "J": _combiUser[i] = Couleurs.Jaune;
                    break;
                case "M": _combiUser[i] = Couleurs.Mauve;
                    break;
                default: ;
                    break;
            }
        }
    }

    private (int, int) Comparaison(Couleurs[] combi, Couleurs[] combiUser)
    {
        int good = 0, bad = 0;
        Couleurs[] cCombi = new Couleurs[combi.Length], cCombiUser = new Couleurs[combiUser.Length];
        combi.CopyTo(cCombi,0);
        combiUser.CopyTo(cCombiUser,0);
        for (int i = 0; i < cCombiUser.Length; i++)
        {
            for (int j = 0; j < cCombi.Length; j++)
            {
                if (cCombiUser[i] == cCombi[j] && i == j && (cCombi[j]!= Couleurs.White || cCombiUser[i] != Couleurs.White))
                {
                    cCombi[j] = Couleurs.White;
                    cCombiUser[i] = Couleurs.White;
                    good++;
                }
            }
        }
        for (int i = 0; i < cCombiUser.Length; i++)
        {
            for (int j = 0; j < cCombi.Length; j++)
            {
                if (cCombiUser[i] == cCombi[j] && i != j && (cCombi[j]!= Couleurs.White || cCombiUser[i] != Couleurs.White))
                {
                    cCombi[j] = Couleurs.White;
                    cCombiUser[i] = Couleurs.White;
                    bad++;
                }
            }
        }
        return (good, bad);
    }

    private bool Win()
    {
        bool win = false;
        (int, int) indices = Comparaison(_combinaison, _combiUser);
        Console.Clear();
        Console.Write("Avec ta combinaison ");
        showCombi(_combiUser);
        Console.WriteLine($"Tu as {indices.Item1} bien placé et {indices.Item2} mal placé");
        if (indices.Item1 == 4)
        {
            win = true;
        }
        return win;
    }

    private void ColorsAvailable()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Rouge ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("Bleu ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Jaune ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Vert ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("Mauve ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
    }

    public void playGame(int maxEssais)
    {
        int essais = 0;
        bool win;
        newCombi((0,5));
        do
        {
                
            AskCombiUser();
            essais++;
            win = Win();
            if(!win) Console.WriteLine($"et il te reste {maxEssais - essais} essai(s) pour trouver !");

        }while (!win && essais < maxEssais);

        if (win)
        {
            Console.WriteLine($"C'est gagné en {essais} essai(s)");
            Console.Write($"C'était bien ");
            showCombi(_combinaison);
        }
        else
        {
            Console.WriteLine("C'est perdu");
            Console.Write($"C'était ");
            showCombi(_combinaison);
        }

    }

    public void showTitle()
    {
        Console.Write("| ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("M");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("A");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("S");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("T");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("E");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("R");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write("M");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("I");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("N");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("D");
        Console.WriteLine(" |");
        Console.WriteLine("Push \"ENTER\" for play");
        Console.ReadKey();
    }

}