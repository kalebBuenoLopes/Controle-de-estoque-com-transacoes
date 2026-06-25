namespace ControleEstoque.Models
{
    public class ModeloAuditoria
    {
        public long Id{get;set;}
        public string Operacao{get;set;}
        public string Descricao{get;set;}
        public string Data_evento{get;set;}
    }
}