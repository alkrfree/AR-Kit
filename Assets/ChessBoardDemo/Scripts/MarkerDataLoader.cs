using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GLTFast;
using Newtonsoft.Json.Linq;
using UnityEngine;


namespace ChessBoardDemo.Scripts
{
  public class MarkerDataLoader : MonoBehaviour
  {
    private const string URL =
      "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/d7a3cc8e51d7c573771ae77a57f16b0662a905c6/2.0/ABeautifulGame/glTF/ABeautifulGame.gltf";

    public event Action DataLoaded;
    public event Action DataCantBeLoaded;

    [SerializeField] private Transform SpawnPoint;

    private GltfImport gltf;
    private string _jsonText;


    private async void Start()
    {
      try
      {
        await DownloadJsonAsync();
        await DownloadGltf();
        LoadingDone();
      }
      catch (Exception ex)
      {
        DataCantBeLoaded?.Invoke();
      }
    }

    private void LoadingDone()
    {
      Debug.Log("IsDone");
    }

    private async Task DownloadJsonAsync()
    {
      try
      {
        using (HttpClient client = new HttpClient())
        {
          string jsonText = await client.GetStringAsync(URL);
          Debug.Log("JSON downloaded successfully.");
          _jsonText = jsonText;
          ProcessJson(jsonText);
        }
      }
      catch (Exception ex)
      {
        Debug.LogError($"Error in DownloadJsonAsync: {ex.Message}");
        throw;
      }
    }

    private async Task DownloadGltf()
    {
      try
      {
        GltfImport gltfImport = new GltfImport();
        bool success = await gltfImport.LoadGltfJson(_jsonText, new Uri(URL));
        Debug.Log("GLTF downloaded successfully.");
        await SpawnGeometry(success, gltfImport);
      }
      catch (Exception ex)
      {
        Debug.LogError($"Error in DownloadJsonAsync: {ex.Message}");
        throw;
      }
    }

    private async Task SpawnGeometry(bool success, GltfImport gltfImport)
    {
      if (success)
      {
        await gltfImport.InstantiateMainSceneAsync(SpawnPoint);
        DataLoaded?.Invoke();
      }
    }

    private void ProcessJson(string jsonText)
    {
      JObject jsonData = JObject.Parse(jsonText);
      string nodeName = "King_B";
      JToken foundNode = FindNodeByName(jsonData["nodes"], nodeName);

      if (foundNode != null)
      {
        Debug.Log($"Object '{nodeName}' found: {foundNode}");
        var translationArray = foundNode["translation"];
        if (translationArray != null)
        {
          Vector3 shift = ShiftSpawnPoint(translationArray);
          Debug.Log($"Translation vector: {shift}");
        }
        else
        {
          Debug.Log($"Translation not found for '{nodeName}'.");
        }
      }
      else
      {
        Debug.Log($"Object '{nodeName}' not found.");
      }
    }

    private Vector3 ShiftSpawnPoint(JToken translationArray)
    {
      float x = translationArray[0].Value<float>();
      float y = 0;
      float z = translationArray[2].Value<float>();

      Vector3 shift = new Vector3(x, y, z);
      SpawnPoint.position -= shift;
      return shift;
    }

    private JToken FindNodeByName(JToken nodesArray, string nodeName)
    {
      foreach (var node in nodesArray)
      {
        if (node["name"] != null && node["name"].ToString() == nodeName)
          return node;
      }

      return null;
    }
  }
}