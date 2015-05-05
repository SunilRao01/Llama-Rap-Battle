using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

	public void TryAgainButton()
	{
		Application.LoadLevel("Sandbox");
	}

	public void ExitButton()
	{
		Application.Quit();
	}
}
