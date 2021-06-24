using System;
using GXPEngine;

public class LevelTransition : AnimationSprite
{
    MyGame _myGame = null;

    int animationDrawsBetweenFrames;
    int step;
    int count;

    bool playAnimation = false;
    bool sceneHasLoaded = false;

    public LevelTransition() : base("Transition_Animation_Spritesheet.png", 11, 4)
    {
        _myGame = (MyGame)game;

        SetXY(0, 0);

        animationDrawsBetweenFrames = 2;
        step = 0;
        count = 0;

        SetFrame(0);
    }

    void HandleAnimation()
    {
        if (playAnimation)
        {
            step = step + 1;

            if (step > animationDrawsBetweenFrames)
            {
                NextFrame();
                step = 0;
                count++;
            }

            if (count == 28)
            {
                if (_myGame.level1Active && !sceneHasLoaded)
                {
                    _myGame.LoadScene(4);
                    sceneHasLoaded = true;
                }

                if (_myGame.level2Active && !sceneHasLoaded)
                {
                    _myGame.LoadScene(5);
                    sceneHasLoaded = true;
                }

                if (_myGame.level3Active && !sceneHasLoaded)
                {
                    _myGame.LoadScene(6);
                    sceneHasLoaded = true;
                }

                if (_myGame.level4Active && !sceneHasLoaded)
                {
                    _myGame.LoadScene(11);
                    sceneHasLoaded = true;
                }
            }

            if (count == 44)
            {
                playAnimation = false;
                LateDestroy();
                LateRemove();
            }
        }
    }

    void Update()
    {
        HandleAnimation();

        if (_myGame.startTransition)
        {
            playAnimation = true;
            _myGame.startTransition = false;
        }
    }
}
