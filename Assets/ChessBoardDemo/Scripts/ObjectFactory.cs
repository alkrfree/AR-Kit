using UnityEngine;

namespace ChessBoardDemo.Scripts
{
    public class ObjectFactory : MonoBehaviour
    {
        [SerializeField] private Marker _markerPrefab;

        private Marker _markerInstance;

        public void SpawnMarker(Vector3 pos)
        {
            if (_markerInstance != null)
                return;
            _markerInstance = Instantiate(_markerPrefab, pos, Quaternion.identity, transform);
        }


        public Marker GetMarker()
        {
            return _markerInstance;
        }
    }
}