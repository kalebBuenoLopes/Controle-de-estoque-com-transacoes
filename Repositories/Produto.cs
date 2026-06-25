using ControleEstoque.Models;

namespace ControleEstoque.Repository
{
    public class TabelaProdutos
    {
        public static void CadastrarProduto(string nome, decimal preco, int quantidade)
        {
            using var conexao = Banco.AbrirConexao();
            var comando = conexao.CreateCommand();

            comando.Parameters.AddWithValue("@NOME", nome);
            comando.Parameters.AddWithValue("@PRECO", preco);
            comando.Parameters.AddWithValue("@QUANTIDADE", quantidade);

            comando.CommandText = @"
            
                INSERT INTO PRODUTOS (NOME,PRECO,QUANTIDADE)
                VALUES (@NOME,@PRECO,@QUANTIDADE);

            ";

            comando.ExecuteNonQuery();
        }

        public static List<Produto> SelecionarProdutos()
        {
            using var conexao = Banco.AbrirConexao();
            var comando = conexao.CreateCommand();

            comando.CommandText = @"
            
                SELECT
                    ID,
                    NOME,
                    PRECO,
                    QUANTIDADE
                FROM PRODUTOS
            
            ";

            var leitor = comando.ExecuteReader();
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

        public static Produto? SelecionarProduto(long id)
        {
            using var conexao = Banco.AbrirConexao();
            var comando = conexao.CreateCommand();
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

            var leitor = comando.ExecuteReader();
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

        public static void EntradaEstoque(long id, int quantidade)
        {
            using var conexao = Banco.AbrirConexao();
            var comando = conexao.CreateCommand();
            comando.Parameters.AddWithValue("@ID", id);
            comando.Parameters.AddWithValue("@QUANTIDADE", quantidade);

            comando.CommandText = @"
            
                UPDATE TABLE PRODUTOS
                SET QUANTIDADE = QUANTIDADE + @QUANTIDADE
                WHERE ID = @ID;
            
            ";

            comando.ExecuteNonQuery();
        }
    }
}