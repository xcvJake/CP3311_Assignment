using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassDisplay : MonoBehaviour {
	void Update () {
	}


	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 2 / 100);
		style.alignment = TextAnchor.UpperCenter;
		style.fontSize = h * 2 / 100;
		style.normal.textColor = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		float mass = PlayerControllerJunkRewrite.cycloneMassLimit;
		string text = string.Format("{0:f} units", mass);
		GUI.Label(rect, text, style);
	}
}
