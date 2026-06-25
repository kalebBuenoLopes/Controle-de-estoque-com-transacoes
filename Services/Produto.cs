using ControleEstoque.Models;
using ControleEstoque.Repository;
using ControleEstoque.Utils;

namespace ControleEstoque.Services
{
    public class ProdutoServices
    {
        public static void CadastrarProduto()
        {
            Escrever.Titulo("cadastro de produto");
            Escrever.Normal("Digite o nome do produto: ");
            string nome = Ler.LerTexto();

            Escrever.Normal("Digite o preço do produto: ");
            decimal preco = Ler.LerDecimal();

            Escrever.Normal("Digite a quantidade inicial do produto: ");
            int quantidade = Ler.LerInteiro();

            while (quantidade < 0)
            {
                Escrever.Alerta("A quantidade não pode ser negativa");
                Escrever.Normal("Tente novamente");
                quantidade = Ler.LerInteiro();
            }

            TabelaProdutos.CadastrarProduto(nome, preco, quantidade);

            DateTime data = DateTime.Now;

            TabelaAuditorias.CadastrarAuditoria("Cadastrar Produto",$"Produto {nome} cadastro com o valor {preco:F2} e quantidade inicial {quantidade}", data);
        }

        public static void ListarProdutos()
        {
            List<Produto> produtos = TabelaProdutos.SelecionarProdutos();
            if(produtos.Count == 0)
            {
                Escrever.Alerta("Não há produtos cadastrados na base");
            }
            else
            {
                Escrever.Titulo("lista de produtos");
                for(int i = 0; i < produtos.Count; i++)
                {
                    Escrever.Normal($"ID: {produtos[i].Id} - {produtos[i].Nome}\nPreço: {produtos[i].Preco} - Quantidade: {produtos[i].Quantidade}");
                }
            }
        }
    }
}