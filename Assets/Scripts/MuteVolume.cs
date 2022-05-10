using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteVolume : MonoBehaviour
{
    public void MuteToggle (bool UnMuted)
    {
        if (!UnMuted)
        {
            AudioListener.volume = 0;
        }
        else{
            AudioListener.volume = 1;
        }
    }
}
