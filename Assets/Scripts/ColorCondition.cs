using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCondition : MonoBehaviour,ICheckable
{
    public Color conditionColor;
    public static Color[] currentColors = { Color.red, Color.blue, Color.green };
    public Dictionary<Color, string> colors = new Dictionary<Color, string>() { {Color.red,"Red" },{Color.blue,"Blue" },{Color.green,"Green" } };

    public ColorCondition()
    {
        conditionColor = currentColors[Random.Range(0, currentColors.Length)];
    }

    public bool CheckCondition(ID id)
    {
        if(id.bodyColor == conditionColor)
        {
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return "-Accept Outfit with " + GetColorName(conditionColor);
    }

    public string GetColorName(Color color)
    {
        return colors[color];
    }
}
