using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Azterik_Core;

namespace Azterik_Networking
{
    public class NetCore : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] InputField nameField;
        [SerializeField] Button submitButton;
        [Header("Text fields")]
        [SerializeField] Text highscoreText;
        [SerializeField] Text errorText;
        [SerializeField] Text scoreText;
        [Header("Dev settings")]
        [SerializeField] int nameMaxChar = 12;
        [SerializeField] int nameMinChar = 6;

        private void Start()
        {
            scoreText.text = GameCore.score.ToString();
            CallGather();
            VerifiyInput();
        }

        public void CallRegister()
        {
            StartCoroutine(SubmitScore());
        }

        IEnumerator SubmitScore()
        {
            WWWForm form = new WWWForm();
            form.AddField("name", nameField.text);
            form.AddField("score", GameCore.score);

            WWW www = new WWW("http://blueparrotgames.com/ext_connect_sql/submitscore.php", form);
            yield return www;

            if(www.text == "0")
            {
                Debug.Log("Score submitted");
            }
            else
            {
                Debug.Log("Submitting score failed");
            }
        }

        public void VerifiyInput()
        {
            submitButton.interactable = (nameField.text.Length >= nameMinChar && nameField.text.Length <= nameMaxChar);

            if (nameField.text.Length == 0)
            {
                if (!errorText.gameObject.activeInHierarchy)
                    errorText.gameObject.SetActive(true);

                errorText.text = "Enter a name!";
            }
            else if (nameField.text.Length < nameMinChar)
            {
                if (!errorText.gameObject.activeInHierarchy)
                    errorText.gameObject.SetActive(true);

                errorText.text = "Minimum name length is " + nameMinChar + " !";
            }

            else if (nameField.text.Length > nameMaxChar)
            {
                if (!errorText.gameObject.activeInHierarchy)
                    errorText.gameObject.SetActive(true);

                errorText.text = "Maximum name length is " + nameMaxChar + " !";
            }
            else
            {
                if (errorText.gameObject.activeInHierarchy)
                    errorText.gameObject.SetActive(false);

                errorText.text = "";
            }
        }

        public void CallGather()
        {
            StartCoroutine(GatherHighscores());
        }

        public IEnumerator GatherHighscores()
        {
            WWW request = new WWW("http://blueparrotgames.com/ext_connect_sql/gatherscores.php");
            yield return request;
            highscoreText.text = request.text;
        }
    }
}