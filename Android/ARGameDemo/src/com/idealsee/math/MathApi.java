package com.idealsee.math;

public class MathApi {
	/** 当前向量的角度(不计算Y值) */
	public static float Vec3ToAngle(Vector3 vec) {
		return (float) (Math.atan2(vec.z, vec.x) * 180 / Mathf.PI);
	}

	/** 角度转单位向量 (不计算Y值) */
	public static Vector3 AngleToVec3(float angle) {
		float rad = angle * Mathf.DEG2RAD;
		return new Vector3((float) Math.cos(rad), 0, (float) Math.sin(rad));
	}

	/** 计算圆与圆相交(不计算Y值) */
	public static boolean CircleIntersectCircle(Vector3 point1, float r1,
			Vector3 point2, float r2) {
		point1.y = 0;
		point2.y = 0;
		if (Vector3.Distance(point1, point2) > r1 + r2)
			return false;
		return true;
	}

	/** 计算圆与矩形相交(不计算Y值) */
	public static boolean CircleIntersectRect(Vector3 cv, float r, Vector3 rv,
			float rx, float rz) {

		float x = Math.abs(cv.x - rv.x);
		float z = Math.abs(cv.z - rv.z);
		if (x > rx + r)
			return false;
		if (z > rz + r)
			return false;
		return true;
	}
}
