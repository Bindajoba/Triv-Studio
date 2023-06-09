using System.Collections.Generic;
using Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Levels
{
    /// <summary>
    /// Generates a level with random level segments.
    /// </summary>
    public sealed class LevelGenerator : MonoBehaviour
    {
        [SerializeField]
        private GameObject _startSegment;

        [SerializeField]
        private Vector3 _startSegmentPosition;

        [Tooltip("List of segments that get randomly spawned")]
        [SerializeField] 
        private List<GameObject> _segments = new();

        [Tooltip("Visible gap between two spawned segments")]
        [SerializeField]
        private float _segmentXOffset;

        private Vector3 _lastSpawnPosition;
        private float _halfCameraWidth;
        private float _currentSegmentWidth;

        private void Awake()
        {
            _lastSpawnPosition = _startSegmentPosition;
            _halfCameraWidth = Camera.main.GetDimensions().Width / 2;
        }
        
        private void Start()
        {
            InitializeSegments();
        }
        
        private void Update()
        {
            if (CanSpawnRandomSegment())
            {
                SpawnRandomSegment();
            }
        }

        private void InitializeSegments()
        {
            Instantiate(_startSegment, _lastSpawnPosition, Quaternion.identity);
            _currentSegmentWidth = _startSegment.GetComponent<SegmentDescriptor>().SegmentWidth;

            while (_lastSpawnPosition.x <= _halfCameraWidth)
            {
               SpawnRandomSegment();
            }
        }

        private bool CanSpawnRandomSegment()
        {
            var x1 = Camera.main.transform.position.x + _halfCameraWidth;
            var x2 = _lastSpawnPosition.x;

            return Mathf.Abs(x2 - x1) < _currentSegmentWidth;
        }

        private void SpawnRandomSegment()
        {
            var index = Random.Range(0, _segments.Count);
            var segment = _segments[index];
            SpawnSegment(segment);
        }

        private void SpawnSegment(GameObject segment)
        {
            _lastSpawnPosition += new Vector3(_currentSegmentWidth + _segmentXOffset, 0, 0);
            Instantiate(segment, _lastSpawnPosition, Quaternion.identity);
            _currentSegmentWidth = segment.GetComponent<SegmentDescriptor>().SegmentWidth;
        }
    }
}
