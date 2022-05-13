using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

namespace Map.OponentAi.WaypointMover
{
    public class Waypoints : MonoBehaviour
    {
        //private static List<GameObject> _activeGameObjects = new List<GameObject>();
        private static List<Vector3> _waypoints = new List<Vector3>();

        private void OnDrawGizmos()
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {

                var obj = this.transform.GetChild(i);
                for (int j = 0; j < obj.transform.childCount; j++)
                {
                    if(obj.transform.GetChild(j).CompareTag("Waypoint"))
                    {
                        
                        
                       Gizmos.color = Color.blue;
                       Gizmos.DrawWireSphere(obj.transform.GetChild(j).position,1f);
                       _waypoints.Add(obj.transform.GetChild(j).position);
                        
                    }
                }
            }
            
            Gizmos.color = Color.red;
            //for (int i = 0; i < this.transform.childCount; i++)
            //{
            //    var obj = this.transform.GetChild(i);
            //    for (int j = 0; j < obj.transform.childCount; j++)
            //    {
            //        if(obj.transform.GetChild(j).CompareTag("Waypoint"))
            //        {
            //            Gizmos.DrawLine(obj.transform.GetChild(j).position, transform.GetChild(i+1).position);
            //            
            //        }
            //    }
            //}

            for (int i = 0; i < _waypoints.Count - 1; i++)
            {
                Gizmos.DrawLine(_waypoints[i],_waypoints[i+1]);
            }
            
            
            
            //for (int i = 0; i < this.transform.childCount; i++)
            //{
            //    Gizmos.color = Color.blue;
            //    Gizmos.DrawWireSphere(this.transform.GetChild(i).position,1f);
            //}
        //
            //Gizmos.color = Color.red;
            //for (int i = 0; i < transform.childCount -1; i++)
            //{
            //    Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i+1).position);
            //}
        }

        public static Vector3 GetNextWayPoint(int index)
        {
            if (index + 1 < _waypoints.Count)
                return _waypoints[index + 1];
            else
                return _waypoints[_waypoints.Count];


            //if (currentWaypoint.GetSiblingIndex() < transform.childCount - 1)
            //{
            //    return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
            //}
            //else
            //{
            //    return transform.GetChild(0);
            //    //return null;
            //}
        }

        //public static void AddGameObject(GameObject newGameObject)
        //{
        //    _activeGameObjects.Add(newGameObject);
        //}

        public static List<Vector3> GetWaypoints()
        {
            return _waypoints;
        }
    }
}