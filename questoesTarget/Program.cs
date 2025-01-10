using Newtonsoft.Json;
using System;

class Program
{
    static void Main()
    {
        int INDICE = 13, SOMA = 0, K = 0;

        while (K < INDICE)
        {
            K += 1;
            SOMA += K;
        }

        Console.WriteLine($"Valor de SOMA: {SOMA}");

        Console.Write("Informe um número para verificar na sequência de Fibonacci: ");
        int numero = int.Parse(Console.ReadLine());
        if (PertenceFibonacci(numero))
        {
            Console.WriteLine($"O número {numero} pertence à sequência de Fibonacci.");
        }
        else
        {
            Console.WriteLine($"O número {numero} não pertence à sequência de Fibonacci.");
        }

        CalculaFaturamento();

        var faturamentoEstados = new Dictionary<string, double>
        {
            { "SP", 67836.43 },
            { "RJ", 36678.66 },
            { "MG", 29229.88 },
            { "ES", 27165.48 },
            { "Outros", 19849.53 }
        };
        CalculaPercentualEstados(faturamentoEstados);

        Console.Write("Informe uma string para inverter: ");
        string texto = Console.ReadLine();
        Console.WriteLine($"String invertida: {InverteString(texto)}");
    }

    static bool PertenceFibonacci(int numero)
    {
        int a = 0, b = 1;
        while (b <= numero)
        {
            if (b == numero) return true;
            int temp = a;
            a = b;
            b = temp + b;
        }
        return false;
    }

    public class FaturamentoDia
    {
        public int Dia { get; set; }
        public double Valor { get; set; }
    }

    static void CalculaFaturamento()
    {
        string json = File.ReadAllText("faturamento.json");
        var faturamentoMensal = JsonConvert.DeserializeObject<List<FaturamentoDia>>(json);

        var valores = faturamentoMensal.Where(f => f.Valor > 0).Select(f => f.Valor).ToList();

        double menorValor = valores.Min();
        double maiorValor = valores.Max();
        double mediaMensal = valores.Average();
        int diasAcimaDaMedia = valores.Count(v => v > mediaMensal);

        Console.WriteLine($"Menor valor de faturamento: {menorValor}");
        Console.WriteLine($"Maior valor de faturamento: {maiorValor}");
        Console.WriteLine($"Número de dias com faturamento acima da média: {diasAcimaDaMedia}");
    }

    static void CalculaPercentualEstados(Dictionary<string, double> faturamentoEstados)
    {
        double totalFaturamento = 0;
        foreach (var valor in faturamentoEstados.Values)
        {
            totalFaturamento += valor;
        }

        Console.WriteLine("Percentual de representação por estado:");
        foreach (var estado in faturamentoEstados)
        {
            double percentual = (estado.Value / totalFaturamento) * 100;
            Console.WriteLine($"{estado.Key}: {percentual:F2}%");
        }
    }

    static string InverteString(string texto)
    {
        string invertida = "";
        for (int i = texto.Length - 1; i >= 0; i--)
        {
            invertida += texto[i];
        }
        return invertida;
    }
}
