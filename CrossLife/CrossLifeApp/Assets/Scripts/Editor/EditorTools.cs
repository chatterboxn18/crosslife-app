using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEditor;

namespace CrossLife
{
    
	public static class EditorTools
	{
		[MenuItem("Tools/Filing/OpenPersistentData")]
		public static void OpenPersistentData()
		{
			EditorUtility.RevealInFinder(Application.persistentDataPath);	
		}
	}
}

