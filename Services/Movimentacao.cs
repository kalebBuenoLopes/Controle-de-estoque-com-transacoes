using ControleEstoque.Models;
using ControleEstoque.Repository;
using ControleEstoque.Utils;

namespace ControleEstoque.Services
{
    public class MovimentacaoServices
    {
        public static void EntradaEstoque()
        {
            Escrever.Titulo("Entrada de estoque");
            List<Produto> produtos = TabelaProdutos.SelecionarProdutos();
            if(produtos.Count == 0)
            {
                Escrever.Alerta("Não há produtos cadastrados na base");
            }
            else
            {
                Produto? produto = null;
                long idProduto = 0;
                while(produto == null)
                {
                    for(int i = 0; i < produtos.Count; i++)
                    {
                        Escrever.Normal($"{produtos[i].Id} - {produtos[i].Nome}\nPreço: {produtos[i].Preco} - Quantidade: {produtos[i].Quantidade}");
                    }
                    Escrever.Normal("Digite o código do produto que deseja cadastrar a entrada de estoque: ");
                    idProduto = Ler.LerLong();

                    produto = TabelaProdutos.SelecionarProduto(idProduto);
                    if(produto == null)
                    {
                        Escrever.Alerta("O código fornecido não está cadastrado");
                        Escrever.Normal("Tente novamente: ");
                    }
                }

                Escrever.Normal($"Digite a quantidade de entrada de estoque para o produto {produto.Id} = {produto.Nome}: ");
                int quantidade = Ler.LerInteiro();

                while(quantidade < 1)
                {
                    Escrever.Alerta("A quantidade digitada deve ser maior que zero");
                    Escrever.Normal("Tente novamente: ");
                    quantidade = Ler.LerInteiro();
                }

                TabelaProdutos.EntradaEstoque(idProduto, quantidade);


            }
        }
    }
}