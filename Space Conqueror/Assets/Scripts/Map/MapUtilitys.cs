using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapUtilitys
{
	public const int mapSize = 500;

	public static float ToMap(float value, float fromLow, float fromHigh, float toLow, float toHigh) 
	{
		return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
	}

	public static Vector2 ToMap(Vector2 value, Vector2 fromLow, Vector2 fromHigh, Vector2 toLow, Vector2 toHigh)
	{
		return new Vector2(ToMap(value.x, fromLow.x, fromHigh.x, toLow.x,toHigh.x),
			ToMap(value.y, fromLow.y, fromHigh.y, toLow.y, toHigh.y));
	}
}
