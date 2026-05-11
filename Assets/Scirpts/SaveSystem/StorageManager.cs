using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class DataRoot
{
    public string ID;

    public DataRoot(string ID)
    {
        this.ID = ID;
    }

    public override bool Equals(object obj)
    {
        if (obj is DataRoot)
        {
            return this.ID == ((DataRoot)obj).ID;
        }
        return false;
    }
}

[System.Serializable]
public class StorageItem
{
    public List<string> itens = new List<string>();
}

public class StorageManager : MonoBehaviour
{
    private static StorageItem storageItem = new StorageItem();
    void Awake()
    {
        Debug.Log("Storage Manager UP 🔝");
        this.name = "Storage Manager";
        this.tag = "StorageManager";
    }

    void Start()
    {
        Debug.Log("Data saved -> " + PlayerPrefs.GetString("DataSave", ""));
        storageItem = JsonUtility.FromJson<StorageItem>(PlayerPrefs.GetString("DataSave", ""));
        if (storageItem == null)
        {
            Debug.Log("Não existia data salva");
            storageItem = new StorageItem();
        }
        DontDestroyOnLoad(this);
        //StartCoroutine(Saving());
    }

    private static List<GameObject> GetStoragesEntities()
    {
        List<GameObject> entities = new List<GameObject>();
        foreach (GameObject gameObject in GameObject.FindObjectsOfType<GameObject>())
        {
            if (gameObject.GetComponent<StorageEntity>() != null) entities.Add(gameObject);
        }
        return entities;
    }

    public void Save(SaveItem item)
    {
        List<DataRoot> saveItems = storageItem.itens.Select((x) => JsonUtility.FromJson<DataRoot>(x)).ToList();
        if (saveItems.Contains(new DataRoot(item.getID()))) return;

        Debug.Log("Item salvo -> " + item.toJSON());
        storageItem.itens.Add(item.toJSON());

        PlayerPrefs.SetString("DataSave", JsonUtility.ToJson(storageItem));
        PlayerPrefs.Save();
        Debug.Log("Save system -> ✅ Jogo Salvo");
    }

    public static void Load<T>() where T : SaveItem
    {
        Debug.Log("ℹ️ Load iniciado.");
        string data = PlayerPrefs.GetString("DataSave", "");
        if (data.Trim().Length == 0) return;
        StorageItem storage = JsonUtility.FromJson<StorageItem>(data);
        if (storage == null)
        {
            Debug.Log("O Storage está vazio não a Load para carregar.");
            return;
        }
        Debug.Log("Loaded Data:");
        Debug.Log("--------------");
        foreach (string item in storage.itens)
        {
            Debug.Log(item);
            if (JsonUtility.FromJson<T>(item) != null)
                JsonUtility.FromJson<T>(item).Load(item);
        }
        Debug.Log("--------------");
    }

    public void UpdateData<T>(T data) where T : SaveItem
    {
        if (storageItem == null)
        {
            Debug.Log("The storage is empty. There is noting to update.");
            return;
        }
        for(int i = 0; i < storageItem.itens.Count; i++)
        {
            if (JsonUtility.FromJson<DataRoot>(storageItem.itens[i]).Equals(new DataRoot(data.getID())))
            {
                storageItem.itens[i] = data.toJSON();
                Debug.Log("Save System -> 🔃 StorageItem update data.");
                PlayerPrefs.SetString("DataSave", JsonUtility.ToJson(storageItem));
                PlayerPrefs.Save();
                Debug.Log(storageItem.itens[i]);
                return;
            }
        }
    }

    public static void DeletedStorage()
    {
        var manager = GameObject.FindAnyObjectByType<StorageManager>();
        manager.StopAllCoroutines();
        storageItem = new StorageItem();
        foreach (GameObject entity in GetStoragesEntities())
        {
            Destroy(entity);
        }
        PlayerPrefs.SetString("DataSave", "");
        PlayerPrefs.DeleteKey("DataSave");
        PlayerPrefs.Save();
    }

    private IEnumerator Saving()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(3f);
            //Save();
        }
    }

    public void ResetStorage(){ PlayerPrefs.DeleteAll(); }
}