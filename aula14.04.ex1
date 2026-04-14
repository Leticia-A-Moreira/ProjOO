using System;

public class TV
{
    public void Ligar() => Console.WriteLine("TV ON.");
    public void Desligar() => Console.WriteLine("TV OFF.");
}

public class Projetor
{
    public void Ligar() => Console.WriteLine("Projetor ON.");
    public void Desligar() => Console.WriteLine("Projetor OFF.");
}
public class Receiver
{
    public void Ligar() => Console.WriteLine("Receiver ON.");
    public void Desligar() => Console.WriteLine("Receiver OFF.");
}

public class PlayMidia
{
    public void Ligar() => Console.WriteLine("Player de Mídia ON.");
    public void Desligar() => Console.WriteLine("Player de Mídia OFF.");
}

public class SistSom
{
    public void Ligar() => Console.WriteLine("Sistema de Som ON.");
    public void Desligar() => Console.WriteLine("Sistema de Som OFF.");
    public void Volume(int nivel) => Console.WriteLine($"Volume: {nivel}.");
}

public class LuzAmb
{
    public void Ligar() => Console.WriteLine("Luz Ambiente ON.");
    public void Desligar() => Console.WriteLine("Luz Ambiente OFF.");
    public void Mudar(int intensidade) => Console.WriteLine($"Luz ambiente: {intensidade}%.");
}






public class HomeTheater
{
    private Projetor _proj;
    private TV _tv;
    private Receiver _receiv;
    private SistSom _sistsom;
    private LuzAmb _luzamb;
    private PlayMidia _playmidia;

    public HomeTheater(TV tv, Projetor projetor, Receiver receiver, PlayMidia player, 
                        SistSom som, LuzAmb luz)
    {
        _proj = projetor;
        _tv = tv;
        _receiv = receiver;
        _sistsom = som;
        _luzamb = luz;
        _playmidia = player;
    }

    public void assistirFilme()
    {
        Console.WriteLine("--ASSISTINDO FILME--");
        _proj.Ligar();
        _tv.Ligar();
        _receiv.Ligar();
        _sistsom.Ligar();
        _sistsom.Volume(33);
        _luzamb.Mudar(40);
        _playmidia.Ligar();
    }

    public void ouvirMusica()
    {
        Console.WriteLine("\n--TOCANDO MUSICA--");
        _proj.Desligar();
        _tv.Ligar();
        _receiv.Ligar();
        _sistsom.Ligar();
        _sistsom.Volume(60);
        _luzamb.Mudar(90);
        _playmidia.Ligar();
    }
}

public class Program
{
    public static void Main()
    {
        TV tv = new TV();
        Projetor projetor = new Projetor();
        Receiver receiver = new Receiver();
        PlayMidia play = new PlayMidia();
        SistSom som = new SistSom();
        LuzAmb luz = new LuzAmb();

        HomeTheater fachada = new HomeTheater(tv, projetor, receiver, play,
                                            som, luz);

        fachada.assistirFilme();

        fachada.ouvirMusica();
    }
}
