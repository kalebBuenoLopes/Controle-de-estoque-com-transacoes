using ControleEstoque.Models;
using ControleEstoque.Repository;
using ControleEstoque.Utils;

namespace ControleEstoque.Services
{
    public class ProdutoServices
    {
        public static void CadastrarProduto()
        {

            using var conexao = Banco.AbrirConexao();
            using var transacao = conexao.BeginTransaction();

            try
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

                TabelaProdutos.CadastrarProduto(nome, preco, quantidade, conexao, transacao);

                DateTime data = DateTime.Now;

                TabelaAuditorias.CadastrarAuditoria("Cadastrar Produto",$"Produto {nome} cadastro com o valor {preco:F2} e quantidade inicial {quantidade}", data,conexao,transacao);

                transacao.Commit();
            }
            catch (Exception ex)
            {
                transacao.Rollback();
                Escrever.Alerta(ex.Message);
            }
        }

        public static void ListarProdutos()
        {
            using var conexao = Banco.AbrirConexao();
            using var transacao = conexao.BeginTransaction();

            try
            {
                List<Produto> produtos = TabelaProdutos.SelecionarProdutos(conexao, transacao);
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

                transacao.Commit();
            }
            catch (Exception ex)
            {
                transacao.Rollback();
                Escrever.Alerta(ex.Message);
            }
        }

        public static void ProdutoMaiorQuantidade()
        {
            using var conexao = Banco.AbrirConexao();
            using var transacao = conexao.BeginTransaction();

            try
            {
                Escrever.Titulo("Produto com maior quantidade");
                Produto produto = TabelaProdutos.ProdutoMaiorEstoque(conexao, transacao);
                if(produto == null)
                {
                    Escrever.Alerta("Não há produtos cadastrados na base");
                }
                else
                {
                    Escrever.Normal($"{produto.Id} - {produto.Nome} Quantidade: {produto.Quantidade}");
                }
                transacao.Commit();
            }catch (Exception ex)
            {
                transacao.Rollback();
                Escrever.Alerta(ex.Message);
            }
        }

        public static void ValorTotalEstoque()
        {
            using var conexao = Banco.AbrirConexao();
            using var transacao = conexao.BeginTransaction();

            try
            {
                decimal valor = TabelaProdutos.ValorTotalEstoque(conexao, transacao);
                if (valor == 0)
                {
                    Escrever.Alerta("Não há produtos em estoque");
                }
                else
                {
                    Escrever.Normal($"Valor total em estoque: {valor}");
                }
                transacao.Commit();
            }catch(Exception ex)
            {
                transacao.Rollback();
            }
        }

    }
}