using TMPro;
using UnityEditor;
using UnityEngine;

public class InputManagement : MonoBehaviour
{
    public TMP_InputField EmailInput;
    public TMP_InputField PasswordInput;
    public GameObject Text;
    public APIClient apiClient;
    private string Email;
    private string Password;
    public void Start()
    {
        Text.gameObject.SetActive(false);
    }
    public void ReadRegisterUsernameInput()
    {
        Email = EmailInput.text;

    }
    public void ReadRegisterPasswordInput()
    {
        Password = PasswordInput.text;

    }
    public void RegisterButton()
    {
        if (Email != null && Password != null)
        {
            Text.gameObject.SetActive(false);
            Debug.Log("Started");
            apiClient.Register(Email, Password);
            Debug.Log("Succes");
        }
        else
        {
            Text.gameObject.SetActive(true);
            Debug.Log("Invalid");
        }
    }
}
