using Smarteye;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempController : MonoBehaviour
{
    public LineFlowQuestController _lineFlow;
    public Transform startPoint;
    public Transform endPoint;

    private void Start()
    {
        _lineFlow.CreateDashedLine(startPoint, endPoint, true);
    }
}
