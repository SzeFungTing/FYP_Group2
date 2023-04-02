using SQLite4Unity3d;

public class FajroTable
{
    public int Id { get; set; }
    public int DefaultPrice { get; set; }
    public int CurrentPrice { get; set; }

    public override string ToString()
    {
        return string.Format("[Fajro: Id={0}, DefaultPrice={1}, CurrentPrice={2}", Id, DefaultPrice, CurrentPrice);
    }
}

