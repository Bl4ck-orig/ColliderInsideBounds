using System.Linq;
using UnityEngine;

/// <summary>
/// Handles getting colliders inside any form of quadrilateral.
/// 
/// 09.07.2022 - Bl4ck?
/// </summary>
public static class ColliderInsideBounds
{
    private static float height;

    private static float xMin;
    private static float zMin;
    private static float xMax;
    private static float zMax;

    private static float xSize;
    private static float zSize;
    private static float maxSizeHalf;

    private static Vector3 center;
    private static Vector3 halfExtents = new Vector3(xSize * 0.5f, height, zSize * 0.5f);

    private static Vector3 rem;

    private static Vector3 abNorm;
    private static Vector3 bcNorm;
    private static Vector3 cdNorm;
    private static Vector3 daNorm;

    private static Collider[] coll;

    private static Vector3 abCenter;
    private static Vector3 bcCenter;
    private static Vector3 cdCenter;
    private static Vector3 daCenter;

    private static Quaternion abRotation;
    private static Quaternion bcRotation;
    private static Quaternion cdRotation;
    private static Quaternion daRotation;

    private static Vector3 removeHalfExtents;

    private static Collider[] remove;

     /// <summary>
    /// Gets all colliders inside of bounds.
    /// Points need to be in clockwise or counterclockwise order for it to work!
    /// </summary>
    /// <param name="_a">First point</param>
    /// <param name="_b">Second point</param>
    /// <param name="_c">Third point</param>
    /// <param name="_d">Fourth point</param>
    /// <param name="_height">Height to use for checks</param>
    /// <param name="_layerMask">Layermask to check in</param>
    /// <returns>All colliders inside of the quadrilateral</returns>
    public static Collider[] GetColliderInsideBounds(Vector3 _a, Vector3 _b, Vector3 _c, Vector3 _d, float _height, LayerMask _layerMask)
    {
        height = _height;

        xMin = Mathf.Min(_a.x, _b.x, _c.x, _d.x);
        zMin = Mathf.Min(_a.z, _b.z, _c.z, _d.z);
        xMax = Mathf.Max(_a.x, _b.x, _c.x, _d.x);
        zMax = Mathf.Max(_a.z, _b.z, _c.z, _d.z);

        xSize = xMax - xMin;
        zSize = zMax - zMin;
        maxSizeHalf = Mathf.Max(xSize, zSize) * 0.5f;

        center = new Vector3((xMax + xMin) * 0.5f, 0, (zMax + zMin) * 0.5f);
        halfExtents = new Vector3(xSize * 0.5f, height, zSize * 0.5f);

        // Special case where input has to be validated.
        if (Vector3.Dot(PerpendicularCounterClockwise((_b - _a).normalized).normalized, ((_a + _b) * 0.5f - center).normalized) < 0)
        {
            rem = _b;
            _b = _d;
            _d = rem;
        }
        _a.y = _b.y = _c.y = _d.y = 0;

        abNorm = (_b - _a).normalized;
        bcNorm = (_c - _b).normalized;
        cdNorm = (_d - _c).normalized;
        daNorm = (_a - _d).normalized;

        coll = Physics.OverlapBox(center, halfExtents, Quaternion.identity, _layerMask);

        abCenter = ((_a + _b) * 0.5f) + Utils.PerpendicularCounterClockwise(abNorm) * maxSizeHalf;
        bcCenter = ((_b + _c) * 0.5f) + Utils.PerpendicularCounterClockwise(bcNorm) * maxSizeHalf;
        cdCenter = ((_c + _d) * 0.5f) + Utils.PerpendicularCounterClockwise(cdNorm) * maxSizeHalf;
        daCenter = ((_d + _a) * 0.5f) + Utils.PerpendicularCounterClockwise(daNorm) * maxSizeHalf;

        abRotation = (abNorm != Vector3.zero) ? Quaternion.LookRotation(abNorm, Vector3.up) : Quaternion.identity;
        bcRotation = (bcNorm != Vector3.zero) ? Quaternion.LookRotation(bcNorm, Vector3.up) : Quaternion.identity;
        cdRotation = (cdNorm != Vector3.zero) ? Quaternion.LookRotation(cdNorm, Vector3.up) : Quaternion.identity;
        daRotation = (daNorm != Vector3.zero) ? Quaternion.LookRotation(daNorm, Vector3.up) : Quaternion.identity;

        removeHalfExtents = new Vector3(maxSizeHalf, height, maxSizeHalf);

        remove = Physics.OverlapBox(abCenter, removeHalfExtents, abRotation, _layerMask);
        remove = remove.Concat(Physics.OverlapBox(bcCenter, removeHalfExtents, bcRotation, _layerMask)).ToArray();
        remove = remove.Concat(Physics.OverlapBox(cdCenter, removeHalfExtents, cdRotation, _layerMask)).ToArray();
        remove = remove.Concat(Physics.OverlapBox(daCenter, removeHalfExtents, daRotation, _layerMask)).ToArray();
        remove.Distinct();

        return (from collider in coll where !remove.Contains(collider) select collider).ToArray();
    }

    /// <summary>
    /// Calculates the perpendicular vector to a vector counterclockwise.
    /// </summary>
    /// <param name="_vector">The vector to calculate the perpendicular to</param>
    /// <returns>The perpendicular vector</returns>
    public static Vector3 PerpendicularCounterClockwise(Vector3 _vector) => new Vector3(-_vector.z, 0, _vector.x);
}
