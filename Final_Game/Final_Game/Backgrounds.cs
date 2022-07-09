using System;
using GXPEngine;

public class Backgrounds : AnimationSprite
{
    public Backgrounds(int frameNumber) : base("Backgrounds_Tile.png", 3, 3, -1, false, false)
    {
        SetOrigin(0, 0);
        SetXY(0, 0);
        SetFrame(frameNumber);
    }
}
