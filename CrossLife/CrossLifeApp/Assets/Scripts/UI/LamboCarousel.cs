using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;

public class LamboCarousel : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	private float _lastPosition;
	private float _initialPosition;

	private float _rectPeek = 0.05f;
	private float _rectRatio = 1.11f;

	[SerializeField] private Camera _uiCamera;
	
	private float _delta;
	private bool _isRightSwipe;
	
	[SerializeField] private RectTransform[] _carouselPages;

	private int _currentPage;

	private float _distanceTraveled;
	//private RectTransform[] _originalPositions;

	private float DEBUG_Variable;

	private void Start()
	{
		_currentPage = 1;

		/*for (int i = 0; i < _carouselPages.Length; i++)
		{
			_originalPositions[i] = _carouselPages[i];
		}*/
		/*Debug.LogError("The width: " + Screen.width + " and the height: " + Screen.height);
		var excess = Screen.width * 0.2f;
		var rectWidth = Screen.width - excess;
		var rectHeight = rectWidth * _rectRatio;
		var rectPeek = _rectPeek * Screen.width;
		var initialPosition = -(rectWidth) - rectPeek;
		foreach (var item in _carouselPages)
		{
			item.sizeDelta = new Vector2(rectWidth, rectHeight);
			var localPosition = item.localPosition;
			localPosition = new Vector3(initialPosition, localPosition.y, localPosition.z);
			item.localPosition = localPosition;
			initialPosition += rectWidth + rectPeek;
		}*/
	}

	private void IncrementIndex(bool up)
	{
		if (up)
			_currentPage = (_currentPage == 2) ? 0 : _currentPage + 1;
		else
			_currentPage = (_currentPage == 0) ? 2 : _currentPage - 1;
	}

	private RectTransform NextPage(bool isRight)
	{
		if (isRight)
		{
			var page = (_currentPage == 2) ? 0 : _currentPage + 1;
			return _carouselPages[page];
		}
		else
		{
			var page = (_currentPage == 0) ? 2 : _currentPage - 1;
			return _carouselPages[page];
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (eventData.position.x > _lastPosition && !_isRightSwipe)
		{
			_initialPosition = eventData.position.x;
			_distanceTraveled = 0;
			_isRightSwipe = true;
		}
		else if (eventData.position.x < _lastPosition && _isRightSwipe)
		{
			_initialPosition = eventData.position.x;
			_distanceTraveled = 0;
			_isRightSwipe = false;
		}

		if ( _distanceTraveled > (Screen.width / 2f))
		{
			if (_isRightSwipe)
			{
				Debug.Log("It shifted to the left");
				_initialPosition = eventData.position.x;
				var rectToMove = NextPage(true).localPosition;
				var offset = NextPage(false).localPosition.x - _carouselPages[0].sizeDelta.x - 54f;
				NextPage(true).localPosition = new Vector3(offset, rectToMove.y, rectToMove.z);
				IncrementIndex(false);
				_distanceTraveled = 0;
			}
			else
			{
				_initialPosition = eventData.position.x;
				Debug.Log("It shifted to the right");
				var rectToMove = NextPage(false).localPosition;
				var offset = NextPage(true).localPosition.x + _carouselPages[0].sizeDelta.x + 54f;
				NextPage(false).localPosition = new Vector3(offset, rectToMove.y, rectToMove.z);
				IncrementIndex(true);
				_distanceTraveled = 0;
			}
		}
		var delta = eventData.position.x - _lastPosition;
		_distanceTraveled += Mathf.Abs(delta);
		foreach (var item in _carouselPages)
		{
			var lastPosition = item.localPosition;
			item.localPosition = new Vector3(lastPosition.x + delta, lastPosition.y, lastPosition.z);
		}

		_lastPosition = eventData.position.x;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		_lastPosition = eventData.position.x;
		_initialPosition = eventData.position.x;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
	}
}
