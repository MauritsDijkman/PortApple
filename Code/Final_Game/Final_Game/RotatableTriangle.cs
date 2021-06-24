using System;
using GXPEngine;

public class RotatableTriangle : GameObject
{
    MyGame _myGame = null;

    Vec2 start = new Vec2(0, 0);
    Vec2 end = new Vec2(0, 0);

    Vec2 rotatePoint = new Vec2(0, 0);

    public LineSegment line = null;

    Ball lineEnd1 = null;
    Ball lineEnd2 = null;

    Vec2 memorizeStartPosition;
    Vec2 memorizeEndPosition;

    public RotatableTriangle(Vec2 startPosition, Vec2 endPosition, Vec2 rotateAroundThisPoint)
    {
        _myGame = (MyGame)game;

        start = startPosition;
        end = endPosition;

        memorizeStartPosition = startPosition;
        memorizeEndPosition = endPosition;

        rotatePoint = rotateAroundThisPoint;

        Spawn();
    }

    void Spawn()
    {
        //line = new LineSegment(start, end, 0xff00ff00, 4);  // Green line
        line = new LineSegment(start, end, 0x0000ff00, 4);  // Invisible line

        _myGame.AddChild(line);
        _myGame._lines.Add(line);

        lineEnd1 = new Ball(0, 0, 0, 0, start, moving: false);
        lineEnd2 = new Ball(0, 0, 0, 0, end, moving: false);

        _myGame._ball.Add(lineEnd1);
        _myGame._ball.Add(lineEnd2);
    }

    void MoveLine()
    {
        if (Input.GetKeyDown(Key.UP))
        {
            DestroyFlipper();

            start.RotateAroundDegrees(+45, rotatePoint);
            end.RotateAroundDegrees(+45, rotatePoint);

            Spawn();
        }

        if (Input.GetKeyDown(Key.DOWN))
        {
            DestroyFlipper();

            start.RotateAroundDegrees(-45, rotatePoint);
            end.RotateAroundDegrees(-45, rotatePoint);

            Spawn();
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      DestroyFlipper()                                                                     //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void DestroyFlipper()
    {
        _myGame._ball.Remove(lineEnd1);
        lineEnd1.Destroy();

        _myGame._ball.Remove(lineEnd2);
        lineEnd2.Destroy();

        _myGame._lines.Remove(line);
        line.Destroy();
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Update()                                                                             //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void Update()
    {
        MoveLine();

        if (Input.GetKeyDown(Key.R))
        {
            start = memorizeStartPosition;
            end = memorizeEndPosition;
        }
    }
}
