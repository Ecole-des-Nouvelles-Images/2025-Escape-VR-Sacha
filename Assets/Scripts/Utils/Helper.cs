using UnityEngine;

namespace Utils
{
    public static class Helper
    {
        /*
         * object number 0 = disable all
         */
        public static void EnableGameObjectInArray(int objectNumber, GameObject[] arrayObjects)
        {
            if (objectNumber == 0)
            {
                foreach (GameObject objects in arrayObjects)
                {
                    objects.SetActive(false);
                }
                return;
            }
            if (objectNumber <= arrayObjects.Length)
            {
                for (int i = 0; i < arrayObjects.Length; i++)
                {
                    if (i != objectNumber - 1)
                        arrayObjects[i].SetActive(false);
                    else
                        arrayObjects[i].SetActive(true);
                }
            }
        }
    }
}
