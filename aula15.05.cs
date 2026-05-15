using System;
using System.Collections.Generic;

public interface Mediator
{
    void EnviarMsg(string msg, Colega remetente);
}
public abstract class Colega
{
    protected Mediator mediator;

    public void SetMediator(Mediator m)
    {
        mediator = m;
    }

    public abstract void ReceberMsg(string msg);
}
public class ConcreteMediator: Mediator
{
    private List<Colega> colegas = new List<Colega>();

    public void Registrar(Colega colega)
    {
        colegas.Add(colega);
        colega.SetMediator(this);
    }

    public void EnviarMsg(string msg, Colega remetente)
    {
        foreach(var colega in colegas)
        {
            if (colega != remetente)
            {
                colega.ReceberMsg(msg);
            }
        }
    }
}

public class Membro: Colega
{
    private string nome;
    public Membro(string nome)
    {
        this.nome = nome;
    }

    public void Enviar(string msg)
    {
        Console.WriteLine($"\n({nome} ENVIANDO MENSAGEM): {msg}\n");
        mediator.EnviarMsg(msg, this);
    }

    public override void ReceberMsg(string msg)
    {
        Console.WriteLine($"({nome} RECEBEU MENSAGEM): {msg}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        ConcreteMediator chat = new ConcreteMediator();

        Membro leticia = new Membro("Letícia");
        Membro nicolas = new Membro("Nicolas");
        Membro pedro = new Membro("Pedro");

        chat.Registrar(leticia);
        chat.Registrar(nicolas);
        chat.Registrar(pedro);

        leticia.Enviar("Oii, boa noite");
        nicolas.Enviar("Boa noiteee grupo");
        pedro.Enviar("Todos bem?");

        Console.ReadLine();
    }
}
