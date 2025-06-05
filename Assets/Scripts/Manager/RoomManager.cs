using System;
using System.Collections;
using Sound;
using UnityEngine;
using Utils;

namespace Manager
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _rooms;
        [SerializeField] private GameObject[] _corridors;

        private int _roomIndex;
        private int _coridorIndex;
        
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
            //SwitchActivation("R1");
            SwitchActivation("R1");
        }

        
        
        private void SwitchActivation(string roomToActivateID)
        {
            /*if (isRoom)
            {
                _musicAudioSource.Stop();
                _roomIndex += 1;
                if(_roomIndex == 3)
                    return;
                Helper.EnableGameObjectInArray(_roomIndex, _rooms);
                Helper.EnableGameObjectInArray(0, _corridors);
                _musicAudioSource.clip = _roomClip[_roomIndex-1];
                _musicAudioSource.Play();
            }
            else
            {
                _musicAudioSource.Stop();
                _coridorIndex += 1;
                Helper.EnableGameObjectInArray(_coridorIndex, _corridors);
                Helper.EnableGameObjectInArray(0, _rooms);
                _musicAudioSource.clip = _coridorClip;
                _musicAudioSource.Play();
            }*/
            switch (roomToActivateID)
            {
                case "R1":
                    Helper.EnableGameObjectInArray(1,_rooms);
                    Helper.EnableGameObjectInArray(0,_corridors);
                    break;
                case "C1":
                    Helper.EnableGameObjectInArray(0,_rooms);
                    Helper.EnableGameObjectInArray(1,_corridors);
                    break;
                
                case "R2":
                    Helper.EnableGameObjectInArray(2,_rooms);
                    Helper.EnableGameObjectInArray(0,_corridors);
                    break;
                case "C2":
                    Helper.EnableGameObjectInArray(0,_rooms);
                    Helper.EnableGameObjectInArray(2,_corridors);
                    break;
                
                case "R3":
                    Helper.EnableGameObjectInArray(3,_rooms);
                    Helper.EnableGameObjectInArray(0,_corridors);
                    //_ambianceAudioSource.Stop();
                    break;
                case "C3":
                    Helper.EnableGameObjectInArray(4,_rooms);
                    Helper.EnableGameObjectInArray(3,_corridors);
                    break;
               
                case "R4":
                    Helper.EnableGameObjectInArray(4,_rooms);
                    Helper.EnableGameObjectInArray(0,_corridors);
                    break;
              
            }
        }
    }
}
