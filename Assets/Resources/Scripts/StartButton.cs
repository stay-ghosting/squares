using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    void FixedUpdate()
    {
        if(Input.GetButton("Submit"))
        {
            loadGame();
        }
    }
    public void loadGame()
    {
        SceneManager.LoadScene("Main");
    }
}
