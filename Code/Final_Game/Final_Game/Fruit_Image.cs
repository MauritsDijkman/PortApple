using System;
using GXPEngine;

public class Fruit_Image : AnimationSprite
{
    public Ball _ball;

    float _boost = 20;

    bool ballIsSpawned;

    MyGame _myGame = null;

    public Fruit_Image(Vec2 position, int frameNumber) : base("Fruit_Tilesheet.png", 3, 1)
    {
        _myGame = (MyGame)game;

        SetOrigin(width / 2, height / 2);
        SetXY(position.x, position.y);
        SetFrame(frameNumber);

        ballIsSpawned = false;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      ShootBallInCurrentDirection()                                                        //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void ShootBallInCurrentDirection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ball.acceleration = new Vec2(0, 1);
            _ball.velocity = Vec2.GetUnitVectorDeg(rotation) * _boost;
            ballIsSpawned = true;
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Update()                                                                             //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void Update()
    {
        if (ballIsSpawned)
        {
            if (!_myGame._paused)
            {
                rotation += _ball.velocity.x;
                x = _ball.x;
                y = _ball.y;
            }
        }

        else
        {
            if (!_myGame._paused)
            {
                ShootBallInCurrentDirection();

                Vec2 mousePos = new Vec2(Input.mouseX, Input.mouseY);
                Vec2 deltaVector = mousePos - new Vec2(x, y);

                // Gets angle in degrees (ball-mouse)
                rotation = deltaVector.GetAngleDegrees();

                // Sets the rotation of the arrow to the rotation of the ball
                _ball.rotation = rotation;
            }
        }
    }
}
