using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //had to look this up, this is necessary for GM to be referenced
    [SerializeField] public TMP_Text scoreText;
    [SerializeField] public TMP_Text clickToStartText;
    [SerializeField] public TMP_Text ggText;
    [SerializeField] public TMP_Text highScore1Text;
	[SerializeField] public TMP_Text highScore2Text;
	[SerializeField] public TMP_Text highScore3Text;
	[SerializeField] public TMP_Text highScore4Text;
	[SerializeField] public TMP_Text highScore5Text;

    
    int score;
    int highScore1;
	int highScore2; //I should be making an array here, but it's 3am and I'm scared I'll ruin everything
	int highScore3;
	int highScore4;
	int highScore5;
    
    public BirdFlap bird;
    public ObstacleBehaviour[] obstacles; //initially I did not have an array, god that was a bad idea
    
    private InputAction leftClick;
    
    public bool gameEnd;
    public bool gameStarted;
    
    private bool GamePlaying => gameStarted && !gameEnd; //which basically means evaluates to true
                                                        // when the game has started but not ended;
    
    void Awake()
    {
        Instance = this; //"singleton pattern". this makes this a single publically usable component
        gameStarted = false;
        gameEnd = false;
        highScore1 = 0;
		highScore2 = 0;
		highScore3 = 0;
		highScore4 = 0;
		highScore5 = 0;
        highScore1Text.text = highScore1.ToString();
		highScore2Text.text = highScore2.ToString(); //I realised this should be highscore1 because its the second one but too late
		highScore3Text.text = highScore3.ToString();	
		highScore4Text.text = highScore4.ToString();
		highScore5Text.text = highScore5.ToString();
        scoreText.text = "0";
        
        clickToStartText.gameObject.SetActive(true); //right now
        ggText.gameObject.SetActive(false); //for end screen
        
        leftClick = new InputAction("Left Click", InputActionType.Button, "<Mouse>/leftButton"); //copied this line
    }
    
    void OnEnable()
    {
        leftClick.Enable();
    }

    void OnDisable()
    {
        leftClick.Disable(); //yup, straight up copied this method. unnecessary but useful?
    }
    
    void Update()
    {
        if (!leftClick.WasPressedThisFrame()) //state will only change on click
        {
            return; //as usual return being super helpful
                    //its honestly a much easier if statement in many situations
        }
        
        if (gameEnd)  // End state. click to return to the start state
        {
            gameEnd = false; //just needs to be true for one frame. now we can pass through the next return
            gameStarted = false;
            
            ResetObjects(); 

            clickToStartText.gameObject.SetActive(true); //"Click to start"
            ggText.gameObject.SetActive(false);
            
            return;
        }
        
        if (!gameStarted) // Start state. click to begin the game
                            // !function has also been very helpful
        {
            StartGame();
            return;
        }
        
        bird.Flap();  // Game state. click to flap. everything else is non existent
    }

    public void StartGame()
    {
        score = 0;
        scoreText.text = score.ToString();

        ResetObjects();
        gameStarted = true;
        clickToStartText.gameObject.SetActive(false);
        return;
    }

    public void AddPoint()
    {
        if (!GamePlaying)
            return; //only get points when game is running

        score++;
        scoreText.text = score.ToString();
     }
//big chunk of high score text below!:

	void Leaderboard(int score)
       {
    		if (score > highScore1)
    		{
        		highScore5 = highScore4;
       			highScore4 = highScore3;
        		highScore3 = highScore2;
       			highScore2 = highScore1;
       			highScore1 = score;
   		 	}
   		 	else if (score > highScore2)
   		 	{
      		  	highScore5 = highScore4;
        		highScore4 = highScore3;
        		highScore3 = highScore2;
       			highScore2 = score;
   		 	}		
    		else if (score > highScore3)
    		{
        		highScore5 = highScore4;
        		highScore4 = highScore3;
        		highScore3 = score;
    		}
    		else if (score > highScore4)
    		{
        		highScore5 = highScore4;
        		highScore4 = score;
    		}
    		else if (score > highScore5)
    		{
        		highScore5 = score;
    		}
    				highScore1Text.text = highScore1.ToString();
    				highScore2Text.text = highScore2.ToString();
   					highScore3Text.text = highScore3.ToString();
   					highScore4Text.text = highScore4.ToString();
   					highScore5Text.text = highScore5.ToString();
		}
    

    public void EndGame()
    {
        if (GamePlaying)
        {
            gameEnd = true;
            clickToStartText.gameObject.SetActive(false); 
            ggText.gameObject.SetActive(true); //"gg, you lost"
			Leaderboard(score);
        }
    }

    void ResetObjects()
    {
        bird.ResetBird();

        foreach (ObstacleBehaviour obstacle in obstacles) //I really forgot this was an option I had to look this up as well
        {
            obstacle.ResetObstacle();
        }
    }
}

