using UnityEngine;

public class Test : MonoBehaviour
{
    private int m = 0;
    private float timer = 0f;
    void Awake()
    {
        Debug.Log("Awake");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        m++;
        if (timer >= 0)
        {
            Debug.Log(m);
            timer = 0f;        }
    }
}
