using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class BirdFlap : MonoBehaviour
{
    public float flapHeight = 4f;
    Rigidbody2D bird; //identify its own body
    Vector3 startPosition; 

    void Start()
    {
		bird = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }

    public void Flap()
    {
        bird.linearVelocity = Vector2.up * flapHeight; //bird flaps upwards. 
													   // Vector2.up/down/etc will put 3rd direction as 0 automatically
        
    }

    public void ResetBird()
    {
        //bird.simulated = true; //didn't know this was a thing
        transform.position = startPosition;
        bird.linearVelocity = Vector2.zero; //(0f, 0f, 0f) but for a Vector2;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        bird.linearVelocity = Vector2.zero;
        //bird.simulated = false;

        GameManager.Instance.EndGame();
    }
}
