using ControleEstoque.Models;
using Microsoft.Data.Sqlite;

namespace ControleEstoque.Repository
{
    public class TabelaAuditorias
    {
        public static void CadastrarAuditoria(string operacao, string descricao, DateTime data_evento, SqliteConnection conexao, SqliteTransaction transacao)
        {
            using var comando = conexao.CreateCommand();
            comando.Transaction = transacao;
            comando.Parameters.AddWithValue("@OPERACAO",operacao);
            comando.Parameters.AddWithValue("@DESCRICAO",descricao);
            comando.Parameters.AddWithValue("@DATA_EVENTO",data_evento);

            comando.CommandText = @"
            
                INSERT INTO AUDITORIAS (OPERACAO,DESCRICAO,DATA_EVENTO)
                VALUES (@OPERACAO,@DESCRICAO,@DATA_EVENTO);
            
            ";

            comando.ExecuteNonQuery();

        }

        public static List<Auditoria>? ListarAuditorias(SqliteConnection conexao, SqliteTransaction transacao)
        {
            using var comando = conexao.CreateCommand();
            comando.Transaction = transacao;

            comando.CommandText = @"
            
                SELECT
                    ID,
                    OPERACAO,
                    DESCRICAO,
                    strftime('%d/%m/%Y %H:%M:%S',DATA_EVENTO)
                FROM AUDITORIAS

            ";

            using var ler = comando.ExecuteReader();
            List<Auditoria> auditorias = new List<Auditoria>();

            while (ler.Read())
            {
                Auditoria auditoria = new Auditoria();
                auditoria.Id = ler.GetInt64(0);
                auditoria.Operacao = ler.GetString(1);
                auditoria.Descricao = ler.GetString(2);
                auditoria.Data_evento = ler.GetString(3);

                auditorias.Add(auditoria);

                return auditorias;
            }

            return null;
        }
    }
}