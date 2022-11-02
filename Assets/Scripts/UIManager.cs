using System;
using AI;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text EnemyHealthtext;

    public Text PlayerHealthText;

    public Text ResultText;

    public GameObject ResultGameObject;
    private void Start()
    {
        PlayerHealth.OnHealthChange += OnPlayerHealthChange;

        GrendelHealth.OnGrendelHurt += OnGrendelHealthChange;

        PlayerHealth.OnPlayerDie += OnGrendelWin;

        GrendelHealth.OnGrendelDie += OnPlayerWin;
    }

    private void OnDestroy()
    {
        PlayerHealth.OnHealthChange -= OnPlayerHealthChange;

        GrendelHealth.OnGrendelHurt -= OnGrendelHealthChange;
        
        PlayerHealth.OnPlayerDie -= OnGrendelWin;
        
        GrendelHealth.OnGrendelDie -= OnPlayerWin;
    }

    public void OnPlayerHealthChange(float f)
    {
        Debug.Log("Player healtj");
        if (f < 0)
        {
            f = 0;
        }
        PlayerHealthText.text = $"Beowulf Health: {(int)f}";
    }
    
    public void OnGrendelHealthChange(int i, int i1)
    {
        if (i1 < 0)
        {
            i1 = 0;
        }
        EnemyHealthtext.text = $"Grendel Health: {(int)i1}";
    }

    public void OnPlayerWin()
    {
        ResultText.text = "You Win!!";
        ResultText.color = Color.green;
        ResultGameObject.SetActive(true);
    }

    public void OnGrendelWin()
    {
        ResultText.text = "You Loose!!";
        ResultText.color = Color.red;
        ResultGameObject.SetActive(true);
    }
    
    public void ReloadGameScene()
    {
        SceneManager.LoadScene("FightScene");
        ResultGameObject.SetActive(false);
    }
}