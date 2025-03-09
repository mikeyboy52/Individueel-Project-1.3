using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using System.Net.Cache;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;
using NUnit.Framework;

public class APIClient : MonoBehaviour
{
    string token;
    string Email;
    string Worldname;
    public Guid WorldId;
    public EnviromentList Enviroments;
    public Enviroment ChosenWorld;
    public ObjectList objects;
    public int Worlds;
    public static APIClient Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }
    public async void Register(string email, string password)
    {
        Email = email;
        var request = new PostRegisterRequestDto()
        {
            email = email,
            password = password
        };
        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall("https://avansict2227459.azurewebsites.net/account/register", "POST", jsondata);
        if (response != null)
        {
            Debug.Log("Succesfully Registered");
            var login = await Login(email, password);
            if (login == null)
            {
                Debug.Log("Login failed");
            }
        }

    }
    public async Task<string> Login(string email, string password)
    {
        if (Email != email)
        {
            Email = email;
        }
        
        var request = new PostLoginRequestDto()
        {
            email = email,
            password = password
        };
        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall("https://avansict2227459.azurewebsites.net/account/login", "POST", jsondata);
        var responseDto = JsonUtility.FromJson<PostLoginResponseDto>(response);
        if (responseDto != null)
        {
            token = responseDto.accessToken;
            SceneManager.LoadScene("WorldSelection");
            return "Succes";
        }
        else
        {
            return null;
        }
    }
    public async Task Logout()
    {
        var request = new PostLogoutRequestDto()
        {
            Email = Email
        };
        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall("https://avansict2227459.azurewebsites.net/account/logout", "POST", jsondata, token);
        if (response != null)
        {
            token = "";
            SceneManager.LoadScene("StartScreen");
        }
    }
    public async void CreateWorld(string worldname)
    {
        var request = new PostCreateWorldRequestDto()
        {
            Name = worldname,
            Email = Email,
            Maxheight = 120,
            MaxLength = 120
        };
        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall("https://avansict2227459.azurewebsites.net/Enviroment", "POST", jsondata, token);
        if (response != null)
        {
            Debug.Log("Succesfully created a new world");
            Worldname = worldname;
        }
    }
    public async Task GetAllWorldsForUser()
    {
        var response = await PerformApiCall($"https://avansict2227459.azurewebsites.net/Enviroment/{Email}", "GET", null, token);
        if (!string.IsNullOrEmpty(response) && response != "[]")
        {
            string wrappedResponse = "{\"Enviroments\":" + response + "}";
            EnviromentList list = JsonUtility.FromJson<EnviromentList>(wrappedResponse);
            Enviroments = list;
            Worlds = Enviroments.Enviroments.Length;
        }
        
    }
    public async Task GetWorldFromNameFromUser()
    {
        var response = await PerformApiCall($"https://avansict2227459.azurewebsites.net/Enviroment?email={Email}&Name={Worldname}", "GET", null, token);
        if (!string.IsNullOrEmpty(response) && response != "[]")
        {
            ChosenWorld = JsonUtility.FromJson<Enviroment>(response);
        }

    }
    public async void EditWorld(string worldname)
    {
        var request = new GetWorldByNameFromUserDto()
        {
            Id = Convert.ToString(WorldId),
            Name = worldname,
            Email = Email,
            Maxheight = 120,
            MaxLength = 120
        };
        var jsondata = JsonUtility.ToJson(request);
        Debug.Log(jsondata);
        var response = await PerformApiCall($"https://avansict2227459.azurewebsites.net/Enviroment/{WorldId}", "PUT", jsondata, token);
        if (response != null)
        {
            Debug.Log("Edited World");
        }
    }
    public async void DeleteWorld(Guid Id)
    {
        var response = await PerformApiCall($"https://avansict2227459.azurewebsites.net/Enviroment/{Id}", "DELETE", null, token);
        if (response != null)
        {
            Debug.Log("Deleted World");
        }
    }
    public async void CreateObject(Object2D object2D)
    {
        var request = new PostCreateObjectRequestDto()
        {
            EnviromentId = Convert.ToString(object2D.EnviromentId),
            PrefabId = object2D.PrefabId,
            PositionX = object2D.posX,
            PositionY = object2D.posY,
            ScaleX = object2D.scaleX,
            ScaleY = object2D.scaleY,
            RotationZ = object2D.rotZ,
            SortingLayer = object2D.sortingLayer
        };
        var jsondata = JsonUtility.ToJson(request);
        Debug.Log(jsondata);
        var response = await PerformApiCall("https://avansict2227459.azurewebsites.net/Object2D", "POST", jsondata, token);
        if (response != null)
        {
            Debug.Log("Succesfully created Object");
        }
    }
    public async Task<string> GetAllObjectsForEnviroment()
    {
        Debug.Log(WorldId);
        var response = await PerformApiCall($"https://avansict2227459.azurewebsites.net/Object2D/{WorldId}", "GET", null, token);
        Debug.Log(response);
        if (!string.IsNullOrEmpty(response) && response != "[]")
        {
            string wrappedResponse = "{\"Objects\":" + response + "}";
            Debug.Log(wrappedResponse);
            ObjectList list = JsonUtility.FromJson<ObjectList>(wrappedResponse);
            if (list.Objects.Length > 0)
            {
                objects = list;
                Debug.Log(list.Objects);
                return "true";
            }
            else
            {
                Debug.Log("Objects not available");
                return null;
            }
        }
        else
        {
            Debug.Log("Empty");
            return null;
        }

    }
    private async Task<string> PerformApiCall(string url, string method, string jsonData = null, string token = null)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, method))
        {
            if (!string.IsNullOrEmpty(jsonData))
            {
                byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            }

            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            if (!string.IsNullOrEmpty(token))
            {
                request.SetRequestHeader("Authorization", "Bearer " + token);
            }

            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("API-aanroep is successvol: ");

                    return request.downloadHandler.text;

            }
            else
            {
                if (request.error == "HTTP/1.1 400 Bad Request")
                {

                    Debug.Log("Request Not good");
                }
                else if (request.error == "HTTP/1.1 401 Unauthorized")
                {
                    Debug.Log("Not Authorized");
                    Debug.Log(token);

                }
                else
                {
                    Debug.Log("API-aanroep Failed: " + request.error);
                }
                return null;
            }
        }
    }
}
