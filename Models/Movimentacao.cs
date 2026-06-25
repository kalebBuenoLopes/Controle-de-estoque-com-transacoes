using ControleEstoque.Enums;

namespace ControleEstoque.Models
{
    public class ModeloMovimentacao
    {
        public long Id{get;set;}
        public long Id_Produto{get;set;}
        public TipoMovimentacao tipoMovimentacao{get;set;}
        public int  Quantidade{get;set;}
        public string Data_Movimentacao{get;set;}
        public string Observacao{get;set;}
    }
}