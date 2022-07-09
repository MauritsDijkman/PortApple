using System;
using GXPEngine;

public class Platform_Image : AnimationSprite
{
    MyGame _myGame = null;

    Platform _platformUp;
    Platform _platformDown;
    Platform _platformRight;
    Platform _platformLeft;

    public bool visibe_YN;


    public Platform_Image(Vec2 position, int frameNumber, float rotationDegrees = 0) : base("lines_Tile.png", 2, 1)
    {
        _myGame = (MyGame)game;

        SetOrigin(width / 2, height / 2);
        SetXY(position.x, position.y);
        SetFrame(frameNumber);
        rotation = rotationDegrees;

        if (frameNumber == 0)
        {
            visibe_YN = true;
        }
        if (frameNumber == 1)
        {
            visibe_YN = false;
        }

        visible = visibe_YN;

        if (rotationDegrees == 0)
        {
            _platformUp = new Platform(new Vec2(this.x + 115, this.y - 17), new Vec2(this.x - 115, this.y - 17), visibe_YN, this);
            AddChild(_platformUp);
            _platformDown = new Platform(new Vec2(this.x - 115, this.y + 17), new Vec2(this.x + 115, this.y + 17), visibe_YN, this);
            AddChild(_platformDown);
            _platformRight = new Platform(new Vec2(this.x + 115, this.y + 17), new Vec2(this.x + 115, this.y - 17), visibe_YN, this);
            AddChild(_platformRight);
            _platformLeft = new Platform(new Vec2(this.x - 115, this.y - 17), new Vec2(this.x - 115, this.y + 17), visibe_YN, this);
            AddChild(_platformLeft);
        }

        if (rotationDegrees == 60)
        {
            _platformUp = new Platform(new Vec2(this.x - 43, this.y - 108), new Vec2(this.x - 75, this.y - 88), visibe_YN, this);
            AddChild(_platformUp);
            _platformDown = new Platform(new Vec2(this.x + 38, this.y + 108), new Vec2(this.x + 70, this.y + 90), visibe_YN, this);
            AddChild(_platformDown);
            _platformRight = new Platform(new Vec2(this.x + 70, this.y + 90), new Vec2(this.x - 43, this.y - 108), visibe_YN, this);
            AddChild(_platformRight);
            _platformLeft = new Platform(new Vec2(this.x - 75, this.y - 88), new Vec2(this.x + 38, this.y + 108), visibe_YN, this);
            AddChild(_platformLeft);
        }

        if (rotationDegrees == 90)
        {
            _platformUp = new Platform(new Vec2(this.x + 20, this.y - 115), new Vec2(this.x - 20, this.y - 115), visibe_YN, this);
            AddChild(_platformUp);
            _platformDown = new Platform(new Vec2(this.x - 20, this.y + 115), new Vec2(this.x + 20, this.y + 115), visibe_YN, this);
            AddChild(_platformDown);
            _platformRight = new Platform(new Vec2(this.x + 20, this.y + 115), new Vec2(this.x + 20, this.y - 115), visibe_YN, this);
            AddChild(_platformRight);
            _platformLeft = new Platform(new Vec2(this.x - 20, this.y - 115), new Vec2(this.x - 20, this.y + 115), visibe_YN, this);
            AddChild(_platformLeft);
        }

        void Update()
        {
            if (Input.GetKeyDown(Key.R))
            {
                if (_platformUp != null)
                    RemoveChild(_platformUp);
                if (_platformDown != null)
                    RemoveChild(_platformDown);
                if (_platformLeft != null)
                    RemoveChild(_platformLeft);
                if (_platformRight != null)
                    RemoveChild(_platformRight);
            }
        }
    }
}
