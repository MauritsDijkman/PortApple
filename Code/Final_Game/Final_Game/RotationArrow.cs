using System;
using GXPEngine;

public class RotationArrow : Sprite
{
    public Ball _ball;

    float _boost = 50;

    public Sound _shootingSound;

    MyGame _myGame = null;


    public RotationArrow(Vec2 position) : base("Arrow.png")
    {
        _myGame = (MyGame)game;

        _shootingSound = new Sound("Shooting_Sound.wav", false, false);

        SetOrigin(width - width, height / 2);
        SetXY(position.x, position.y);
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
            _shootingSound.Play();
            LateDestroy();
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Update()                                                                             //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void Update()
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
