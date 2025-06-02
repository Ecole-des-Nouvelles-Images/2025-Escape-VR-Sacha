using System;
using System.Collections;
using UnityEngine;
using Utils;

namespace Manager
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _rooms;
        [SerializeField] private GameObject[] _corridors;

        [SerializeField]private DialogManager _dialogManager;
        
        [SerializeField]private AudioSource _ambianceAudioSource;
        [SerializeField] private AudioClip[] _ambiances; 
        
        
        
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
                    Helper.EnableGameObjectInArray(1,_rooms, false,0);
                    Helper.EnableGameObjectInArray(1,_corridors, true,3);
                    _ambianceAudioSource.clip = _ambiances[1];
                    _ambianceAudioSource.Play();
                    break;
                case "r1":
                    Helper.EnableGameObjectInArray(1,_rooms, false,0);
                    Helper.EnableGameObjectInArray(1,_corridors, false,0);
                    _ambianceAudioSource.clip = _ambiances[1];
                    _ambianceAudioSource.Play();
                    break;
                case "C1":
                    Helper.EnableGameObjectInArray(2,_rooms, false,0);
                    Helper.EnableGameObjectInArray(1,_corridors, false,0);
                    _ambianceAudioSource.clip = _ambiances[0];
                    _ambianceAudioSource.Play();
                    break;
                case "c1":
                    Helper.EnableGameObjectInArray(2,_rooms, false,0);
                    Helper.EnableGameObjectInArray(1,_corridors, false,0);
                    _ambianceAudioSource.clip = _ambiances[0];
                    _ambianceAudioSource.Play();
                    break;
                
                case "R2":
                    Helper.EnableGameObjectInArray(2,_rooms, false,0);
                    Helper.EnableGameObjectInArray(2,_corridors, false,0);
                    _ambianceAudioSource.clip = _ambiances[2];
                    _ambianceAudioSource.Play();
                    break;
                case "r2":
                    Helper.EnableGameObjectInArray(2,_rooms, false,0);
                    Helper.EnableGameObjectInArray(2,_corridors, false,0);
                    _ambianceAudioSource.clip = _ambiances[2];
                    _ambianceAudioSource.Play();
                    break;
                case "C2":
                    Helper.EnableGameObjectInArray(3,_rooms, false,0);
                    Helper.EnableGameObjectInArray(2,_corridors, false,0);
                    _ambianceAudioSource.clip = _ambiances[0];
                    _ambianceAudioSource.Play();
                    break;
                case "c2":
                    Helper.EnableGameObjectInArray(3,_rooms, false,0);
                    Helper.EnableGameObjectInArray(2,_corridors, false,0);
                    _ambianceAudioSource.clip = _ambiances[0];
                    _ambianceAudioSource.Play();
                    break;
                
                case "R3":
                    Helper.EnableGameObjectInArray(3,_rooms, false,0);
                    Helper.EnableGameObjectInArray(3,_corridors, false,0);
                    _ambianceAudioSource.Stop();
                    break;
                case "r3":
                    Helper.EnableGameObjectInArray(3,_rooms, false,0);
                    Helper.EnableGameObjectInArray(3,_corridors, false,0);
                    _ambianceAudioSource.Stop();
                    break;
                case "C3":
                    Helper.EnableGameObjectInArray(4,_rooms, false,0);
                    Helper.EnableGameObjectInArray(3,_corridors, false,0);
                    _ambianceAudioSource.clip = _ambiances[0];
                    _ambianceAudioSource.Play();
                    break;
                case "c3":
                    Helper.EnableGameObjectInArray(4,_rooms, false,0);
                    Helper.EnableGameObjectInArray(3,_corridors, false,0);
                    _ambianceAudioSource.clip = _ambiances[0];
                    _ambianceAudioSource.Play();
                    break;
                
                case "R4":
                    Helper.EnableGameObjectInArray(4,_rooms, false,0);
                    Helper.EnableGameObjectInArray(4,_corridors, false,0);
                    _ambianceAudioSource.clip = _ambiances[3];
                    _ambianceAudioSource.Play();
                    break;
                case "r4":
                    Helper.EnableGameObjectInArray(4,_rooms, false,0);
                    Helper.EnableGameObjectInArray(4,_corridors, false,0);
                    _ambianceAudioSource.clip = _ambiances[3];
                    _ambianceAudioSource.Play();
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
