using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderControl : MonoBehaviour
{
    private Animator _animator;
    
    public Action Evt_OnBack = delegate { };

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void ButtonEvt_Back()
    {
        
    }

    public void ButtonEvt_Menu()
    {
        
    }
    
    public enum HeaderContent
    {
        Menu = 0, 
        Back = 1
    } 
		
    private void UpdateHeader(HeaderContent content)
    {
        switch (content)
        {
            case HeaderContent.Back:
                break;
            case HeaderContent.Menu:
					
                break;
        }
    }
    
    private HeaderContent IntHeaderContent { get { return (HeaderContent) _animator.GetInteger("IntHeaderContent"); } set { _animator.SetInteger("IntHeaderContent", (int) value); } }

}
