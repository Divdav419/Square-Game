using UnityEngine;

public class FollowPlayerGround : MonoBehaviour
{
	public Transform player;
	public PlayerMovement movement;
	public FollowPlayer cam;

	// Update is called once per frame
	void Update()
	{
		if (!movement.gameOver) transform.position = new Vector3(0, 0, (player.position.z + (transform.localScale.z/2f) - cam.offset));
	}
}
