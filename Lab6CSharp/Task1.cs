// Task 1.7 — Hierarchy: Assessment, Test, Exam, FinalExam

public interface IShowable  { void Show(); }
public interface IDescribable { string GetDescription(); }

public abstract class Assessment : IShowable, IDescribable, IComparable<Assessment>, ICloneable
{
    public string Subject { get; set; }
    public string Date    { get; set; }
    public int    MaxScore { get; set; }

    protected Assessment(string subject, string date, int maxScore)
    {
        Subject = subject; Date = date; MaxScore = maxScore;
    }

    public abstract void   Show();
    public abstract string GetDescription();
    public abstract bool   IsPassed(int score);

    public virtual string Type => "Assessment";

    public string GetBasicInfo() =>
        $"[{Type}] Subject: {Subject}, Date: {Date}, Max: {MaxScore}";

    public int    CompareTo(Assessment? other) => other == null ? 1 : MaxScore.CompareTo(other.MaxScore);
    public virtual object Clone() => MemberwiseClone();
}

public class Test : Assessment
{
    public int  QuestionCount { get; set; }
    public bool IsOnline      { get; set; }

    public Test(string subject, string date, int maxScore, int questionCount, bool isOnline)
        : base(subject, date, maxScore)
    { QuestionCount = questionCount; IsOnline = isOnline; }

    public override string Type => "Test";

    public override void Show()
    {
        Console.WriteLine($"  [{Type}] {Subject} | {Date} | Max: {MaxScore}");
        Console.WriteLine($"    Questions: {QuestionCount}, Online: {(IsOnline ? "yes" : "no")}");
    }

    public override string GetDescription() =>
        $"{Type} on {Subject} ({QuestionCount} questions, {(IsOnline ? "online" : "offline")})";

    public override bool IsPassed(int score) => score >= MaxScore * 0.6;

    public override object Clone() => new Test(Subject, Date, MaxScore, QuestionCount, IsOnline);
}

public class Exam : Assessment
{
    public string   Professor { get; set; }
    public TimeSpan Duration  { get; set; }

    public Exam(string subject, string date, int maxScore, string professor, TimeSpan duration)
        : base(subject, date, maxScore)
    { Professor = professor; Duration = duration; }

    public override string Type => "Exam";

    public override void Show()
    {
        Console.WriteLine($"  [{Type}] {Subject} | {Date} | Max: {MaxScore}");
        Console.WriteLine($"    Professor: {Professor}, Duration: {Duration.TotalMinutes} min");
    }

    public override string GetDescription() => $"{Type} on {Subject} by {Professor}";
    public override bool   IsPassed(int score) => score >= MaxScore * 0.5;
    public override object Clone() => new Exam(Subject, Date, MaxScore, Professor, Duration);
}

public class FinalExam : Exam
{
    public string Commission { get; set; }
    public bool   IsPublic   { get; set; }

    public FinalExam(string subject, string date, int maxScore,
                     string professor, TimeSpan duration, string commission, bool isPublic)
        : base(subject, date, maxScore, professor, duration)
    { Commission = commission; IsPublic = isPublic; }

    public override string Type => "Final Exam";

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"    Commission: {Commission}, Public: {(IsPublic ? "yes" : "no")}");
    }

    public override string GetDescription() => $"{Type} on {Subject}, commission: {Commission}";
    public override bool   IsPassed(int score) => score >= MaxScore * 0.65;
    public override object Clone() =>
        new FinalExam(Subject, Date, MaxScore, Professor, Duration, Commission, IsPublic);
}
