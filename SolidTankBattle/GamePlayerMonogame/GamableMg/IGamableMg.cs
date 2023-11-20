#nullable enable
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GamePlayerMonogame.GamableMg;

public interface IGamableMg
{
    void SetSpriteBatch(SpriteBatch spriteBatch);

    SpriteBatch? GetSpriteBatch();

    void SetTexture(Texture2D texture2D);

    Texture2D GetTexture();

    void SetColor(Color color);

    Color? GetColor();

    void SetLocation(Vector2 location);

    Vector2? GetLocation();

    void SetAngular(int angular);

    int? GetAngular();

    void Update();

    void Draw();
}