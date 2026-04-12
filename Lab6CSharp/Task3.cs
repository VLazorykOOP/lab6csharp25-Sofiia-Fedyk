// Task 3.1 — ArrayTypeMismatchException + custom StudentDataException

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

public static class RatingValidator
{
    public static void Validate(double rating)
    {
        if (rating < 0)   throw new StudentDataException($"Rating cannot be negative: {rating}");
        if (rating > 100) throw new StudentDataException($"Rating cannot exceed 100: {rating}");
        Console.WriteLine($"  Rating {rating} — valid.");
    }
}
