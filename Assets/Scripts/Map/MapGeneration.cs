using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using random = UnityEngine.Random;


namespace Map
{
    public class MapGeneration : MonoBehaviour
    {
        //[field: SerializeField] private GameObject Car { get; set; }
        [field: SerializeField] private GameObject StartMapPart { get; set; }
        [field: SerializeField] private GameObject EndMapPart { get; set; }
        [field: SerializeField] private GameObject[] MapParts { get; set; }
        [field: SerializeField] private int Size { get; set; }

        private void Start()
        {
            var position = this.transform.position;
            //Instantiate(Car, position, Quaternion.identity);
            
            for (int i = 0; i < Size; i++)
            {
                var index = random.Range(0, MapParts.Length);
                
                if (i == 0) {
                    Instantiate(StartMapPart, position, Quaternion.identity);
                    PositionMovement.MovePosition.Move(ref position, StartMapPart.tag);
                    
                }
                else if (i == Size - 1) {
                    Instantiate(EndMapPart, position, Quaternion.identity);
                    //PositionMovement.MovePosition.Move(ref position, EndMapPart.tag);
                }
                else {
                    Instantiate(MapParts[index], position, Quaternion.identity);
                    PositionMovement.MovePosition.Move(ref position, MapParts[index].tag);
                }
            }
        }
    }
}