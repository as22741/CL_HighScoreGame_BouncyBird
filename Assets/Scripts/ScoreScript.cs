using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public Transform bird;
    private bool hasScored;

    void Update()
    {
        if (!GameManager.Instance.gameStarted || GameManager.Instance.gameEnd) //ensuring this works only during game running
            return;
        
        if (transform.position.x > bird.position.x) // The obstacle is ahead of the bird again, so it can score
        {
            hasScored = false;
            return;
        }

        if (!hasScored) //so once it crosses it scores!
        {
            hasScored = true;
            GameManager.Instance.AddPoint(); //this is now handled by game manager (previous iteration had it here)
        }
    }
}



