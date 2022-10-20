using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RadialLayout : LayoutGroup
{
    public float fDistance;
    [Range(0f, 360f)] public float minAngle, maxAngle, startAngle;

    protected override void OnEnable()
    {
        base.OnEnable();
        CalculateRadial();
    }

    public override void SetLayoutHorizontal()
    {
    }

    public override void SetLayoutVertical()
    {
    }

    public override void CalculateLayoutInputVertical()
    {
        CalculateRadial();
    }

    public override void CalculateLayoutInputHorizontal()
    {
        CalculateRadial();
    }
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        CalculateRadial();
    }
#endif
    void CalculateRadial()
    {
        m_Tracker.Clear();
        if (transform.childCount == 0)
        {
            return;
        }

        float fOffsetAngle = ((maxAngle - minAngle)) / (transform.childCount - 1);

        float fAngle = startAngle;
        for (int i = 0; i < transform.childCount; i++)
        {
            RectTransform child = (RectTransform) transform.GetChild(i);
            if (child != null)
            {
                m_Tracker.Add(this, child,
                    DrivenTransformProperties.Anchors |
                    DrivenTransformProperties.AnchoredPosition |
                    DrivenTransformProperties.Pivot);
                Vector3 vPos = new Vector3(Mathf.Cos(fAngle * Mathf.Deg2Rad), Mathf.Sin(fAngle * Mathf.Deg2Rad), 0);

                var newPos = vPos * fDistance;
                SetLocalPos(child, newPos);

                child.anchorMin = child.anchorMax = child.pivot = new Vector2(0.5f, 0.5f);
                fAngle += fOffsetAngle;
            }
        }
    }

    void SetLocalPos(RectTransform rt, Vector3 pos)
    {
        if (Application.isPlaying)
        {
            rt.transform.DOLocalMove(pos, 1f);
        }
        else
        {
            rt.localPosition = pos;
        }
    }
}