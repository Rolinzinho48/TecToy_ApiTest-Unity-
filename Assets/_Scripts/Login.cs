using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

public class Login : MonoBehaviour
{

    [SerializeField]
    private InputField input;
 
    public string url = "";

    
    public MyPlayer player = new MyPlayer();
    private PlayerMove move = new PlayerMove();
    
    

    void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }
   
    public void Pesquisar()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText() {
      
        using(UnityWebRequest www = new UnityWebRequest("https://testeapi-91bb1-default-rtdb.firebaseio.com/Login.json"))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {

                string jsonString = www.downloadHandler.text;
                var players = JsonConvert.DeserializeObject<MyPlayer[]>(jsonString);

                for(int i=0;i<players.Length;i++) {
                    if(players[i].Nome == input.text){
                        url = "https://testeapi-91bb1-default-rtdb.firebaseio.com/Login/"+players[i].Id.ToString() + ".json";
                        i=players.Length;
                        Obter();
                    }
                    else if(i ==(players.Length-1))
                    {
                        Debug.Log("NAO EXISTE");
                    }
                }
                
            }
        }
    }

    public async void Obter(){
       
        using(var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);

            if(response.IsSuccessStatusCode){
                string jsonString = await response.Content.ReadAsStringAsync();
                JsonUtility.FromJsonOverwrite(jsonString,player);  
                move.SetCoinMoney(player.Saldo,player.PosX,player.Nome,url,player.Id);
                SceneManager.LoadScene("TecToy");
            }
            else{
                Debug.Log(response.ReasonPhrase);
            }
        }
    }

    
}

