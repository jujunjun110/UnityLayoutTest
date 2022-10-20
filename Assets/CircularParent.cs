using System;
using System.Linq;
using UnityEngine;

public class CircularParent : MonoBehaviour
{
    [SerializeField] private bool isReversed;
    [SerializeField] [Range(0, 1000)] private float radius;
    [SerializeField] [Range(0, 360)] private float startAngle;
    [SerializeField] [Range(1, 360)] private float expandAngle;

    private void OnValidate()
    {
        SetPositions();
    }

    private float DegToRad(float deg)
    {
        return (float) Math.PI * deg / 180;
    }

    private Vector2 RadToPos(float rad)
    {
        return new Vector2(
            (float) Math.Cos(rad),
            (float) Math.Sin(rad)
        );
    }

    private void SetPositions()
    {
        var children = GetComponentsInChildren<RectTransform>().ToList();
        int childCount = children.Count;

        if (childCount <= 1) return;

        float dir = isReversed ? -1 : 1;
        float angleUnit = DegToRad(expandAngle) / (childCount - 1) * dir;
        float offset = DegToRad(startAngle);

        var angles = Enumerable.Range(0, childCount).Select(i => (angleUnit * i) + offset);
        var positions = angles.Select(angle => RadToPos(angle) * radius);

        var objPosPairs = children.Zip(positions, (child, pos) => new {child, pos});

        foreach (var pair in objPosPairs)
        {
            pair.child.localPosition = pair.pos;
        }
    }
}