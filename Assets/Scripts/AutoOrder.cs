using UnityEngine;
using System.Collections;

public class AutoOrder : MonoBehaviour
{

    public bool IAmPivot = false;
    public Transform OtherPivot = null;

    Transform pivot;
    Renderer orderedRenderer;
    int startOrder;

    void Awake()
    {
        if (OtherPivot != null)
            pivot = OtherPivot;
        else if (IAmPivot)
            pivot = transform;
        else if (GetComponentInChildren<AutoOrderPivot>() != null)
            pivot = GetComponentInChildren<AutoOrderPivot>().transform;
        else
            pivot = GetComponentInParent<AutoOrderPivot>().transform;

        orderedRenderer = GetComponent<Renderer>();
        startOrder = orderedRenderer.sortingOrder + 500;
    }

    void Update()
    {
        int baseOrder = (int)(-pivot.transform.position.y * 1000);
        orderedRenderer.sortingOrder = baseOrder + startOrder;
    }

}
