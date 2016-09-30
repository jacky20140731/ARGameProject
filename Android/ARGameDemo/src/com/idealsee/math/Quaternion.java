package com.idealsee.math;

public class Quaternion {
	public float x;

	public float y;

	public float z;

	public float w;

	public Quaternion(float x, float y, float z, float w) {
		this.x = x;
		this.y = y;
		this.z = z;
		this.w = z;
	}

	public void set(float x, float y, float z, float w) {
		this.x = x;
		this.y = y;
		this.z = z;
		this.w = z;
	}

	public Quaternion clone() {
		return new Quaternion(x, y, z, w);
	}

	/** 单位化 */
	public void normalize() {
		float n = x * x + y * y + z * z + w * w;
		if (n != 1 && n > 0) {
			n = 1 / (float) Math.sqrt(n);
			x = x * n;
			y = y * n;
			z = z * n;
			w = w * n;
		}
	}

	/** 绕向量axis旋转angle角度 */
	public Quaternion angleAxis(float angle, Vector3 axis) {
		Vector3 normAxis = axis.normalized();
		angle = angle * Mathf.HALFRAD2DEG;
		float s = (float) Math.sin((double) angle);
		float w = (float) Math.cos((double) angle);
		float x = normAxis.x * s;
		float y = normAxis.y * s;
		float z = normAxis.z * s;
		set(x, y, z, w);
		return this;
	}

	/** vec绕向量axis旋转angle角度 在vec上设置 */
	public Vector3 angleAxis(Vector3 vec, float angle, Vector3 axis) {
		return angleAxis(vec, angle, axis, false);
	}

	/** vec绕向量axis旋转angle角度 (b为true返回新向量 否则vec上设置) */
	public Vector3 angleAxis(Vector3 vec, float angle, Vector3 axis, boolean b) {
		angleAxis(angle, axis);
		if (b) {
			return mulVec3(vec);
		}
		return mulVec3(vec, vec);
	}

	/** 设置旋转四元数 */
	public Quaternion setEuler(Vector3 a) {
		return setEuler(a.x, a.y, a.z);
	}

	/** 设置旋转四元数 */
	public Quaternion setEuler(float x, float y, float z) {
		x = x * Mathf.HALFRAD2DEG;
		y = y * Mathf.HALFRAD2DEG;
		z = z * Mathf.HALFRAD2DEG;

		float sinX = (float) Math.sin(x);
		float cosX = (float) Math.cos(x);
		float sinY = (float) Math.sin(y);
		float cosY = (float) Math.cos(y);
		float sinZ = (float) Math.sin(z);
		float cosZ = (float) Math.cos(z);

		w = cosY * cosX * cosZ + sinY * sinX * sinZ;
		x = cosY * sinX * cosZ + sinY * cosX * sinZ;
		y = sinY * cosX * cosZ - cosY * sinX * sinZ;
		z = cosY * cosX * sinZ - sinY * sinX * cosZ;

		return this;
	}

	/** 向量变换(* vector3) 生成新向量 vec */
	public Vector3 mulVec3(Vector3 point) {
		return mulVec3(Vector3.Zero(), point);
	}

	/** 向量变换(* vector3) 在向量 vec设置 */
	public Vector3 mulVec3(Vector3 vec, Vector3 point) {
		if (vec == null) {
			return null;
		}
		float num = x * 2;
		float num2 = y * 2;
		float num3 = z * 2;
		float num4 = x * num;
		float num5 = y * num2;
		float num6 = z * num3;
		float num7 = x * num2;
		float num8 = x * num3;
		float num9 = y * num3;
		float num10 = w * num;
		float num11 = w * num2;
		float num12 = w * num3;

		float x = (((1 - (num5 + num6)) * point.x) + ((num7 - num12) * point.y))
				+ ((num8 + num11) * point.z);
		float y = (((num7 + num12) * point.x) + ((1 - (num4 + num6)) * point.y))
				+ ((num9 - num10) * point.z);
		float z = (((num8 - num11) * point.x) + ((num9 + num10) * point.y))
				+ ((1 - (num4 + num5)) * point.z);

		vec.x = x;
		vec.y = y;
		vec.z = z;

		return vec;
	}

	/** 四元数点乘 */
	public static float Dot(Quaternion a, Quaternion b) {
		return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
	}

	/** 两四元数夹角 */
	public static float Angle(Quaternion a, Quaternion b) {
		float dot = Dot(a, b);
		if (dot < 0) {
			dot = -dot;
		}
		return (float) (Math.acos((double) Math.min(dot, 1)) * 2 * Mathf.RAD2DEG);
	}

	/** 旋转 */
	public static Quaternion Euler(float x, float y, float z) {
		Quaternion quat = new Quaternion(0, 0, 0, 0);
		quat.setEuler(x, y, z);
		return quat;
	}
}
