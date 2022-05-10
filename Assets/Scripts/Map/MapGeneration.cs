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
        [field: SerializeField] private GameObject[] MapParts { get; set; }
        [field: SerializeField] private int Size { get; set; }

        private void Start()
        {
            var position = this.transform.position;

            for (int i = 0; i < Size; i++)
            {
                var index = random.Range(0, MapParts.Length);
                
                Instantiate(MapParts[index], position, Quaternion.identity);
                PositionMovement.MovePosition.Move(ref position, MapParts[index].tag);
            }
        }
    }
}