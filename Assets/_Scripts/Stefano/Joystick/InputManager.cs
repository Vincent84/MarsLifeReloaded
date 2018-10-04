using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{

	#region Axis

	public static float MainHorizontal()
	{

		float r = 0.0f;
		r += Input.GetAxis ("LeftAnalogHorizontal");
		r += Input.GetAxis ("RightAnalogHorizontal");

		return Mathf.Clamp (r, -1.0f, 1.0f); 

	}

	public static float MainVertical()
	{

		float r = 0.0f;
		r += Input.GetAxis ("LeftAnalogVertical");
		r += Input.GetAxis ("RightAnalogVertical");

		return Mathf.Clamp (r, -1.0f, 1.0f); 

  
	}

	public static Vector3 MainJoystick()
	{

		return new Vector3 (MainHorizontal (), 0, MainVertical ());

	}

	public static float RTbutton()
	{

		return Input.GetAxis ("RT");

	}

	public static float LTbutton()
	{

		return Input.GetAxis ("LT");

	}

	#endregion

	#region Buttons

	public static bool ABotton()
	{

		return Input.GetButtonDown ("A");

	}

	public static bool BBotton()
	{

		return Input.GetButtonDown ("B");

	}

	public static bool XBotton()
	{

		return Input.GetButtonDown ("X");

	}

	public static bool YBotton()
	{

		return Input.GetButtonDown ("Y");

	}

	public static bool StartButton()
	{

		return Input.GetButtonDown ("Start");

	}

	public static bool SelectButton()
	{

		return Input.GetButtonDown ("Select");

	}


	public static bool RBbutton()
	{

		return Input.GetButtonDown ("RB");

	}

	public static bool LBbutton()
	{

		return Input.GetButtonDown ("LB");

	}
		

	#endregion

	#region Arrow

	public static bool UPArrow()
	{

		return Input.GetAxis ("D-Pad Vertical") > 0;

	}

	public static bool DOWNArrow()
	{

		return Input.GetAxis ("D-Pad Vertical") < 0;

	}

	public static bool LEFTArrow()
	{

		return Input.GetAxis ("D-Pad Horizontal") < 0;

	}

	public static bool RIGHTArrow()
	{

		return Input.GetAxis ("D-Pad Horizontal") > 0;

	}

	#endregion
}
