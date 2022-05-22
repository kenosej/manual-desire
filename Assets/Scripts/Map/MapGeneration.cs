using System.Collections.Generic;
using UnityEngine;
using random = UnityEngine.Random;

namespace Map
{
    public class MapGeneration : MonoBehaviour
    {
        [field: SerializeField] private GameObject startMapPart { get; set; }
        [field: SerializeField] private GameObject endMapPart { get; set; }
        [field: SerializeField] private GameObject[] mapParts { get; set; }
        [field: SerializeField] private int size = 25;

        private void Awake()
        {
            var position = this.transform.position;

            for (int i = 0; i < size; i++)
            {
                var index = random.Range(0, mapParts.Length);

                if (i == 0)
                {
                    Instantiate(startMapPart, position, Quaternion.identity, this.transform);
                    PositionMovement.MovePosition.Move(ref position, startMapPart.tag);
                }
                else if (i == size - 1)
                {
                    Instantiate(endMapPart, position, Quaternion.identity,this.transform);
                    PositionMovement.MovePosition.Move(ref position, endMapPart.tag);
                }
                else
                {
                    Instantiate(mapParts[index], position, Quaternion.identity,this.transform);
                    PositionMovement.MovePosition.Move(ref position, mapParts[index].tag);
                }
            }
        }

        public List<Vector3> FindWaypoints()
        {
            var result = new List<Vector3>();
            for (int i = 0; i < this.transform.childCount; i++)
            {
                var obj = this.transform.GetChild(i);
                for (int j = 0; j < obj.transform.childCount; j++)
                {
                    if (obj.transform.GetChild(j).CompareTag("Waypoint"))
                    {
                        result.Add(obj.transform.GetChild(j).position);
                    }
                }
            }

            return result;
        }
    }
}