using Smarteye;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempController : MonoBehaviour
{
    public ABaseTrackerProses proses; 
    public LineFlowQuestController _lineFlow;
    public Transform startPoint;
    public Transform endPoint;

    private void Start()
    {
        proses.StartProses();
    }

    public void DebuggingString()
    {
        Debug.Log("Debugging");
    }


}
