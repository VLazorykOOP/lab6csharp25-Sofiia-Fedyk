// Task 2.2 — IFunction: Line, Kub, Hyperbola

public interface IFunction : IComparable
{
    double Calculate(double x);
    string Formula { get; }
    void   Show(double x);
}

public abstract class MathFunction : IFunction
{
    public abstract double Calculate(double x);
    public abstract string Formula { get; }

    public void Show(double x) =>
        Console.WriteLine($"  {Formula,-30} => f({x}) = {Calculate(x):F4}");

    public int CompareTo(object? obj)
    {
        if (obj is IFunction other) return Calculate(1).CompareTo(other.Calculate(1));
        throw new ArgumentException("Object is not IFunction");
    }
}

public class Line : MathFunction
{
    public double A { get; set; }
    public double B { get; set; }

    public Line(double a, double b) { A = a; B = b; }

    public override double Calculate(double x) => A * x + B;
    public override string Formula => $"y = {A}*x + {B}";
}

public class Kub : MathFunction
{
    public double A { get; set; }
    public double B { get; set; }
    public double C { get; set; }

    public Kub(double a, double b, double c) { A = a; B = b; C = c; }

    public override double Calculate(double x) => A * x * x + B * x + C;
    public override string Formula => $"y = {A}*x^2 + {B}*x + {C}";
}

public class Hyperbola : MathFunction
{
    public double A { get; set; }
    public double B { get; set; }

    public Hyperbola(double a, double b) { A = a; B = b; }

    public override double Calculate(double x)
    {
        if (x == 0) throw new DivideByZeroException("Hyperbola argument cannot be 0!");
        return A / x + B;
    }

    public override string Formula => $"y = {A}/x + {B}";
}
