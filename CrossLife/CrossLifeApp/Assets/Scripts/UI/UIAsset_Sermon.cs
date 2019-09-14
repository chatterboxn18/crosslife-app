using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAsset_Sermon : UIAsset
{
	[Header("UI Elements")]
	public Image SermonImage;
	public Text Title;
	public Text Byline;
	public Text Duration;
	public Text Date;

	public Action Evt_PlaySermon = () => { };
	
	public override IEnumerator LoadAssets()
	{
		return base.LoadAssets();
	}
	
}
