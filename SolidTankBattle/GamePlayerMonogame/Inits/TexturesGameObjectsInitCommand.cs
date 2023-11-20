using GamePlayerMonogame.GamableMg;
using LibraryClasses.Commands;
using Microsoft.Xna.Framework.Graphics;

namespace GamePlayerMonogame.Inits;

public class TexturesGameObjectsInitCommand : ICommand
{
    private readonly GraphicsDevice _graphicsDevice;
    private readonly SpriteBatch _spriteBatch;

    public TexturesGameObjectsInitCommand(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
    {
        this._graphicsDevice = graphicsDevice;
        _spriteBatch = spriteBatch;
    }

    public void Execute()
    {
        TexturesObjects.TexturesDictionary.Clear();

        TexturesObjects.Set("TankOneType", Texture2D.FromFile(_graphicsDevice, "Content/tanks2.png"));
        TexturesObjects.Set("FireShoot", Texture2D.FromFile(_graphicsDevice, "Content/fireball.png"));
        TexturesObjects.Set("SpriteBatch", _spriteBatch);
    }
}