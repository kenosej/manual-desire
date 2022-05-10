using UnityEngine;

namespace Map.PositionMovement
{
    public class MovePosition
    {
        public static void Move(ref Vector3 position, string direction)
        {
            switch (direction)
            {
                case "Forward": MoveForward(ref position); break;
                case "Right": MoveRight(ref position); break;
                case "Left": MoveLeft(ref position); break;
            }
        }

        private static void MoveLeft(ref Vector3 position) {
            position += new Vector3(-100, 0, 100);
        }
        private static void MoveRight(ref Vector3 position) {
            position += new Vector3(100, 0, 100);
        }
        private static void MoveForward(ref Vector3 position) {
            position += new Vector3(0, 0, 100);
        }
    }
}