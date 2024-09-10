using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
  public Animator animator;


  public void PlayerDamaged()
  {
    animator.Play("Damage");
    SceneManager.LoadScene(sceneManager.GetActiveScene().name);
  }

}
