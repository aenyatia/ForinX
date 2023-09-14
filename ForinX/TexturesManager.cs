using SFML.Graphics;

namespace ForinX;

public enum Id
{
    Ahri
}

public class TexturesManager
{
    private readonly Dictionary<Id, Texture> _map = new();

    public TexturesManager()
    {
        Load(Id.Ahri, "../../../Images/ahri.png");
    }
    
    public void Load(Id id, string filename) => _map[id] = new Texture(filename);

    public Texture Get(Id id) => _map[id];
}