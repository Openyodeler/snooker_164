using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int playerScore;

    public int PlayerScore { get { return playerScore; } set { playerScore = value; } }
    public static GameManager instance;

    [SerializeField] private GameObject[] ballPositions;
    [SerializeField] private GameObject ballPrefab;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        int i = 0;
        foreach (var ball in ballPositions)
        {   
            SetBall((BallColor)i, i);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetBall(BallColor col,int i)
    {
        GameObject obj = Instantiate(ballPrefab,
                    ballPositions[i].transform.position,
                    Quaternion.identity);

        Ball b = obj.GetComponent<Ball>();
        b.colorthis(col);
    }
}
