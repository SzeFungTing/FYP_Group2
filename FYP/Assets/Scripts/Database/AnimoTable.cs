using SQLite4Unity3d;

public class AnimoTable
{
    [PrimaryKey, AutoIncrement]
    public int? Id { get; set; }
    public int AnimoId { get; set; }
    public float PosX { get; set; }
    public float PosY { get; set; }
    public float PosZ { get; set; }
    public bool IsHungry { get; set; }
    public int MapId { get; set; }
    

    public override string ToString()
    {
        return string.Format("[Animo: Id={0}, AnimoId={1}, PosX={2}, PosY={3}, PosZ={4}, IsHungry={5}, MapId={6}", Id, AnimoId, PosX, PosY, PosZ, IsHungry, MapId);
    }
}
