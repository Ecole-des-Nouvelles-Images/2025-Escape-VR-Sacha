using System;
using UnityEngine;
using Utils;

namespace Manager
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _rooms;
        [SerializeField] private GameObject[] _corridors;
        
        private void OnEnable()
        {
            GameEvents.OnRoomChanged += SwitchActivation;
        }

        private void OnDisable()
        {
            GameEvents.OnRoomChanged -= SwitchActivation;
        }

        private void Start()
        {
            SwitchActivation("R1");
        }

        private void SwitchActivation(string roomToActivateID)
        {
            switch (roomToActivateID)
            {
                case "R1":
                    Helper.EnableGameObjectInArray(1,_rooms);
                    Helper.EnableGameObjectInArray(0,_corridors);
                    break;
                case "r1":
                    Helper.EnableGameObjectInArray(1,_rooms);
                    Helper.EnableGameObjectInArray(0,_corridors);
                    break;
                case "C1":
                    Helper.EnableGameObjectInArray(0,_rooms);
                    Helper.EnableGameObjectInArray(1,_corridors);
                    break;
                case "c1":
                    Helper.EnableGameObjectInArray(0,_rooms);
                    Helper.EnableGameObjectInArray(1,_corridors);
                    break;
                
                case "R2":
                    Helper.EnableGameObjectInArray(2,_rooms);
                    Helper.EnableGameObjectInArray(0,_corridors);
                    break;
                case "r2":
                    Helper.EnableGameObjectInArray(2,_rooms);
                    Helper.EnableGameObjectInArray(0,_corridors);
                    break;
                case "C2":
                    Helper.EnableGameObjectInArray(0,_rooms);
                    Helper.EnableGameObjectInArray(2,_corridors);
                    break;
                case "c2":
                    Helper.EnableGameObjectInArray(0,_rooms);
                    Helper.EnableGameObjectInArray(2,_corridors);
                    break;
                
                case "R3":
                    Helper.EnableGameObjectInArray(3,_rooms);
                    Helper.EnableGameObjectInArray(0,_corridors);
                    break;
                case "r3":
                    Helper.EnableGameObjectInArray(3,_rooms);
                    Helper.EnableGameObjectInArray(0,_corridors);
                    break;
                case "C3":
                    Helper.EnableGameObjectInArray(0,_rooms);
                    Helper.EnableGameObjectInArray(3,_corridors);
                    break;
                case "c3":
                    Helper.EnableGameObjectInArray(0,_rooms);
                    Helper.EnableGameObjectInArray(3,_corridors);
                    break;
                
                case "R4":
                    Helper.EnableGameObjectInArray(4,_rooms);
                    Helper.EnableGameObjectInArray(0,_corridors);
                    break;
                case "r4":
                    Helper.EnableGameObjectInArray(4,_rooms);
                    Helper.EnableGameObjectInArray(0,_corridors);
                    break;
                /*case "C4":
                    Helper.EnableGameObjectInArray(0,_rooms);
                    Helper.EnableGameObjectInArray(4,_corridors);
                    break;
                case "c4":
                    Helper.EnableGameObjectInArray(0,_rooms);
                    Helper.EnableGameObjectInArray(4,_corridors);
                    break;*/
            }
        }
    }
}
