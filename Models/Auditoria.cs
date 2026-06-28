namespace ControleEstoque.Models
{
    public class Auditoria
    {
        public long Id{get;set;}
        public string Operacao{get;set;}
        public string Descricao{get;set;}
        public string Data_evento{get;set;}
    }
}