using ControleEstoque.Enums;
using ControleEstoque.Models;
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

        public static List<Movimentacao> ListarMovimentaçoes(SqliteConnection conexao, SqliteTransaction transacao)
        {
            using var comando = conexao.CreateCommand();
            comando.Transaction = transacao;

            comando.CommandText = @"
            
                SELECT
                    M.ID,
                    P.NOME,
                    M.TIPO_MOVIMENTACAO,
                    M.QUANTIDADE,
                    M.DATA_MOVIMENTACAO,
                    M.OBSERVACAO
                FROM MOVIMENTACOES M
                INNER JOIN PRODUTOS P
                ON P.ID = M.ID_PRODUTO;
            
            ";

            using var leitor = comando.ExecuteReader();
            List<Movimentacao> movimentacaos = new List<Movimentacao>();
            while (leitor.Read())
            {
                Movimentacao movimentacao = new Movimentacao();
                movimentacao.Id = leitor.GetInt64(0);
                movimentacao.NomeProduto = leitor.GetString(1);
                movimentacao.MovimentacaoTexto = leitor.GetString(2);
                movimentacao.Quantidade = leitor.GetInt32(3);
                movimentacao.Data_Movimentacao = leitor.GetString(4);
                movimentacao.Observacao = leitor.GetString(5);

                movimentacaos.Add(movimentacao);
            }

            return movimentacaos;
        }

        public static Movimentacao? ProdutoComMaisMovimentacao(SqliteConnection conexao, SqliteTransaction transacao)
        {
            using var comando = conexao.CreateCommand();
            comando.Transaction = transacao;
            comando.CommandText = @"
            
                SELECT
                    P.NOME, COUNT(*) AS QUANTIDADE
                FROM MOVIMENTACOES AS M
                INNER JOIN PRODUTOS AS P
                ON P.ID = M.ID_PRODUTO
                GROUP BY ID_PRODUTO
                ORDER BY QUANTIDADE DESC
                LIMIT 1

            ";

            using var ler = comando.ExecuteReader();
            Movimentacao movimentacao = new Movimentacao();
            if (ler.Read())
            {
                movimentacao.NomeProduto = ler.GetString(0);
                movimentacao.Quantidade = ler.GetInt32(1);

                return movimentacao;
            }

            return null;
        }

        public static long QuantidadeEntrada(SqliteConnection conexao, SqliteTransaction transacao)
        {
            using var comando = conexao.CreateCommand();
            comando.Transaction = transacao;

            comando.CommandText = @"
            
                SELECT
                    count(*)
                FROM MOVIMENTACOES
                WHERE TIPO_MOVIMENTACAO = 0

            ";

            long quantidade = (long)comando.ExecuteScalar();

            return quantidade;
        }

        public static long QuantidadeSaida(SqliteConnection conexao, SqliteTransaction transacao)
        {
            using var comando = conexao.CreateCommand();
            comando.Transaction = transacao;

            comando.CommandText = @"
            
                SELECT
                    count(*)
                FROM MOVIMENTACOES
                WHERE TIPO_MOVIMENTACAO = 1

            ";

            long quantidade = (long)comando.ExecuteScalar();

            return quantidade;
        }
        
    }
}