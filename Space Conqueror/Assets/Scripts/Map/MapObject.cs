using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
	public Texture2D model;

	public Vector2 size;

	public void DrawOnMap(Texture2D mapTex)
	{
		int hw = (int)size.x / 2;
		int hh = (int)size.y / 2;

		int MapPosX = MapUtilitys.ToMap((int)transform.position.x, -MapUtilitys.mapSize, MapUtilitys.mapSize, 0,
			mapTex.width);
		
		int MapPosY = MapUtilitys.ToMap((int)transform.position.y, -MapUtilitys.mapSize, MapUtilitys.mapSize, 0,
			mapTex.height);
			
		for (int y = -hh; y < hh; y++)
		{
			for (int x = -hw; x < hw; x++)
			{
				int currentX = x + MapPosX;
				int currentY = y + MapPosY;

				if (currentX < 0 || currentX > mapTex.width || currentY < 0 || currentY > mapTex.height)
				{
					continue;
				}

				int texCoordX = MapUtilitys.ToMap(x, -hw, hw, 0, model.width);
				int texCoordY = MapUtilitys.ToMap(y, -hh, hh, 0, model.height);
				
				
				var newColor = model.GetPixel(texCoordX, texCoordY);
				var oldColor = mapTex.GetPixel(currentX,currentY);
				var finalColor = (newColor.a * new Vector3(newColor.r, newColor.g, newColor.b) +
				                    ((1 - newColor.a) * new Vector3(oldColor.r, oldColor.g, oldColor.b)));


				Debug.LogError("Posições do mapa, X: " + currentX + ", Y: " + currentY + "De: " + model.name);
				mapTex.SetPixel(currentX, currentY, new Color(finalColor.x, finalColor.y, finalColor.z, 1));
			}
		}
	}
}