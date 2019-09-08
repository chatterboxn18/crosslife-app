using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CrossLife
{
	public class AppUtilities : MonoBehaviour
	{
		public static GameObject AppUtility;

		private void Awake()
		{
			AppUtility = gameObject;
		}

		public static GameObject GetAppUtilies()
		{
			return AppUtility;
		}
	}

}
