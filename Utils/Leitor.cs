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

        public static string LerTexto()
        {
            string texto = Console.ReadLine();

            while (String.IsNullOrWhiteSpace(texto))
            {
                Escrever.Alerta("O texto deve ser digitado");
                Escrever.Normal("Tente novamente: ");
                texto = Console.ReadLine();
            }

            return texto;
        }

        public static decimal LerDecimal()
        {
            string texto = Console.ReadLine();
            decimal i;

            while(!decimal.TryParse(texto, out i) || i < 0)
            {
                Escrever.Alerta("O conteúdo digitado deve ser um número maior que zero");
                Escrever.Normal("Tente novamente: ");
                texto = Console.ReadLine();
            }

            return i;
        }

        public static int LerInteiro()
        {
            string texto = Console.ReadLine();
            int i;

            while (!int.TryParse(texto, out i))
            {
                Escrever.Alerta("O conteúdo digitado deve ser um número inteiro");
                Escrever.Normal("Tente novamente: ");
                texto = Console.ReadLine();
            }
            
            return i;
        }
    }
}