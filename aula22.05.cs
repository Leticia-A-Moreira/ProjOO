using System;
using System.Collections.Generic;
using System.Diagnostics;

public interface IEspecieFlyweight
{
    void MostrarArv(int x, int y, double altura, double diametro);
}
public class EspecieArv : IEspecieFlyweight
{
    public string Nome { get;  }
    public string Coloracao { get;  }
    public string Textura { get;  }
    public EspecieArv(string nome, string coloracao, string textura)
    {
        this.Nome = nome;
        this.Coloracao = coloracao;
        this.Textura = textura;
    }
    public void MostrarArv(int x, int y, double altura, double diametro)
    {
        //impressão desativada para não fazer um gargalo no console com o teste de desempenho com 100 mil instâncias
        //Console.WriteLine($"Árvore espécie {Nome}, coloração {Coloracao}, na posicao {x}, {y}. Altura = {altura}, Diâmetro = {diametro}");
    }
}

public class EspecieFactory
{
    public static Dictionary<string, IEspecieFlyweight> cache = new Dictionary<string, IEspecieFlyweight>();

    public static IEspecieFlyweight GetEspecie(string nome, string coloracao, string textura)
    {
        if (!cache.ContainsKey(nome))
        {
            cache[nome] = new EspecieArv(nome, coloracao, textura);
        }
        return cache[nome];
    }
}
public class Arvore
{
    public int X { get; set; }
    public int Y { get; set; }
    public double Altura { get; set; }
    public double Diametro { get; set; }

    private IEspecieFlyweight especie;
    public Arvore(int x, int y, double altura, double diametro, IEspecieFlyweight especie)
    {
        this.X = x;
        this.Y = y;
        this.Altura = altura;
        this.Diametro = diametro;
        this.especie = especie;
    }
    public void Desenhar()
    {
        especie.MostrarArv(this.X, this.Y, this.Altura, this.Diametro);
    }

}

class Program
{
    static void Main()
    {
        int totalArvores = 100000;
        Random rnd = new Random();
        Stopwatch marcaTempo = new Stopwatch();
        // simula o estado intrínseco "pesado" da espécie (como uma imagem ou textura 3D)
        string texturaPesada = new string('*', 500);




        // teste arvores memória com Flyweight
        Console.WriteLine($"--Teste {totalArvores} árvores COM Flyweight--");

        List<Arvore> florestaComFlyweight = new List<Arvore>();
        long memAntesFW = GC.GetTotalMemory(true);

        marcaTempo.Start();
        for (int i = 0; i < totalArvores; i++)
        {
            string nomeEspecie = $"Especie_{rnd.Next(1, 51)}";
            IEspecieFlyweight especie = EspecieFactory.GetEspecie(nomeEspecie, "Verde Escuro", texturaPesada);
            florestaComFlyweight.Add(new Arvore(10, 10, 5.5, 1.2, especie));
        }
        marcaTempo.Stop();
        long tempoComF = marcaTempo.ElapsedMilliseconds;

        long memDepoisFW = GC.GetTotalMemory(true);
        double memoriaComFlyweightMB = (memDepoisFW - memAntesFW) / 1024.0 / 1024.0;




        // teste arvores memória sem Flyweight
        Console.WriteLine($"--Teste {totalArvores} árvores SEM Flyweight--");

        List<Arvore> florestaSemFlyweight = new List<Arvore>();
        long memAntesSemFW = GC.GetTotalMemory(true);

        marcaTempo.Restart();
        for (int i = 0; i < totalArvores; i++)
        {
            string nomeEspecie = $"Especie_{rnd.Next(1, 51)}";
            IEspecieFlyweight especieRepetida = new EspecieArv(nomeEspecie, "Verde Escuro", texturaPesada);
            florestaSemFlyweight.Add(new Arvore(10, 10, 5.5, 1.2, especieRepetida));
        }
        marcaTempo.Stop();
        long tempoSemF = marcaTempo.ElapsedMilliseconds;

        long memDepoisSemFW = GC.GetTotalMemory(true);
        double memoriaSemFlyweightMB = (memDepoisSemFW - memAntesSemFW) / 1024.0 / 1024.0;

        Console.WriteLine("\n\n--RESULTADO TESTES (TEMPO E MEMÓRIA)--");
        Console.WriteLine($"Quantidade árvores criadas no mapa: {totalArvores}");
        Console.WriteLine($"\nObjetos de espécie instanciados com Flyweight: {EspecieFactory.cache.Count}");

        Console.WriteLine($"\nConsumo de memória\n - Com Flyweight: {memoriaComFlyweightMB:F2} MB \n - Sem Flyweight: {memoriaSemFlyweightMB:F2} MB.");
        Console.WriteLine($"   {(memoriaSemFlyweightMB) - (memoriaComFlyweightMB):F2} MB foram economizados.");

        Console.WriteLine($"\nTempo de execução\n - Com Flyweight: {tempoComF} milissegundos\n - Sem Flyweight: {tempoSemF} milissegundos.");
    }
}
