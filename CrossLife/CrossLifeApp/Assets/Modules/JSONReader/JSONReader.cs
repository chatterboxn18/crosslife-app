using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossLife
{
	public class JSONReader : Tool
	{
		private readonly string _crossLifeSermonJSON = "https://raw.githubusercontent.com/chatterboxn18/crosslife-app/master/sermon.json";
		
		private CrossLifeSermons _crossLifeSermons;
		private WebRequestTools _webRequestTools;
		
		[Serializable]
		public class CrossLifeSermons
		{
			public List<CrosslifeSermon> crosslifeSermons;
		
			[Serializable]
			public class CrosslifeSermon
			{
				public string seriesTitle;
				public string thumbnailUrl;
				public string thumbnailPath;
				public List<Sermon> sermons;
			
				[Serializable]
				public class Sermon
				{
					public string sermonTitle;
					public string sermonUrl;
					public string sermonDate;
				}

			}
		}

		public class CrossLifeBanners
		{
			
		}
		
		private IEnumerator Start()
		{
			_webRequestTools = AppUtilities.GetAppUtilies().GetComponentInChildren<WebRequestTools>();

			yield return _webRequestTools.DownloadFile(_crossLifeSermonJSON, "jsons/crossLifeSermon.json");
			yield return _webRequestTools.GetJsonFile("jsons/crossLifeSermon.json", CreateCrossLifeSermonJson);

			//Getting CrossLifeSermons JSON
		}

		private void CreateCrossLifeSermonJson(string jsonString)
		{
			_crossLifeSermons = JsonUtility.FromJson<CrossLifeSermons>(jsonString);
		}

		public CrossLifeSermons GetCrossLifeSermons()
		{
			return _crossLifeSermons;
		}
		
	}
}

