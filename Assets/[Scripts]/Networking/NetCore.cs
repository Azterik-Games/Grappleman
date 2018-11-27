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

        private void Start()
        {
            scoreText.text = "Your score: " + GameCore.score;
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
            submitButton.interactable = (nameField.text.Length >= 6 && nameField.text.Length <= 12);

            if (nameField.text.Length == 0)
                errorText.text = "Enter a name!";

            else if (nameField.text.Length < 6)
                errorText.text = "Minimum name length is 6!";

            else if (nameField.text.Length > 12)
                errorText.text = "Maximum name length is 12!";

            else
                errorText.text = "";

        }

        public void CallGather()
        {
            StartCoroutine(GatherHighscores());
        }

        public IEnumerator GatherHighscores()
        {
            WWW request = new WWW("http://localhost/ext_connect_sql/gatherscores.php");
            yield return request;
            Debug.Log(request.text);
            highscoreText.text = request.text;
        }
    }
}