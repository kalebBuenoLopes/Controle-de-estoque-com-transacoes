using ControleEstoque.Enums;
using ControleEstoque.Models;
using ControleEstoque.Repository;
using ControleEstoque.UI;
using ControleEstoque.Utils;

namespace ControleEstoque.Services
{
    public class MovimentacaoServices
    {
        public static void EntradaEstoque()
        {
            using var conexao = Banco.AbrirConexao();
            using var transacao = conexao.BeginTransaction();
            try
            {
                Escrever.Titulo("Entrada de estoque");
                List<Produto> produtos = TabelaProdutos.SelecionarProdutos(conexao, transacao);
                if(produtos.Count == 0)
                {
                    ProdutoServices.ListarProdutos();
                }
                else
                {
                    Produto? produto = null;
                    long idProduto = 0;
                    while(produto == null)
                    {
                        ProdutoServices.ListarProdutos();
                        Escrever.Normal("Digite o código do produto que deseja cadastrar a entrada de estoque: ");
                        idProduto = Ler.LerLong();

                        produto = TabelaProdutos.SelecionarProduto(idProduto,conexao,transacao);
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

                    Escrever.Normal("Deseja incluir uma observação?\n1 - Sim\n2 - Não\n");
                    int menuObservacao = Ler.LerInteiroLimitador(2,1);
                    string? observacao;
                    if(menuObservacao == 1)
                    {
                        Escrever.Normal("Digite a observação: ");
                        observacao = Ler.LerTexto();
                    }
                    else
                    {
                    observacao = null; 
                    }

                    TabelaProdutos.EntradaEstoque(idProduto, quantidade,conexao,transacao);
                    TipoMovimentacao movimentacao = TipoMovimentacao.Entrada;
                    string tipoMovimentacao = movimentacao.ToString();
                    DateTime dataMovimentacao = DateTime.Now;
                    TabelaMovimentacoes.CadastrarMovimentacao(idProduto,TipoMovimentacao.Entrada,quantidade,dataMovimentacao,observacao,conexao,transacao);
                    TabelaAuditorias.CadastrarAuditoria("Entrada de estoque",$"Produto {produto.Id} - {produto.Nome} quantidade movimentada {quantidade}", dataMovimentacao,conexao,transacao);
                    transacao.Commit();
                }
            }
            catch (Exception ex)
            {
                transacao.Rollback();
                Escrever.Alerta(ex.Message);
            }
        }

        public static void SaidaEstoque()
        {
            using var conexao = Banco.AbrirConexao();
            using var transacao = conexao.BeginTransaction();

            try
            {
                Escrever.Titulo("Saída de estoque");
                List<Produto> produtos = TabelaProdutos.SelecionarProdutos(conexao, transacao);
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
                        ProdutoUI.ListarProdutos(produtos);
                        Escrever.Normal("Digite o código do produto que deseja cadastrar a entrada de estoque: ");
                        idProduto = Ler.LerLong();

                        produto = TabelaProdutos.SelecionarProduto(idProduto,conexao,transacao);
                        if(produto == null)
                        {
                            Escrever.Alerta("O código fornecido não está cadastrado");
                            Escrever.Normal("Tente novamente: ");
                        }
                    }

                    Escrever.Normal("Digite a quantidade sainte: ");
                    int quantidade = Ler.LerInteiro();

                    while(quantidade < 1 || produto.Quantidade - quantidade < 0)
                    {
                        if (quantidade < 1)
                        {
                            Escrever.Alerta("A quantidade não pode ser menor que 1");
                            Escrever.Normal("Tente novamente: ");
                            quantidade = Ler.LerInteiro();
                        }
                        else
                        {
                            Escrever.Alerta($"saldo em estoque insuficiente, quantidade no armazem: {produto.Quantidade}");
                            Escrever.Normal("Tente novamente: ");
                            quantidade = Ler.LerInteiro();
                        }
                    }
                    
                    Escrever.Normal("Deseja registrar observação?\n1 - Sim\n2 - Não\n");
                    int opcaoObservacao = Ler.LerInteiroLimitador(2,1);

                    string? observao = null;
                    if (opcaoObservacao == 1)
                    {
                        Escrever.Normal("Digite a observação: ");
                        observao = Ler.LerTexto();
                    }

                    TabelaProdutos.SaidaEstoque(idProduto,quantidade,conexao,transacao);
                    TabelaMovimentacoes.CadastrarMovimentacao(idProduto, TipoMovimentacao.Saida ,quantidade,DateTime.Now,observao,conexao,transacao);
                    TabelaAuditorias.CadastrarAuditoria("Saída estoque", $"O estoque do produto: {produto.Id} - {produto.Nome}, saiu em {quantidade}", DateTime.Now, conexao,transacao);
                    transacao.Commit();
                }

                transacao.Commit();
            }catch(Exception ex)
            {
                transacao.Rollback();
                Escrever.Alerta(ex.Message);
            }

        }
    }
}