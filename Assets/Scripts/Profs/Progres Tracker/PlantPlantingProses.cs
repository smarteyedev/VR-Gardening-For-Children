using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Smarteye
{
    public class PlantPlantingProses : ABaseTrackerProses
    {
        [Header("Hint Component")]
        [SerializeField] private LineFlowQuestController _lineFlow;

        public struct HintProses
        {
            public Transform startTransform;
            public Transform targetTransform;
            public Transform transformHint;
            public string titleHint;
            public string descHint;
            public List<Sprite> gifImage;
        }

        [Header("Hint Properties")]
        [SerializeField] private List<HintProses> _prosesHint;
        [SerializeField] private GameObject _canvasHint;
        [SerializeField] private TextMeshProUGUI _textTitle;
        [SerializeField] private TextMeshProUGUI _textDesc;
        [SerializeField] private Image _imageHint;
        private float _frameRate = 1f / 30f;

        public void StartHint()
        {
            _hasHint = true;
            TellNextProses();
        }

        public override void OnFinishProses()
        {

        }

        public override void OnFinsihAllProses()
        {
            if (_hasHint)
            {
                _lineFlow.HideDashedLine();
            }
        }

        public override void TellNextProses()
        {
            int i = GetCurrentProses();
            _lineFlow.CreateDashedLine(_prosesHint[i].startTransform, _prosesHint[i].targetTransform, true);

            _canvasHint.SetActive(true);
            _textTitle.text = _prosesHint[i].titleHint;
            _textDesc.text = _prosesHint[i].descHint;
        }

        private IEnumerator PlayGIF()
        {
            List<Sprite> sprites = _prosesHint[GetCurrentProses()].gifImage;
            int index = 0;

            while (true)
            {
                _imageHint.sprite = sprites[index];
                index = (index + 1) % sprites.Count;

                yield return new WaitForSeconds(_frameRate);
            }
        }
    }


}
