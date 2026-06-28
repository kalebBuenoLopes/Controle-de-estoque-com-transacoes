using ControleEstoque.Models;
using ControleEstoque.Utils;

namespace ControleEstoque.UI
{
    public class AuditoriaUI
    {
        public static void ListarAuditorias(List<Auditoria> auditorias)
        {
            foreach(var auditoria in auditorias)
            {
                Escrever.Normal($"{auditoria.Id} - {auditoria.Data_evento}"+
                $"\nOperação efetuada: {auditoria.Operacao}"+
                $"\nDescrição: {auditoria.Descricao}");
            }
        }
    }
}