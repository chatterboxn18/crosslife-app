using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
	public static Sprite ConvertToSprite(Texture2D texture)
	{
		var sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100, 0,
			SpriteMeshType.FullRect);
		return sprite;
	}
}
