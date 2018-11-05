using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WelcomeGameManager : MonoBehaviour
{
    [SerializeField] private PlayerInfo info;
    [SerializeField] private InputField inputField;
    public void Begin()
    {
        if (inputField.text == "")
        {
            return;
        }
        info.SetID(inputField.text);
        SceneManager.LoadScene("Menu");
    }

}
