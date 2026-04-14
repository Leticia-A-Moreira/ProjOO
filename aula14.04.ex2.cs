using System;
using System.Linq.Expressions;
public interface IntBebida
{
    string descricaoFinal();
    double custoTotal();
}
public class CafeExpresso : IntBebida
{
    public string descricaoFinal() => "Café Expresso";
    public double custoTotal() => 3.00;
}

public class Cappuccino : IntBebida
{
    public string descricaoFinal() => "Cappuccino de Avelã";
    public double custoTotal() => 8.00;
}

public class Cha : IntBebida
{
    public string descricaoFinal() => "Chá Preto com Frutas Vermelhas";
    public double custoTotal() => 5.00;
}



public abstract class addDecorator : IntBebida
{
    protected IntBebida _bebida;

    public addDecorator(IntBebida bebida)
    {
        _bebida = bebida;
    }

    public virtual string descricaoFinal()
    {
        return _bebida.descricaoFinal();
    }
    public virtual double custoTotal()
    {
        return _bebida.custoTotal();
    }
}

public class Leite : addDecorator
{
    public Leite(IntBebida bebida) : base(bebida) { }

    public override string descricaoFinal() => base.descricaoFinal() + ", com Leite";
    public override double custoTotal() => base.custoTotal() + 2.00;
}

public class Chantilly : addDecorator
{
    public Chantilly(IntBebida bebida) : base(bebida) { }

    public override string descricaoFinal() => base.descricaoFinal() + ", com Chantilly";
    public override double custoTotal() => base.custoTotal() + 3.00;
}

public class CaldaChocolate : addDecorator
{
    public CaldaChocolate(IntBebida bebida) : base(bebida) { }

    public override string descricaoFinal() => base.descricaoFinal() + ", com Calda de Chocolate";
    public override double custoTotal() => base.custoTotal() + 5.00;
}



public class Program
{
    public static void Main()
    {
        Console.WriteLine("\n--OLÁ, BEM VINDO A CAFETERIA--\n");

        IntBebida pedido1 = new CafeExpresso();
        Console.WriteLine($"\nCliente 1, faça seu pedido: {pedido1.descricaoFinal()}");
        Console.WriteLine($"Beleza, o total ficou R$ {pedido1.custoTotal():0.00}");

        IntBebida pedido2 = new Cappuccino();
        pedido2 = new Leite(pedido2);
        pedido2 = new Chantilly(pedido2); 

        Console.WriteLine($"\nCliente 2, faça seu pedido: {pedido2.descricaoFinal()}");
        Console.WriteLine($"Beleza, o total ficou R$ {pedido2.custoTotal():0.00}");

        IntBebida pedido3 = new Leite(new Cha());

        Console.WriteLine($"\nCliente 3, faça seu pedido: {pedido3.descricaoFinal()}");
        Console.WriteLine($"Beleza, o total ficou R$ {pedido3.custoTotal():0.00}");
    }
}
