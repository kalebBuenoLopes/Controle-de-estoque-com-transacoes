using ControleEstoque.Models;
using ControleEstoque.Repository;
using ControleEstoque.UI;
using ControleEstoque.Utils;

namespace ControleEstoque.Services
{
    public class AuditoriaServices
    {
        public static void ListarAuditorias()
        {
            using var conexao = Banco.AbrirConexao();
            using var transacao = conexao.BeginTransaction();
            try
            {
                List<Auditoria> auditorias = TabelaAuditorias.ListarAuditorias(conexao, transacao);
                if(auditorias == null)
                {
                    Escrever.Alerta("Não há auditorias cadastras");
                }
                else
                {
                    AuditoriaUI.ListarAuditorias(auditorias);
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