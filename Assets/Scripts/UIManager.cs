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
        PlayerHealth.OnHealthChange += delegate(float f)
        {
            PlayerHealthText.text = $"Health_Player: {(int)f}";
        };
        
        GrendelHealth.OnGrendelHurt += delegate(int i, int i1)
        {
            EnemyHealthtext.text = $"Health_Grendel: {(int)i1}";
        };
    }
}