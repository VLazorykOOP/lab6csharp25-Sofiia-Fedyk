// Task 4 — StudentCollection with IEnumerable + IEnumerator

using System.Collections;

public struct StudentStruct
{
    public string FullName;
    public string Address;
    public string Group;
    public double Rating;

    public StudentStruct(string fullName, string address, string group, double rating)
    { FullName = fullName; Address = address; Group = group; Rating = rating; }

    public override string ToString() =>
        $"  {FullName,-20} | {Group,-6} | Rating: {Rating:F1} | {Address}";
}

public class StudentCollection : IEnumerable, IEnumerator
{
    private readonly StudentStruct[] _students;
    private int _index = -1;

    public StudentCollection(StudentStruct[] students) => _students = students;

    public int Count => _students.Length;

    public IEnumerator GetEnumerator() { Reset(); return this; }

    public bool MoveNext()
    {
        if (_index >= _students.Length - 1) { Reset(); return false; }
        _index++;
        return true;
    }

    public void Reset() => _index = -1;

    public object Current
    {
        get
        {
            if (_index < 0 || _index >= _students.Length)
                throw new InvalidOperationException("Enumerator out of range");
            return _students[_index];
        }
    }

    public StudentStruct this[int i]
    {
        get
        {
            if (i < 0 || i >= _students.Length)
                throw new IndexOutOfRangeException($"Index {i} out of range [0..{_students.Length - 1}]");
            return _students[i];
        }
    }

    public StudentCollection RemoveBelow(double minRating) =>
        new(Array.FindAll(_students, s => s.Rating >= minRating));

    public StudentCollection AddStudent(StudentStruct student)
    {
        var arr = new StudentStruct[_students.Length + 1];
        Array.Copy(_students, arr, _students.Length);
        arr[_students.Length] = student;
        return new StudentCollection(arr);
    }
}
