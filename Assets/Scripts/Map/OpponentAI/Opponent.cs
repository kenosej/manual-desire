using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TimeManagement;

namespace Map.OpponentAI
{
   public class Opponent : MonoBehaviour
   {
      [field: SerializeField] private float Speed = 20f;
      [field: SerializeField] private MapGeneration MapGenerator;    
      
      private List<Vector3> WayPoints { get; set; }
      private Vector3 CurrentWaypoint { get; set; }
      private int WayPointCounter = 0;
      private bool IsFinishingLineReached = false;

      private Vector3 TargetPosition;
      private float SpeedForRoation = 2.0f;
      
      private void Start()
      {
         this.transform.position = new Vector3(-2.25f, 0, 0);
         WayPoints = MapGenerator.FindWaypoints();
         CurrentWaypoint = WayPoints[0];
         TargetPosition = WayPoints[1];
      }

      private void Update()
      {
         if (TimeCounterController.TimerGoing == true)
            NavigateMap();
       
      }
      
      private void NavigateMap()
      {
         if (IsFinishingLineReached)
         {
            return;
         }

         if (Vector3.Distance(this.transform.position, CurrentWaypoint) >= 0.02f)
         {
            this.transform.position = Vector3.MoveTowards(
               this.transform.position, 
               CurrentWaypoint,
               Speed * Time.deltaTime);
            
            //TODO: Rotate the car towards the target waypoint
            
            Vector3 TargetDirection = TargetPosition - this.transform.position;
            float SingleStep = SpeedForRoation * Time.deltaTime;

            Vector3 NewDirection = Vector3.RotateTowards(
               this.transform.forward, 
               TargetDirection, 
               SingleStep, 
               0.0f);
            
            
            transform.rotation = Quaternion.LookRotation(NewDirection);
            return;
         }

         WayPointCounter++;
         if (WayPointCounter == WayPoints.Count) //Changed to: WayPoints.Count, from: WayPoints.Count - 1.
            IsFinishingLineReached = true;

         CurrentWaypoint = WayPoints[WayPointCounter];
         TargetPosition = CurrentWaypoint;
      }
   }
}