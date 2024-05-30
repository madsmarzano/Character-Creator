using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(MeshFilter))]
public class RoadSegment : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float tTest = 0; //point that travels along the bezier curve

    [SerializeField] Transform[] controlPoints = new Transform[4];

    Vector3 GetPos(int i) => controlPoints[i].position; //access position of a certain control point 



    public void OnDrawGizmos()
    {
        for(int i = 0; i< 4; i++)
        {
            Gizmos.DrawSphere(GetPos(i), 0.05f);
        }

        Handles.DrawBezier(
            GetPos(0), //start post
            GetPos(3), //end pos
            GetPos(1),
            GetPos(2), Color.white, EditorGUIUtility.whiteTexture, 1f);

        Vector3 testPoint = GetBezierPoint(tTest);
        Quaternion testOrientation = GetBezierOrientation(tTest);

        Gizmos.color = Color.red;

        //Gizmos.DrawSphere(testPoint, 0.05f);

        Handles.PositionHandle(testPoint, testOrientation);

        Gizmos.color = Color.white;
    }

    Quaternion GetBezierOrientation(float t)
    {

        return Quaternion.identity;
    }

    Vector3 GetBezierPoint(float t)
    {
        Vector3 p0 = GetPos(0);
        Vector3 p1 = GetPos(1);
        Vector3 p2 = GetPos(2);
        Vector3 p3 = GetPos(3);

        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(d, e, t);
    }

    Vector3 GetBezierTangent(float t)
    {
        Vector3 p0 = GetPos(0);
        Vector3 p1 = GetPos(1);
        Vector3 p2 = GetPos(2);
        Vector3 p3 = GetPos(3);

        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        Vector3 c = Vector3.Lerp(p2, p3, t);

        Vector3 d = Vector3.Lerp(a, b, t);
        Vector3 e = Vector3.Lerp(b, c, t);

        return (e - d).normalized;
    }
}
