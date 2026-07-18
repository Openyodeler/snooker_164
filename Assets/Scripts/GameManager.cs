using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int playerScore;

    public int PlayerScore { get { return playerScore; } set { playerScore = value; } }
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
