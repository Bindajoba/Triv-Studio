using Extensions;
using UnityEngine;

namespace Levels
{
    public sealed class DestroySegment : MonoBehaviour
    {
        [SerializeField] private float _segmentWidth;

        private float _halfCameraWidth;
        private bool _destroyed;

        private void Awake()
        {
            _halfCameraWidth = Camera.main.GetDimensions().Width / 2;
        }

        private void Update()
        {
            if (_destroyed)
            {
                return;
            }

            if (!CanDestroySegment())
            {
                return;
            }

            Destroy(gameObject);
            _destroyed = true;
        }

        private bool CanDestroySegment()
        {
            var x1 = Camera.main.transform.position.x - _halfCameraWidth;
            var x2 = gameObject.transform.position.x;

            return x2 < x1 && Mathf.Abs(x1 - x2) > _segmentWidth;
        }
    }
}