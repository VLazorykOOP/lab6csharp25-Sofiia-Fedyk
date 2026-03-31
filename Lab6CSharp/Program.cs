using System.Collections;


// ЗАВДАННЯ 1.7 — Ієрархія: Випробування, Тест, Іспит, Випускний іспит
// Базові: користувацькі інтерфейси + .NET IComparable, ICloneable


//  Користувацькі інтерфейси 

public interface IShowable
{
    void Show();
}

public interface IDescribable
{
    string GetDescription();
}

//  Абстрактний базовий клас 

public abstract class Assessment : IShowable, IDescribable,
                                    IComparable<Assessment>, ICloneable
{
    public string Subject { get; set; }
    public string Date { get; set; }
    public int MaxScore { get; set; }

    protected Assessment(string subject, string date, int maxScore)
    {
        Subject = subject;
        Date = date;
        MaxScore = maxScore;
    }

    public abstract void Show();
    public abstract string GetDescription();
    public abstract bool IsPassed(int score);

    public virtual string Type => "Випробування";

    // Не віртуальний метод
    public string GetBasicInfo() =>
        $"[{Type}] Предмет: {Subject}, Дата: {Date}, Макс. балів: {MaxScore}";

    // IComparable<Assessment> — сортування за MaxScore
    public int CompareTo(Assessment? other)
    {
        if (other == null) return 1;
        return MaxScore.CompareTo(other.MaxScore);
    }

    // ICloneable
    public virtual object Clone() => MemberwiseClone();
}

//  Тест 

public class Test : Assessment
{
    public int QuestionCount { get; set; }
    public bool IsOnline { get; set; }

    public Test(string subject, string date, int maxScore, int questionCount, bool isOnline)
        : base(subject, date, maxScore)
    {
        QuestionCount = questionCount;
        IsOnline = isOnline;
    }

    public override string Type => "Тест";

    public override void Show()
    {
        Console.WriteLine($"  [{Type}] {Subject} | {Date} | Макс: {MaxScore}");
        Console.WriteLine($"    Питань: {QuestionCount}, Онлайн: {(IsOnline ? "так" : "ні")}");
    }

    public override string GetDescription() =>
        $"{Type} з {Subject} ({QuestionCount} питань, {(IsOnline ? "онлайн" : "офлайн")})";

    public override bool IsPassed(int score) => score >= MaxScore * 0.6;

    public override object Clone() =>
        new Test(Subject, Date, MaxScore, QuestionCount, IsOnline);
}

//  Іспит 

public class Exam : Assessment
{
    public string Professor { get; set; }
    public TimeSpan Duration { get; set; }

    public Exam(string subject, string date, int maxScore,
                string professor, TimeSpan duration)
        : base(subject, date, maxScore)
    {
        Professor = professor;
        Duration = duration;
    }

    public override string Type => "Іспит";

    public override void Show()
    {
        Console.WriteLine($"  [{Type}] {Subject} | {Date} | Макс: {MaxScore}");
        Console.WriteLine($"    Викладач: {Professor}, Тривалість: {Duration.TotalMinutes} хв");
    }

    public override string GetDescription() =>
        $"{Type} з {Subject} у викладача {Professor}";

    public override bool IsPassed(int score) => score >= MaxScore * 0.5;

    public override object Clone() =>
        new Exam(Subject, Date, MaxScore, Professor, Duration);
}

//  Випускний іспит 

public class FinalExam : Exam
{
    public string Commission { get; set; }
    public bool IsPublic { get; set; }

    public FinalExam(string subject, string date, int maxScore,
                     string professor, TimeSpan duration,
                     string commission, bool isPublic)
        : base(subject, date, maxScore, professor, duration)
    {
        Commission = commission;
        IsPublic = isPublic;
    }

    public override string Type => "Випускний іспит";

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"    Комісія: {Commission}, Відкритий: {(IsPublic ? "так" : "ні")}");
    }

    public override string GetDescription() =>
        $"{Type} з {Subject}, комісія: {Commission}";

    public override bool IsPassed(int score) => score >= MaxScore * 0.65;

    public override object Clone() =>
        new FinalExam(Subject, Date, MaxScore, Professor, Duration, Commission, IsPublic);
}


// ЗАВДАННЯ 2.2 — Інтерфейс IFunction: Line, Kub, Hyperbola
// IFunction успадковує стандартний .NET IComparable


//  Інтерфейс IFunction успадковує IComparable (.NET) 

public interface IFunction : IComparable
{
    double Calculate(double x);
    string Formula { get; }
    void Show(double x);
}

//  Абстрактний базовий клас 

public abstract class MathFunction : IFunction
{
    public abstract double Calculate(double x);
    public abstract string Formula { get; }

    public void Show(double x)
    {
        Console.WriteLine($"  {Formula,-30} => f({x}) = {Calculate(x):F4}");
    }

    // IComparable — порівняння за значенням у x = 1
    public int CompareTo(object? obj)
    {
        if (obj is IFunction other)
            return Calculate(1).CompareTo(other.Calculate(1));
        throw new ArgumentException("Об'єкт не є IFunction");
    }
}

//  y = ax + b 

public class Line : MathFunction
{
    public double A { get; set; }
    public double B { get; set; }

    public Line(double a, double b) { A = a; B = b; }

    public override double Calculate(double x) => A * x + B;
    public override string Formula => $"y = {A}*x + {B}";
}

//  y = ax² + bx + c 

public class Kub : MathFunction
{
    public double A { get; set; }
    public double B { get; set; }
    public double C { get; set; }

    public Kub(double a, double b, double c) { A = a; B = b; C = c; }

    public override double Calculate(double x) => A * x * x + B * x + C;
    public override string Formula => $"y = {A}*x^2 + {B}*x + {C}";
}

//  y = a/x + b 

public class Hyperbola : MathFunction
{
    public double A { get; set; }
    public double B { get; set; }

    public Hyperbola(double a, double b) { A = a; B = b; }

    public override double Calculate(double x)
    {
        if (x == 0)
            throw new DivideByZeroException("Аргумент гіперболи не може бути 0!");
        return A / x + B;
    }

    public override string Formula => $"y = {A}/x + {B}";
}


// ЗАВДАННЯ 3.1 — Обробка ArrayTypeMismatchException + власний виняток


//  Власний клас винятку 

[Serializable]
public class StudentDataException : ApplicationException
{
    public StudentDataException() { }

    public StudentDataException(string message) : base(message) { }

    public StudentDataException(string message, Exception inner) : base(message, inner) { }

    protected StudentDataException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
        : base(info, context) { }
}


// ЗАВДАННЯ 4 — StudentCollection з IEnumerable + IEnumerator
// Додано до StudentStruct зі старої лабораторної №4


public struct StudentStruct
{
    public string FullName;
    public string Address;
    public string Group;
    public double Rating;

    public StudentStruct(string fullName, string address, string group, double rating)
    {
        FullName = fullName;
        Address = address;
        Group = group;
        Rating = rating;
    }

    public override string ToString() =>
        $"  {FullName,-20} | {Group,-6} | Rating: {Rating:F1} | {Address}";
}

//  Колекція студентів з підтримкою foreach 

public class StudentCollection : IEnumerable, IEnumerator
{
    private readonly StudentStruct[] _students;
    private int _index = -1;

    public StudentCollection(StudentStruct[] students)
    {
        _students = students;
    }

    public int Count => _students.Length;

    //  IEnumerable 
    public IEnumerator GetEnumerator()
    {
        Reset();      // щоразу починаємо з початку
        return this;
    }

    //  IEnumerator 
    public bool MoveNext()
    {
        if (_index >= _students.Length - 1)
        {
            Reset();
            return false;
        }
        _index++;
        return true;
    }

    public void Reset() => _index = -1;

    public object Current
    {
        get
        {
            if (_index < 0 || _index >= _students.Length)
                throw new InvalidOperationException("Перерахунок поза межами колекції");
            return _students[_index];
        }
    }

    //  Індексатор 
    public StudentStruct this[int i]
    {
        get
        {
            if (i < 0 || i >= _students.Length)
                throw new IndexOutOfRangeException($"Індекс {i} поза межами [0..{_students.Length - 1}]");
            return _students[i];
        }
    }

    //  Видалити студентів з рейтингом нижче порогу 
    public StudentCollection RemoveBelow(double minRating)
    {
        var filtered = Array.FindAll(_students, s => s.Rating >= minRating);
        return new StudentCollection(filtered);
    }

    //  Додати студента в кінець 
    public StudentCollection AddStudent(StudentStruct student)
    {
        var newArr = new StudentStruct[_students.Length + 1];
        Array.Copy(_students, newArr, _students.Length);
        newArr[_students.Length] = student;
        return new StudentCollection(newArr);
    }
}

class Program
{
    static void Main()
    {
        //  Завдання 1.7 
        Console.WriteLine(" Завдання 1.7: Ієрархія класів \n");

        var assessments = new List<Assessment>
        {
            new Test("Математика",  "10.04.2025", 100, 20, true),
            new Exam("Фізика",      "15.04.2025",  60, "Іванов І.І.",
                     TimeSpan.FromMinutes(90)),
            new FinalExam("Диплом", "20.06.2025", 200, "Петров П.П.",
                          TimeSpan.FromMinutes(30), "Вчена рада", true),
            new Test("Алгоритми",   "05.04.2025",  80, 15, false),
        };

        Console.WriteLine(" Вивід усіх випробувань ");
        foreach (var a in assessments)
            a.Show();

        Console.WriteLine("\n Перевірка типів (is / as) ");
        foreach (var a in assessments)
        {
            if (a is FinalExam fe)
                Console.WriteLine($"  FinalExam → комісія: {fe.Commission}");
            else if (a is Exam ex)
                Console.WriteLine($"  Exam → викладач: {ex.Professor}");
            else if (a is Test t)
                Console.WriteLine($"  Test → питань: {t.QuestionCount}, онлайн: {t.IsOnline}");

            // as — отримати IDescribable
            var d = a as IDescribable;
            Console.WriteLine($"    Опис: {d?.GetDescription()}");
        }

        Console.WriteLine("\n Сортування за MaxScore (IComparable) ");
        assessments.Sort();
        foreach (var a in assessments)
            Console.WriteLine($"  {a.GetBasicInfo()}");

        Console.WriteLine("\n IsPassed(score=55) ");
        foreach (var a in assessments)
            Console.WriteLine($"  {a.Subject}: {(a.IsPassed(55) ? "склав" : "не склав")}");

        Console.WriteLine("\n Клонування (ICloneable) ");
        var orig = new Test("Хімія", "12.04.2025", 100, 10, true);
        var copy = (Test)orig.Clone();
        copy.Subject = "Хімія (копія)";
        Console.WriteLine($"  Оригінал: {orig.GetDescription()}");
        Console.WriteLine($"  Клон:     {copy.GetDescription()}");

        //  Завдання 2.2 
        Console.WriteLine("\n Завдання 2.2: Інтерфейс IFunction \n");

        IFunction[] functions =
        {
            new Line(2, 3),
            new Kub(1, -2, 1),
            new Hyperbola(5, 0),
            new Line(-1, 10),
            new Kub(0.5, 0, -3),
        };

        double x = 2.5;
        Console.WriteLine($"Значення функцій у точці x = {x}:");
        foreach (var fn in functions)
            fn.Show(x);

        // Сортування через IComparable (за значенням у x=1)
        Array.Sort(functions);
        Console.WriteLine("\nПісля сортування за f(1):");
        foreach (var fn in functions)
            fn.Show(1);

        // Перевірка is / as
        Console.WriteLine("\nПеревірка типів (is / as):");
        foreach (var fn in functions)
        {
            if (fn is Hyperbola h)
                Console.WriteLine($"  Hyperbola: a={h.A}, b={h.B}");
            else if (fn is Kub k)
                Console.WriteLine($"  Kub: a={k.A}, b={k.B}, c={k.C}");
            else if (fn is Line l)
                Console.WriteLine($"  Line: a={l.A}, b={l.B}");
        }

        // Обробка винятку для x=0 у гіперболи
        Console.WriteLine("\nСпроба обчислити Hyperbola(5,0) у x=0:");
        try
        {
            var hyp = new Hyperbola(5, 0);
            hyp.Show(0);
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"  [DivideByZeroException] {ex.Message}");
        }

        //  Завдання 3.1 
        Console.WriteLine("\n Завдання 3.1: Обробка винятків \n");

        //  ArrayTypeMismatchException (стандартний) 
        Console.WriteLine(" ArrayTypeMismatchException ");
        try
        {
            // Спроба записати несумісний тип у масив рядків через object[] — викликає виняток
            object[] objs = new string[3];
            objs[0] = 42;     // <- ArrayTypeMismatchException
        }
        catch (ArrayTypeMismatchException ex)
        {
            Console.WriteLine($"  [ArrayTypeMismatchException] {ex.Message}");
        }

        //  Власний виняток StudentDataException 
        Console.WriteLine("\n Власний виняток StudentDataException ");
        try
        {
            ValidateRating(-5);
        }
        catch (StudentDataException ex)
        {
            Console.WriteLine($"  [StudentDataException] {ex.Message}");
        }

        //  finally 
        Console.WriteLine("\n Блок finally ");
        try
        {
            ValidateRating(120);
        }
        catch (StudentDataException ex)
        {
            Console.WriteLine($"  [StudentDataException] {ex.Message}");
        }
        finally
        {
            Console.WriteLine("  [finally] Перевірку завершено.");
        }

        //  Завдання 4 
        Console.WriteLine("\n Завдання 4: StudentCollection + foreach \n");

        StudentStruct[] initial =
        {
            new("Kovalenko Ivan",   "Shevchenko St. 1",    "CS-31", 75.5),
            new("Petrenko Maria",   "Lesi Ukrainky St. 5", "CS-31", 45.0),
            new("Boiko Oleg",       "Franka St. 12",       "CS-32", 88.0),
            new("Melnyk Yulia",     "Sadova St. 3",        "CS-32", 55.0),
            new("Hrytsenko Dmytro", "Centralna St. 7",     "CS-31", 30.0),
        };

        var collection = new StudentCollection(initial);

        Console.WriteLine("Початковий список (foreach через IEnumerable):");
        foreach (StudentStruct s in collection)
            Console.WriteLine(s);

        // Індексатор
        Console.WriteLine($"\nІндексатор [1]: {collection[1]}");

        // Видалення студентів з рейтингом < 50
        double minRating = 50.0;
        collection = collection.RemoveBelow(minRating);
        Console.WriteLine($"\nПісля видалення з рейтингом < {minRating}:");
        foreach (StudentStruct s in collection)
            Console.WriteLine(s);

        // Додавання нового студента
        collection = collection.AddStudent(
            new StudentStruct("New Student", "Nova St. 1", "CS-33", 91.0));
        Console.WriteLine("\nПісля додавання нового студента:");
        foreach (StudentStruct s in collection)
            Console.WriteLine(s);
    }

    //  Допоміжний метод для Завдання 3.1 
    static void ValidateRating(double rating)
    {
        if (rating < 0)
            throw new StudentDataException($"Рейтинг не може бути від'ємним: {rating}");
        if (rating > 100)
            throw new StudentDataException($"Рейтинг не може перевищувати 100: {rating}");
        Console.WriteLine($"  Рейтинг {rating} — коректний.");
    }
}