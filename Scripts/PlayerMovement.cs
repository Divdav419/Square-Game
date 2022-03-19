using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

	public Rigidbody rb;
	public Transform player;
	public float forwardForce = 1000f;
	public float endForce = 10000f;
	public float sideForce = 500f;
	public bool gameOver = false;
	public float upForce = 150;
	public BoxCollider ground;
	public bool started = false;
	public float speed;
	public float endWait;

	IEnumerator waiter()
    {
		yield return new WaitForSeconds(endWait);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (started) {
			if (gameOver) {
				if (transform.position.y >= 1) {
					ground.material.dynamicFriction = 1;
					ground.material.staticFriction = 1;
					rb.AddForce(0, upForce, endForce);
					upForce = 0;
					endForce = 0;
				}
					waiter();
					FindObjectOfType<MapGeneration>().endGame();
			} else {
				rb.AddForce(0, 0, forwardForce*Time.deltaTime);
				if (rb.velocity[2] >= speed) rb.velocity = new Vector3(rb.velocity[0], rb.velocity[1], speed);
				if (Input.GetKey("d") || Input.GetKey("right")) {
					rb.AddForce(sideForce * Time.deltaTime, 0, 0);
				}
				if (Input.GetKey("a") || Input.GetKey("left")) {
					rb.AddForce(-sideForce * Time.deltaTime, 0, 0);
				}/*
				if (Input.GetKey("w") || Input.GetKey("up")) {
					rb.AddForce(0, 0, forwardForce*Time.deltaTime);
				}*/

				if (transform.position.y < 0) gameOver = true;
			}
		}
	}
}
