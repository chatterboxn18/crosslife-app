using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossLife
{
	public class RectList : MonoBehaviour
	{
		[SerializeField]
		private RectTransform[] _listOfTransforms;

		private int _totalSize;

		private IEnumerator Start()
		{
			foreach (var item in _listOfTransforms)
			{
			}

			yield return null;
		}

	}


}
