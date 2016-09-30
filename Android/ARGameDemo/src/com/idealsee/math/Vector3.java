package com.idealsee.math;

public class Vector3 {
	public final float KEPSILON = 1E-05f;

	public float x;

	public float y;

	public float z;

	public static Vector3 Up() {
		return new Vector3(0f, 1f, 0f);
	}

	public static Vector3 Down() {
		return new Vector3(0f, -1f, 0f);
	}

	public static Vector3 Left() {
		return new Vector3(-1f, 0f, 0f);
	}

	public static Vector3 Right() {
		return new Vector3(1f, 0f, 0f);
	}

	public static Vector3 Forward() {
		return new Vector3(0f, 0f, 1f);
	}

	public static Vector3 Back() {
		return new Vector3(0f, 0f, -1f);
	}

	public static Vector3 Zero() {

		return new Vector3(0f, 0f, 0f);
	}

	public static Vector3 One() {
		return new Vector3(1f, 1f, 1f);
	}

	/** 向量长度 */
	public float magnitude() {
		return (float) Math
				.sqrt((double) (this.x * this.x + this.y * this.y + this.z
						* this.z));
	}

	/** 单位向量化 */
	public Vector3 normalized() {
		float num = magnitude();
		if (num > 1E-05f)
			div(num);
		else {
			x = 0;
			y = 0;
			z = 0;
		}
		return this;
	}

	/** 向量平方 */
	public float sqrMagnitude() {
		return this.x * this.x + this.y * this.y + this.z * this.z;
	}

	public Vector3(float x, float y) {
		this.x = x;
		this.y = y;
		this.z = 0f;
	}

	public Vector3(float x, float y, float z) {
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public void set(float x, float y, float z) {
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public Vector3 clone() {
		return new Vector3(x, y, z);
	}

	/** 两向量之间的夹角(0~pi) */
	public static float Radian(Vector3 from, Vector3 to) {
		return (float) Math.acos((double) (Mathf.Clamp(
				Dot(from.normalized(), to.normalized()), -1f, 1f)));
	}

	/** 两向量之间的夹角(0 ~ 180) */
	public static float Angle(Vector3 from, Vector3 to) {
		return Radian(from, to) * Mathf.RAD2DEG;
	}

	/** 两向点之间的距离 */
	public static float Distance(Vector3 a, Vector3 b) {
		return (float) Math.sqrt((double) SqrVector3Sub(a, b));
	}

	/** 向量的长度, 最大不能超过maxLength所指示的长度 */
	public static Vector3 ClampMagnitude(Vector3 a, float maxLength) {
		if (a.sqrMagnitude() > maxLength * maxLength) {
			Mul(a.normalized(), maxLength);
		}
		return a;
	}

	/** 向量叉乘, 求与两向量垂直的向量 */
	public static Vector3 Cross(Vector3 a, Vector3 b) {
		return new Vector3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x
				* b.y - a.y * b.x);
	}

	/** 向量点乘 */
	public static float Dot(Vector3 a, Vector3 b) {
		return a.x * b.x + a.y * b.y + a.z * b.z;
	}

	/** 插值 */
	public static Vector3 Lerp(Vector3 from, Vector3 to, float t) {
		t = Mathf.Clamp01(t);
		return new Vector3(from.x + (to.x - from.x) * t, from.y
				+ (to.y - from.y) * t, from.z + (to.z - from.z) * t);
	}

	/** 两向量差的平方 */
	public static float SqrVector3Sub(Vector3 a, Vector3 b) {
		float x = a.x - b.x;
		float y = a.y - b.y;
		float z = a.z - b.z;
		return x * x + y * y + z * z;
	}

	/** 两个向量组成的最大向量 */
	public static Vector3 Max(Vector3 a, Vector3 b) {
		return new Vector3(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y), Mathf.Max(
				a.z, b.z));
	}

	/** 两个向量组成的最小向量 */
	public static Vector3 Min(Vector3 a, Vector3 b) {
		return new Vector3(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y), Mathf.Min(
				a.z, b.z));
	}

	/** from绕axis旋转到to的夹角 */
	public static float AngleAroundAxis(Vector3 from, Vector3 to, Vector3 axis) {
		from.sub(Vector3.Project(from, axis));
		to.sub(Vector3.Project(to, axis));
		float angle = Angle(from, to);
		return angle * (Dot(axis, Cross(from, to)) < 0 ? -1 : 1);
	}

	/** 相等 */
	public static boolean equals(Vector3 a, Vector3 b) {
		return SqrVector3Sub(a, b) < 9.99999944E-11f;
	}

	/** 不等 */
	public static boolean not(Vector3 a, Vector3 b) {
		return !equals(a, b);
	}

	/** 投影 */
	public static Vector3 Project(Vector3 vector, Vector3 onNormal) {
		float num = onNormal.sqrMagnitude();

		if (num < 1.175494e-38) {
			return Vector3.Zero();
		}
		float num2 = Dot(vector, onNormal);
		Vector3 v3 = onNormal.clone();
		v3.mul(num2 / num);
		return v3;
	}

	// ------------------------Vector3运算符-----------------------------
	public static float SqrMagnitude(Vector3 a) {
		return a.x * a.x + a.y * a.y + a.z * a.z;
	}

	/** + */
	public static void Add(Vector3 a, Vector3 b) {
		a.x += b.x;
		a.y += b.y;
		a.z += b.z;
	}

	/** - */
	public static void Sub(Vector3 a, Vector3 b) {
		a.x -= b.x;
		a.y -= b.y;
		a.z -= b.z;
	}

	/** * */
	public static void Mul(Vector3 a, float d) {
		a.x *= d;
		a.y *= d;
		a.z *= d;
	}

	/** * */
	public static void Mul(float d, Vector3 a) {
		Mul(a, d);
	}

	/** / */
	public static void Div(Vector3 a, float d) {
		a.x /= d;
		a.y /= d;
		a.z /= d;
	}

	/** + */
	public Vector3 add(Vector3 a) {
		this.x += a.x;
		this.y += a.y;
		this.z += a.z;
		return this;
	}

	/** - */
	public Vector3 sub(Vector3 a) {
		this.x -= a.x;
		this.y -= a.y;
		this.z -= a.z;
		return this;
	}

	/** * */
	public Vector3 mul(float d) {
		this.x *= d;
		this.y *= d;
		this.z *= d;
		return this;
	}

	/** * */
	public Vector3 mul(Vector3 a) {
		this.x *= a.x;
		this.y *= a.y;
		this.z *= a.z;
		return this;
	}

	/** / */
	public Vector3 div(float d) {
		this.x /= d;
		this.y /= d;
		this.z /= d;
		return this;
	}

	/** / */
	public Vector3 div(Vector3 a) {
		this.x /= a.x;
		this.y /= a.y;
		this.z /= a.z;
		return this;
	}
}
