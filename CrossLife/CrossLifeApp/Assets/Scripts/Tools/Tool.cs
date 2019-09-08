using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
	private IEnumerator Start()
	{
		yield return SetUp();
	}

	protected virtual IEnumerator SetUp()
	{
		yield return null;
	}
}
