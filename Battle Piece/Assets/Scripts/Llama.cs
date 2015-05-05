using UnityEngine;
using System.Collections;

public class Llama : MonoBehaviour 
{
	public GameObject neckJoint;
	private bool movementCheck; // false = left, true = right
	private Vector3 nextRotation;
	private bool isCentered;
	public bool startRight;
	public float time;

	// Middle lower center: (0, 35, 0)
	// Upper Left: (20, 70, -35)
	// Upper Right: (20, 70, 35)

	void Start () 
	{
		if (startRight)
		{
			// Llama starts by moving its head to the right
			nextRotation.Set(20, 70, 35);
			iTween.RotateTo(neckJoint, iTween.Hash("rotation", nextRotation, "time", time, "OnCompleteTarget", gameObject,
		                                       	"OnComplete", "afterMovement", "islocal", true, "easetype", iTween.EaseType.linear));
			movementCheck = true;
		}
		else
		{
			// Llama starts by moving its head to the left
			nextRotation.Set(20, 70, -35);
			iTween.RotateTo(neckJoint, iTween.Hash("rotation", nextRotation, "time", time, "OnCompleteTarget", gameObject,
			                                       "OnComplete", "afterMovement", "islocal", true, "easetype", iTween.EaseType.linear));
			movementCheck = false;
		}

		isCentered = false;
	}
	
	void Update () 
	{
	
	}

	void afterMovement()
	{
		if (!isCentered)
		{
			nextRotation.Set(0, 35, 0);
			iTween.RotateTo(neckJoint, iTween.Hash("rotation", nextRotation, "time", time, "OnCompleteTarget", gameObject,
			                                       "OnComplete", "afterMovement", "islocal", true, "easetype", iTween.EaseType.linear));
			isCentered = true;
		}
		else
		{
			if (movementCheck)
			{
				// Move to left
				nextRotation.Set(20, 70, -35);
				iTween.RotateTo(neckJoint, iTween.Hash("rotation", nextRotation, "time", time, "OnCompleteTarget", gameObject,
				                                       "OnComplete", "afterMovement", "islocal", true, "easetype", iTween.EaseType.linear));
				isCentered = false;
				movementCheck = false;
			}
			else
			{
				// Move to right
				nextRotation.Set(20, 70, 35);
				iTween.RotateTo(neckJoint, iTween.Hash("rotation", nextRotation, "time", time, "OnCompleteTarget", gameObject,
				                                       "OnComplete", "afterMovement", "islocal", true, "easetype", iTween.EaseType.linear));
				isCentered = false;
				movementCheck = true;
			}
		}
	}
}
