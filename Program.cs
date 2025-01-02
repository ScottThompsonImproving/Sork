public class Program
{
    public static void Main(string[] args)
    {
        do
        {
            Console.Write(" > ");
            string input = Console.ReadLine();
            input = input.ToLower();
            input = input.Trim();
            if (input == "lol") { Console.WriteLine("You laugh out loud!"); }
            else if (input == "dance") { Console.WriteLine("You spin around in circles!"); }
            else if (input == "sing") { Console.WriteLine("You wail like a banshee!"); }
            else if (input == "whistle") { Console.WriteLine("You sound like a boiling tea kettle!"); }
            else if (input == "exit") { break; }
            else { Console.WriteLine("Unkown command"); }
        } while (true);
    }
}
