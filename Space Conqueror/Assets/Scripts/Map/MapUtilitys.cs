﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapUtilitys
{
	public const int mapSize = 1500;

	public static int ToMap(int value, int fromLow, int fromHigh, int toLow, int toHigh) 
	{
		return (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;
	}	
}
