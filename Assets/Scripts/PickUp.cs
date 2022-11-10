using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//:) This script is responsible for:
public class PickUp : MonoBehaviour
{
    private GameController gameController;
    private Player_Health player_Health;
    private SoundPlayerPool soundPlayerPool;
    void Start()
    {
        soundPlayerPool = FindObjectOfType<SoundPlayerPool>();
        player_Health = FindObjectOfType<Player_Health>();
        gameController = FindObjectOfType<GameController>();
        Invoke(nameof(KillThis), 9.9f);
        soundPlayerPool.PlaySound(transform.position, soundPlayerPool.pickupAppear);
    }
   
    private void KillThis()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameController.score += 10;
            player_Health.Heal();
            KillThis();
            GameObject FX_PickUp = Instantiate(Resources.Load("FX_PickUp"), transform.position, transform.rotation) as GameObject;
            Destroy(FX_PickUp, 10);
            soundPlayerPool.PlaySound(transform.position, soundPlayerPool.pickup);

        }
    }
}
