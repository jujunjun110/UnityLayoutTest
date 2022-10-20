using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CircularParent : MonoBehaviour
{
    [SerializeField] private bool isReversed;
    [SerializeField] [Range(0, 1000)] private float radius;
    [SerializeField] [Range(0, 360)] private float startAngle;
    [SerializeField] [Range(1, 360)] private float expandAngle;

    private void OnEnable()
    {
        SetPositions();
    }

    private void OnValidate()
    {
        SetPositions();
    }

    private float DegToRad(float deg)
    {
        return (float) Math.PI * deg / 180;
    }

    private Vector2 RadToVec2(float rad)
    {
        return new Vector2(
            (float) Math.Cos(rad),
            (float) Math.Sin(rad)
        );
    }

    private List<RectTransform> GetChildRectTransforms()
    {
        return GetComponentsInChildren<RectTransform>().Where(c => c.gameObject != gameObject).ToList();
    }

    private void SetPositions()
    {
        var targets = GetChildRectTransforms();
        int targetCount = targets.Count;

        if (targetCount <= 1) return;

        float dir = isReversed ? -1 : 1;
        float angleUnit = DegToRad(expandAngle) / (targetCount - 1) * dir;
        float offset = DegToRad(startAngle);

        var angles = Enumerable.Range(0, targetCount).Select(i => (angleUnit * i) + offset);
        var positions = angles.Select(angle => RadToVec2(angle) * radius);
        var objPosPairs = targets.Zip(positions, (obj, pos) => new {obj, pos});

        foreach (var pair in objPosPairs)
        {
            pair.obj.localPosition = pair.pos;
        }
    }
}