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
        if (col.gameObject.layer != 10)
        {
            if (gameObject.CompareTag("goodItem"))
            {
                if (GameManager.gm.goodItems.Contains(col.gameObject.tag))
                {

                    GameManager.gm.IncreaseScore();
                    Destroy(col.gameObject);
                    playSound(correctItem);
                }
                else if (GameManager.gm.badItems.Contains(col.gameObject.tag))
                {
                    GameManager.gm.DecreaseScore();
                    Destroy(col.gameObject);
                    playSound(incorrectItem);
                }
            }
            else if (gameObject.CompareTag("badItem"))
            {
                if (GameManager.gm.badItems.Contains(col.gameObject.tag))
                {
                    GameManager.gm.IncreaseScore();
                    Destroy(col.gameObject);
                    playSound(correctItem);
                }
                else if (GameManager.gm.goodItems.Contains(col.gameObject.tag))
                {
                    GameManager.gm.DecreaseScore();
                    Destroy(col.gameObject);
                    playSound(incorrectItem);
                }
            }
        }
        else 
        {
            GameManager.gm.DecreaseScore();
            Destroy(col.gameObject);
        }
    }

    void playSound(AudioClip clip)
    {
        notificationSource.clip = clip;
        notificationSource.Play();
    }
}
