using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarouselElement : MonoBehaviour {
    public Text GameName;
    public Text GameDescription;
    public Image GameImage;

    public RectTransform RectTransform { get; protected set; }

    void Awake () {
        RectTransform = GetComponent<RectTransform>();
	}
}
