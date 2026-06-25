namespace ControleEstoque.Models
{
    public class ModeloMovimentacao
    {
        public long Id{get;set;}
        public long Id_Produto{get;set;}
        public string Tipo_Movimentacao{get;set;}
        public int  Quantidade{get;set;}
        public string Data_Movimentacao{get;set;}
        public string Observacao{get;set;}
    }
}