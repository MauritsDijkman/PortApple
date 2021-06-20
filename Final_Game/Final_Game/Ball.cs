using System;
using GXPEngine;

public class Ball : EasyDraw
{
    // These four public static fields are changed from MyGame, based on key input (see Console):
    public static bool showGravity = false;
    public static float bounciness = 0.70f;

    // For ease of testing / changing, we assume every ball has the same acceleration (gravity):
    public static Vec2 acceleration = new Vec2(0, 0);

    public Vec2 velocity;
    public Vec2 position;

    Vec2 _oldPosition;

    public ScoreBall _scoreball = null;

    public readonly int radius;
    public readonly bool moving;

    bool ballHitsBluePortal1 = false;
    bool ballHitsBluePortal2 = false;
    bool ballHitsOrangePortal1 = false;
    bool ballHitsOrangePortal2 = false;

    bool ballHitsBlender1 = false;
    bool ballHitsBlender2 = false;
    bool ballHitsBlender3 = false;
    bool ballHitsBlender4 = false;

    public Sound _portalSound;

    float _density = 1;

    // Mass = density * volume
    // In 2D, we assume volume = area (=all objects are assumed to have the same "depth")
    public float Mass
    {
        get
        {
            return radius * radius * _density;
        }
    }


    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Ball()                                                                               //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public Ball(byte _red, byte _green, byte _blue, int pRadius, Vec2 pPosition, Vec2 pVelocity = new Vec2(), bool moving = true, bool visible = true) : base(pRadius * 2 + 1, pRadius * 2 + 1)
    {
        _portalSound = new Sound("Portal_Sound.wav", false, false);

        radius = pRadius;
        position = pPosition;
        velocity = pVelocity;
        this.moving = moving;

        position = pPosition;
        UpdateScreenPosition();
        SetOrigin(radius, radius);

        if (visible)
        {
            Draw(_red, _green, _blue);
        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Draw()                                                                               //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void Draw(byte red, byte green, byte blue)
    {
        Fill(red, green, blue);
        Stroke(red, green, blue);
        Ellipse(radius, radius, 2 * radius, 2 * radius);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      UpdateScreenPosition()                                                               //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void UpdateScreenPosition()
    {
        x = position.x;
        y = position.y;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      Step()                                                                               //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    public void Step()
    {
        bool firstTime = true;

        if (firstTime)
        {
            velocity += acceleration;
            _oldPosition = position;
            position += velocity;

            CollisionInfo firstCollision = FindEarliestCollision();
            if (firstCollision != null)
            {
                ResolveCollision(firstCollision);
                if (firstCollision.timeOfImpact < 0.001f && firstTime)
                {
                    firstTime = false;
                }
            }
        }

        if (!firstTime)
        {
            _oldPosition = position;
            position += velocity;

            CollisionInfo firstCollision = FindEarliestCollision();
            if (firstCollision != null)
            {
                ResolveCollision(firstCollision);
                if (firstCollision.timeOfImpact < 0.001f && firstTime)
                {
                    velocity = acceleration;
                }
            }
        }

        UpdateScreenPosition();
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      CollisionInfo FindEarliestCollision()                                                //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    CollisionInfo FindEarliestCollision()
    {
        MyGame myGame = (MyGame)game;
        CollisionInfo earliestCol = null;

        // Check other movers:
        for (int i = 0; i < myGame.GetNumberOfBalls(); i++)
        {
            Ball mover = myGame.GetBall(i);
            if (mover != this)
            {
                Vec2 u = _oldPosition - mover.position;

                float a = Mathf.Pow(velocity.Length(), 2);
                float b = 2 * (u.Dot(velocity));
                float c = Mathf.Pow(u.Length(), 2) - Mathf.Pow((radius + mover.radius), 2);

                float D = (b * b) - ((4 * a) * c);
                float t = (-b - Mathf.Sqrt(D)) / (2 * a);

                Vec2 PointOfImpact = _oldPosition + (velocity * t);
                Vec2 normal = (PointOfImpact - mover.position).Normalized();

                if (c < 0)
                {
                    if (b < 0)
                    {
                        t = 0;
                    }
                    else
                    {
                        continue;
                    }
                }

                if (0 <= t && t < 1)
                {
                    if (earliestCol == null || t < earliestCol.timeOfImpact)
                    {
                        earliestCol = new CollisionInfo(normal, mover, t);
                    }
                }
            }
        }

        for (int i = 0; i < myGame.GetNumberOfLines(); i++)
        {
            LineSegment line = myGame.GetLine(i);

            Vec2 oldDifferenceVec = (_oldPosition - line.start);
            Vec2 lineNormal = (line.end - line.start).Normal();

            float a = oldDifferenceVec.Dot(lineNormal) - radius;
            float b = -velocity.Dot(lineNormal);

            if (b < 0)
            {
                continue;
            }

            float t = 0;

            if (a >= 0)
            {
                t = a / b;
            }
            else if (a >= -radius)
            {
                t = 0;
            }
            else
            {
                continue;
            }

            if (t <= 1)
            {
                Vec2 POI = _oldPosition + velocity * t;

                float LineLength = (line.start - line.end).Length();

                //lineVec = distance along the line
                Vec2 lineVec = (line.end - line.start);

                Vec2 impactVec = (POI - line.start);
                float distance = impactVec.Dot(lineVec.Normalized());

                if (distance >= 0 && distance <= LineLength)
                {
                    if (earliestCol == null || t < earliestCol.timeOfImpact)
                    {
                        earliestCol = new CollisionInfo(lineNormal, line, t);

                        if (myGame._portalBlueLineInLevel1 != null)
                        {
                            if (line == myGame._portalBlueLineInLevel1.line)
                            {
                                ballHitsBluePortal1 = true;
                            }
                        }
                        if (myGame._portalBlueLineInLevel2 != null)
                        {
                            if (line == myGame._portalBlueLineInLevel2.line)
                            {
                                ballHitsBluePortal1 = true;
                            }
                        }
                        if (myGame._portalBlueLineInLevel3 != null)
                        {
                            if (line == myGame._portalBlueLineInLevel3.line)
                            {
                                ballHitsBluePortal1 = true;
                            }
                        }
                        if (myGame._portalBlueLineInLevel4 != null)
                        {
                            if (line == myGame._portalBlueLineInLevel4.line)
                            {
                                ballHitsBluePortal1 = true;
                            }
                        }

                        if (myGame._portalBlueLineOutLevel1 != null)
                        {
                            if (line == myGame._portalBlueLineOutLevel1.line)
                            {
                                ballHitsBluePortal2 = true;
                            }
                        }
                        if (myGame._portalBlueLineOutLevel2 != null)
                        {
                            if (line == myGame._portalBlueLineOutLevel2.line)
                            {
                                ballHitsBluePortal2 = true;
                            }
                        }
                        if (myGame._portalBlueLineOutLevel3 != null)
                        {
                            if (line == myGame._portalBlueLineOutLevel3.line)
                            {
                                ballHitsBluePortal2 = true;
                            }
                        }
                        if (myGame._portalBlueLineOutLevel4 != null)
                        {
                            if (line == myGame._portalBlueLineOutLevel4.line)
                            {
                                ballHitsBluePortal2 = true;
                            }
                        }

                        if (myGame._portalOrangeLine1Level1 != null)
                        {
                            if (line == myGame._portalOrangeLine1Level1.line)
                            {
                                ballHitsOrangePortal1 = true;
                            }
                        }
                        if (myGame._portalOrangeLine1Level2 != null)
                        {
                            if (line == myGame._portalOrangeLine1Level2.line)
                            {
                                ballHitsOrangePortal1 = true;
                            }
                        }
                        if (myGame._portalOrangeLine1Level3 != null)
                        {
                            if (line == myGame._portalOrangeLine1Level3.line)
                            {
                                ballHitsOrangePortal1 = true;
                            }
                        }
                        if (myGame._portalOrangeLine1Level4 != null)
                        {
                            if (line == myGame._portalOrangeLine1Level4.line)
                            {
                                ballHitsOrangePortal1 = true;
                            }
                        }

                        if (myGame._portalOrangeLine2Level1 != null)
                        {
                            if (line == myGame._portalOrangeLine2Level1.line)
                            {
                                ballHitsOrangePortal2 = true;
                            }
                        }
                        if (myGame._portalOrangeLine2Level2 != null)
                        {
                            if (line == myGame._portalOrangeLine2Level2.line)
                            {
                                ballHitsOrangePortal2 = true;
                            }
                        }
                        if (myGame._portalOrangeLine2Level3 != null)
                        {
                            if (line == myGame._portalOrangeLine2Level3.line)
                            {
                                ballHitsOrangePortal2 = true;
                            }
                        }
                        if (myGame._portalOrangeLine2Level4 != null)
                        {
                            if (line == myGame._portalOrangeLine2Level4.line)
                            {
                                ballHitsOrangePortal2 = true;
                            }
                        }

                        if (myGame._blenderLine1 != null)
                        {
                            if (line == myGame._blenderLine1.line && !myGame.nextLevel1)
                            {
                                myGame._appleimage.LateDestroy();

                                ballHitsBlender1 = true;
                            }
                        }
                        if (myGame._blenderLine2 != null)
                        {
                            if (line == myGame._blenderLine2.line && !myGame.nextLevel2)
                            {
                                myGame._appleimage.LateDestroy();

                                ballHitsBlender2 = true;
                            }
                        }
                        if (myGame._blenderLine3 != null)
                        {
                            if (line == myGame._blenderLine3.line && !myGame.nextLevel3)
                            {
                                myGame._appleimage.LateDestroy();

                                ballHitsBlender3 = true;
                            }
                        }
                        if (myGame._blenderLine4 != null)
                        {
                            if (line == myGame._blenderLine4.line && !myGame.nextLevel4)
                            {
                                myGame._appleimage.LateDestroy();

                                ballHitsBlender4 = true;
                            }
                        }
                    }
                }
            }
        }
        return earliestCol;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    //                                                                      ResolveCollision(CollisionInfo col)                                                  //
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------//
    void ResolveCollision(CollisionInfo col)
    {
        MyGame myGame = (MyGame)game;
        CollisionInfo earliestCol = null;

        position = _oldPosition + velocity * col.timeOfImpact;
        if (col.other is Ball)
        {
            Ball otherBall = (Ball)col.other;

            // Checks if the other object the ball collides with is a score ball
            if (otherBall._scoreball != null)
            {
                otherBall._scoreball.SoundEffect();
                otherBall._scoreball.DeleteScoreBall();
                ((MyGame)game).score++;
                return;
            }

            // Other ball is allowed to move
            if (otherBall.moving)
            {
                // Calculates the center of mass
                // u = (m1 * v1 + m2 * v2) / (m1 + m2)
                Vec2 CenterOfMass = (Mass * velocity + otherBall.Mass * otherBall.velocity) / (Mass + otherBall.Mass);

                // Controles the velocity of the main ball and the other ball it collided with
                // v = v - (1 + C) * ((v - u) Dot n) * n
                velocity -= (1 + bounciness) * ((velocity - CenterOfMass).Dot(col.normal)) * col.normal;
                otherBall.velocity -= (1 + bounciness) * ((otherBall.velocity - CenterOfMass).Dot(col.normal)) * col.normal;
            }

            // Other ball is not allowed to move
            else
            {
                velocity.Reflect(col.normal, bounciness);
            }
        }

        // Collides with something different than other.ball
        else
        {
            if (ballHitsBluePortal1 || ballHitsBluePortal2 || ballHitsOrangePortal1 || ballHitsOrangePortal2 || ballHitsBlender1 || ballHitsBlender2 || ballHitsBlender3 || ballHitsBlender4)
            {
                _portalSound.Play();

                if (ballHitsBluePortal1)
                {
                    if (myGame.level1Active)
                        position = new Vec2(myGame._portalBlueImageOutLevel1.x, myGame._portalBlueImageOutLevel1.y);
                    if (myGame.level2Active)
                        position = new Vec2(myGame._portalBlueImageOutLevel2.x, myGame._portalBlueImageOutLevel2.y);
                    if (myGame.level3Active)
                        position = new Vec2(myGame._portalBlueImageOutLevel3.x, myGame._portalBlueImageOutLevel3.y);
                    if (myGame.level4Active)
                        position = new Vec2(myGame._portalBlueImageOutLevel4.x, myGame._portalBlueImageOutLevel4.y);

                    velocity.y *= 1.35f;
                    velocity.x = 0f;
                    ballHitsBluePortal1 = false;
                }

                if (ballHitsBluePortal2)
                {
                    if (myGame.level1Active)
                        position = new Vec2(myGame._portalBlueImageInLevel1.x, myGame._portalBlueImageInLevel1.y);
                    if (myGame.level2Active)
                        position = new Vec2(myGame._portalBlueImageInLevel2.x, myGame._portalBlueImageInLevel2.y);
                    if (myGame.level3Active)
                        position = new Vec2(myGame._portalBlueImageInLevel3.x, myGame._portalBlueImageInLevel3.y);
                    if (myGame.level4Active)
                        position = new Vec2(myGame._portalBlueImageInLevel4.x, myGame._portalBlueImageInLevel4.y);

                    velocity.y *= 1.35f;
                    velocity.x = 0f;
                    ballHitsBluePortal2 = false;
                }

                if (ballHitsOrangePortal1)
                {
                    if (myGame.level1Active)
                        position = new Vec2(myGame._portalOrangeImageOutLevel1.x, myGame._portalOrangeImageOutLevel1.y);
                    if (myGame.level2Active)
                        position = new Vec2(myGame._portalOrangeImageOutLevel2.x, myGame._portalOrangeImageOutLevel2.y);
                    if (myGame.level3Active)
                        position = new Vec2(myGame._portalOrangeImageOutLevel3.x, myGame._portalOrangeImageOutLevel3.y);
                    if (myGame.level4Active)
                        position = new Vec2(myGame._portalOrangeImageOutLevel4.x, myGame._portalOrangeImageOutLevel4.y);

                    velocity.y *= 1.35f;
                    velocity.x = 0f;
                    ballHitsOrangePortal1 = false;
                }

                if (ballHitsOrangePortal2)
                {
                    if (myGame.level1Active)
                        position = new Vec2(myGame._portalOrangeImageInLevel1.x, myGame._portalOrangeImageInLevel1.y);
                    if (myGame.level2Active)
                        position = new Vec2(myGame._portalOrangeImageInLevel2.x, myGame._portalOrangeImageInLevel2.y);
                    if (myGame.level3Active)
                        position = new Vec2(myGame._portalOrangeImageInLevel3.x, myGame._portalOrangeImageInLevel3.y);
                    if (myGame.level4Active)
                        position = new Vec2(myGame._portalOrangeImageInLevel4.x, myGame._portalOrangeImageInLevel4.y);

                    velocity.y *= 1.35f;
                    velocity.x = 0f;
                    ballHitsOrangePortal2 = false;
                }

                if (ballHitsBlender1)
                {
                    myGame.nextLevel1 = true;
                    ballHitsBlender1 = false;
                }
                if (ballHitsBlender2)
                {
                    myGame.nextLevel2 = true;
                    ballHitsBlender2 = false;
                }
                if (ballHitsBlender3)
                {
                    myGame.nextLevel3 = true;
                    ballHitsBlender3 = false;
                }
                if (ballHitsBlender4)
                {
                    myGame.nextLevel4 = true;
                    ballHitsBlender4 = false;
                }
            }

            //The moving ball collides with something different than other.ball
            else
            {
                velocity.Reflect(col.normal, bounciness);
            }
        }
    }
}
