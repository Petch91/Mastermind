namespace Mastermind;


public class Program
{
    public static void Main(string[] args)
    {
        const int MAXESSAIS = 15;
        bool newGame = false;
        Mastermind game = new Mastermind();
        game.showTitle();
        do
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            newGame = false;
            game.playGame(MAXESSAIS);
            Console.WriteLine("Voulez vous rejouer? (O / N)");
            string reponse = (Console.ReadLine() ?? "");
            if ( reponse == "O" || reponse == "o") newGame = true;
        } while (newGame);
    }
}
