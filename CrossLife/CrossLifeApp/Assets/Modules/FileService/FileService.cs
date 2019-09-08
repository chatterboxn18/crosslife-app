using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.iOS;
using UnityEngine;
using UnityEngine.Networking;

namespace CrossLife
{
	public class FileService : MonoBehaviour
	{
		[SerializeField] private AudioSource _player;

		public enum MediaType
		{
			Texture, 
			Audio
		}
		
		/////////////////
		/// Interface ///
		/////////////////

		public IEnumerator GetAudioFile(string path)
		{
			yield return GetFile(path, MediaType.Audio, error => {  } , request =>
			{
				AudioClip clip = ((DownloadHandlerAudioClip) request.downloadHandler).audioClip;
				if (clip == null)
				{
					Debug.LogError("The file that came back might be null or corrupt");
				}
				_player.clip = clip;
				_player.Play();
			});
			yield return null;
			
		}
		

		private IEnumerator DownloadFile(string path, string destPath, Action onError = null, Action onComplete = null)
		{
			var umr = UnityWebRequest.Get(path);
			umr.downloadHandler = new DownloadHandlerFile(Path.Combine(Application.persistentDataPath, destPath));
			yield return umr.SendWebRequest();
			if (umr.isNetworkError || umr.isHttpError)
			{
				Debug.LogError("There was a networking error.");
				yield break;
			}
			while (!umr.isDone)
			{
				if(umr.error != null)
					yield break;
				Debug.Log(umr.downloadProgress);
				yield return null;
			}

			if (!string.IsNullOrEmpty(umr.error))
			{
				Debug.LogError("An error [" + umr.error + "] occured");
				if (onError != null) onError();
			}
			else
			{
				Debug.Log("Clip has been successfully downloaded");
				if (onComplete != null) onComplete();
			}
		}
		
		private IEnumerator GetFile(string path, MediaType type, Action<string> onError = null, Action<UnityWebRequest> onComplete = null)
		{
			var finalPath = Path.Combine(Application.persistentDataPath, path);
#if UNITY_ANDROID && !UNITY_EDITOR
			path = "file://" + path;
#endif
			UnityWebRequest umr;
			switch (type)
			{
				case MediaType.Audio:
					umr = UnityWebRequestMultimedia.GetAudioClip(finalPath, AudioType.WAV);
					break;
				default:
					umr = UnityWebRequest.Get(finalPath);
					break;
			}
			var request = umr.SendWebRequest();
			while (!umr.isDone)
			{
				Debug.Log(request.progress);
				yield return null;
			}
			if (!string.IsNullOrEmpty(umr.error))
				Debug.LogError("This error [" + umr.error + "] occured");
			else
			{
				if (onComplete != null) onComplete(umr);
			}
			
		}

		public void DeleteFile()
		{
			var path = Path.Combine(Application.persistentDataPath, "test/audio.wav");
			if (File.Exists(path))
				File.Delete(path);
			Resources.UnloadUnusedAssets();
		}

		
		/////////////////
		/// Examples ////
		/////////////////
		
		public void Evt_DownloadAudio(string path)
		{
			StartCoroutine(DownloadFile(path, "test/audio.wav"));
			//StartCoroutine(GetAudioClip());
		}

		public void Evt_LoadAudio(string filePath)
		{
			StartCoroutine(GetAudioFile(filePath));
			//StartCoroutine(GetFile(filePath, MediaType.Audio));
		}
		
	}
}

