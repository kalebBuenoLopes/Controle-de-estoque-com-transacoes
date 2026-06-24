namespace ControleEstoque.Utils
{
    public class Ler
    {
        public static int LerInteiroLimitador(long maximo, int minimo)
        {
            string texto = Console.ReadLine();
            int i;

            while(!int.TryParse(texto, out i) || i > maximo || i < minimo)
            {
                Escrever.Alerta("O número deve ser inteiro entre 1 e 9");
                Escrever.Normal("Tente novamente: ");
                texto = Console.ReadLine();
            }

            return i;

        }
    }
}