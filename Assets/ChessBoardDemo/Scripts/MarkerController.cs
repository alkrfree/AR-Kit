using UnityEngine;

namespace ChessBoardDemo.Scripts
{
  public class MarkerController : MonoBehaviour
  {
    private const string WaitingForMarker = "Waiting for Marker";
    private const string PleaseClickThePlaneToInstantiateTheMarker = "Please click the plane to instantiate the marker";
    private const string LoadingGeometry = "Loading geometry";
    private const string PleaseWaitWhileGeometryIsBeingLoaded = "Please wait while geometry is being loaded";

    private const string DataCantBeLoaded = "Data can't be loaded";
    private const string Sorry = "I'm so sorry";


    [SerializeField] private InputManager _inputManager;
    [SerializeField] private MarkerStateUI _markerStateUI;
    [SerializeField] private ObjectFactory _objectFactory;


    private void Start()
    {
      ChangeMarkerTextBeforeCreation();

      _inputManager.MarkerCreated += AfterMarkerCreated;
    }


    private void AfterMarkerCreated()
    {
      ChangeMarkerTextAfterCreation();
      AddListenersToMarker();

      _inputManager.MarkerCreated -= AfterMarkerCreated;
    }

    private void AddListenersToMarker()
    {
      _objectFactory.GetMarker().DataLoaded += ChangeMarkerTextAfterDataLoaded;
      _objectFactory.GetMarker().DataCantBeLoaded += ChangeTextIfDataCantBeLoaded;
    }

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

    private void ChangeTextIfDataCantBeLoaded()
    {
      _markerStateUI.ChangeStateText(DataCantBeLoaded);
      _markerStateUI.ChangeClickText(Sorry);
    }

    private void ChangeMarkerTextAfterDataLoaded()
    {
      _markerStateUI.ChangeStateText("");
      _markerStateUI.ChangeClickText("");
    }
  }
}