using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManager : MonoBehaviour
{
   public static SceneManager Instance { get; private set; }

   private void Awake()
   {
      if (Instance != null && Instance != this)
      {
         Destroy(gameObject);
      }
      else
      {
         Instance = this;
         DontDestroyOnLoad(this);
      }
   }

   public void LoadResourceGatherScene()
   {
      UnityEngine.SceneManagement.SceneManager.LoadScene("ResourceCollectScene");
   }

   public void LoadTowerDefenseScene()
   {
      UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
   }
   
}
