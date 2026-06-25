namespace ControleEstoque.Repository
{
    public class TabelaAuditorias
    {
        public static void CadastrarAuditoria(string operacao, string descricao, DateTime data_evento)
        {
            using var conexao = Banco.AbrirConexao();

            var comando = conexao.CreateCommand();
            comando.Parameters.AddWithValue("@OPERACAO",operacao);
            comando.Parameters.AddWithValue("@DESCRICAO",descricao);
            comando.Parameters.AddWithValue("@DATA_EVENTO",data_evento);

            comando.CommandText = @"
            
                INSERT INTO AUDITORIAS (OPERACAO,DESCRICAO,DATA_EVENTO)
                VALUES (@OPERACAO,@DESCRICAO,@DATA_EVENTO);
            
            ";

            comando.ExecuteNonQuery();

        }
    }
}