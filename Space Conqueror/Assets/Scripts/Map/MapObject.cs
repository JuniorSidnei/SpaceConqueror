using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
	public Texture2D model;

	public Vector2 size;
	
	public void DrawOnMap(Texture2D mapText)
	{
		int hw = (int)size.x / 2;
		int hh = (int)size.y / 2;

		int MapPosX = MapUtilitys.ToMap((int)transform.position.x, -MapUtilitys.height, MapUtilitys.height, 0,
			mapText.width);
		
		int MapPosY = MapUtilitys.ToMap((int)transform.position.y, -MapUtilitys.height, MapUtilitys.height, 0,
			mapText.height);
			
		for (int y = -hh; y < hh; y++)
		{
			for (int x = -hw; x < hw; x++)
			{
				int currentX = x + MapPosX;
				int currentY = y + MapPosY;

				if (currentX < 0 || currentX > mapText.width || currentY < 0 || currentY > mapText.height)
				{
					continue;
				}

				int texCoordX = MapUtilitys.ToMap(x, -hw, hw, 0, model.width);
				int texCoordY = MapUtilitys.ToMap(y, -hh, hh, 0, model.height);
				
				
				Color newColor = model.GetPixel(texCoordX, texCoordY);
				Color oldColor = mapText.GetPixel(currentX,currentY);
				Vector3 finalColor = (newColor.a * new Vector3(newColor.r, newColor.g, newColor.b) +
				                    ((1 - newColor.a) * new Vector3(oldColor.r, oldColor.g, oldColor.b)));


				mapText.SetPixel(currentX, currentY, new Color(finalColor.x, finalColor.y, finalColor.z, 1));
			}
		}
	}
}