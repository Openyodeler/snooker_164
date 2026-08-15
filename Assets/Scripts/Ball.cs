using System;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.EventSystems;

public enum BallColor
{
    White,
    Red,
    Yellow,
    Green,
    Brown,
    Blue,
    Pink,
    Black
}

public class Ball : MonoBehaviour , IPointerClickHandler
{
    [SerializeField] private BallColor color;
    [SerializeField] private int point;
    public int Point {  get { return point; }  set { point = value; } }
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material[] ballMaterials;


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(point);
        GameManager.instance.PlayerScore += point;
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        point = (int)color;
        if (meshRenderer != null && ballMaterials != null)
        {
            int index = (int)color;

            if (index >= 0 && index < ballMaterials.Length && ballMaterials[index] != null)
            {
                meshRenderer.sharedMaterial = ballMaterials[index];
            }

        }

    }

    public void colorthis(BallColor ballColor)
    {   
        color = ballColor;
        point = (int)color;
        if (meshRenderer != null && ballMaterials != null)
        {
            int index = (int)color;

            if (index >= 0 && index < ballMaterials.Length && ballMaterials[index] != null)
            {
                meshRenderer.sharedMaterial = ballMaterials[index];
            }

        }
    }
}
