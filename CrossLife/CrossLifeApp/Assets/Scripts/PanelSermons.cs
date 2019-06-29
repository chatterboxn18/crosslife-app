﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
 using UnityEngine.UI;
 using Image = UnityEngine.Experimental.UIElements.Image;

 public class PanelSermons : MonoBehaviour {

	private AudioSource _audioSource;
	[SerializeField] private UIAsset_SermonTitleIcon _sermonIconPrefab;
	[SerializeField] private UIAsset_Sermon _sermonItemPrefab;
	[SerializeField] private RectTransform _sermonGroup;

	private int _iconBuffer = 10;
	private int _iconsInRow = 2;
	private int _sermonBuffer = 5;
	
	private List<UIAsset_SermonTitleIcon> _sermonIcons;
	private List<GameObject> _sermonItems;
	
	[Serializable]
	public class CrossLifeSermons
	{
		public List<CrosslifeSermon> crosslifeSermons;
		
		[Serializable]
		public class CrosslifeSermon
		{
			public string seriesTitle;
			public string thumbnailUrl;
			public string thumbnailPath;
			public List<Sermon> sermons;
			
			[Serializable]
			public class Sermon
			{
				public string sermonTitle;
				public string sermonUrl;
				public string sermonDate;
			}

		}

		public static CrossLifeSermons CreateFromJson(string jsonString)
		{
			return JsonUtility.FromJson<CrossLifeSermons>(jsonString);
		}
	}


	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	/*public class CrossLifeSermons
	{
		public List<CrosslifeSermon> crosslifeSermons;
		
		public static CrossLifeSermons CreateFromJson(string jsonString)
		{
			return JsonUtility.FromJson<CrossLifeSermons>(jsonString);
		}
		
		public class CrosslifeSermon
		{
			public string sermon;
		}
	}*/
	
	private IEnumerator Start()
	{
		
		Debug.Log(Application.persistentDataPath);
		var www = UnityWebRequest.Get("https://raw.githubusercontent.com/chatterboxn18/crosslife-app/master/sermon.json");
		yield return www.SendWebRequest();

		if (www.error != null)
		{
			Debug.Log("Something happened");
			yield break;
		}

		Debug.Log(www.downloadHandler.text);
		
		yield return CreateSermonIcons(www.downloadHandler.text);
		
		/*using (UnityWebRequest music = UnityWebRequestMultimedia.GetAudioClip("https://s3.us-west-2.amazonaws.com/crosslife-sermons-2.0/1%20%26%202%20Thessalonians/20181104%20%7C%20Respecting%20the%20Leaders%20of%20the%20Church%20%7C%201%20Thessalonians%205%3A12.mp3?response-content-disposition=inline&X-Amz-Security-Token=AgoGb3JpZ2luEOL%2F%2F%2F%2F%2F%2F%2F%2F%2F%2FwEaCXVzLXdlc3QtMSKAAoNjO5dBTn9HXk4VxlolvEdZS9d9g4mPh2e0yCVfO%2FMuFCgmrnxlMyKsV8o4VlvFQTLIh9h4ysX3WzRl0NPYKrXRk5bweXF73TEHc6YTA8UATauNy384tyokZJuVgW6aIllwgWXZo8Dt2dzNJ2ONZYSD0E9Pe6lgl%2FeCC4Bz2jminayjkK3Q91mspoxFv0yV%2B6VM8ecK6mPDrV%2F2R8WZIujC9WlGMiSUfBwm%2FwntcYKEolnjO4sMbixTwZJ9Iwie3GoXhyHSMayqNyt0xM7VkVdkcS0YCdqT32PSL35dV1ORf9lb37pGIfikw199K%2FlKK2zwmTctlGF0S7cYsieiQbcq%2BwMIh%2F%2F%2F%2F%2F%2F%2F%2F%2F%2F%2FARAAGgw0NjQ4Njg3OTI4ODYiDNM9IlHQAJxyWnucJSrPA%2BBJ85Otq9JSGppjOwGCQ%2FNpF%2BVQzWn1iD8hRryGoEcve%2FQVw1FMAg5Q3tIIWDse%2F9iIY4f%2BMsrVjdT6t7O690Fi7j7sqhgoGTCujzAYeycQlAor3mSX4ey9c24yGByuCh%2FdxWFmcHtNpiNpx5evejz3WPJ05kzP3D0a6auH3jaIbhjBxHYJnwF0ymEiOAQKWQvr2UquO1sW4Vrx0HVmX2O2fch90IFygVrgyPUwvaRMdQsrEiDQYQGDXJR%2FF6XE2DSUWMzHZyrAI0eFuqCkxriHvQdnPPl6J8bP3GVFgFwz0%2BOePWZrE7xCBvQKW2bZhWtuBw7C%2Bw2lkKlAE0ShTz5YNvYywmUXfxPbTFNb8%2BAiTK08L18knlAXWL5FZ7cuHR706Ujxd3OgpzPxRw9Yp90%2B1XQyu4pGahStj%2BUwFzVYsJ4cRlk6ycoF8smdH796Ic5gXpF9B6HDnu%2FJ7%2Bp6vuZyWER6e6fzSTXcvnPJFH%2B8S9c%2BVLX91TQjR%2FMHVGHRNlK8t4TKiRkCBg3%2BLHjIz30B8x5hKdwkhRmlX6bHufVWpvvQALp%2FqD%2Bqtk743RrJG0ehb3FV4Z%2Fj6RfAtK79xb4FMIbfQnyzOMVg8fpeN88wucbZ5gU%3D&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Date=20190511T055301Z&X-Amz-SignedHeaders=host&X-Amz-Expires=300&X-Amz-Credential=ASIAWYPCW3I3L3BWBNUD%2F20190511%2Fus-west-2%2Fs3%2Faws4_request&X-Amz-Signature=156c5d9cc57602674ea43fa8e7eb9eaab4ed84fa7e8e0e4d4ff81fa0880c71d0", AudioType.MPEG))
		{
			yield return music.Send();

			if (music.isNetworkError)
			{
				Debug.Log(music.error);
			}
			else
			{
				AudioClip myClip = DownloadHandlerAudioClip.GetContent(music);
				_audioSource.clip = myClip;
				_audioSource.Play();
			}
		}*/
		//ReadJson(www.downloadHandler.data);
		yield return null;
	}

	private IEnumerator CreateSermonIcons(string json)
	{
		var sermons = CrossLifeSermons.CreateFromJson(json);

		_sermonIcons = new List<UIAsset_SermonTitleIcon>();

		var totalBuffer = 3 * _iconBuffer;
		var iconWidth = Screen.width - totalBuffer;
		iconWidth = iconWidth / _iconsInRow;
		var totalIcons = 0;
		var yBuffer = _iconBuffer;
		
		foreach (var sermon in sermons.crosslifeSermons)
		{
			var sermonIcon = Instantiate(_sermonIconPrefab, _sermonGroup);
			StartCoroutine(WebRequestTools.GetTextureFromWeb(sermon.thumbnailUrl,
				(image) => { sermonIcon.SermonIcon.sprite = WebRequestTools.ConvertToSprite(image); }, "test.jpg") );
			sermonIcon.RectTransform.sizeDelta = new Vector2(iconWidth, iconWidth);
			var rect = sermonIcon.RectTransform.rect;
			sermonIcon.RectTransform.anchoredPosition = new Vector2((totalIcons % 2 == 0) ? _iconBuffer : _iconBuffer * 2 + rect.width, rect.height * -Mathf.FloorToInt( (totalIcons/(float) _iconsInRow)) - yBuffer);
			sermonIcon.TextTitle.text = sermon.seriesTitle;
			sermonIcon.Sermons = sermon.sermons;
			sermonIcon.Evt_DisplaySermons = () => ButtonEvt_CreateSermonItems(sermon);
			_sermonIcons.Add(sermonIcon);
			totalIcons++;
			if (totalIcons%_iconsInRow == 0) yBuffer += _iconBuffer;
		}

		yield return null;
	}

	private void ButtonEvt_CreateSermonItems(CrossLifeSermons.CrosslifeSermon sermonIcon)
	{
		UnloadSermonIcons();

		var yBuffer = 0;
		for (int i = 0; i < sermonIcon.sermons.Count; i++)
		{
			var sermonItem = Instantiate(_sermonItemPrefab, _sermonGroup);
			sermonItem.RectTransform.anchoredPosition = new Vector2(0, -sermonItem.RectTransform.rect.height * i - yBuffer);
			yBuffer += _sermonBuffer;
		}
	}

	private void UnloadSermonIcons()
	{
		if (_sermonIcons == null || _sermonIcons.Count <= 0)
			return;
		foreach (var sermonTitle in _sermonIcons)
		{
			Destroy(sermonTitle.gameObject);
		}

	}
	
	private void ReadJson( )
	{
		
		//StreamReader reader = new StreamReader(www);
		//Debug.Log(reader.ReadToEnd());
	}
}
