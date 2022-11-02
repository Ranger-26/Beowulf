using System;
using AI;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text EnemyHealthtext;

    public Text PlayerHealthText;
    
    private void Start()
    {
        PlayerHealth.OnHealthChange += OnPlayerHealthChange;

        GrendelHealth.OnGrendelHurt += OnGrendelHealthChange;
    }

    private void OnDestroy()
    {
        PlayerHealth.OnHealthChange -= OnPlayerHealthChange;

        GrendelHealth.OnGrendelHurt -= OnGrendelHealthChange;
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
}