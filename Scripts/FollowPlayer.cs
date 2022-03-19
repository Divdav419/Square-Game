using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	//Map player position to smaller interval
	private float map(float originMin, float originMax, float val, float newMin, float newMax)
	{
		float originRange = originMax - originMin;
		float newRange = newMax - newMin;

		float mapped = (val - originMin) / originRange;
		float newMapped = (mapped * newRange) + newMin;
		return newMapped;

	}

	public Transform player;
	public Transform ground;
	public float flexibility = 0.5f;
	public PlayerMovement movement;
	public float offset;
	// Update is called once per frame
	void Update()
	{
		if (!movement.gameOver) {
			float groundMin = ((float)ground.position.x + 1f)-((float)ground.localScale.x/2f + 0.5f);
			float flexMin = ((float)ground.position.x + 1f)-((float)flexibility/2f + 0.5f);
			transform.position = new Vector3 (map(groundMin, groundMin+ground.localScale.x-1, player.position.x, flexMin, flexMin+flexibility-1), 2.5f, player.position.z - offset);
		} 
	}
}
