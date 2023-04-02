using SQLite4Unity3d;

public class BuildingTable
{
    public int Id { get; set; }
    public float PosX { get; set; }
    public float PosY { get; set; }
    public float PosZ { get; set; }
    public int MapId { get; set; }

    public override string ToString()
    {
        return string.Format("[Building: Id={0}, PosX={1}, PosY={2}, PosZ={3}, MapId={4}", Id, PosX, PosY, PosZ, MapId);
    }
}
