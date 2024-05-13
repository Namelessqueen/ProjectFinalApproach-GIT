using System;
using System.Runtime.InteropServices;
using GXPEngine; // Allows using Mathf functions

public struct Vec2
{
    public float x;
    public float y;



    public Vec2(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }

    public static float Deg2Rad(float degrees)
    {
        return (degrees * Mathf.PI) / 180;
    }

    public static float Rad2Deg(float radians)
    {
        return (radians * 180) / Mathf.PI;
    }

    public static Vec2 GetUnitVectorDeg(float degrees)
    {
        float newX = Mathf.Cos(Deg2Rad(degrees));
        float newY = Mathf.Sin(Deg2Rad(degrees));
        return new Vec2(newX, newY).Normalized();
    }

    public static Vec2 GetUnitVectorRad(float radians)
    {
        float newX = Mathf.Cos(radians);
        float newY = Mathf.Sin(radians);
        return new Vec2(newX, newY).Normalized();
    }

    public static Vec2 RandomUnitVector()
    {
        float randomAngle = Utils.Random(-Mathf.PI, Mathf.PI);
        return GetUnitVectorRad(randomAngle);
    }

    public void SetAngleDegrees(float degrees)
    {
        this = GetUnitVectorDeg(degrees) * Length();
    }

    public void SetAngleRadians(float radians)
    {
        this = GetUnitVectorRad(radians) * Length();
    }

    public float GetAngleRadians()
    {
        return Mathf.Atan2(y, x);
    }

    public float GetAngleDegrees()
    {
        return (Mathf.Atan2(y, x) * 180) / Mathf.PI;
    }

    public void RotateDegrees(float degrees)
    {
        float newAngle = GetAngleDegrees() + degrees;
        SetAngleDegrees(newAngle);
    }

    public void RotateRadians(float radians)
    {
        float newAngle = GetAngleRadians() + radians;
        SetAngleRadians(newAngle);
    }

    public void RotateAroundDegrees(Vec2 p, float degrees)
    {
        this -= p;
        RotateDegrees(degrees);
        this += p;
    }

    public void RotateAroundRadians(Vec2 p, float radians)
    {
        this -= p;
        RotateRadians(radians);
        this += p;
    }

    //RotateAroundSetDegrees is a new Vec2 void that rotates around a point to a set angle.
    public void RotateAroundSetDegrees(Vec2 p, float degrees)
    {
        this -= p;
        SetAngleDegrees(degrees);
        this += p;
    }
    public float Length()
    {
        return Mathf.Sqrt(x * x + y * y);
    }

    public void Normalize()
    {
        float pLength = Length();

        if (Length() != 0)
        {
            y = y / pLength;
            x = x / pLength;
        }
    }

    public Vec2 Normalized()
    {
        float pLength = Length();
        if (Length() != 0)
        {
            return new Vec2(x / pLength, y / pLength);
        }
        else return new Vec2(0, 0);
    }

    public void SetXY(float pX, float pY)
    {
        x = pX;
        y = pY;
    }

    public float Dot(Vec2 other)
    {
        return (x * other.x + y * other.y);
    }

    public Vec2 Normal()
    {
        return new Vec2(-y, x).Normalized();
    }

    public void Reflect(Vec2 normal, float bounciness = 1)
    {
        float oldX = x;
        float oldY = y;
        float oldDot = Dot(normal);

        x = oldX - (1 + bounciness) * oldDot * normal.x;
        y = oldY - (1 + bounciness) * oldDot * normal.y;


    }

    public static Vec2 operator -(Vec2 left, Vec2 right)
    {
        return new Vec2(right.x - left.x, right.y - left.y);
    }

    public static Vec2 operator *(float scale, Vec2 vector)
    {
        return new Vec2(scale * vector.x, scale * vector.y);
    }

    public static Vec2 operator *(Vec2 vector, float scale)
    {
        return new Vec2(scale * vector.x, scale * vector.y);
    }

    public static Vec2 operator +(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x + right.x, left.y + right.y);
    }

    public static Vec2 operator /(Vec2 vector, float scale)
    {
        return new Vec2(vector.x / scale, vector.y / scale);
    }

    public override string ToString()
    {
        return String.Format("({0},{1})", x, y);
    }
}

