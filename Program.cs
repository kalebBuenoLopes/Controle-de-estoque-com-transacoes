using ControleEstoque.Repository;
using ControleEstoque.Utils;

namespace ControleEstoque
{
    class Program
    {
        public static void Main()
        {
            Banco.CriarTabelas();
            int selecaoMenu = 0;
            while(selecaoMenu != 9)
            {
                Escrever.Menu();
                selecaoMenu = Ler.LerInteiroLimitador(9,1);

                switch (selecaoMenu)
                {
                    
                    case 9:
                        Console.Clear();
                    break;
                    default:
                    break;
                }
            }
        }
    }
}