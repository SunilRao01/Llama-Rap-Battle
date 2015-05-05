using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Intro : MonoBehaviour 
{
	public Text dialogueLabel;
	public List<string> introDialogue;
	public List<AudioClip> introDialogueClip;
	private bool onPlayer = true;

	// Llama y rotation: 50
	// Llama y rotation: 120

	void Start () 
	{
		StartCoroutine(startIntroduction());
	}
	
	void Update () 
	{
	
	}

	IEnumerator startIntroduction()
	{
		int count = 0;

		while (count < introDialogue.Count)
		{
			dialogueLabel.text = introDialogue[count];

			GetComponent<AudioSource>().clip = introDialogueClip[count];
			GetComponent<AudioSource>().Play();
			
			yield return new WaitForSeconds(introDialogueClip[count].length);

			if (onPlayer)
			{
				Vector3 newRotation = transform.eulerAngles;
				newRotation.y = 120;
				transform.eulerAngles = newRotation;

				onPlayer = false;
			}
			else
			{
				Vector3 newRotation = transform.eulerAngles;
				newRotation.y = 50;
				transform.eulerAngles = newRotation;
				
				onPlayer = true;
			}

			count++;
		}

		Application.LoadLevel("Sandbox");
	}
}
