using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAsset_Sermon : UIAsset
{
	public Image SermonImage;
	public Text Title;
	public Text Byline;
	public Text Duration;
	public Text Date;
	
	public override IEnumerator LoadAssets()
	{
		return base.LoadAssets();
	}
	
}
