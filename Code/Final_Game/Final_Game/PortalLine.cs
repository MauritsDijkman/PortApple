using System;
using GXPEngine;

public class PortalLine : GameObject
{
    MyGame _myGame = null;

    public LineSegment line = null;

    Ball lineEnd1 = null;
    Ball lineEnd2 = null;

    Vec2 start = new Vec2(0, 0);
    Vec2 end = new Vec2(0, 0);


    public PortalLine(Vec2 startPos, Vec2 endPos)
    {
        _myGame = (MyGame)game;

        start = startPos;
        end = endPos;

        Spawn();
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Spawn()                                                                              //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void Spawn()
    {
        line = new LineSegment(start, end, 0x0000ff00, 4);
        //line = new LineSegment(start, end, 0xff00ff00, 4);

        _myGame.AddChild(line);
        _myGame._lines.Add(line);

        lineEnd1 = new Ball(0, 0, 0, 0, start, moving: false);
        lineEnd2 = new Ball(0, 0, 0, 0, end, moving: false);

        _myGame._ball.Add(lineEnd1);
        _myGame._ball.Add(lineEnd2);
    }
}
