using UnityEngine;
using System.Collections;

public static class UnityExtensionMethos  {

	public static bool IsValid(this Quaternion quaternion)
	{
		bool isNan = float.IsNaN(quaternion.x+quaternion.y+quaternion.z+quaternion.w);
		bool isZero = quaternion.x == 0&& quaternion.y ==0&& quaternion.z == 0&&quaternion.w == 0;
		return !(isNan || isZero);
	}

    public static Vector3 RotatePointAroundPivot(this Transform transform, Vector3 pivot, Vector3 angles)
    {
        Vector3 point = transform.position;
        Vector3 dir = point - pivot;
        dir = Quaternion.Euler(angles) * dir;
        point = dir + pivot;
        return point;
    }

    public static Vector3 RotatePointAroundPivotLocal(this Transform transform, Vector3 pivot, Vector3 angles)
    {
        Vector3 point = Vector3.zero;
        Vector3 dir = point - pivot;
        dir = Quaternion.Euler(angles) * dir;
        point = dir + pivot;
        return point;
    }

    public static float[] ColorToArray(this Color color)
    {
        return new float[] { color.r, color.g, color.b };
    }

    public static Color InInvers(this Color color)
    {
        return new Color(1 - color.r, 1 - color.g, 1 - color.b);
    }
}
