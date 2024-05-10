using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PathCreation.Examples {
    [RequireComponent(typeof(PathCreator))]
    public class GeneratePathExample : MonoBehaviour {

        [SerializeField] private bool _closedLoop = true;
        [SerializeField] private int _waypointsCount;
        [SerializeField] private float _minDistance;
        [SerializeField] private float _maxDistance;
        [SerializeField] private Slider slider;

        private Transform[] _waypoints;
        private RoadMeshCreator _roadMeshCreator;


        private void Start()
        {
            slider.onValueChanged.AddListener((float val) => ChangeValue(val));
            _roadMeshCreator = GetComponent<RoadMeshCreator>();
        }

        private void ChangeValue(float newValue)
        {
            _waypointsCount = (int)(20 + newValue * 30);
        }

        public void GenerateNewPath()
        {
            StartCoroutine(GenerateNewPath_Coroutine());
        }

        private IEnumerator GenerateNewPath_Coroutine()
        {
            yield return null;

            if (_waypoints != null)
            {
                foreach (var point in _waypoints)
                {
                    Destroy(point.gameObject);
                }
            }

            _waypoints = new Transform[_waypointsCount];
            var step = 720 / _waypointsCount;

            for (int i = _waypointsCount - 1; i >= 0; i--)
            {
                var randomDistance = Random.Range(_minDistance, _maxDistance);
                Quaternion newRotation = Quaternion.Euler(i * step, 0, 0);
                var randPosition = new Vector3(newRotation.x * randomDistance, 0, newRotation.w * randomDistance);

                Transform trans = new GameObject().transform;
                trans.position = randPosition;
                _waypoints[i] = trans;
            }

            BezierPath bezierPath = new(_waypoints, _closedLoop, PathSpace.xz);
            GetComponent<PathCreator>().bezierPath = bezierPath;
            _roadMeshCreator.TriggerUpdate();
        }
    }
}