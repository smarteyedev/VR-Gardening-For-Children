using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace Smarteye
{
    public class LineFlowQuestController : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private Transform _startPoint;
        private Transform _endPoint;
        public bool _hasFollowing = false;

        [Header("Materials Property")]
        private Material _lineMaterial;
        [SerializeField] private float _dashCount;
        [SerializeField] private float _dashSpeed;
        [SerializeField] private float _dashDevide;
        private float _distance = 0;


        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineMaterial = _lineRenderer.material;
            _lineRenderer.enabled = false;
        }

        public void CreateDashedLine(Transform startPosition, Transform endPosition, bool isFollowing)
        {
            _hasFollowing = isFollowing;
            _lineRenderer.enabled = true;
            _startPoint = startPosition;
            _endPoint = endPosition;

            SetLinePosition();
        }

        private void SetLinePosition()
        {
            _distance = Vector3.Distance(_startPoint.position, _endPoint.position);
            _dashCount = _distance / _dashDevide;
            _lineRenderer.material.SetFloat("_dashCount", _dashCount);
            _lineRenderer.material.SetFloat("_speed", _dashSpeed);

            _lineRenderer.SetPosition(0, _startPoint.position);
            _lineRenderer.SetPosition(1, _endPoint.position);
        }

        public void HideDashedLine()
        {
            _hasFollowing = false;
            _lineRenderer.enabled = false;
        }

        private void Update()
        {
            if (!_hasFollowing)
                return;

            if (_startPoint == null && _endPoint == null)
                return;


            SetLinePosition();
        }
    }
}

