// --- INVERSÃO DE CONTROLE ---
// Inversão de Controle (IoC): O controle do fluxo de dados foi invertido.
// A plataforma não "empurra" (Push) mais os dados para as universidades.

// Estratégia "Pull" (Puxar): A plataforma apenas avisa passando
// sua própria referência (usando a palavra-chave 'this' na notificação).

// Flexibilidade: O observador (Universidade) recebe essa referência (ISubject),
// converte para a plataforma específica e decide quais dados quer "puxar".

using System;
using System.Collections.Generic;

namespace ExAula1704
{
    public interface ISubject
    {
        void AddObserver(IObserver observador);
        void RemObserver(IObserver observador);

    }
    public interface IObserver
    {
        string Nome
        {
            get;
        }
        // Recebe o sujeito inteiro em vez de parâmetros específicos.
        void Atualizar(ISubject plataforma);
    }


    public class PlataformaDeColetaDados : ISubject
    {
        private readonly List<IObserver> _observadores;
        private double _temp;
        private double _ph;
        private double _umid;

        // Propriedades públicas (getters) criadas para permitir que o observador "puxe" os dados.
        public double Temp { get { return _temp; } }
        public double Ph { get { return _ph; } }
        public double Umid { get { return _umid; } }

        public string NomePlataforma
        {
            get;
        }

        public PlataformaDeColetaDados(string nomePlataforma)
        {
            NomePlataforma = nomePlataforma;
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

        private void Notificacao()
        {
            // A plataforma passa a si mesma (this) no callback.
            _observadores.ForEach(obs => obs.Atualizar(this));
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

        public void Atualizar(ISubject plataforma)
        {
            // O observador converte a interface genérica e "puxa" os dados necessários.
            var pdc = plataforma as PlataformaDeColetaDados;

            if (pdc != null)
            {
                Console.WriteLine($"NOTIFICAÇÃO PARA {Nome}\n Dados da Plataforma de '{pdc.NomePlataforma}': Temp: {pdc.Temp}°C; pH: {pdc.Ph}; Umidade: {pdc.Umid}%\n\n");
            }
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
