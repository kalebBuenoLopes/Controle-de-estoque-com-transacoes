namespace ControleEstoque.Repository
{
    public class TabelaMovimentacoes
    {
        public static void CadastrarMovimentacao(long id_produto, )
        {
            using var conexao = Banco.AbrirConexao();
            var comando = conexao.CreateCommand();

        }
    }
}