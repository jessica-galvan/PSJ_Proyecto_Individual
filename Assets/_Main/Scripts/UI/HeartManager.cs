using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    [Header("Heart Settings")]
    [SerializeField] private List<GameObject> hearts = new List<GameObject>();
    [SerializeField] private GameObject heart = null;
    private LifeController lifeController;

    void Start()
    {
        LevelManager.instance.OnPlayerAssing += Initialize;
    }

    private void Initialize()
    {
        LevelManager.instance.OnPlayerAssing -= Initialize;
        lifeController = LevelManager.instance.Player.GetComponent<LifeController>();
        lifeController.UpdateLifeBar += UpdateLifeBar;
        lifeController.OnDie += OnRespawn;
        for (int i = 0; i < lifeController.MaxLife; i++)
        {
            GameObject newHeart = Instantiate(heart);
            newHeart.transform.parent = gameObject.transform;
            hearts.Add(newHeart);
        }
    }

    private void UpdateLifeBar(int currentLife, int maxLife)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < currentLife)
                hearts[i].SetActive(true);
            else
                hearts[i].SetActive(false);
        }
    }
    private void OnRespawn()
    {
        UpdateLifeBar(lifeController.MaxLife, lifeController.MaxLife);
    }
}
