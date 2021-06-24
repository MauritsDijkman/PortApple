using System;
using GXPEngine;

public class RotatableTriangle_Image : Sprite
{
    MyGame _myGame = null;

    int rotationLines;

    RotatableTriangle _rotatableLine1;
    RotatableTriangle _rotatableLine2;
    RotatableTriangle _rotatableLine3;


    public RotatableTriangle_Image(Vec2 position, int rotationDegrees = 0) : base("Rotatable_Triangle.png")
    {
        _myGame = (MyGame)game;

        SetOrigin(width / 2, height / 2);
        SetXY(position.x, position.y);
        rotation = rotationDegrees;

        rotationLines = rotationDegrees;

        AddLines();
    }

    void HandleRotation()
    {
        if (Input.GetKeyDown(Key.UP))
        {
            rotation += 45;
        }

        if (Input.GetKeyDown(Key.DOWN))
        {
            rotation -= 45;
        }
    }

    void AddLines()
    {
        if (rotationLines == 0)
        {
            _rotatableLine1 = new RotatableTriangle(new Vec2(this.x - 50, this.y + 50), new Vec2(this.x + 50, this.y + 50), new Vec2(this.x, this.y));
            AddChild(_rotatableLine1);
            _rotatableLine2 = new RotatableTriangle(new Vec2(this.x + 50, this.y + 50), new Vec2(this.x + 50, this.y - 50), new Vec2(this.x, this.y));
            AddChild(_rotatableLine2);
            _rotatableLine3 = new RotatableTriangle(new Vec2(this.x + 50, this.y - 50), new Vec2(this.x - 50, this.y + 50), new Vec2(this.x, this.y));
            AddChild(_rotatableLine3);
        }

        if (rotationLines == 90)
        {
            _rotatableLine1 = new RotatableTriangle(new Vec2(this.x - 50, this.y + 50), new Vec2(this.x + 50, this.y + 50), new Vec2(this.x, this.y));
            AddChild(_rotatableLine1);
            _rotatableLine2 = new RotatableTriangle(new Vec2(this.x - 50, this.y - 50), new Vec2(this.x - 50, this.y + 50), new Vec2(this.x, this.y));
            AddChild(_rotatableLine2);
            _rotatableLine3 = new RotatableTriangle(new Vec2(this.x + 50, this.y + 50), new Vec2(this.x - 50, this.y - 50), new Vec2(this.x, this.y));
            AddChild(_rotatableLine3);
        }
    }

    void Update()
    {
        HandleRotation();
    }
}
