using UnityEngine;

/*
    This class is used to check whether the player has already had an
    old version of the game installed.
    If so, delete the entire save, to avoid errors between elements.

    Esta calsse serve para vereficar se o jogador já teve uma versão
    antiga do jogo instalada.
    Se sim apaga o save completo, para evitar erros entre elementos.
*/

public class ResetGame : MonoBehaviour{
    void Awake()
    {
        string version_Save = PlayerPrefs.GetString("Application_Version");
        string version = Application.version;
        #if UNITY_EDITOR
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            MoneyBank.Reset();
            BankAccount.Reset(Application.persistentDataPath);
        #else
            if (!PlayerPrefs.HasKey("Application_Version") || !version_Save.Equals(version)){
                PlayerPrefs.DeleteKey("DataSave");
                PlayerPrefs.SetString("Application_Version",Application.version);
                PlayerPrefs.Save();
                MoneyBank.Reset();
                BankAccount.Reset(Application.persistentDataPath);
            }
        #endif
        GameObject.FindAnyObjectByType<FirebaseManager>().Play();
    }
}
