using System.IO;
using Cars.Models;
using UnityEngine;
using Cars.Movement;
using Newtonsoft.Json;
using Cars.CameraController;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;

namespace Cars
{
    public class LoadCar : MonoBehaviour
    {
        public GameObject PlayerCanvas;
        public GameObject PauseCanvas;

        private void Awake()
        {
            var filename = FetchFilenameOfSelectedCar();
            var instantiatedCar = InstantiateSelectedCar(filename);
            instantiatedCar.GetComponent<ParentControl>().Car = LoadJsonCarInfo(filename);
            LoadAfterCarLoad();
        }

        private Car LoadJsonCarInfo(in string filename)
        {
            var fs = new FileStream($"{Application.streamingAssetsPath}/CarsInfo/{filename}.json", FileMode.Open, FileAccess.Read, FileShare.Read); // can be buggy
            using var sr = new StreamReader(fs);
            
            return JsonConvert.DeserializeObject<Car>(sr.ReadToEnd());
        }

        private void LoadAfterCarLoad()
        {
            transform.Find("/MainCamera").AddComponent<CameraFollowCar>();
            PlayerCanvas.SetActive(true);
            PauseCanvas.SetActive(true);
        }

        private GameObject InstantiateSelectedCar(in string filename)
        {
            return Instantiate(Resources.Load<GameObject>($"Prefab/Cars/{filename}"), gameObject.transform); // can be buggy
        }
    
        private string FetchFilenameOfSelectedCar()
        {
            var fs = new FileStream($"{Application.streamingAssetsPath}/Preferences/SelectedCar.json", FileMode.Open, FileAccess.Read, FileShare.Read);
            using var sr = new StreamReader(fs);
    
            var parsed = JObject.Parse(sr.ReadToEnd());
            return parsed["filenameOfJsonAndPrefab"].ToString();
        }
    }
}
