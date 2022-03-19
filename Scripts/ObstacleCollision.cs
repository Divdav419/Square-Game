using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
	public PlayerMovement player;
	public Rigidbody obstacles;
	public Rigidbody cube;

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.tag == "Obstacle") {
			obstacles.isKinematic = false;
			cube.freezeRotation = false;
			player.gameOver = true;
		}
	}
}
