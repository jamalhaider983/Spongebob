using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class LinkEditor : MonoBehaviour
{
    [SerializeField] private NavMeshLink link;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;

    [ContextMenu("Setup Link")]
    private void SetupLink()
    {
        link.startPoint = point1.transform.localPosition;
        link.endPoint = point2.transform.localPosition;
    }
}
