using SQLite4Unity3d;

public class TestDatabase
{
    [PrimaryKey, AutoIncrement]
    public int    Id     { get; set; }
    public string Name   { get; set; }
    public int    Age    { get; set; }
    public float  Height { get; set; }
    public float  Weight { get; set; }

    public override string ToString()
    {
        return string.Format("[Person: Id={0}, Name={1}, Age={2}, Height={3}], Weight={4}]", Id, Name, Age, Height, Weight);
    }
}