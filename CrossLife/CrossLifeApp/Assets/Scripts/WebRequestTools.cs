using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public static class WebRequestTools
{
	public static IEnumerator GetTextureFromWeb(string url, Action<Texture2D> onComplete, string fileUrl = "")
	{
		var www = UnityWebRequestTexture.GetTexture(url);
		//string path = Path.Combine(Application.persistentDataPath, fileUrl);
		//www.downloadHandler = new DownloadHandlerFile(path);
		yield return www.SendWebRequest();

		if(www.isNetworkError || www.isHttpError) {
			Debug.Log(www.error);
		}
		else {
			var text = ((DownloadHandlerTexture)www.downloadHandler).texture;
			onComplete(text);
		}
		yield return null;
	}
	
	public static Sprite ConvertToSprite(Texture2D texture)
	{
		var sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100, 0,
			SpriteMeshType.FullRect);
		return sprite;
	}

}
