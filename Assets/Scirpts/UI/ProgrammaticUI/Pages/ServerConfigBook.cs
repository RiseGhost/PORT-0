using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ServerConfigBook : MonoBehaviour, UIBook
{
    [SerializeField] private UIPages[] pages;
    [SerializeField] private ShowCase showCase;
    [SerializeField] private GameObject SpawnContent;
    private CPUPage CPUPage;
    private FansPage FansPage;
    private MotherBoardPage MotherBoardPage;
    private DiskPage diskPage;
    private OSPage ospage;
    private List<UIPages> PageList = new List<UIPages>();
    private byte index = 0;

    void Start()
    {
        foreach (UIPages page in pages)
        {
            if (page is CPUPage)
            {
                CPUPage = Instantiate(page, SpawnContent.transform).GetComponent<CPUPage>();
                PageList.Add(CPUPage);
            }
            if (page is FansPage)
            {
                FansPage = Instantiate(page, SpawnContent.transform).GetComponent<FansPage>();
                PageList.Add(FansPage);
            }
            if (page is MotherBoardPage)
            {
                MotherBoardPage = Instantiate(page, SpawnContent.transform).GetComponent<MotherBoardPage>();
                PageList.Add(MotherBoardPage);
            }
            if (page is DiskPage)
            {
                diskPage = Instantiate(page, SpawnContent.transform).GetComponent<DiskPage>();
                PageList.Add(diskPage);
            }
            if (page is OSPage)
            {
                ospage = Instantiate(page, SpawnContent.transform).GetComponent<OSPage>();
                PageList.Add(ospage);
            }
            PageList.Last().gameObject.SetActive(false);
        }
        PageList.First().gameObject.SetActive(true);
        if (CPUPage != null && MotherBoardPage != null) MotherBoardPage.setCPUGroup(CPUPage.getGroudCpuWidget());
        if (showCase == null)
        {
            GameObject gameObject_ShowCase = GameObject.FindGameObjectWithTag("ShowCase");
            if (gameObject_ShowCase == null) return;
            showCase = gameObject_ShowCase.GetComponent<ShowCase>();
        }
    }

    void Update()
    {
        CameraFollow.LockRotate();
        if (showCase == null) return;
        var select_CPU = CPUPage.getGroudCpuWidget().getSelect();
        if (select_CPU == null)
            showCase.RemoveType(ExhibitorType.Processor);
        else showCase.AddExhibitorBottom(select_CPU.GetExhibitor());
        var select_FAN = FansPage.getGrounpFans().getSelect();
        if (select_FAN == null) showCase.RemoveType(ExhibitorType.Fan);
        else showCase.AddExhibitorStart(select_FAN.GetValue().exhibitor);
    }

    public void BackOver()
    {
        new NotificationDefault("Delivery Fast", "Server on the Way.").Show();
        ServerStatusStruct serverStatus = new ServerStatusStruct();
        serverStatus.setCPU(CPUPage.getGroudCpuWidget().getSelect());
        serverStatus.fanStatus = FansPage.getGrounpFans().getSelect();
        serverStatus.motherBoardStatus = MotherBoardPage.getMotherBoardWidget().getSelect();
        serverStatus.disks = diskPage.getDisksStatuses();
        serverStatus.os = ospage.getGROUP_OS_Widget().getSelect().GetValue();
        new Server(serverStatus);
        Close();
    }

    public void Close()
    {
        CameraFollow.UnlockRotate();
        Destroy(this.gameObject);
    }

    public GameObject getGameObject()
    {
        return this.gameObject;
    }

    public void nextPage()
    {
        if (PageList.Count() == 0) return;
        PageList[index].gameObject.SetActive(false);
        if (++index >= PageList.Count()) BackOver();
        else
            PageList[index].gameObject.SetActive(true);
    }

    public void backPage()
    {
        if (index == 0)
        {
            Close();
            return;
        }
        if (PageList.Count() == 0) return;
        PageList[index].gameObject.SetActive(false);
        PageList[--index].gameObject.SetActive(true);
    }

    public UIPages getPreviousPage()
    {
        if (index == 0) return null;
        else return PageList[index - 1];
    }

    public List<UIPages> getAllPages(){ return PageList; }

    public GROUP_CPUS_Widget getGROUP_CPUS() { return CPUPage.getGroudCpuWidget(); }
    public GROUP_FANS_Widget getFANS_Widget() { return FansPage.getGrounpFans(); }
    public GROUP_MotherBoards_Widget GetMotherBoards_Widget() { return MotherBoardPage.getMotherBoardWidget(); }

}