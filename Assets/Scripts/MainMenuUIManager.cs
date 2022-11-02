using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("FightScene");
    }
}