using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Points : MonoBehaviour
{
    public TMP_Text p1PointsText;
    public TMP_Text p2PointsText;

    private void OnEnable()
    {
        PointCollision.OnPointMade += AttTexts;
    }
    private void OnDisable()
    {
        PointCollision.OnPointMade -= AttTexts;
    }

    void AttTexts(int points1, int points2)
    {
        p1PointsText.text = points1.ToString();
        p2PointsText.text = points2.ToString();
    }
}
