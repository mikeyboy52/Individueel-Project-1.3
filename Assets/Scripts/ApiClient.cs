using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using System.Net.Cache;
using UnityEngine.SceneManagement;

public class APIClient : MonoBehaviour
{
    string token;
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
            Login(email, password);
        }

    }
    public async void Login(string email, string password)
    {
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
            
            Debug.Log(responseDto.accessToken);
            token = responseDto.accessToken;
            SceneManager.LoadScene("WorldSelection");
        }
    }
    public async void CreateWorld(string worldname)
    {
        var request = new PostCreateWorldRequestDto()
        {
            Worldname = worldname
        };
        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall("https://avansict2227459.azurewebsites.net/Enviroment", "POST", jsondata, token);
        if (response != null)
        {
            Debug.Log("Succesfully created a new world");
        }
    }
    public async void EditWorld(string worldname)
    {
        var request = new PostCreateWorldRequestDto()
        {
            Worldname = worldname
        };
        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall("https://avansict2227459.azurewebsites.net/Enviroment", "GET", jsondata, token);
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

                    Debug.Log("Email or Password invalid");
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
