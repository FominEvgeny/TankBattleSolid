#nullable enable
using System;
using System.Collections.Concurrent;
using LibraryClasses.Commands;
using LibraryClasses.Entity;
using LibraryClasses.GameObjects;
using Microsoft.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static System.Collections.Specialized.BitVector32;

namespace GamePlayerMonogame.GamableMg;

public class GamableAdapter : IGamableMg
{
    private readonly UObject _uObject;

    public GamableAdapter(UObject uObject)
    {
        _uObject = uObject;
    }


    public void SetSpriteBatch(SpriteBatch spriteBatch)
    {
        _uObject.SetProperty("SpriteBatch", spriteBatch);
    }

    public SpriteBatch? GetSpriteBatch()
    {
        return (SpriteBatch?)_uObject.GetProperty("SpriteBatch");
    }

    public void SetTexture(Texture2D texture2D)
    {
        _uObject.SetProperty("Texture2D", texture2D);
    }

    public Texture2D GetTexture()
    {
        return (Texture2D)_uObject.GetProperty("Texture2D");
    }

    public void SetColor(Color color)
    {
        _uObject.SetProperty("Color", color);
    }

    public Color? GetColor()
    {
        return (Color?)_uObject.GetProperty("Color");
    }

    public void SetLocation(Vector2 location)
    {
        _uObject.SetProperty("Position", location);
    }

    public Vector2? GetLocation()
    {
        var location = (Coord2d?)_uObject.GetProperty("Position");

        if (location != null) return new Vector2(location.X, location.Y);

        return null;
    }

    public void SetAngular(int angular)
    {
        _uObject.SetProperty("Direction", angular);
    }

    public int? GetAngular()
    {
        return (int?)_uObject.GetProperty("Direction");
    }

    public void SetDelegateDraw(Action<object[]> action)
    {
        _uObject.SetProperty("DelegateDraw", action);
    }

    public Action<object[]>? GetDelegateDraw()
    {
        return (Action<object[]>?)_uObject.GetProperty("DelegateDraw");
    }

    public void Update()
    {
        var queueCommands = (BlockingCollection<ICommand>?)_uObject.GetProperty("QueueCommandsObject");
        if (queueCommands != null)
            foreach (var item in queueCommands)
            {
                item.Execute();
                var a = _uObject.Properties;
            }
    }

    public void Draw()
    {
        SpriteBatch? spriteBatch = GetSpriteBatch();
        var location = GetLocation();
        var angular = GetAngular();
        var texture = GetTexture();

        var positionRotate = new Vector2(texture.Width / 2, texture.Height / 2);

        var props = _uObject.Properties;

        if (spriteBatch != null 
            && location != null
            && angular != null)
        {
            spriteBatch.Draw(
                GetTexture(),
                (Vector2)location,
                null,
                Color.White,
                MathHelper.ToRadians((int)angular),
                positionRotate,
                1, SpriteEffects.None, 0);
        }
    }
}