using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CrossLife
{
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(CanvasGroup))]
	public class lambo_button : Graphic, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
	{

		public bool Disabled;
		public bool Visible;
		public bool isToggle;
		private bool Toggled;
		
		[Serializable]
		public class ButtonEvent : UnityEvent
		{
		}

		[SerializeField] public ButtonEvent OnPointerUp = new ButtonEvent();
		[SerializeField] public ButtonEvent OnPointerDown = new ButtonEvent();
		[Serializable]
		public class ToggledEvent : UnityEvent<bool> { }
		[SerializeField]
		public ToggledEvent OnToggle = new ToggledEvent();
		
		private Animator _animator;

		protected override void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		// Update is called once per frame
		void Update()
		{
			isDisabled = Disabled;
			isVisible = Visible;
			isToggled = Toggled;
			raycastTarget = Visible;
		}

		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			if (isDisabled)
				return;
			isPressed = true;

			if (isToggle)
			{
				Toggled = !Toggled;
				OnToggle.Invoke(Toggled);
			}
			else 
				OnPointerDown.Invoke();

		}

		void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
		{
			if (isDisabled)
				return;
			isHovered = true;
		}

		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			isHovered = false;
		}

		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			isPressed = false;
			OnPointerUp.Invoke();
		}

		//animator booleans
		public bool isDisabled
		{
			get { return _animator != null && _animator.isInitialized && _animator.GetBool("isDisabled"); }
			protected set
			{
				if (_animator == null || !_animator.isInitialized) return;
				_animator.SetBool("isDisabled", value);
			}
		}

		public bool isVisible
		{
			get { return _animator != null && _animator.isInitialized && _animator.GetBool("isVisible"); }
			protected set
			{
				if (_animator == null || !_animator.isInitialized) return;
				_animator.SetBool("isVisible", value);
			}
		}

		public bool isPressed
		{
			get { return _animator != null && _animator.isInitialized && _animator.GetBool("isPressed"); }
			protected set
			{
				if (_animator == null || !_animator.isInitialized) return;
				_animator.SetBool("isPressed", value);
			}
		}

		public bool isHovered
		{
			get { return _animator != null && _animator.isInitialized && _animator.GetBool("isHovered"); }
			protected set
			{
				if (_animator == null || !_animator.isInitialized) return;
				_animator.SetBool("isHovered", value);
			}
		}
		
		public bool isToggled
		{
			get { return _animator != null && _animator.isInitialized && _animator.GetBool("isToggled"); }
			protected set
			{
				if (_animator == null || !_animator.isInitialized) return;
				_animator.SetBool("isToggled", value);
			}
		}
		
	}
}