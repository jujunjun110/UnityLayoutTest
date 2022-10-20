using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class CircularParent : MonoBehaviour
{
    [SerializeField] [Range(0, 1000)] private float radius;
    [SerializeField] [Range(0, 360)] private float startAngle;
    [SerializeField] [Range(1, 360)] private float expandAngle;

    private void OnValidate()
    {
        SetPositions();
    }

    private List<RectTransform> GetAllChildren()
    {
        return GetComponentsInChildren<RectTransform>().ToList();
    }


    private void SetPositions()
    {
        var children = GetAllChildren();
        int childCount = children.Count;

        if (childCount <= 1)
        {
            return;
        }

        float DegToRad(float deg) => ((float) Math.PI * deg / 180);

        float angleUnit = DegToRad(expandAngle) / (childCount - 1);
        float offset = DegToRad(startAngle);

        var angles = Enumerable.Range(0, childCount).Select(i => (angleUnit * i) + offset);

        var positions = angles.Select(angle => new Vector2(
            (float) Math.Cos(angle),
            (float) Math.Sin(angle)
        ) * radius);

        var pairs = children.Zip(positions, (child, pos) => new {child, pos});
        foreach (var pair in pairs)
        {
            pair.child.localPosition = pair.pos;
        }
    }
}