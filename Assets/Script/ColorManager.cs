using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] private Material GroundMaterials;
    [SerializeField] private Color[] colors;
    [SerializeField][Range(0, 3)] private float LerpValue;
    [SerializeField] private float time;

    private int colorIndex = 0;
    private float CurrentTime;

    private void Update()
    {
        SetColorChangeTime();
        SetGroundMaterialColor();
    }
    private void SetColorChangeTime()
    {
        if (CurrentTime <= 0)
        {
            CurrentTime = time;
            CheckColorIndexValue();
        }
        else CurrentTime -= Time.deltaTime;
    }

    private void CheckColorIndexValue()
    {
        colorIndex++;
        if (colorIndex >= colors.Length) colorIndex = 0;
    }
    private void SetGroundMaterialColor()
    {
        GroundMaterials.color = Color.Lerp(GroundMaterials.color, colors[colorIndex], LerpValue * Time.deltaTime);
    }
    private void OnDestroy()
    {
        GroundMaterials.color = colors[0];
    }
}