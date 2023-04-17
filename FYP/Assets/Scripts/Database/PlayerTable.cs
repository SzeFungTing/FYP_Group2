using SQLite4Unity3d;

public class PlayerTable
{
    //[PrimaryKey]
    //public int Id { get; set; }
    public float PosX { get; set; }
    public float PosY { get; set; }
    public float PosZ { get; set; }
    public int Coin { get; set; }
    public bool HvJetpack { get; set; }
    public int MapId { get; set; }

    public override string ToString()
    {
        //return string.Format("[Player: Id={0}, PosX={1}], PosY={2}], PosZ={3}], Coin={4}, HvJetpack={5}", Id, PosX, PosY, PosZ, Coin, HvJetpack);
        return string.Format("[Player: PosX={1}], PosY={2}], PosZ={3}], Coin={4}, HvJetpack={5}", PosX, PosY, PosZ, Coin, HvJetpack);
    }
}
