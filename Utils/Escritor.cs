namespace ControleEstoque.Utils
{
    public class Escrever
    {
        public static void Menu()
        {
            Titulo("menu principal");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n1 - Cadastrar Produto");
            Console.WriteLine("2 - Listar Produtos");
            Console.WriteLine("3 - Entrada de Estoque");
            Console.WriteLine("4 - Saída de Estoque");
            Console.WriteLine("5 - Ajuste Manual");
            Console.WriteLine("6 - Histórico de Movimentações");
            Console.WriteLine("7 - Relatório Geral");
            Console.WriteLine("8 - Auditoria do Sistema");
            Console.WriteLine("9 - Sair\n");
            Console.WriteLine("Digite o número desejado: \n");
        }

        public static void Titulo(string texto)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            texto = texto.ToUpper();

            Console.WriteLine($"\n==={texto}===\n");
        }
        public static void Alerta(string texto)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            texto = texto.ToUpper();

            Console.WriteLine($"\n!!!{texto}!!!\n");
            
        }

        public static void Normal(string texto)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"\n{texto}\n");
        }
    }
}