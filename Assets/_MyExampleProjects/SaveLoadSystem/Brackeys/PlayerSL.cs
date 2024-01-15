using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSL : MonoBehaviour
{
    public int level = 3;   
    public int health = 40;

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        level = data.level;
        health = data.health;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

        levelText.text = level.ToString();
        healthText.text = health.ToString();
    }

    #region UI Methods
    [SerializeField] private TMPro.TextMeshProUGUI levelText;
    [SerializeField] private TMPro.TextMeshProUGUI healthText;

    private void Start()
    {
        levelText.text = level.ToString();
        healthText.text = health.ToString();
    }


    public void ChangeLevel(int amount)
    {
        level += amount;
        levelText.text = level.ToString();
    }

    public void ChangeHealth(int amount)
    {
        health += amount;
        healthText.text = health.ToString();
    }


    #endregion
}
