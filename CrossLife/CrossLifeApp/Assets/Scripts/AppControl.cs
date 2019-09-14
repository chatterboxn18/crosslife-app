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

		[Header("Panels")] 
		[SerializeField] private RectTransform _panelHome;
		[SerializeField] private RectTransform _panelSermon;
		[SerializeField] private RectTransform _panelAudioPlayer;

		[Header("Transitions")] 
		private AppState _currentAppState;
		[SerializeField] private RectTransform _screenCanvas;
		[SerializeField] private RectTransform _panelPrevious;
		[SerializeField] private RectTransform _panelCurrent;
		private static readonly int TransitionIndex = Animator.StringToHash("TransitionIndex");
		
		private void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		private IEnumerator Start()
		{

			//Transition Setup
			_panelPrevious.anchorMin = new Vector2(0.5f, 0.5f);
			_panelPrevious.anchorMax = new Vector2(0.5f, 0.5f);
			_panelPrevious.sizeDelta = new Vector2(1080f, 1920f);

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
				case AppState.Sermons:
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

		public void ButtonEvt_GoTo(int state)
		{
			IsMenuVisible = false;
			if ((int) _currentAppState == state)
				return;
			var transition = (state > (int) _currentAppState) ? TransitionStyle.SlideRight: TransitionStyle.SlideLeft;
			Evt_TransitionTo(_currentAppState, (AppState) state, transition);
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

		public void Evt_TransitionTo(AppState previousState, AppState currentState, TransitionStyle style)
		{
			StartCoroutine(SetNextTransition(previousState, currentState, style));
		}

		
		private RectTransform GetPanel(AppState state)
		{
			switch (state)
			{
				case AppState.Home:
					return _panelHome;
				case AppState.Sermons:
					return _panelSermon;
				default:
					return _panelHome;
			}
		}
		
		private IEnumerator SetNextTransition(AppState previousState, AppState currentState, TransitionStyle style = TransitionStyle.None)
		{
			IsInTransition = true;
			GetPanel(previousState).SetParent(_panelPrevious, false);
			GetPanel(previousState).gameObject.SetActive(true);
			GetPanel(currentState).SetParent(_panelCurrent, false);
			GetPanel(currentState).gameObject.SetActive(true);
			yield return null;
			Transition = style;
			yield return new WaitForSeconds(1/3f);
			GetPanel(previousState).SetParent(_screenCanvas, false);
			GetPanel(previousState).gameObject.SetActive(false);
			GetPanel(currentState).SetParent(_screenCanvas, false);
			Transition = TransitionStyle.None;
			_currentAppState = currentState;
			IsInTransition = false;
			yield return null;
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
		private bool IsInTransition { get { return _animator.GetBool("IsInTransition"); } set { _animator.SetBool("IsInTransition", value); } }		
		private AboutContent IntAboutContent { get { return (AboutContent) _animator.GetInteger("IntAboutContent"); } set { _animator.SetInteger("IntAboutContent", (int) value); } }
		
		private TransitionStyle Transition { get => (TransitionStyle)_animator.GetInteger(TransitionIndex); set => _animator.SetInteger(TransitionIndex, (int)value); }

	}
}

