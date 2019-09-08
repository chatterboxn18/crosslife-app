using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CrossLife;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

namespace CrossLife
{
	[RequireComponent(typeof(EventSystem))]
	[RequireComponent(typeof(Animator))]
	public class AppControl : MonoBehaviour
	{
		private AppState _nextState;
		private AppState _state;
		private Animator _animator;

		[SerializeField]
		private lambo_button MenuButton;

		[SerializeField] private Text _aboutHeader;
		[SerializeField] private RectTransform _panelMenu;

		private void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		private IEnumerator Start()
		{

			_panelMenu.sizeDelta = new Vector2(Screen.width, _panelMenu.sizeDelta.y);
			yield return null;
		}

		private void MenuOff()
		{
			MenuButton.Toggled = false;
			IsMenuVisible = false;
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

		public void ButtonEvt_MenuOff()
		{
			MenuOff();
		}
		
		public void ButtonEvt_About()
		{
			DoAbout();
		}

		public void ButtonEvt_Doctine()
		{
			StartCoroutine(DisplayContent(AboutContent.Doctrine));
		}

		public void ButtonEvt_Leadership()
		{
			StartCoroutine(DisplayContent(AboutContent.Leadership));
		}

		public void ButtonEvt_Membership()
		{
			StartCoroutine(DisplayContent(AboutContent.Membership));
		}

		private IEnumerator DisplayContent(AboutContent content)
		{
			IsAboutVisible = false;
			var text = "";
			switch (content)
			{
				case AboutContent.Doctrine: text = "Doctrine"; break;
				case AboutContent.Leadership: text = "Leadership"; break;
				case AboutContent.Membership: text = "Membership"; break;
			}

			_aboutHeader.text = text;
			IntAboutContent = content;
			IsAboutVisible = true;
			MenuOff();
			yield return null;
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
			IsMenuVisible = false;
			DoMedia();
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
		private bool IsAboutVisible { get { return _animator.GetBool("IsAboutVisible"); } set { _animator.SetBool("IsAboutVisible", value); } }		
		private AboutContent IntAboutContent { get { return (AboutContent) _animator.GetInteger("IntAboutContent"); } set { _animator.SetInteger("IntAboutContent", (int) value); } }
	}
}

