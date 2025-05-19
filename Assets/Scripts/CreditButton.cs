using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditButton : MonoBehaviour
{
   public void ReturnMenu()
   {
      SceneManager.LoadScene("MainMenu");
   }
}
