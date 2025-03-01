using TMPro;
using UnityEditor;
using UnityEngine;

public class InputManagement : MonoBehaviour
{
    public TMP_InputField EmailInput;
    public TMP_InputField PasswordInput;
    public GameObject Text;
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
            APIClient.Instance.Register(Email, Password);
            
        }
        else
        {
            Text.gameObject.SetActive(true);
            Debug.Log("Invalid");
        }
    }
    public void ReadLoginUsernameInput()
    {
        Email = EmailInput.text;

    }
    public void ReadLoginPasswordInput()
    {
        Password = PasswordInput.text;

    }
    public void LoginButton()
    {
        Debug.Log("Button pressed");
        if (Email != null && Password != null)
        {
            Debug.Log("Debug: Mail and Password not null");
            Text.gameObject.SetActive(false);
            Debug.Log(Email);
            APIClient.Instance.Login(Email, Password);
        }
        else
        {
            Text.gameObject.SetActive(true);
            Debug.Log("Invalid");
        }
    }
}
