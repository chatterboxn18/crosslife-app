using System;
using System.Collections;
using System.Collections.Generic;
using CrossLife;
using UnityEngine;
using UnityEngine.UI;

public class UIAsset_SermonTitleIcon : UIAsset
{
	public Image SermonIcon;
	public Text TextTitle;
	public List<JSONReader.CrossLifeSermons.CrosslifeSermon.Sermon> Sermons;
	public Action Evt_DisplaySermons = () => { }; 
	
	[SerializeField] private UIAsset_Sermon _sermonPrefab;
	
	public override IEnumerator LoadAssets()
	{
		return base.LoadAssets();
	}

	public void LoadSermons(RectTransform parent)
	{
		if (Sermons == null)
			return;
		foreach (var sermon in Sermons)
		{
			var sermonTile = Instantiate(_sermonPrefab, parent);
		}
	}

	public void ButtonEvt_DisplaySermons()
	{
		Evt_DisplaySermons();
	}
	
	public override void UnloadAssets()
	{
		SermonIcon = null;
		TextTitle = null;
	}
}
