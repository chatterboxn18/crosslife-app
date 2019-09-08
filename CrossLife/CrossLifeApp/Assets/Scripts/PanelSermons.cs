﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
 using CrossLife;
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

	private WebRequestTools _webRequestTools;
	private JSONReader _jsonReader;
	
	


	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}
	
	private IEnumerator Start()
	{

		_webRequestTools = AppUtilities.GetAppUtilies().GetComponentInChildren<WebRequestTools>();
		_jsonReader = AppUtilities.GetAppUtilies().GetComponentInChildren<JSONReader>();
		
		yield return null;
		yield return CreateSermonIcons();
	}

	private IEnumerator CreateSermonIcons()
	{
		var sermons = _jsonReader.GetCrossLifeSermons();

		_sermonIcons = new List<UIAsset_SermonTitleIcon>();

		var totalBuffer = 3 * _iconBuffer;
		var iconWidth = Screen.width - totalBuffer;
		iconWidth = iconWidth / _iconsInRow;
		var totalIcons = 0;
		var yBuffer = _iconBuffer;
		
		foreach (var sermon in sermons.crosslifeSermons)
		{
			var sermonIcon = Instantiate(_sermonIconPrefab, _sermonGroup);
			yield return _webRequestTools.DownloadFile(sermon.thumbnailUrl, sermon.thumbnailPath, () => { }, () =>
				{
					StartCoroutine(_webRequestTools.GetTextureFile(sermon.thumbnailPath,(image) => { sermonIcon.SermonIcon.sprite = Utils.ConvertToSprite(image); }) );
				});
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

	private void ButtonEvt_CreateSermonItems(JSONReader.CrossLifeSermons.CrosslifeSermon sermonIcon)
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
