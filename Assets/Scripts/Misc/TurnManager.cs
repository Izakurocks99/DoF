using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TurnManager : MonoBehaviour {

    Player player;
    List<EnemyCard> enemyList = new List<EnemyCard>();
    List<BiomeScript> biomeList = new List<BiomeScript>();

    bool isStarted;
    [SerializeField]
    LevelGeneration levelGenerator;
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    GameObject startingScreen;
    [SerializeField]
    GameObject optionsMenu;

    [SerializeField]
    AudioMixer audioMixer;

    // Use this for initialization
    void Start () {
        levelGenerator.enabled = false;
        inventory.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!player)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            }
        }

    }

    public void AddToList(EnemyCard enemyCard)
    {
        enemyList.Add(enemyCard);
    }

    public void AddToList(BiomeScript biomeCard)
    {
        biomeList.Add(biomeCard);
    }

    public void RemoveFromList(EnemyCard enemyCard)
    {
        enemyList.Remove(enemyCard);
        Destroy(enemyCard.gameObject);

        if (enemyList.Count == 0)
            player.inCombat = false;
    }

    public void RemoveFromList(BiomeScript biomeCard)
    {
        biomeList.Remove(biomeCard);
        Destroy(biomeCard.gameObject);
    }

    //public void EndTurn()
    //{
    //    foreach(EnemyCard var in enemyList)
    //    {
    //        int attack = var.GetAttack();
    //        player.ModifyHealth(-attack);
    //    }
    //}

    public IEnumerator EndTurn()
    {
        player.isPlayerTurn = false;
        yield return new WaitForSeconds(0.01f);
        foreach (EnemyCard var in enemyList)
        {
            int attack = var.GetAttack();
            player.ModifyHealth(-attack);
            var.PlayAttackAnim();
            player.PlayDamagedAnim();
            yield return new WaitForSeconds(0.01f);
        }
        player.isPlayerTurn = true;
        yield break;
    }

    public void RemoveAll()
    {
        foreach(EnemyCard var in enemyList)
        {
            Destroy(var.gameObject);
        }
        enemyList.Clear();

        foreach (BiomeScript var in biomeList)
        {
            Destroy(var.gameObject);
        }
        biomeList.Clear();
    }

    public void WinGame()
    {
        player.WinGame();
    }

    public void StartGame()
    {
        startingScreen.SetActive(false);  
        levelGenerator.enabled = true;
        inventory.enabled = true;
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {

        audioMixer.SetFloat("Music", volume);
    }

    public void SetSoundVolume(float volume)
    {

        audioMixer.SetFloat("Sound", volume);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
