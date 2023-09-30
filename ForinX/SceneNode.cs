using SFML.Graphics;
using SFML.System;

namespace ForinX;

public class SceneNode : Drawable
{
    private readonly IList<SceneNode> _children = new List<SceneNode>();

    public SceneNode? Parent { get; set; }

    public void AttachChild(SceneNode child)
    {
        child.Parent = this;
        _children.Add(child);
    }

    public SceneNode? DetachChild(SceneNode child)
    {
        var item = _children.FirstOrDefault(c => c == child);

        if (item is null) return item;

        item.Parent = null;
        _children.Remove(item);

        return item;
    }

    public void Draw(RenderTarget target, RenderStates states)
    {
        target.Draw(this, states);
    }

    public void Update(Time dt)
    {
        UpdateThis(dt);
        UpdateChildren(dt);
    }

    private void UpdateChildren(Time dt)
    {
        foreach (var child in _children)
            child.Update(dt);
    }

    private void UpdateThis(Time dt)
    {
    }
}