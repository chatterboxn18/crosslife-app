using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAsset : MonoBehaviour
{
	protected internal RectTransform RectTransform;

	private void Awake()
	{
		RectTransform = GetComponent<RectTransform>();
	}

	private IEnumerator Start()
	{
		yield return LoadAssets();
	}

	public virtual void UnloadAssets()
	{
		
	}
	
	public virtual IEnumerator LoadAssets()
	{
		yield return null;
	}

	
	private void OnDisable()
	{
		UnloadAssets();
	}
}
