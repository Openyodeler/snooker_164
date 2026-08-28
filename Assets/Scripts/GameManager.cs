using System.Collections;
using System.Threading;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class GameManager : MonoBehaviour
{   
    enum state
    {
        idle,
        lining,
        shoted
    }
    public enum GameState
    {
        Default,
        Win,
        Lose
    }
    [SerializeField] private int playerScore;

    public int PlayerScore { get { return playerScore; } set { playerScore = value; } }
    public static GameManager instance;
    public GameState gamestate = GameState.Default;

    [SerializeField] private GameObject[] ballPositions;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private state State = state.idle;
    [SerializeField] private GameObject cueball;
    [SerializeField] private GameObject Line;
    [SerializeField] private GameObject cuestick;
    [SerializeField] private TMP_Text NotiText;



    private float xInput = 0f;
    [SerializeField] private float Force = 0f;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        int i = 0;
        foreach (var ball in ballPositions)
        {
            SetBall((BallColor)i+1, i);
            i++;
        }
        NotiText.text = ("");
    }
    private void Update()
    {
            if (Keyboard.current.upArrowKey.isPressed && State != state.shoted)
            {
                State = state.lining;
                Force += 0.1f;
                if (Force > 50)
                {
                    Force = 50;
                }
                Line.transform.localScale = new Vector3(0f, 0f, Force / 5);

                cuestick.transform.localPosition = new Vector3(
                cuestick.transform.localPosition.x,
                cuestick.transform.localPosition.y,
                -8f - (Force / 25f)
            );
            }
            else if (State != state.shoted)
            {
                State = state.idle;
                if (Force > 0)
                {
                    Force -= 0.1f;

                }
                else
                {
                    Force = 0f;
                }
                Line.transform.localScale = new Vector3(0f, 0f, Force / 5);
                cuestick.transform.localPosition = new Vector3(
                cuestick.transform.localPosition.x,
                cuestick.transform.localPosition.y,
                -8f - (Force / 25f));
            }
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                State = state.shoted;
                ShotBall();
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
            if (playerScore >= 20)
            {
                gamestate = GameState.Win;
                ShowString("You have Beated the Game \npress r to restart");
            }


            if (Keyboard.current.rKey.wasPressedThisFrame &&
                (gamestate == GameState.Win || gamestate == GameState.Lose))
            {   
                Time.timeScale = 1f;
                ReplayGame();
            }
    }
    private void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        cuestick.transform.localPosition = new Vector3(
            cuestick.transform.localPosition.x,
            cuestick.transform.localPosition.y,
            -7f
        );

        Line.SetActive(false);

        Invoke(nameof(ApplyForce), 0.1f);
    }

    private void ApplyForce()
    {
        Rigidbody rb = cueball.GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * Force, ForceMode.Impulse);
        Force = 0;

        cuestick.SetActive(false);

        Invoke(nameof(CheckBallStopped), 0.1f);
    }
    private void CheckBallStopped()
    {
        Rigidbody rb = cueball.GetComponent<Rigidbody>();

        if (rb.linearVelocity.magnitude < 0.01f && State == state.shoted)
        {
            StopBall();
        }
        else
        {
            Invoke(nameof(CheckBallStopped), 0.1f);
        }
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
        State = state.idle;
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
