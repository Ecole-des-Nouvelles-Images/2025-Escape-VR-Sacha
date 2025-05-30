using UnityEngine;

namespace Utils
{
    public static class Helper
    {
        /*
         * object number 0 = disable all
         */
        public static void EnableGameObjectInArray(int objectNumber, GameObject[] arrayObjects, bool haveException, int exceptionIndex)
        {
            if (objectNumber == 0)
            {
                foreach (GameObject objects in arrayObjects)
                {
                    objects.SetActive(false);
                }
                return;
            }
            if(objectNumber <= arrayObjects.Length)
            {
                for (int i = 0; i < arrayObjects.Length; i++)
                {
                    /*if (objectNumber == arrayObjects.Length && i != objectNumber - 1 && i != 0)
                    {
                        arrayObjects[i].SetActive(false);
                    }*/
                    if (i != objectNumber - 1 )
                        arrayObjects[i].SetActive(false);
                    else
                    {
                        Debug.Log("active room : " + arrayObjects[i].name);
                        arrayObjects[i].SetActive(true);
                    }
                }
                if(haveException)
                    arrayObjects[exceptionIndex].SetActive(true);
            }
        }

        public static bool IntArrayEquals(int[] array1, int[] array2)
        {
            if(array1.Length != array2.Length)
                return false;
                
            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
