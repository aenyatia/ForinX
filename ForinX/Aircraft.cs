using SFML.Graphics;
using SFML.System;

namespace ForinX;

public class Aircraft : Entity
{
    private readonly AircraftType _aircraftType;
    private readonly TexturesManager _holder;

    public Sprite Sprite { get; set; }
    public Vector2f Position { get; set; }
    
    public Aircraft(AircraftType aircraftType, TexturesManager holder)
    {
        _aircraftType = aircraftType;
        _holder = holder;
    }

    public void Draw(RenderTarget target, RenderStates states)
    {
        target.Draw(Sprite, states);
    }

    public enum AircraftType
    {
        Eagle,
        Raptor
    }

    public void AttachChild(Aircraft leftEscort)
    {
        throw new NotImplementedException();
    }
}

public class TexturesManager
{
    public Dictionary<Texture, TextureId> TextureHolder { get; set; }

    public void Load(TextureId id, string path)
    {
        
    }

    public Texture Get(TextureId textureId)
    {
        return new Texture("");
    }
}

public enum Layers
{
    Background,
    Air,
    LayerCount
}