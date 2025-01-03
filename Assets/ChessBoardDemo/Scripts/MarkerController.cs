using UnityEngine;

namespace ChessBoardDemo.Scripts
{
    public class MarkerController : MonoBehaviour
    {
        private const string WaitingForMarker = "Waiting for Marker";
        private const string PleaseClickThePlaneToInstantiateTheMarker = "Please click the plane to instantiate the marker";
        private const string LoadingGeometry = "Loading geometry";
        private const string PleaseWaitWhileGeometryIsBeingLoaded = "Please wait while geometry is being loaded";
        
        
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private MarkerStateUI _markerStateUI;
        [SerializeField] private ObjectFactory _objectFactory;


        private void Start()
        {
            ChangeMarkerTextBeforeCreation();
            
            _inputManager.MarkerCreated += ChangeMarkerTextAfterCreation;
            _inputManager.MarkerCreated += AddListenersToMarker;
        }

        private void AddListenersToMarker() => 
            _objectFactory.GetMarker().DataLoaded += ChangeMarkerTextAfterDataLoaded;

        private void ChangeMarkerTextBeforeCreation()
        {
            _markerStateUI.ChangeStateText(WaitingForMarker);
            _markerStateUI.ChangeClickText(PleaseClickThePlaneToInstantiateTheMarker);
         
        }
        
        private void ChangeMarkerTextAfterCreation()
        {
            _markerStateUI.ChangeStateText(LoadingGeometry);
            _markerStateUI.ChangeClickText(PleaseWaitWhileGeometryIsBeingLoaded);
        }
        
        private void ChangeMarkerTextAfterDataLoaded()
        {
            _markerStateUI.ChangeStateText("");
            _markerStateUI.ChangeClickText("");
        }
    }
}