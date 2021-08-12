using UnityEngine;

public static class Henrix
{
    public static Vector2 Lerp(Vector2 from, Vector2 to, float t)
    {
        return from + (to - from) * t;
    }

    public static Vector3 Lerp(Vector3 from, Vector3 to, float t)
    {
        return from + (to - from) * t;
    }

    public static Vector2 QuadraticCurve(Vector2 from, Vector2 to, float t)
    {
        Vector2 point0 = Lerp(from, to, t);
        Vector2 point1 = Lerp(from, to, t);
        return Lerp(point0, point1, t);
    }

    public static Vector3 QuadraticCurve(Vector3 from, Vector3 to, float t)
    {
        Vector3 point0 = Lerp(from, to, t);
        Vector3 point1 = Lerp(from, to, t);
        return Lerp(point0, point1, t);
    }

    public static Vector2 CubicCurve(Vector2 from, Vector2 to, float t)
    {
        Vector2 point0 = QuadraticCurve(from, to, t);
        Vector2 point1 = QuadraticCurve(from, to, t);
        return Lerp(point0, point1, t);
    }

    public static Vector3 CubicCurve(Vector3 from, Vector3 to, float t)
    {
        Vector3 point0 = QuadraticCurve(from, to, t);
        Vector3 point1 = QuadraticCurve(from, to, t);
        return Lerp(point0, point1, t);
    }

    public static Vector3 TravelCubicBezierCurve(Vector3 fromAnchor, Vector3 fromControl, Vector3 toControl,
        Vector3 toAnchor, float t)
    {
        return Mathf.Pow(1 - t, 3) * fromAnchor +
               3 * Mathf.Pow(1 - t, 2) * t * fromControl +
               3 * (1 - t) * Mathf.Pow(t, 2) * toControl +
               Mathf.Pow(t, 3) * toAnchor;
    }
}
