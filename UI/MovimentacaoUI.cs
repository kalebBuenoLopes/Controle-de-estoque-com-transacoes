using ControleEstoque.Models;
using ControleEstoque.Utils;

namespace ControleEstoque.UI
{
    public class MovimentacaoUI
    {
        public static void ListarMovimentacoes(List<Movimentacao> movimentacaos)
        {
            foreach (var movimentacao in movimentacaos)
            {
                if (movimentacao.MovimentacaoTexto == "0")
                {
                    movimentacao.MovimentacaoTexto = "Entrada";
                }else if (movimentacao.MovimentacaoTexto == "1")
                {
                    movimentacao.MovimentacaoTexto = "Saída";
                }
                else
                {
                    movimentacao.MovimentacaoTexto = "Ajuste";
                }

                Escrever.Normal($"{movimentacao.Id}"+
                $"\n{movimentacao.NomeProduto}"+
                $"\n{movimentacao.MovimentacaoTexto}"+
                $"\n{movimentacao.Data_Movimentacao}"+
                $"\n{movimentacao.Quantidade}"+
                $"\n{movimentacao.Observacao}");
            }
        }
    }
}