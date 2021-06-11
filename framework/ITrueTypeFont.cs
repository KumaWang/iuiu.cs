using System.Numerics;

namespace engine.framework
{
    public interface ITrueTypeFontChar
    {
        Vector2 Size { get; }

        Vertices[] Triangles { get; }
    }

    public interface ITrueTypeFont
    {
        float GetScale(float size);

        ITrueTypeFontChar GetChar(char @char);
    }
}
