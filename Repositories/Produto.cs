using ControleEstoque.Models;
using Microsoft.Data.Sqlite;

namespace ControleEstoque.Repository
{
    public class TabelaProdutos
    {
        public static void CadastrarProduto(string nome, decimal preco, int quantidade, SqliteConnection conexao, SqliteTransaction transacao)
        {
            using var comando = conexao.CreateCommand();
            comando.Transaction = transacao;

            comando.Parameters.AddWithValue("@NOME", nome);
            comando.Parameters.AddWithValue("@PRECO", preco);
            comando.Parameters.AddWithValue("@QUANTIDADE", quantidade);

            comando.CommandText = @"
            
                INSERT INTO PRODUTOS (NOME,PRECO,QUANTIDADE)
                VALUES (@NOME,@PRECO,@QUANTIDADE);

            ";

            comando.ExecuteNonQuery();
        }

        public static List<Produto> SelecionarProdutos(SqliteConnection conexao, SqliteTransaction transacao)
        {
            using var comando = conexao.CreateCommand();
            comando.Transaction = transacao;

            comando.CommandText = @"
            
                SELECT
                    ID,
                    NOME,
                    PRECO,
                    QUANTIDADE
                FROM PRODUTOS
            
            ";

            using var leitor = comando.ExecuteReader();
            List<Produto> produtos = new List<Produto>();
            while (leitor.Read())
            {
                Produto produto = new Produto();
                produto.Id = leitor.GetInt64(0);
                produto.Nome = leitor.GetString(1);
                produto.Preco = leitor.GetDecimal(2);
                produto.Quantidade = leitor.GetInt32(3);

                produtos.Add(produto);

            }

            return produtos;
        }

        public static Produto? SelecionarProduto(long id, SqliteConnection conexao, SqliteTransaction transacao)
        {
            using var comando = conexao.CreateCommand();
            comando.Transaction = transacao;
            comando.Parameters.AddWithValue("@ID", id);

            comando.CommandText = @"
            
                SELECT
                    ID,
                    NOME,
                    PRECO,
                    QUANTIDADE
                FROM PRODUTOS
                WHERE ID = @ID
            
            ";

            using var leitor = comando.ExecuteReader();
            Produto produto = new Produto();
            if (leitor.Read())
            {
             produto.Id = leitor.GetInt64(0);
             produto.Nome = leitor.GetString(1);
             produto.Preco = leitor.GetDecimal(2);
             produto.Quantidade = leitor.GetInt32(3);

             return produto;   
            }

            return null;
        }

        public static void EntradaEstoque(long id, int quantidade, SqliteConnection conexao, SqliteTransaction transacao)
        {
            using var comando = conexao.CreateCommand();
            comando.Transaction = transacao;
            comando.Parameters.AddWithValue("@ID", id);
            comando.Parameters.AddWithValue("@QUANTIDADE", quantidade);

            comando.CommandText = @"
            
                UPDATE PRODUTOS
                SET QUANTIDADE = QUANTIDADE + @QUANTIDADE
                WHERE ID = @ID;
            
            ";

            comando.ExecuteNonQuery();
        }

        public static void SaidaEstoque(long id, int quantidade, SqliteConnection conexao, SqliteTransaction transacao)
        {
            using var comando = conexao.CreateCommand();
            comando.Transaction = transacao;
            comando.Parameters.AddWithValue("@ID", id);
            comando.Parameters.AddWithValue("@QUANTIDADE", quantidade);

            comando.CommandText = @"
            
                UPDATE PRODUTOS
                SET QUANTIDADE = QUANTIDADE - @QUANTIDADE
                WHERE ID = @ID;

            ";

            comando.ExecuteNonQuery();
        }

        public static void AlterarEstoque(SqliteConnection conexao, SqliteTransaction transacao, long id, int quantidade)
        {
            using var comando = conexao.CreateCommand();
            comando.Transaction = transacao;
            comando.Parameters.AddWithValue("@ID", id);
            comando.Parameters.AddWithValue("@QUANTIDADE", quantidade);

            comando.CommandText = @"
            
                UPDATE PRODUTOS
                SET QUANTIDADE = @QUANTIDADE
                WHERE ID = @ID;

            ";

            comando.ExecuteNonQuery();
        }

        public static Produto? ProdutoMaiorEstoque(SqliteConnection conexao, SqliteTransaction transacao)
        {
            using var comando = conexao.CreateCommand();
            comando.Transaction = transacao;

            comando.CommandText = @"
            
                SELECT 
                    ID,
                    NOME,
                    QUANTIDADE
                FROM PRODUTOS
                ORDER BY QUANTIDADE DESC

            ";

            using var ler = comando.ExecuteReader();

            Produto produto = new Produto();

            if (ler.Read())
            {
                produto.Id = ler.GetInt64(0);
                produto.Nome = ler.GetString(1);
                produto.Quantidade = ler.GetInt32(2);

                return produto;
            }

            return null;
        }

        public static decimal ValorTotalEstoque(SqliteConnection conexao, SqliteTransaction transacao)
        {
            using var comando = conexao.CreateCommand();
            comando.Transaction = transacao;

            comando.CommandText = @"
            
                SELECT 
                    SUM(QUANTIDADE * PRECO) AS TOTAL
                FROM PRODUTOS
            
            ";

            decimal total = (decimal)comando.ExecuteScalar();

            return total;
        }
    }
}