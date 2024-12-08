using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActicateGGVoice : MonoBehaviour
{
   public void PlayIntroVoice()
   {
      SoundManager.Instance.PlaySound("ГГГоворит1", transform.position,2f);
   }
}
