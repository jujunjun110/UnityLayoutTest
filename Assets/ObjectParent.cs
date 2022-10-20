using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectParent : MonoBehaviour
{
    [SerializeField] [Range(0, 1)] private float progress;
    [SerializeField] private RectTransform verObj;
    [SerializeField] private RectTransform horObj;

    private void OnValidate()
    {
        SetPositions();
    }

    private void SetPositions()
    {
        verObj.localPosition = new Vector3(100, 100 * progress, 0);
        horObj.localPosition = new Vector3(100 * progress, 100, 0);
    }
}