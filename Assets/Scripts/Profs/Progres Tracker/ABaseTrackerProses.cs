using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Smarteye
{
    public abstract class ABaseTrackerProses : MonoBehaviour
    {
        public List<ProsesInformation> listProses;
        public UnityEvent onStartProses;
        public UnityEvent onFinishAllProses;

        [Header("Play Properties")]
        protected bool _hasStart = false;
        protected bool _hasHint = false;
        [SerializeField] protected float _timeLimit;
        [SerializeField] protected float _timeRemaining;

        public void StartProses()
        {
            _hasStart = true;
            _hasHint = false;
            _timeRemaining = _timeLimit;

            onStartProses?.Invoke();
        }

        private void Update()
        {
            if (_hasStart)
            {
                _timeRemaining -= Time.deltaTime;
                
                if (_timeRemaining <= 0)
                {
                    _hasStart = false;
                    _hasHint = true;
                    TellNextProses();
                }
            }
        }

        public void ClearProses(int index)
        {
            if (listProses[index] != null)
            {
                listProses[index].hasClear = true;
                OnFinishProses();
                CheckAllProses();

                if (_hasHint)
                {
                    TellNextProses();
                }
            }
            else
            {
                Debug.LogWarning("Tidak ada Proses di Index ini");
            }
        }

        public abstract void OnFinishProses();
        public abstract void TellNextProses();

        public abstract void OnFinsihAllProses();

        public bool CheckAllProses()
        {
            for (int i = 0; i < listProses.Count; i++)
            {
                if (!listProses[i].hasClear)
                {
                    return false;
                }
            }

            OnFinsihAllProses();
            onFinishAllProses.Invoke();
            _hasStart = false;
            _hasHint = false;
            return true;
        }

        public int GetCurrentProses()
        {
            for (int i = 0; i < listProses.Count; i++)
            {
                if (!listProses[i].hasClear)
                {
                    return i;
                }
            }

            return 0;
        }

        public string GetStringProsesOngoing()
        {
            string message = string.Empty;

            for (int i = 0; i < listProses.Count; i++)
            {
                if (!listProses[i].hasClear)
                {
                    message += listProses[i].descriptionProses + ", ";
                }
            }

            return message;
        }

        public void ResetAllProses()
        {
            foreach(ProsesInformation proses in listProses)
            {
                proses.hasClear = false;
            }
        }
    }
}

[System.Serializable]
public class ProsesInformation
{
    private bool _hasClear = false;
    public string descriptionProses;
    public UnityEvent _onFinishQuest;

    public bool hasClear
    {
        set
        {
            if (!_hasClear && value == true)
            {
                _onFinishQuest?.Invoke();
            }

            _hasClear = value;
        }
        get => _hasClear;
    }
}
