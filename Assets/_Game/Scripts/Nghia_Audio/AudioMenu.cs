using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenu : MonoBehaviour
{
   
    void Start()
    {
        AudioManager.Instance.PlayMenuMusic();
    }

   
}
