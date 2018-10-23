using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrossLife;
using UnityEngine.EventSystems;

namespace CrossLife
{
	[RequireComponent(typeof(EventSystem))]
	[RequireComponent(typeof(Animator))]
	public class AppControl : MonoBehaviour
	{
		private AppState _nextState;
		private AppState _state;
		private Animator _animator;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		private IEnumerator Start()
		{

			yield return null;
		}

		private void SetNextState(AppState nextState)
		{
			_nextState = nextState;
			switch (_nextState)
			{
				case AppState.Home:
					DoHome();
					break;
				case AppState.Media:
					DoMedia();
					break;
				case AppState.Ministry:
					DoMinistry();
					break;
				case AppState.About:
					DoAbout();
					break;
				case AppState.Menu:
					DoMenu();
					break;
				default:
					break;
			}
		}

		public void ButtonEvt_Menu(bool on)
		{
			IsMenuVisible = on;
		}

		public void ButtonEvt_About()
		{

		}

		public void ButtonEvt_Rides()
		{
			Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLScIPHy1Cc57pqOTm2KE3ls-mCpUs0UKNrnEreXQOckplc2qPw/viewform");
		}

		public void ButtonEvt_Ministry()
		{

		}

		public void ButtonEvt_Media()
		{

		}

		/// <summary>
		/// Animator Controls
		/// </summary>
		private void DoHome()
		{
			_animator.SetTrigger("DoHome");
		}
		private void DoMedia()
		{
			_animator.SetTrigger("DoMedia");
		}
		private void DoMinistry()
		{
			_animator.SetTrigger("DoMinistry");
		}
		private void DoAbout()
		{
			_animator.SetTrigger("DoAbout");
		}
		private void DoMenu()
		{
			_animator.SetTrigger("DoMenu");
		}
		private bool IsMenuVisible { get { return _animator.GetBool("IsMenuVisible"); } set { _animator.SetBool("IsMenuVisible", value); } }		
	}
}

