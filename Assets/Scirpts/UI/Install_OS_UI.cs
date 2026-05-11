using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
    Description:
        This class is responsible for managing the Canvas that allows the player to install an OS on the server.
        At the end of all pages, the OS is installed on the server.
        The Canvas has the colors corresponding to the OS.
*/

public class Install_OS_UI : MonoBehaviour
{
    [SerializeField] private Install_OS_UI NextPage;
    [SerializeField] private Button NextButtom;
    [SerializeField] private TextMeshProUGUI LabelOSName_Installing, LabelOSName, LabelOSDescription, WarringLabel;
    [SerializeField] private Image panel;
    [SerializeField] private RawImage Background, OS_Logo;
    [SerializeField] private bool Install = false; // If the value are true, install de OS in server
    [SerializeField] private TMP_InputField hostname, password;
    [SerializeField] private GameObject hiddenContent;
    private Server server;

    void Start()
    {
        CameraFollow.LockRotate();
    }

    public void setServer(Server server)
    {
        this.server = server;
        OperatingSystem os = server.serverStatus.os;
        if (LabelOSName != null)
        {
            LabelOSName.text = os.DisplayName;
            LabelOSName.color = os.TextColor;
        }
        if (LabelOSName_Installing != null)
        {
            LabelOSName_Installing.text = os.DisplayName;
            LabelOSName_Installing.color = os.PrimaryColor;
        }
        if (OS_Logo != null && os.icon != null)
        {
            OS_Logo.texture = os.icon;
        }
        else {
            Debug.LogWarning("OS icon is null for " + os.DisplayName);
            Debug.LogWarning("Os_logo is null: " + (OS_Logo == null));
            Debug.LogWarning("os.icon is null: " + (os.icon == null));
        }
        if (LabelOSDescription != null)
        {
            LabelOSDescription.text = os.Description;
            LabelOSDescription.color = os.TextColor;
        }
        if (panel != null) panel.color = os.ThirdColor;
        if (NextButtom != null)
        {
            ColorBlock colorBlock = NextButtom.colors;
            colorBlock.normalColor = os.PrimaryColor;
            NextButtom.colors = colorBlock;
        }
        if (Background != null)
        {
            Background.color = os.PrimaryColor;
        }
        if (hiddenContent != null) StartCoroutine(ShowContent());
    }

    public void NextButtomFunc()
    {
        if (WarringLabel != null && Install && hostname != null && hostname.text.Length < 3)
        {
            WarringLabel.text = "* Erro, The hostname must be longer than 3 characters";
            return;
        }
        if (WarringLabel != null && Install && password != null && password.text.Length < 3)
        {
            WarringLabel.text = "* Erro, The password must be longer than 3 characters";
            return;
        }
        if (WarringLabel != null) WarringLabel.text = "";
        if (NextPage == null)
        {
            CameraFollow.UnlockRotate();
            if (Install && hostname != null && password != null && hostname.name.Length > 3 && password.name.Length > 3)
            {
                server.serverStatus.OS_Install = true;
                server.serverStatus.HostName = hostname.text;
                server.serverStatus.Password = password.text;
                StorageManager storageManager = GameObject.FindGameObjectWithTag("StorageManager").GetComponent<StorageManager>();
                if (storageManager == null)
                {
                    NotificationServer.AddNotification(new NotificationDefault("Save System","Erro to load save manager"));
                    return;
                }
                storageManager.UpdateData(server);
                NotificationServer.AddNotification(new NotificationDefault("OS Install","OS has been installed successfully."));
            }
            Debug.Log("NextPage is null, closing the Install_OS_UI.");
            Destroy(this.gameObject);
            return;
        }
        Install_OS_UI UI = Instantiate(NextPage,Vector3.zero,Quaternion.identity);
        UI.setServer(server);
        Destroy(this.gameObject);
    }

    private IEnumerator ShowContent()
    {
        if (hiddenContent != null) hiddenContent.SetActive(false);
        yield return new WaitForSeconds(2f);
        if (hiddenContent != null) hiddenContent.SetActive(true);
    }

    public Server GetServer()
    {
        return server;
    }
}
