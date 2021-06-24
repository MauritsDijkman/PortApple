using System;
using GXPEngine;

public class Instruction4 : AnimationSprite
{
    MyGame _myGame = null;

    int animationDrawsBetweenFrames;
    int step;


    public Instruction4() : base("Instruction4_Tile.png", 10, 6)
    {
        _myGame = (MyGame)game;

        SetOrigin(width / 2, height / 2);
        SetXY(_myGame.width / 2, _myGame.height / 2);

        animationDrawsBetweenFrames = 2;
        step = 0;
    }

    void PlayAnimation()
    {
        step = step + 1;

        if (step > animationDrawsBetweenFrames)
        {
            NextFrame();
            step = 0;
        }
    }

    void Update()
    {
        PlayAnimation();

        if (Input.GetMouseButtonUp(0))
        {
            LateDestroy();
            _myGame.LoadScene(3);
        }
    }
}
