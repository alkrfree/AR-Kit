using UnityEngine;
using UnityEngine.Serialization;

namespace ChessBoardDemo.Scripts
{
    public class ObjectFactory : MonoBehaviour
    {
        [FormerlySerializedAs("_markerPrefab")] [SerializeField] private MarkerDataLoader markerDataLoaderPrefab;

        private MarkerDataLoader _markerDataLoaderInstance;

        public void SpawnMarker(Vector3 pos)
        {
            if (_markerDataLoaderInstance != null)
                return;
            _markerDataLoaderInstance = Instantiate(markerDataLoaderPrefab, pos, Quaternion.identity, transform);
        }


        public MarkerDataLoader GetMarker()
        {
            return _markerDataLoaderInstance;
        }
    }
}