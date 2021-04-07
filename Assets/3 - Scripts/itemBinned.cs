using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class itemBinned : MonoBehaviour
{
    public AudioSource notificationSource;
    public AudioClip correctItem;
    public AudioClip incorrectItem;

    void OnTriggerEnter(Collider col)
    {
        //if (col.isTrigger)
        //{
        if (col.gameObject.layer != 10)
        {
            if (gameObject.CompareTag("goodItem"))
            {
                if (GameManager.gm.goodItems.Contains(col.gameObject.tag))
                {
                    GameManager.gm.IncreaseScore(2);
                    Destroy(col.gameObject);
                    GameManager.gm.itemsRemoved += 1;
                    playSound(correctItem);
                }
                else if (GameManager.gm.badItems.Contains(col.gameObject.tag))
                {
                    GameManager.gm.DecreaseScore(-4);
                    Destroy(col.gameObject);
                    GameManager.gm.itemsRemoved += 1;
                    playSound(incorrectItem);
                }
            }
            else if (gameObject.CompareTag("badItem"))
            {
                if (GameManager.gm.badItems.Contains(col.gameObject.tag))
                {
                    GameManager.gm.IncreaseScore(2);
                    Destroy(col.gameObject);
                    GameManager.gm.itemsRemoved += 1;
                    playSound(correctItem);
                }
                else if (GameManager.gm.goodItems.Contains(col.gameObject.tag))
                {
                    GameManager.gm.DecreaseScore(-4);
                    Destroy(col.gameObject);
                    GameManager.gm.itemsRemoved += 1;
                    playSound(incorrectItem);
                }
            }
        }
        else if (col.gameObject.layer == 10)
        {
            GameManager.gm.DecreaseScore(-8);
            Destroy(col.gameObject);
            GameManager.gm.itemsRemoved += 1;
            playSound(incorrectItem);
        }
        //}
    }

    void playSound(AudioClip clip)
    {
        notificationSource.clip = clip;
        notificationSource.Play();
    }
}
