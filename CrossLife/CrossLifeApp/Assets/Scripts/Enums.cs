using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrossLife
{
	public enum AppState
	{
		None = 0,
		Load = 1, 
		Home = 2, 
		Sermons = 3,
		Ministry = 4, 
		About = 5,
		Menu = 6
	}

	public enum AboutContent
	{
		Doctrine = 0, 
		Leadership = 1, 
		Membership = 2
	}
	
	public enum TransitionStyle
	{
		None = 0, 
		SlideLeft = 1, 
		SlideRight = 2, 
		SlideUp = 3, 
		SlideDown = 4
	}

}
