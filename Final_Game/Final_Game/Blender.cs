using System;
using GXPEngine;

public class Blender : AnimationSprite
{
    MyGame _myGame = null;

    int animationDrawsBetweenFrames;
    int step;

    public int count;

    bool playBlenderAnimation;
    bool blenderSoundHasPlayed;

    Sound _blenderSound;

    public Blender(Vec2 position, int frameNumber, int rotationDegrees = 0) : base("Blender_Tile.png", 10, 3)
    {
        _myGame = (MyGame)game;

        animationDrawsBetweenFrames = 3;
        step = 0;

        playBlenderAnimation = false;
        blenderSoundHasPlayed = false;

        _blenderSound = new Sound("Blender_Sound.wav", false, false);

        SetOrigin(width / 2, height / 2);
        SetXY(position.x, position.y);
        SetFrame(frameNumber);
        rotation = rotationDegrees;
    }

    void HandleSound()
    {
        if (_myGame.nextLevel1 && !blenderSoundHasPlayed && _myGame.level1Active)
        {
            playBlenderAnimation = true;
            SetFrame(0);

            _blenderSound.Play();
            _myGame.nextLevel1 = false;
            blenderSoundHasPlayed = true;
        }

        if (_myGame.nextLevel2 && !blenderSoundHasPlayed && _myGame.level2Active)
        {
            playBlenderAnimation = true;
            SetFrame(0);

            _blenderSound.Play();
            _myGame.nextLevel2 = false;
            blenderSoundHasPlayed = true;
        }

        if (_myGame.nextLevel3 && !blenderSoundHasPlayed && _myGame.level3Active)
        {
            playBlenderAnimation = true;
            SetFrame(10);

            _blenderSound.Play();
            _myGame.nextLevel3 = false;
            blenderSoundHasPlayed = true;
        }

        if (_myGame.nextLevel4 && !blenderSoundHasPlayed && _myGame.level4Active)
        {
            playBlenderAnimation = true;
            SetFrame(20);

            _blenderSound.Play();
            _myGame.nextLevel4 = false;
            blenderSoundHasPlayed = true;
        }
    }

    void HandleAnimation()
    {
        if (playBlenderAnimation)
        {
            step = step + 1;

            if (step > animationDrawsBetweenFrames)
            {
                NextFrame();
                step = 0;
                count++;
            }

            if (count == 9)
            {
                count = 0;
                _myGame.startTransition = true;
                playBlenderAnimation = false;
            }
        }
    }

    void Update()
    {
        HandleSound();
        HandleAnimation();
    }
}
