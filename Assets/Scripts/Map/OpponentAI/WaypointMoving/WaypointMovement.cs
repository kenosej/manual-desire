using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.OponentAi.WaypointMover
{
    //public class WaypointMovement : MonoBehaviour
    //{
    //    //[field: SerializeField] private Waypoints waypoints;
    //    [field: SerializeField] private float speed = 10f;
    //    [field: SerializeField] private float distanceThreshold = 0.1f;
    //    private Vector3 _currentWaypoint;
    //    private int _currentWaypointIndex = 0;
//
    //    private void Start()
    //    {
    //        Debug.Log("Prije");
    //        _currentWaypoint = Waypoints.GetWaypoints()[0];
    //        Debug.Log("Poslije");
    //        //List<Vector3> waypoints = Waypoints.GetWaypoints();
//
    //        //_currentWaypoint = waypoints.GetNextWaypoint(_currentWaypoint);
    //        _currentWaypoint = Waypoints.GetNextWayPoint(_currentWaypointIndex);
    //        _currentWaypointIndex++;
    //        transform.position = _currentWaypoint;
    //        
    //        //_currentWaypoint = waypoints.GetNextWaypoint(_currentWaypoint);
    //        transform.LookAt(_currentWaypoint);
    //    }
//
    //    private void Update()
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position,
    //            _currentWaypoint, speed * Time.deltaTime);
    //        
    //        if(Vector3.Distance(transform.position, _currentWaypoint) < distanceThreshold)
    //        {
    //            //_currentWaypoint = waypoints.GetNextWaypoint(_currentWaypoint);
    //            transform.LookAt(_currentWaypoint);
    //        }
    //    }
    //}
}