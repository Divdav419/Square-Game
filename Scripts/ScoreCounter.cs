using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
	public PlayerMovement gameState;
	public Transform player;
	public MapGeneration generator;
	public Text scoreText;
	public long score = 0;
	float pos = 0;
	float next;

    void Start()
    {
		pos = player.position.z;
		next = generator.startSpace + pos;
    }

    // Update is called once per frame
    void Update()
	{
		if (player.position.z > next && !gameState.gameOver) // If passed next obstacle and still playing
		{
			next += generator.collumnSpace;
			score += 1;
		}
		scoreText.text = score.ToString();
	}
}
