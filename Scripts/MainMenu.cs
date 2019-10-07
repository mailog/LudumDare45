using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameManager gameManager;

    public EnemySpawner enemySpawner;
    
    public NakedHero noodicus;

    public GameObject statKeeper;

    public GameObject creditsPage;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleCredits(bool active)
    {
        creditsPage.SetActive(active);
    }
    
    public void StartGame()
    {
        gameManager.enabled = true;
        gameManager.LevelStart();
        statKeeper.SetActive(true);
        gameObject.SetActive(false);
    }
}
