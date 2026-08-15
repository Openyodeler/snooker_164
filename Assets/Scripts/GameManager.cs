using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;


public class GameManager : MonoBehaviour
{
    [SerializeField] private int playerScore;

    public int PlayerScore { get { return playerScore; } set { playerScore = value; } }
    public static GameManager instance;

    [SerializeField] private GameObject[] ballPositions;
    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private GameObject cueball;
    [SerializeField] private GameObject Line;
    [SerializeField] private GameObject cuestick;
    [SerializeField] private TMP_Text NotiText;



    private float xInput = 0f;
    private float Force = 0f;
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
        NotiText.text = ("");
    }
    private void Update()
    {
        if (Keyboard.current.upArrowKey.isPressed)
        {
            Force += 0.1f;
            if (Force > 50)
            {
                Force = 50;
            }
            Line.transform.localScale = new Vector3(0f, 0f, Force / 5);
        }
        else 
        {
            
            if (Force > 0)
            {
                Force -= 0.1f;
                
            }
            else
            {
                Force = 0f;
            }
            Line.transform.localScale = new Vector3(0f, 0f, Force / 5);
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ShotBall();
            Force = 0;
        }

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) 
        {
            xInput = -0.1f;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            xInput = 0.1f;
        }
        else
        {
            xInput = 0f;
        }
        RotateBall();
        if (Keyboard.current.backspaceKey.wasPressedThisFrame)
        {
            StopBall();
        }
    }
    private void SetBall(BallColor col,int i)
    {
        GameObject obj = Instantiate(ballPrefab,
                    ballPositions[i].transform.position,
                    Quaternion.identity);

        Ball b = obj.GetComponent<Ball>();
        b.colorthis(col);
    }

    private void ShotBall()
    {
        Rigidbody rb = cueball.GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * Force,ForceMode.Impulse);

        Line.SetActive(false);
        cuestick.SetActive(false);
    }

    private void RotateBall()
    {
        if (xInput >= 360 || xInput <= -360)
        {
            xInput = 0f;
        }

        if (cueball != null)
        {
            cueball.transform.Rotate(0f, xInput, 0f);
        }
    }

    private void StopBall()
    {
        Rigidbody rb = cueball.GetComponent <Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        cueball.transform.eulerAngles = new Vector3 (0f,0f,0f);

        Line.SetActive(true);
        cuestick.SetActive(true);
    }

    public void ShowNotiText(int i)
    {
        PlayerScore += i;
        NotiText.text = string.Format("Total Point : {0} \n Scored {1} Point",playerScore, i.ToString());
    }

    public void ShowString(string str)
    {
        NotiText.text = str;
    }
}
