package com.idealsee.math;

public class Mathf {
	public static final float EPSILON = 0;

	public static final float FLT_EPSILON = 0.000001f;

	public static final float DBL_EPSILON = 0.0000000001f;

	public static final float PI = 3.14159265f;

	public static final float DEG2RAD = 0.0174533f;

	public static final float RAD2DEG = 57.29578f;

	public static final float HALFRAD2DEG = 0.5f * RAD2DEG;

	public static float Clamp(float value, float min, float max) {
		if (value < min)
			value = min;
		else {
			if (value > max) {
				value = max;
			}
		}
		return value;
	}

	public static float Clamp01(float value) {
		if (value < 0f)
			return 0f;
		if (value > 1f)
			return 1f;
		return value;
	}

	public static float Max(float a, float b) {
		return (a <= b) ? b : a;
	}

	public static float Min(float a, float b) {
		return (a >= b) ? b : a;
	}
}
