using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class BattleRapSystem : MonoBehaviour 
{
	// Word selection
	private Text wordText1;
	private Text wordText2;
	private Text wordText3;
	private int lyricIndex;

	// Lyric templates
	public List<string> lyrics;
	public List<string> wordOptions;
	public List<string> selectedWords;
	private Text lyricsLabel;
	private int lyricCount;
	private int audioCount;

	// Audio lists
	public List<AudioClip> firstPartClip;
	public List<AudioClip> secondPartClip;
	public List<AudioClip> wordOptionClip;

	// Enemy audio
	public List<string> opponentLyrics;
	public List<AudioClip> opponentLyricsClip;

	void Start() 
	{
		lyricIndex = 0;
		lyricCount = 1;

		// Set button/word settings
		selectedWords = new List<string>();
		wordText1 = transform.GetChild(0).GetChild(0).GetComponent<Text>();
		wordText2 = transform.GetChild(1).GetChild(0).GetComponent<Text>();
		wordText3 = transform.GetChild(2).GetChild(0).GetComponent<Text>();

		lyricsLabel = transform.GetChild(3).GetComponent<Text>();

		// Update lyric label
		lyricsLabel.text = lyrics[lyricIndex];

		// Update word options
		wordText1.text = wordOptions[lyricIndex * 3];
		wordText2.text = wordOptions[lyricIndex * 3 + 1];
		wordText3.text = wordOptions[lyricIndex * 3 + 2];
	}
	
	void Update() 
	{

	}

	public void buttonSelection(int buttonIndex)
	{
		Debug.Log(lyricIndex.ToString());
		selectedWords.Add(wordOptions[(lyricIndex * 3) + buttonIndex]);
		//Debug.Log("Selected word: " + selectedWords[lyricIndex]);

		if (lyricCount == 8 || lyricIndex == 7)
		{
			// Make buttons invisible and uninteractable
			Color newColor = wordText1.color;
			newColor.a = 0;
			wordText1.transform.parent.GetComponent<Button>().interactable = false;
			wordText2.transform.parent.GetComponent<Button>().interactable = false;
			wordText3.transform.parent.GetComponent<Button>().interactable = false;
			wordText1.color = newColor;
			wordText2.color = newColor;
			wordText3.color = newColor;
			
			StartCoroutine(playVoice());
			return;
		}
		else
		{
			lyricCount++;
			lyricIndex++;

			//lyricsLabel.text = lyricsLabel.text.Replace("_", wordOptions[(lyricIndex * 3) + buttonIndex].ToString());
			lyricsLabel.text = lyrics[lyricIndex];
			// Update word options
			wordText1.text = wordOptions[lyricIndex * 3];
			wordText2.text = wordOptions[lyricIndex * 3 + 1];
			wordText3.text = wordOptions[lyricIndex * 3 + 2];
		}
	}

	IEnumerator playVoice()
	{
		lyricIndex = 0;

		while (lyricIndex < 8)
		{
			// Display lyrics
			lyrics[lyricIndex] = lyrics[lyricIndex].Replace("_", selectedWords[lyricIndex]);
			lyricsLabel.text = lyrics[lyricIndex];

			// Play audio
			List<AudioClip> audioSequence = new List<AudioClip>();
			audioSequence.Add(firstPartClip[lyricIndex]); // Add first part of lyric

			audioSequence.Add(wordOptionClip.Find(ni => ni.name == selectedWords[lyricIndex])); // Add word option

			audioSequence.Add(secondPartClip[lyricIndex]); // Add ending lyric

			StartCoroutine(playAudioSequence(audioSequence));
			yield return new WaitForSeconds(audioSequence[0].length + audioSequence[1].length + audioSequence[2].length);
			lyricIndex++;
		}

		StartCoroutine(playOpponentAudio());
	}

	IEnumerator playAudioSequence(List<AudioClip> inputClips)
	{
		GetComponent<AudioSource>().clip = inputClips[0];
		GetComponent<AudioSource>().Play();

		yield return new WaitForSeconds(inputClips[0].length);

		GetComponent<AudioSource>().clip = inputClips[1];
		GetComponent<AudioSource>().Play();

		yield return new WaitForSeconds(inputClips[1].length);

		GetComponent<AudioSource>().clip = inputClips[2];
		GetComponent<AudioSource>().Play();

		yield return new WaitForSeconds(inputClips[2].length);
	}

	IEnumerator playOpponentAudio()
	{
		int count = 0;
		while (count < opponentLyrics.Count)
		{
			// Display lyrics
			lyricsLabel.text = opponentLyrics[count];

			// Play audio clip
			GetComponent<AudioSource>().clip = opponentLyricsClip[count];
			GetComponent<AudioSource>().Play();

			yield return new WaitForSeconds(opponentLyricsClip[count].length);

			count++;
		}

		Application.LoadLevel("GameOver");
	}










}
