using Unity.VisualScripting;
using UnityEngine;

public class Hole : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Ball b = other.GetComponent<Ball>();

        if (b == null)
            return;
        if (b.Point == 0)
        {
            GameManager.instance.ShowString("Game OVer");
            Time.timeScale = 0;
            return;
        }
        GameManager.instance.ShowNotiText(b.Point);
        Destroy (b.gameObject);
    }
}
