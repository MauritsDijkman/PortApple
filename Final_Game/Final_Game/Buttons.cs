using System;
using GXPEngine;

public class Buttons : AnimationSprite
{
    public Buttons(Vec2 position, int frameNumber) : base("Buttons_Tile.png", 4, 2)
    {
        SetXY(position.x, position.y);
        SetOrigin(width /2, height / 2);
        SetFrame(frameNumber);
    }
}
