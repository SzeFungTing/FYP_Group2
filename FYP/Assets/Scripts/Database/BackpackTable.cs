using SQLite4Unity3d;

public class BackpackTable
{
    //public int PlayerId { get; set; }
    public int ItemId { get; set; }
    public int Count { get; set; }
    public int PosId { get; set; }

    public override string ToString()
    {
        //return string.Format("[Backpack: PlayerId={0}, ItemId={1}, Count={2}, PosId={3}", PlayerId, ItemId, Count, PosId);
        return string.Format("[Backpack: ItemId={1}, Count={2}, PosId={3}", ItemId, Count, PosId);
    }
}
