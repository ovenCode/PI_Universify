namespace webapi.Models;

public class GrupaStudent
{
    public long GrupyIdGrupy { get; set; }
    public long StudenciIdStudenta { get; set; }

    public Grupa Grupa { get; set; } = null!;
    public Student Student { get; set; } = null!;
}
