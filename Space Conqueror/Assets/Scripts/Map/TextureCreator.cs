using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpriteMask {

	public class TextureCreator : MonoBehaviour {

		public Material MaterialToApply;

		public int Size;
		
		private Texture2D m_generatedTexture;
		
		private Texture2D m_generatedTexture2;

		private Vector2 m_lastPlayerPosition;

		public Image m_imgToApply;

		public ControlPlayer m_player;
		
		private void Start() {
			Create();
		}
		
		[ContextMenu("Create")]
		public void Create() {
			
			Color[] pixels = new Color[Screen.width * Screen.height];

			for (int y = 0; y < Screen.height; y++) {
				for (int x = 0; x < Screen.width; x++) {
					pixels[x + y * Screen.width] = Color.white;
				}
			}
			
			
			m_generatedTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false, true);
			m_generatedTexture.SetPixels(pixels);
			MaterialToApply.SetTexture("_MaskTex", m_generatedTexture);
			GenerateObjects();
		}
		

		private void Update()
		{

			Vector2 playerPos = m_player.transform.position;
			
			if (playerPos == m_lastPlayerPosition) return;
			
			int w = m_generatedTexture.width;
			int h = m_generatedTexture.height;

			int halfSize = Size / 2;
			
			var playerTexturePosX = MapUtilitys.ToMap((int) playerPos.x, -200, 200, 0,
				m_generatedTexture.width);
			var playerTexturePosY = MapUtilitys.ToMap((int)  playerPos.y, -200, 200, 0,
				m_generatedTexture.height);
			
			//Posição relativa do jogador
			for (int y = -halfSize; y < halfSize; y++) {
				for (int x = -halfSize; x < halfSize; x++)
				{
					int currentX = x + playerTexturePosX;
					int currentY = y + playerTexturePosY;
					
					if (currentX < 0 || currentX > w || currentY < 0 || currentY > h) {
						continue;
					}
					
					m_generatedTexture.SetPixel(currentX, currentY, new Color(0,0,0,0));
				}
			}
			Debug.Log("map player pos: X: " + playerTexturePosX + " e Y: " + playerTexturePosY);
			Debug.Log("player mundo: " + playerPos);
			
			m_generatedTexture.Apply();

			m_lastPlayerPosition = playerPos;
		}
		
		//Posição do player, o tamanho do mundo, o tamanho do mapa 
		
		
		private void GenerateObjects()
		{
			m_generatedTexture2 = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false, true);
			Color[] pixels = new Color[Screen.width * Screen.height];

			
			for (int y = 0; y < Screen.height; y++) {
				for (int x = 0; x < Screen.width; x++) {
					pixels[x + y * Screen.width] = Color.blue;
				}
			}
			
			m_generatedTexture2.SetPixels(pixels);
			
			var meteorObjects = FindObjectsOfType<MapObject>();

			for (int i = 0; i < meteorObjects.Length; i++)
			{
				meteorObjects[i].DrawOnMap(m_generatedTexture2);
			}

			m_generatedTexture2.Apply();
			MaterialToApply.SetTexture("_MainTex", m_generatedTexture2);
			m_imgToApply.sprite = Sprite.Create(m_generatedTexture2, new Rect(0,0, m_generatedTexture2.width, m_generatedTexture2.height),
				new Vector2(0.5f,0.5f));
		}

	}
}