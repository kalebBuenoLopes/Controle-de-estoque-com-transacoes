using ControleEstoque.Utils;

namespace ControleEstoque.Services
{
    public class GeralServices
    {
        public static void HistoricoMovimentacoes()
        {
            Escrever.Titulo("Histórico de movimentações");
            MovimentacaoServices.MaiorQuantidadeMovimentacao();
            MovimentacaoServices.QuantidadeTotalEntradas();
            MovimentacaoServices.QuantidadeTotalSaida();
        }

        public static void HistoricoProduto()
        {
            Escrever.Titulo("Histórico produtos");
            ProdutoServices.ProdutoMaiorQuantidade();
            ProdutoServices.ValorTotalEstoque();
        }
    }
}