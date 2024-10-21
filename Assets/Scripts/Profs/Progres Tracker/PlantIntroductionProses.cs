using Smarteye;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Smarteye
{
    public class PlantIntroductionProses : ABaseTrackerProses
    {
        [SerializeField] private LineFlowQuestController _lineFlow;

        [SerializeField] private Transform _controllerOriginPoint;
        [SerializeField] private List<Transform> _hintQuestTransform;

        public override void OnFinishProses()
        {
            Debug.Log("Finish Proses");
        }

        public override void OnFinsihAllProses()
        {
            if (_hasHint)
            {
                _lineFlow.HideDashedLine();
                Debug.Log("Has Hide");
            }
            else
            {
                Debug.Log("No Hide");
            }
        }

        public override void TellNextProses()
        {
            _lineFlow.CreateDashedLine(_controllerOriginPoint, _hintQuestTransform[GetCurrentProses()], true);
        }


    }
}
