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
	private List<UIAsset_Sermon> _sermonItems = new List<UIAsset_Sermon>();

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
		UnloadSermonGroup();
		yield return new WaitUntil(() => _sermonGroup.childCount == 0);
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
		UnloadSermonGroup();
		var totalSize = 0f;
		
		var yBuffer = 0;
		for (int i = 0; i < sermonIcon.sermons.Count; i++)
		{
			var sermon = sermonIcon.sermons[i];
			var sermonItem = Instantiate(_sermonItemPrefab, _sermonGroup).GetComponent<UIAsset_Sermon>();
			_sermonItems.Add(sermonItem);
			sermonItem.RectTransform.anchoredPosition = new Vector2(0, -sermonItem.RectTransform.rect.height * i - yBuffer);
			sermonItem.Date.text = sermon.sermonDate;
			sermonItem.Title.text = sermon.sermonTitle;
			sermonItem.Byline.text = sermon.sermonSpeaker;
			yBuffer += _sermonBuffer;
		}
	}
	
	private void UnloadSermonGroup()
	{
		if (_sermonGroup.childCount > 0)
		{
			for(var i = 0; i < _sermonGroup.childCount; i++)
			{
				Destroy(_sermonGroup.GetChild(i).gameObject);
			}

			Resources.UnloadUnusedAssets();
		}
	}
	
	private void ReadJson( )
	{
		
		//StreamReader reader = new StreamReader(www);
		//Debug.Log(reader.ReadToEnd());
	}
}
