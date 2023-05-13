using SQLite4Unity3d;

public class BuildingTable
{
    [SQLite4Unity3d.PrimaryKey, SQLite4Unity3d.AutoIncrement]
    public int? Id { get; set; }
    public int BuildingId { get; set; }
    public float PosX { get; set; }
    public float PosY { get; set; }
    public float PosZ { get; set; }
    public float Rotation { get; set; }
    public int MapId { get; set; }

    public override string ToString()
    {
        return string.Format("[Building: Id={0}, BuildingId={1}, PosX={2}, PosY={3}, PosZ={4}, Rotation={5}, MapId={6}", Id, BuildingId, PosX, PosY, PosZ, Rotation, MapId);
    }
}
