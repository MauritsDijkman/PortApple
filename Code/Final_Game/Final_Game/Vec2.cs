using System;
using GXPEngine;	// For Mathf

public struct Vec2
{
    public float x;
    public float y;

    public Vec2(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }

    public override string ToString()
    {
        return String.Format("({0},{1})", x, y);
    }

    public void SetXY(float pX, float pY)
    {
        x = pX;
        y = pY;
    }

    public float Length()
    {
        return Mathf.Sqrt(x * x + y * y);
    }

    public void Normalize()
    {
        float len = Length();
        if (len > 0)
        {
            x /= len;
            y /= len;
        }
    }

    public Vec2 Normalized()
    {
        Vec2 result = new Vec2(x, y);
        result.Normalize();
        return result;
    }

    public float Distance(Vec2 value)
    {
        float disX = x - value.x;
        float disY = y - value.y;

        return Mathf.Sqrt(disX * disX + disY * disY);
    }

    public float Dot(Vec2 other)
    {
        float result = (x * other.x) + (y * other.y);
        return result;
    }

    public Vec2 Normal()
    {
        return new Vec2(-y, x).Normalized();
    }

    public void Reflect(Vec2 pNormal, float pBounciness = 1)
    {
        this -= (1 + pBounciness) * (Dot(pNormal)) * pNormal;
    }

    public static float Deg2Rad(float value)
    {
        return (value * (Mathf.PI / 180));
    }

    public static float Rad2Deg(float value)
    {
        return (value * (180 / Mathf.PI));
    }

    public static Vec2 GetUnitVectorDeg(float degrees)
    {
        //Uses cosinus to calculate x and sinus to calculate y.
        Vec2 vectorDegrees = new Vec2();

        vectorDegrees.x = Mathf.Cos(Deg2Rad(degrees));
        vectorDegrees.y = Mathf.Sin(Deg2Rad(degrees));

        return vectorDegrees;
    }

    public static Vec2 GetUnitVectorRad(float radians)
    {
        //Uses cosinus to calculate x and sinus to calculate y.
        Vec2 vectorRadians = new Vec2();

        vectorRadians.x = Mathf.Cos(radians);
        vectorRadians.y = Mathf.Sin(radians);

        return vectorRadians;
    }

    public static Vec2 RandomUnitVector()
    {
        float randomRadian = new Random().Next(-180, 180);
        return GetUnitVectorDeg(randomRadian);
    }

    public void SetAngleDegrees(float value)
    {
        float length = Length();
        y = length * Mathf.Sin(Deg2Rad(value));
        x = length * Mathf.Cos(Deg2Rad(value));
    }

    public void SetAngleRadians(float value)
    {
        float length = Length();
        y = length * Mathf.Sin(value);
        x = length * Mathf.Cos(value);
    }

    public float GetAngleDegrees()
    {
        return Rad2Deg(GetAngleRadians());
    }

    public float GetAngleRadians()
    {
        return Mathf.Atan2(y, x);
    }

    public void RotateDegrees(float value)
    {
        SetAngleDegrees(GetAngleDegrees() + value);
    }

    public void RotateRadians(float value)
    {
        SetAngleRadians(GetAngleRadians() + value);
    }

    public void RotateAroundDegrees(float value, Vec2 point)
    {
        this -= point;
        RotateDegrees(value);
        this += point;
    }

    public void RotateAroundRadians(float value, Vec2 point)
    {
        RotateAroundDegrees(Rad2Deg(value), point);
    }

    public static Vec2 operator +(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x + right.x, left.y + right.y);
    }

    public static Vec2 operator -(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x - right.x, left.y - right.y);
    }

    public static Vec2 operator *(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x * right.x, left.y * right.y);
    }

    public static Vec2 operator *(Vec2 v, float scalar)
    {
        return new Vec2(v.x * scalar, v.y * scalar);
    }

    public static Vec2 operator *(float scalar, Vec2 v)
    {
        return new Vec2(v.x * scalar, v.y * scalar);
    }

    public static Vec2 operator /(Vec2 v, float scalar)
    {
        return new Vec2(v.x / scalar, v.y / scalar);
    }

    public static Vec2 operator /(float scalar, Vec2 v)
    {
        return new Vec2(scalar / v.x, scalar / v.y);
    }
}
