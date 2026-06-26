using ControleEstoque.Models;
using ControleEstoque.Utils;

namespace ControleEstoque.UI
{
    public class ProdutoUI
    {
        public static void ListarProdutos(List<Produto> produtos)
        {
            foreach (var produto in produtos)
            {
                Escrever.Normal($"{produto.Id} - {produto.Nome}\n"+
                $"Preço: {produto.Preco}\n"+
                $"Quantidade: {produto.Quantidade}");
            }
        }
    }
}