using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class PutMethod : PlayerMove
{
    MyPlayer player2 = new MyPlayer();
    
    

    public IEnumerator Upload()
    {   
        
        string json = JsonConvert.SerializeObject(player2);
        Debug.Log(url);

        using (UnityWebRequest www = UnityWebRequest.Put(url, json))  
        {
 
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Upload complete!");
            }
        }
    }

    public void SaveApi(){  
        
        StartCoroutine(Upload());
    }

   
    
    void Update(){
        player2.Nome = name;
        player2.PosX = transform.position.x;
        player2.Saldo = money;
        player2.Id = id;
    }
    
}
