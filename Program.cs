using ControleEstoque.Enums;
using ControleEstoque.Repository;
using ControleEstoque.Services;
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
                    case 1:
                        ProdutoServices.CadastrarProduto();
                    break;
                    case 2:
                        ProdutoServices.ListarProdutos();
                    break;
                    case 3:
                        MovimentacaoServices.EntradaEstoque();
                    break;  
                    case 4:
                        MovimentacaoServices.SaidaEstoque();
                    break;
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