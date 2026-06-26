using ControleEstoque.Enums;
using Microsoft.Data.Sqlite;

namespace ControleEstoque.Repository
{
    public class TabelaMovimentacoes
    {
        public static void CadastrarMovimentacao(long id_produto, TipoMovimentacao tipoMovimentacao, int quantidade, DateTime dataMovimentacao, string? observacao, SqliteConnection conexao, SqliteTransaction transacao)
        {
            using var comando = conexao.CreateCommand();
            comando.Transaction = transacao;
            comando.Parameters.AddWithValue("@ID_PRODUTO", id_produto);
            comando.Parameters.AddWithValue("@TIPO_MOVIMENTACAO", tipoMovimentacao);
            comando.Parameters.AddWithValue("@QUANTIDADE", quantidade);
            comando.Parameters.AddWithValue("@DATA_MOVIMENTACAO", dataMovimentacao);
            comando.Parameters.AddWithValue("@OBSERVACAO", observacao);

            comando.CommandText = @"
            
                INSERT INTO MOVIMENTACOES (ID_PRODUTO, TIPO_MOVIMENTACAO, QUANTIDADE, DATA_MOVIMENTACAO, OBSERVACAO)
                VALUES (@ID_PRODUTO, @TIPO_MOVIMENTACAO, @QUANTIDADE, @DATA_MOVIMENTACAO, @OBSERVACAO);
            
            ";

            comando.ExecuteNonQuery();
        }
        
    }
}