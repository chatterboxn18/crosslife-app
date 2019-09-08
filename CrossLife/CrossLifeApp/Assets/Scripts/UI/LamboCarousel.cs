using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LamboCarousel : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	private Vector3 _initialPosition;
	private Vector3 _finalPosition;
	private void BeginDrag()
	{
	}

	private void Update()
	{
		
		if (Input.GetMouseButtonDown(0))
		{
			_initialPosition = Input.mousePosition;
			Debug.Log(_initialPosition);
		}

		if (Input.GetMouseButtonUp(0))
		{
			_finalPosition = Input.mousePosition;
			Debug.Log(_finalPosition);
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		
	}

	public void OnEndDrag(PointerEventData eventData)
	{
	}
}
