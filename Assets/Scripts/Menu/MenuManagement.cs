using TMPro;
using System;
using System.IO;
using UnityEngine;
using Cars.Models;
using Newtonsoft.Json;
using Cars.InputScripts;
using Newtonsoft.Json.Linq;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class MenuManagement : MonoBehaviour
    {
        private MenuInputActions _mIA;
        
        private GameObject _main;
        private GameObject _cars;
        private GameObject _maps;
        private GameObject _instructions;
        
        private const string _carsInfoPath = "./Assets/CarsInfo";
        private const string _mapsInfoPath = "./Assets/MapsInfo";

        private List<MapModel> _mapsFromJsons = new List<MapModel>();
        private int _mapsFromJsonsCounter;

        private int MapsFromJsonsCounter
        {
            get => _mapsFromJsonsCounter;
            set
            {
                if (value >= _mapsFromJsons.Count)
                {
                    _mapsFromJsonsCounter = 0;
                    return;
                }

                _mapsFromJsonsCounter = value;
            }
        }


        private List<Car> _carsFromJsons = new List<Car>();
        private int _carsFromJsonsCounter;

        private int CarsFromJsonsCounter
        {
            get => _carsFromJsonsCounter;
            set
            {
                if (value >= _carsFromJsons.Count)
                {
                    _carsFromJsonsCounter = 0;
                    return;
                }

                _carsFromJsonsCounter = value;
            }
        }

        private void Awake()
        {
            _mIA = new MenuInputActions();
            _mIA.Menu.Enable();
            _mIA.Menu.Back.performed += GoBack;
            
            LinkGameObjects();
            DisableAllMenus();
            _main.SetActive(true);
        }

        private void OnDestroy()
        {
            _mIA.Menu.Disable();
        }

        private void GoBack(InputAction.CallbackContext obj)
        {
            ReturnToMainMenu();
        }

        private void ReturnToMainMenu()
        {
            DisableAllMenus();
            _main.SetActive(true);
        }

        private void LinkGameObjects()
        {
            _main = transform.Find("Main").gameObject;
            _cars = transform.Find("Cars").gameObject;
            _maps = transform.Find("Maps").gameObject;
            _instructions = transform.Find("Instructions").gameObject;
        }

        private void DisableAllMenus()
        {
            _main.SetActive(false);
            _cars.SetActive(false);
            _maps.SetActive(false);
            _instructions.SetActive(false);
        }

        public void Play()
        {
            LoadMapsJsons();
            
            var selectedMapIndex = FetchMapIndexOfSelectedMap();

            foreach (var mapModel in _mapsFromJsons)
            {
                if (mapModel.MapIndex == selectedMapIndex)
                {
                    SceneManager.LoadScene(mapModel.SceneName, LoadSceneMode.Single);
                    return;
                }
            }

            throw new Exception("Play scene not loaded!");
        }

        public void OpenCarsMenu()
        {
            DisableAllMenus();

            LoadCars();
            
            _cars.SetActive(true);
        }
        
        public void OpenMapsMenu()
        {
            DisableAllMenus();

            LoadMaps();
            
            _maps.SetActive(true);
        }

        public void OpenInstructionsMenu()
        {
            DisableAllMenus();
            
            _instructions.SetActive(true);
        }

        public void Quit()
        {
            Debug.Log("Quitting..");
            Application.Quit();
        }
        
        public void ShowNextMap()
        {
            ++MapsFromJsonsCounter;
            LoadMapInfo(MapsFromJsonsCounter);
        }
        
        public void SelectTheMap()
        {
            var fs = new FileStream("./Assets/Resources/Preferences/SelectedMap.json", FileMode.Create, FileAccess.Write, FileShare.Read);
            using var sw = new StreamWriter(fs);

            var jsonString = "{\"mapIndex\":";
            jsonString += _mapsFromJsons[MapsFromJsonsCounter].MapIndex;
            jsonString += "}";
            
            sw.Write(jsonString);
            
            ReturnToMainMenu();
        }

        public void ShowNextCar()
        {
            _cars.transform.Find("Left").GetComponent<RotateCarForShowcase>().DestroyTheCar();
            
            ++CarsFromJsonsCounter;
            var filenameOfJsonAndPrefab = _carsFromJsons[CarsFromJsonsCounter].PrefabName;

            LoadCarInfo(CarsFromJsonsCounter, filenameOfJsonAndPrefab);
        }
        
        public void SelectTheCar()
        {
            var fs = new FileStream("./Assets/Resources/Preferences/SelectedCar.json", FileMode.Create, FileAccess.Write, FileShare.Read);
            using var sw = new StreamWriter(fs);

            var jsonString = "{\"filenameOfJsonAndPrefab\":\"";
            jsonString += _carsFromJsons[_carsFromJsonsCounter].PrefabName;
            jsonString += "\"}";
            
            sw.Write(jsonString);
            
            ReturnToMainMenu();
        }
        
        //
        
        private void LoadMaps()
        {
            LoadMapsJsons();
            
            var selectedMapIndex = FetchMapIndexOfSelectedMap();
            
            MapsFromJsonsCounter = FindSelectedMapIndex(selectedMapIndex);

            LoadMapInfo(MapsFromJsonsCounter);
        }

        private void LoadMapsJsons()
        {
            var di = new DirectoryInfo(_mapsInfoPath);
            FileInfo[] files = di.GetFiles("*.json", SearchOption.TopDirectoryOnly);

            _mapsFromJsons = new List<MapModel>();

            foreach (var file in files)
                _mapsFromJsons.Add(LoadJsonMapInfo(file.Name));
        }
        
        private int FetchMapIndexOfSelectedMap()
        {
            var fs = new FileStream("./Assets/Resources/Preferences/SelectedMap.json", FileMode.Open, FileAccess.Read, FileShare.Read);
            using var sr = new StreamReader(fs);
    
            var parsed = JObject.Parse(sr.ReadToEnd());
            return (int) parsed["mapIndex"];
        }
        
        private int FindSelectedMapIndex(in int selectedMapIndex)
        {
            for (int i = 0; i < _mapsFromJsons.Count; i++)
                if (_mapsFromJsons[i].MapIndex == selectedMapIndex)
                    return i;

            throw new Exception("Map not found!");
        }
        
        private MapModel LoadJsonMapInfo(in string filename)
        {
            var fs = new FileStream($"./Assets/MapsInfo/{filename}", FileMode.Open, FileAccess.Read, FileShare.Read); // can be buggy
            using var sr = new StreamReader(fs);
            
            return JsonConvert.DeserializeObject<MapModel>(sr.ReadToEnd());
        }

        private void LoadMapInfo(in int selectedMapIndex)
        {
            var left = _maps.transform.Find("Left");
            
            left.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Menu/Maps/{_mapsFromJsons[selectedMapIndex].PhotoFilename}");
            
            var right = _maps.transform.Find("Right");
            
            right.Find("Title").gameObject.GetComponent<TextMeshProUGUI>().text =
                _mapsFromJsons[selectedMapIndex].Title;
            
            right.Find("Info").gameObject.GetComponent<TextMeshProUGUI>().text = _mapsFromJsons[selectedMapIndex].Description;
        }
        
        //

        private void LoadCars()
        {
            _cars.transform.Find("Left").GetComponent<RotateCarForShowcase>().DestroyTheCar();
            
            LoadCarsJsons();
            
            var selectedCarFilenameOfJsonAndPrefab = FetchFilenameOfSelectedCar();
            
            CarsFromJsonsCounter = FindSelectedCarIndex(selectedCarFilenameOfJsonAndPrefab);
            
            LoadCarInfo(CarsFromJsonsCounter, selectedCarFilenameOfJsonAndPrefab);
        }

        private void LoadCarsJsons()
        {
            var di = new DirectoryInfo(_carsInfoPath);
            FileInfo[] files = di.GetFiles("*.json", SearchOption.TopDirectoryOnly);

            foreach (var file in files)
                _carsFromJsons.Add(LoadJsonCarInfo(file.Name));
        }

        private Car LoadJsonCarInfo(in string filename)
        {
            var fs = new FileStream($"./Assets/CarsInfo/{filename}", FileMode.Open, FileAccess.Read, FileShare.Read); // can be buggy
            using var sr = new StreamReader(fs);
            
            return JsonConvert.DeserializeObject<Car>(sr.ReadToEnd());
        }

        private string FetchFilenameOfSelectedCar()
        {
            var fs = new FileStream("./Assets/Resources/Preferences/SelectedCar.json", FileMode.Open, FileAccess.Read, FileShare.Read);
            using var sr = new StreamReader(fs);
    
            var parsed = JObject.Parse(sr.ReadToEnd());
            return parsed["filenameOfJsonAndPrefab"].ToString();
        }

        private int FindSelectedCarIndex(in string selectedCarFilenameOfJsonAndPrefab)
        {
            for (int i = 0; i < _carsFromJsons.Count; i++)
                if (_carsFromJsons[i].PrefabName == selectedCarFilenameOfJsonAndPrefab)
                    return i;

            throw new Exception("Car not found!");
        }

        private void LoadCarInfo(in int selectedCarIndex, in string selectedCarFilenameOfJsonAndPrefab)
        {
            GenerateCarForShowcase(selectedCarFilenameOfJsonAndPrefab);
            
            var right = _cars.transform.Find("Right");

            right.Find("Title").gameObject.GetComponent<TextMeshProUGUI>().text =
                _carsFromJsons[selectedCarIndex].Name;

            var infoString = $"Weight: {_carsFromJsons[selectedCarIndex].Weight} kg\n" +
                             $"Number of gears: {_carsFromJsons[selectedCarIndex].NumberOfGears}\n" +
                             $"Maximum speed: {_carsFromJsons[selectedCarIndex].TotalMaxSpeed} km/h\n" +
                             $"{_carsFromJsons[selectedCarIndex].CalcDrive.ToString()}-wheel drive";

            right.Find("Info").gameObject.GetComponent<TextMeshProUGUI>().text = infoString;
        }

        private void GenerateCarForShowcase(in string selectedCarFilenameOfJsonAndPrefab)
        {
            var left = _cars.transform.Find("Left");
            
            var car = Instantiate(Resources.Load<GameObject>($"Prefab/Cars/{selectedCarFilenameOfJsonAndPrefab}"), left); // can be buggy
            ChangeCarLayerRecursively(car, 5);
            left.GetComponent<RotateCarForShowcase>().Car = car.transform;
        }

        private void ChangeCarLayerRecursively(GameObject car, in int layer)
        {
            car.layer = layer;

            foreach (Transform child in car.transform)
                ChangeCarLayerRecursively(child.gameObject, layer);
        }
    }
}
