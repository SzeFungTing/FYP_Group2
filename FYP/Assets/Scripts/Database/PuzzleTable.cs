using SQLite4Unity3d;

public class PuzzleTable
{
    public int Id { get; set; }
    public bool IsClear { get; set; }

    public override string ToString()
    {
        return string.Format("[Puzzle: Id={0}, IsClear={1}", Id, IsClear);
    }
}
