using SFML.Graphics;
using SFML.System;

namespace ForinX;

public class World
{
    private const float ScrollSpeed = 10f;

    private readonly RenderWindow _window;
    private readonly View _worldView;
    private readonly TexturesManager _textures;

    private readonly SceneNode _sceneGraph = new();
    private readonly SceneNode[] _sceneLayers = new SceneNode[(int)Layers.LayerCount];

    private readonly FloatRect _worldBounds;

    private readonly Vector2f _spawnPosition;

    private Aircraft? _aircraft;

    public World(RenderWindow window, TexturesManager textures)
    {
        _window = window;
        _textures = textures;
        _worldView = window.DefaultView;
        _worldBounds = new FloatRect(0f, 0f, _worldView.Size.X, 2000f);
        _spawnPosition = new Vector2f(_worldView.Size.X / 2f, _worldBounds.Height - _worldView.Size.Y); // ???
        _aircraft = null;

        LoadTextures();
        BuildScene();

        _worldView.Center = _spawnPosition;
    }

    public void Update(Time dt)
    {
        _worldView.Move(new Vector2f(0f, ScrollSpeed * dt.AsSeconds()));

        if (_aircraft is not null)
        {
            var position = _aircraft.Position;
            var velocity = _aircraft.Velocity;

            if (position.X <= _worldBounds.Left + 150 ||
                position.X >= _worldBounds.Left + _worldBounds.Width - 150)
            {
                velocity.X = -velocity.X;
                _aircraft.Velocity = velocity;
            }
        }

        _sceneGraph.Update(dt);
    }

    public void Draw()
    {
        _window.SetView(_worldView);
        _window.Draw(_sceneGraph);
    }

    private void LoadTextures()
    {
        _textures.Load(TextureId.Eagle, "Media/Textures/Eagle.png");
        _textures.Load(TextureId.Raptor, "Media/Textures/Raptor.png");
        _textures.Load(TextureId.Desert, "Media/Textures/Desert.png");
    }

    private void BuildScene()
    {
        for (var i = 0; i < _sceneLayers.Length; i++)
        {
            var layer = new SceneNode();

            _sceneLayers[i] = layer;

            _sceneGraph.AttachChild(layer);
        }

        var texture = _textures.Get(TextureId.Desert);
        var textureRect = _worldBounds;
        texture.Repeated = true;

        var backgroundSprite = new SpriteNode(texture, textureRect);
        backgroundSprite.Position = new Vector2f(_worldBounds.Left, _worldBounds.Top);
        _sceneLayers[(int)Layers.Background].AttachChild(backgroundSprite);

        var leader = new Aircraft(Aircraft.AircraftType.Eagle, _textures);
        _aircraft = leader;
        _aircraft.Position = _spawnPosition;
        _aircraft.Velocity = new Vector2f(40f, ScrollSpeed);
        _sceneLayers[(int)Layers.Air].AttachChild(leader);

        var leftEscort = new Aircraft(Aircraft.AircraftType.Raptor, _textures);
        leftEscort.Position = new Vector2f(-80f, 50f);
        _aircraft.AttachChild(leftEscort);

        var rightEscort = new Aircraft(Aircraft.AircraftType.Raptor, _textures);
        leftEscort.Position = new Vector2f(80f, 50f);
        _aircraft.AttachChild(rightEscort);
    }
}