using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public void StartButton()
	{
		Application.LoadLevel("Intro");
	}

	public void ExitButton()
	{
		Application.Quit();
	}
}
