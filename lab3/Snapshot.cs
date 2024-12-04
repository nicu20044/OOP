namespace lab3;
public class Snapshot
{
    private DateTime lastSnapshotTime;

    public Snapshot()
    {
        lastSnapshotTime = DateTime.Now;
    }

    public void Update()
    {
        lastSnapshotTime = DateTime.Now;
    }

    public DateTime GetLastSnapshotTime()
    {
        return lastSnapshotTime;
    }

    public string GetFormattedSnapshotTime()
    {
        return lastSnapshotTime.ToString("dd.mm.yyyy HH:mm:ss");
    }

    public override string ToString()
    {
        return $"Snapshot taken at: {lastSnapshotTime}";
    }
}
