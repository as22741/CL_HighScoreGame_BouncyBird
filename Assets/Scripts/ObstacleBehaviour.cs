using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 startPosition; //this is for the loop only
	private Vector3 realStartPosition; //this is for the game reset
    private float startingSpeed;
    
	void Awake() //because this script would be called again?
    {
        startPosition = new Vector3 (12f, -6f, 0f);
        startingSpeed = speed;
		realStartPosition = transform.position;
    }
    
    void Update()
    {
 		if (!GameManager.Instance.gameStarted || GameManager.Instance.gameEnd) //because otherwise Game End -> Game Start allows for this to slip through
		{
            return;
		}

        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= -14f)
         {
			transform.position = startPosition;
		 }
       
		 speed += 0.1f* Time.deltaTime;
    }

    public void ResetObstacle()
    {
        transform.position = realStartPosition;
        speed = startingSpeed;
    }
} 


