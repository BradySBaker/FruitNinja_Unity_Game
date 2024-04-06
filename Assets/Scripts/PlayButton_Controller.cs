using TMPro;
using UnityEngine;

public class PlayButton_Controller : MonoBehaviour
{
    public GameManager gm;
    public void OnClick() {
        gm.StartGame(GetComponentInChildren<TextMeshProUGUI>().text);
    }
}
