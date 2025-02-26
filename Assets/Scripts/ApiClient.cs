using System.Text;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;

public class APIClient : MonoBehaviour
{
    string token;
    public async void Register(string email, string password)
    {
        var request = new PostRegisterRequestDto()
        {
            email = email,
            password = password
        };
        var jsondata = JsonUtility.ToJson(request);
        Debug.Log(jsondata);
        await PerformApiCall("https://localhost:7032/account/register", "Post", jsondata);
    }
    public async void Login(string email, string password)
    {
        var request = new PostLoginRequestDto()
        {
            email = email,
            password = password
        };
        var jsondata = JsonUtility.ToJson(request);
        var response = await PerformApiCall("https://avansict2227459.azurewebsites.net/account/login", "Post", jsondata);
        var responseDto = JsonUtility.FromJson<PostLoginResponseDto>(response);
        Debug.Log(responseDto.accessToken);
        token = responseDto.accessToken;
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
                Debug.Log("API-aanroep is successvol: " + request.downloadHandler.text);

                return request.downloadHandler.text;
            }
            else
            {
                Debug.Log("Fout bij API-aanroep: " + request.error);
                return null;
            }
        }
    }
}
