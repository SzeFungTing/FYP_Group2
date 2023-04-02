using SQLite4Unity3d;

public class AnimoTable
{
    public int Id { get; set; }
    public float PosX { get; set; }
    public float PosY { get; set; }
    public float PosZ { get; set; }
    public bool IsHungry { get; set; }
    public int MapId { get; set; }

    public override string ToString()
    {
        return string.Format("[Animo: Id={0}, PosX={1}, PosY={2}, PosZ={3}, IsHungry={4}, MapId={5}", Id, PosX, PosY, PosZ, IsHungry, MapId);
    }
}
