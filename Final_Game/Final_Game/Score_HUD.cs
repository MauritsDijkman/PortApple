using System;
using GXPEngine;

public class Score_HUD : AnimationSprite
{
    MyGame _myGame = null;

    public Score_HUD() : base("Stars_Tile.png", 4, 1)
    {
        _myGame = (MyGame)game;

        SetXY(0, 0);
    }

    void Update()
    {
        SetFrame(_myGame.score);
    }
}
