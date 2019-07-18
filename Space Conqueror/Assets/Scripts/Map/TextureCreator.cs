using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpriteMask {

	public class TextureCreator : MonoBehaviour {

		public Material MaterialToApply;

		public int Size;
		
		private Texture2D m_maskTexture;
		
		private Texture2D m_mapTexture;

		private Vector2 m_lastPlayerPosition;

		public Image m_imgToApply;

		public ControlPlayer m_player;

		public GameObject _mapPlayer;
		
		private const int m_texSize = 4000;
		
		private void Start() {
			Create();
		}
		
		[ContextMenu("Create")]
		public void Create() {
			
			Color[] pixels = new Color[m_texSize * m_texSize];

			for (int y = 0; y < m_texSize; y++) {
				for (int x = 0; x < m_texSize; x++) {
					pixels[x + y * m_texSize] = Color.white;
				}
			}
			
			
			m_maskTexture = new Texture2D(m_texSize,m_texSize, TextureFormat.ARGB32, false, true);
			m_maskTexture.SetPixels(pixels);
			MaterialToApply.SetTexture("_MaskTex", m_maskTexture);
			GenerateObjects();
		}
		

		private void Update()
		{

			Vector2 playerPos = m_player.transform.position;
			//_mapPlayer.gameObject.transform.position = playerPos;
			
			if (playerPos == m_lastPlayerPosition) return;
			
			int w = m_maskTexture.width;
			int h = m_maskTexture.height;

			int halfSize = Size / 2;
			
//			var playerTexturePosX = MapUtilitys.ToMap((int) playerPos.x, -m_size, m_size, 0,
//				m_maskTexture.width);
//			var playerTexturePosY = MapUtilitys.ToMap((int)  playerPos.y, -m_size, m_size, 0,
//				m_maskTexture.height);
			
			var playerTexturePosX = MapUtilitys.ToMap((int) playerPos.x, -MapUtilitys.mapSize, MapUtilitys.mapSize, 0,
				m_maskTexture.width);
			var playerTexturePosY = MapUtilitys.ToMap((int)  playerPos.y, -MapUtilitys.mapSize, MapUtilitys.mapSize, 0,
				m_maskTexture.height);
			
			var newPos = _mapPlayer.gameObject.transform.position;
			
			
			//Posição relativa do jogador
			for (int y = -halfSize; y < halfSize; y++) {
				for (int x = -halfSize; x < halfSize; x++)
				{
					int currentX = x + playerTexturePosX;
					int currentY = y + playerTexturePosY;
					
					if (currentX < 0 || currentX > w || currentY < 0 || currentY > h)
						continue;
					
					
					newPos.x = currentX;
					newPos.y = currentY;
					
					m_maskTexture.SetPixel(currentX, currentY, new Color(0,0,0,0));

				}
			}
			Debug.Log("map player pos: X: " + newPos.x + " e Y: " + newPos.y);
			Debug.Log("player mundo: " + playerPos);

			m_maskTexture.Apply();

			m_lastPlayerPosition = playerPos;
		}

	
		
		private void GenerateObjects()
		{
			
			m_mapTexture = new Texture2D(m_texSize, m_texSize, TextureFormat.ARGB32, false, true);
//			Color[] pixels = new Color[Screen.width * Screen.height];
//
//			
//			for (int y = 0; y < m_size; y++) {
//				for (int x = 0; x < m_size; x++) {
//					pixels[x + y * m_size] = Color.blue;
//				}
//			}
//			
//			m_mapTexture.SetPixels(pixels);
			
			var DrawObjectsMap = FindObjectsOfType<MapObject>();

			for (int i = 0; i < DrawObjectsMap.Length; i++)
			{
				DrawObjectsMap[i].DrawOnMap(m_mapTexture);
			}

			m_mapTexture.Apply();
			MaterialToApply.SetTexture("_MainTex", m_mapTexture);
			m_imgToApply.sprite = Sprite.Create(m_mapTexture, new Rect(0,0, m_mapTexture.width, m_mapTexture.height),
				new Vector2(0.5f,0.5f));
		}

	}
}