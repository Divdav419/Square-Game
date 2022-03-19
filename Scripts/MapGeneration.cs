using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{

	public GameObject restartButton;
	public Transform ground;
	public GameObject obstacle;
	public Transform player;
	public PlayerMovement started;
	public Transform cam;
	public float rowSpace;
	public float minBlockSize;
	public float startSpace;
	public float collumnSpace;
	GameObject placeholder;
	List<GameObject> obs = new List<GameObject>();

	int length = 0;

	void row(float z)
	{
		//	Creating space
		float groundLeft = ground.position.x - (ground.localScale.x/2f);
		float groundRight = ground.position.x + (ground.localScale.x/2f);

		float spacingLeft = Random.Range(groundLeft, (groundRight - rowSpace));
		float spacingRight = spacingLeft + rowSpace;

		//Debug.Log("Ground Left: " + groundLeft);
		//Debug.Log("Ground Right: " + groundRight);
		//Debug.Log("Spacing Left: " + spacingLeft);
		//Debug.Log("Spacing Right: " + spacingRight);

		//	Clone Left Obstacle
		if ((spacingLeft - groundLeft) >= minBlockSize) {
			placeholder = Instantiate(obstacle, new Vector3(((groundLeft + spacingLeft)/2f), 1, z), ground.rotation);
			placeholder.transform.localScale = new Vector3((spacingLeft - groundLeft), 1, 1);
			obs.Add(placeholder);
			length+=1;
		}
		//Debug.Log("Left Block Size: "+(spacingLeft - groundLeft));
		//Debug.Log("Left Block Position: " + ((groundLeft + spacingLeft)/2f));

		//	Clone Right Obstacle
		if ((groundRight - spacingRight) >= minBlockSize) {
			placeholder = Instantiate(obstacle, new Vector3(((((groundRight - spacingRight)/2f)) + spacingRight), 1, z), ground.rotation);
			placeholder.transform.localScale = new Vector3((groundRight - spacingRight), 1, 1);
			obs.Add(placeholder);
			length+=1;
		}
		//Debug.Log("Right Block Size: "+(groundRight - spacingRight));
		//Debug.Log("Right Block Position: " + (((groundRight - spacingRight)/2f) + spacingRight)); 
	}

	void generateMap()
	{
		// Generate first
		row(startSpace + player.position.z);

		// Generate rest
		for (float z = (player.position.z + startSpace + collumnSpace); z < ((ground.localScale.z/2f) + ground.position.z); z += collumnSpace) row(z);
		started.started = true;
		obs.TrimExcess();
	}


	void generateNew()
	{
		// Destroy unseen obstacles
		if (obs[0].transform.position.z == obs[1].transform.position.z){
			//Debug.Log("Deleting 2");
			Destroy(obs[0]);
			Destroy(obs[1]);
			obs.RemoveAt(0);
			obs.RemoveAt(0);
			length-=2;
		} else {
			Destroy(obs[0]);
			obs.RemoveAt(0);
			length-=1;
		}

		//Create new obstacles to the end
		//Debug.Log((obs[(length-1)].transform.position.z + collumnSpace));
		row((obs[(length-1)].transform.position.z + collumnSpace));
	}

	bool ended = false;
	public void endGame()
    {
		if (!ended)
		{
			ended = true;

			restartButton.SetActive(true);
			Debug.Log("GAME OVER");
		}
    }

	void Start()
	{
		restartButton.SetActive(false);
		generateMap();
	}

	// Update is called once per frame
	void Update()
	{
		if (obs[0].transform.position.z < cam.position.z) generateNew();
	}
}
