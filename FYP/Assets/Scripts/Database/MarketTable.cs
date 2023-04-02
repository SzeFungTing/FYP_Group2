using SQLite4Unity3d;

public class MarketTable
{
    public int Id { get; set; }
    public int NumRemind { get; set; }

    public override string ToString()
    {
        return string.Format("[Market: Id={0}, NumRemind={1}", Id, NumRemind);
    }
}

