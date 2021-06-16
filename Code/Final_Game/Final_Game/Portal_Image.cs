using System;
using GXPEngine;

public class Portal_Image : AnimationSprite
{
    public Portal_Image(Vec2 position, int frameNumber, int rotationDegrees = 0) : base("Portals_Tile.png", 2, 1)
    {
        SetOrigin(width / 2, height / 2);
        SetXY(position.x, position.y);
        SetFrame(frameNumber);
        rotation = rotationDegrees;
    }
}
