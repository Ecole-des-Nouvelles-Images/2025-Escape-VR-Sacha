using System;
using System.Collections;
using Sound;
using UnityEngine;
using Utils;

namespace Manager
{
    public class RoomManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] _allRooms;

        private int _roomIndex;
        
        private void OnEnable()
        {
            GameEvents.OnNextRoom += NextRoomActivation;
        }

        private void OnDisable()
        {
            GameEvents.OnNextRoom -= NextRoomActivation;
        }

        private void OnDestroy()
        {
            GameEvents.OnNextRoom -= NextRoomActivation;
        }

        private void Awake()
        {
            _roomIndex = 0;
        }

        private void Start()
        {
            for (int i = 0; i < _allRooms.Length; i++)
            {
                _allRooms[i].SetActive(false);
            }
            //SwitchActivation("R1");
            _allRooms[_roomIndex].SetActive(true);
            GameEvents.OnPlayBGM.Invoke("intro");
        }

        private void NextRoomActivation()
        {
            if(_roomIndex < _allRooms.Length-1)
            {
                _allRooms[_roomIndex].SetActive(false);
                _roomIndex += 1;
                _allRooms[_roomIndex].SetActive(true);
            }
            else
            {
                _allRooms[_roomIndex].SetActive(false);
                _roomIndex = 0;
                _allRooms[_roomIndex].SetActive(true);
            }

            switch (_roomIndex)
            {
                case 0:
                    GameEvents.OnPlayBGM.Invoke("intro");
                    break;
                case 1:
                    GameEvents.OnPlayBGM.Invoke("corridor");
                    break;
                case 2:
                    GameEvents.OnPlayBGM.Invoke("kid");
                    break;
                case 3:
                    GameEvents.OnPlayBGM.Invoke("corridor");
                    break;
                case 4:
                    GameEvents.OnStopBGM.Invoke();
                    break;
                case 5:
                    GameEvents.OnPlayBGM.Invoke("corridor");
                    break;
                case 6:
                    GameEvents.OnPlayBGM.Invoke("office");
                    break;
            }
        }
    }
}
