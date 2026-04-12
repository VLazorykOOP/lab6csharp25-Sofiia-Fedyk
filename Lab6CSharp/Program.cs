//  Task 1.7: Class Hierarchy 
Console.WriteLine(" Task 1.7: Class Hierarchy \n");

var assessments = new List<Assessment>
{
    new Test("Mathematics", "10.04.2025", 100, 20, true),
    new Exam("Physics",     "15.04.2025",  60, "Ivanov I.",  TimeSpan.FromMinutes(90)),
    new FinalExam("Thesis", "20.06.2025", 200, "Petrov P.",  TimeSpan.FromMinutes(30), "Academic Council", true),
    new Test("Algorithms",  "05.04.2025",  80, 15, false),
};

Console.WriteLine(" All assessments ");
foreach (var a in assessments) a.Show();

Console.WriteLine("\n Type checks (is / as) ");
foreach (var a in assessments)
{
    if      (a is FinalExam fe) Console.WriteLine($"  FinalExam → commission: {fe.Commission}");
    else if (a is Exam ex)      Console.WriteLine($"  Exam → professor: {ex.Professor}");
    else if (a is Test t)       Console.WriteLine($"  Test → questions: {t.QuestionCount}, online: {t.IsOnline}");

    Console.WriteLine($"    Description: {(a as IDescribable)?.GetDescription()}");
}

Console.WriteLine("\n Sorted by MaxScore (IComparable) ");
assessments.Sort();
foreach (var a in assessments) Console.WriteLine($"  {a.GetBasicInfo()}");

Console.WriteLine("\n IsPassed(score=55) ");
foreach (var a in assessments)
    Console.WriteLine($"  {a.Subject}: {(a.IsPassed(55) ? "passed" : "failed")}");

Console.WriteLine("\n Cloning (ICloneable) ");
var orig = new Test("Chemistry", "12.04.2025", 100, 10, true);
var copy = (Test)orig.Clone();
copy.Subject = "Chemistry (copy)";
Console.WriteLine($"  Original: {orig.GetDescription()}");
Console.WriteLine($"  Clone:    {copy.GetDescription()}");

//  Task 2.2: IFunction 
Console.WriteLine("\n Task 2.2: IFunction \n");

IFunction[] functions =
{
    new Line(2, 3),
    new Kub(1, -2, 1),
    new Hyperbola(5, 0),
    new Line(-1, 10),
    new Kub(0.5, 0, -3),
};

double x = 2.5;
Console.WriteLine($" Values at x = {x} ");
foreach (var fn in functions) fn.Show(x);

Array.Sort(functions);
Console.WriteLine("\n Sorted by f(1) ");
foreach (var fn in functions) fn.Show(1);

Console.WriteLine("\n Type checks ");
foreach (var fn in functions)
{
    if      (fn is Hyperbola h) Console.WriteLine($"  Hyperbola: a={h.A}, b={h.B}");
    else if (fn is Kub k)       Console.WriteLine($"  Kub: a={k.A}, b={k.B}, c={k.C}");
    else if (fn is Line l)      Console.WriteLine($"  Line: a={l.A}, b={l.B}");
}

Console.WriteLine("\n Hyperbola(5,0) at x=0 ");
try { new Hyperbola(5, 0).Show(0); }
catch (DivideByZeroException ex) { Console.WriteLine($"  [DivideByZeroException] {ex.Message}"); }

//  Task 3.1: Exceptions 
Console.WriteLine("\n Task 3.1: Exceptions \n");

Console.WriteLine(" ArrayTypeMismatchException ");
try
{
    object[] objs = new string[3];
    objs[0] = 42;
}
catch (ArrayTypeMismatchException ex) { Console.WriteLine($"  [ArrayTypeMismatchException] {ex.Message}"); }

Console.WriteLine("\n Custom StudentDataException ");
try   { RatingValidator.Validate(-5); }
catch (StudentDataException ex) { Console.WriteLine($"  [StudentDataException] {ex.Message}"); }

Console.WriteLine("\n finally block ");
try   { RatingValidator.Validate(120); }
catch (StudentDataException ex) { Console.WriteLine($"  [StudentDataException] {ex.Message}"); }
finally { Console.WriteLine("  [finally] Validation complete."); }

//  Task 4: StudentCollection 
Console.WriteLine("\n Task 4: StudentCollection \n");

var collection = new StudentCollection(new StudentStruct[]
{
    new("Kovalenko Ivan",   "Shevchenko St. 1",    "CS-31", 75.5),
    new("Petrenko Maria",   "Lesi Ukrainky St. 5", "CS-31", 45.0),
    new("Boiko Oleg",       "Franka St. 12",       "CS-32", 88.0),
    new("Melnyk Yulia",     "Sadova St. 3",        "CS-32", 55.0),
    new("Hrytsenko Dmytro", "Centralna St. 7",     "CS-31", 30.0),
});

Console.WriteLine(" Initial list (foreach) ");
foreach (StudentStruct s in collection) Console.WriteLine(s);

Console.WriteLine($"\nIndexer [1]: {collection[1]}");

double minRating = 50.0;
collection = collection.RemoveBelow(minRating);
Console.WriteLine($"\n After removing rating < {minRating} ");
foreach (StudentStruct s in collection) Console.WriteLine(s);

collection = collection.AddStudent(new("New Student", "Nova St. 1", "CS-33", 91.0));
Console.WriteLine("\n After adding new student ");
foreach (StudentStruct s in collection) Console.WriteLine(s);
