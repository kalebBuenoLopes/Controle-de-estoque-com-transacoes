using ControleEstoque.Enums;

namespace ControleEstoque.Models
{
    public class Movimentacao
    {
        public long Id{get;set;}
        public long Id_Produto{get;set;}
        public string NomeProduto{get;set;}
        public TipoMovimentacao TipoMovimentacao{get;set;}
        public string MovimentacaoTexto{get;set;}
        public int  Quantidade{get;set;}
        public string Data_Movimentacao{get;set;}
        public string Observacao{get;set;}
    }
}