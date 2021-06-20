using System;
using GXPEngine;

public class Platform : GameObject
{
    MyGame _myGame = null;

    Vec2 start = new Vec2(0, 0);
    Vec2 end = new Vec2(0, 0);

    public LineSegment line = null;

    Ball lineEnd1 = null;
    Ball lineEnd2 = null;

    public bool destroyLine;
    bool lineHasSpawned;
    bool imageVisible;

    bool spawnStart;

    Platform_Image _platformImage;

    public Platform(Vec2 startPosition, Vec2 endPosition, bool spawnAtStart, Platform_Image platformImage)
    {
        _myGame = (MyGame)game;

        _platformImage = platformImage;

        start = startPosition;
        end = endPosition;

        destroyLine = spawnAtStart;
        spawnStart = spawnAtStart;

        lineHasSpawned = true;

        imageVisible = _platformImage.visibe_YN;

        ResetLine();
        Spawn();
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Spawn()                                                                              //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void Spawn()
    {
        line = new LineSegment(start, end, 0xff00ff00, 4);  // Green line
        //line = new LineSegment(start, end, 0x0000ff00, 4);  // Invisible line

        //_myGame.AddChild(line);
        _myGame._lines.Add(line);

        lineEnd1 = new Ball(0, 0, 0, 0, start, moving: false);
        lineEnd2 = new Ball(0, 0, 0, 0, end, moving: false);

        _myGame._ball.Add(lineEnd1);
        _myGame._ball.Add(lineEnd2);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      MoveLine()                                                                           //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void ToggleOnOff()
    {
        if (Input.GetKeyUp(Key.D))
        {
            destroyLine ^= true;
            imageVisible ^= true;

            if (_platformImage.visible)
            {
                if (imageVisible && _platformImage != null)
                {
                    _platformImage.visible = true;
                }

                if (!imageVisible && _platformImage != null)
                {
                    _platformImage.visible = false;
                }
            }

            if (!_platformImage.visible)
            {
                if (imageVisible && _platformImage != null)
                {
                    _platformImage.visible = true;
                }

                if (!imageVisible && _platformImage != null)
                {
                    _platformImage.visible = false;
                }
            }
        }

        if (!destroyLine && lineHasSpawned)
        {
            DestroyLine();
            lineHasSpawned = false;
        }

        if (destroyLine && !lineHasSpawned)
        {
            Spawn();
            lineHasSpawned = true;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      DestroyFlipper()                                                                     //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public void DestroyLine()
    {
        _myGame._ball.Remove(lineEnd1);
        lineEnd1.Destroy();

        _myGame._ball.Remove(lineEnd2);
        lineEnd2.Destroy();

        _myGame._lines.Remove(line);
        line.Destroy();
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      ResetLine()                                                                          //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void ResetLine()
    {
        start = new Vec2(start.x, start.y);
        end = new Vec2(end.x, end.y);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Update()                                                                             //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void Update()
    {
        ToggleOnOff();

        if (Input.GetKeyDown(Key.R))
        {
            destroyLine = spawnStart;
            lineHasSpawned = true;
            imageVisible = _platformImage.visibe_YN;
        }
    }
}
