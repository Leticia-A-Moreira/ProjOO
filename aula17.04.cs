using System;
using System.Collections.Generic;

namespace ExAula1704
{
    public interface ISubject
    {
        void AddObserver(IObserver observador);
        void RemObserver(IObserver observador);
        void Notificacao();

    }
    public interface IObserver
    {
        string Nome 
        { 
            get; 
        }
        void Atualizar(string PCD, double temp, double ph, double umid);
    }
    

    public class PlataformaDeColetaDados: ISubject
    {
        private readonly List<IObserver> _observadores;
        private double _temp;
        private double _ph;
        private double _umid;

        public string NomePlataforma
        {
            get; 
        }

        public PlataformaDeColetaDados(string nomePlataforma)
        {
            NomePlataforma= nomePlataforma;
            _observadores = new List<IObserver>();
        }

        public void AddObserver(IObserver observador)
        {
            _observadores.Add(observador);
            Console.WriteLine($"{observador.Nome} esta conectada à plataforma {NomePlataforma}.");
        }

        public void RemObserver(IObserver observador)
        {
            _observadores.Remove(observador);
            Console.WriteLine($"{observador.Nome} foi removido da plataforma {NomePlataforma}.");
        }

        public void Notificacao()
        {
            _observadores.ForEach(obs => obs.Atualizar(NomePlataforma, _temp, _ph, _umid));
        }
        public void SetDados(double temp, double ph, double umid)
        {
            Console.WriteLine($"\n--- NOTIFICAÇÃO DA PLATAFORMA {NomePlataforma} ---");
            _temp = temp;
            _ph = ph;
            _umid = umid;

            Notificacao();
        }
    }

    public class Universidade : IObserver
    {
        public string Nome { get; }

        public Universidade(string nome)
        {
            Nome = nome;
        }

        public void Atualizar(string nomePcd, double temp, double ph, double umid)
        {
            Console.WriteLine($"NOTIFICAÇÃO PARA {Nome}\n Dados da Plataforma de '{nomePcd}': Temp: {temp}°C; pH: {ph}; Umidade: {umid}%\n\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var pcdRJurua = new PlataformaDeColetaDados("Rio Juruá");
            var pcdRSolimoes = new PlataformaDeColetaDados("Rio Solimões");

            var POA = new Universidade("Universidade de POA");
            var SP = new Universidade("Universidade de SP");
            var RJ = new Universidade("Universidade do RJ");
            var SJC = new Universidade("Universidade de SJC");
            var Brasilia = new Universidade("Universidade de Brasília");

            Console.WriteLine("--- CONFIGURANDO DADOS E UNIS ---");

            pcdRJurua.AddObserver(SP);
            pcdRSolimoes.AddObserver(SP);

            pcdRJurua.AddObserver(SJC);
            pcdRSolimoes.AddObserver(SJC);

            pcdRJurua.AddObserver(POA);

            pcdRSolimoes.AddObserver(Brasilia);

            pcdRSolimoes.AddObserver(RJ);
            

            Console.WriteLine("\n--- PROCURANDO DADOS ---");

            pcdRJurua.SetDados(28.5, 6.8, 87.0);
            pcdRSolimoes.SetDados(27.0, 7.1, 92.5);

            Console.WriteLine("\n--- ATUALIZANDO ---");

            pcdRJurua.RemObserver(POA);
            pcdRJurua.SetDados(29.1, 6.5, 79.0);

            Console.ReadLine();
        }
    }
}
