using Microsoft.Data.Sqlite;

namespace ControleEstoque.Repository
{
    public class Banco
    {
        private static readonly string caminhoBanco = $"Data Source = {Path.Combine(AppContext.BaseDirectory, "controle-estoque.db")}";

        public static SqliteConnection AbrirConexao()
        {
            var conexao = new SqliteConnection(caminhoBanco);
            conexao.Open();

            var comando = conexao.CreateCommand();
            comando.CommandText = @"PRAGMA foreign_keys = ON;";
            comando.ExecuteNonQuery();

            return conexao;
        }

        public static void CriarTabelas()
        {
            using var conexao = AbrirConexao();

            var comando = conexao.CreateCommand();

            comando.CommandText = @"
            
                CREATE TABLE IF NOT EXISTS PRODUTOS(
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    NOME TEXT NOT NULL,
                    PRECO REAL NOT NULL,
                    QUANTIDADE INTEGER NOT NULL
                );

            ";

            comando.ExecuteNonQuery();

            comando.CommandText = @"
            
              CREATE TABLE IF NOT EXISTS MOVIMENTACOES(
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    ID_PRODUTO INTEGER NOT NULL,
                    TIPO_MOVIMENTACAO TEXT NOT NULL,
                    QUANTIDADE INTEGER NOT NULL,
                    DATA_MOVIMENTACAO TEXT NOT NULL,
                    OBSERVACAO TEXT,
                    FOREIGN KEY (ID_PRODUTO)
                        REFERENCES PRODUTOS (ID)
                );
            
            ";

            comando.ExecuteNonQuery();

            comando.CommandText = @"
            
                CREATE TABLE IF NOT EXISTS AUDITORIAS(
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    OPERACAO TEXT NOT NULL,
                    DESCRICAO TEXT NOT NULL,
                    DATA_EVENTO TEXT NOT NULL
                );
            
            ";
            comando.ExecuteNonQuery();

            comando.CommandText = @"
            
                CREATE TABLE IF NOT EXISTS TESTES(
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    NOME TEXT NOT NULL
                );
            
            ";
            comando.ExecuteNonQuery();
        }
    }
}