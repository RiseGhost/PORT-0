using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections.Generic;

public class FirebaseManager : MonoBehaviour
{
    // Referências do Firebase
    private FirebaseAuth auth;
    private DatabaseReference dbReference;
    private FirebaseUser user;
    private const string Player_UID_Key = "Firebase_UID_Key";
    private string old_player = "";

    async void Start()
    {
        name = "FireBase Manager";
        DontDestroyOnLoad(this);
    }

    public async void Play()
    {
        old_player = PlayerPrefs.GetString(Player_UID_Key,"");
        Debug.Log("Verificando dependências do Firebase...");
        
        // Verifica as dependências de forma assíncrona
        string urlDaBelgica = "https://port-0-6d282-default-rtdb.europe-west1.firebasedatabase.app/";
        dbReference = FirebaseDatabase.GetInstance(urlDaBelgica).RootReference;
        var dependencyTask = await FirebaseApp.CheckAndFixDependenciesAsync();
        
        if (dependencyTask == DependencyStatus.Available){
            // Inicializa o Auth normalmente
            auth = FirebaseAuth.DefaultInstance;

            await FazerLoginAnonimo();
        }
        else{
            Debug.LogError($"Não foi possível resolver as dependências do Firebase: {dependencyTask}");
        }
    }

    // --- PARTE 1: AUTENTICAÇÃO ---
    async System.Threading.Tasks.Task FazerLoginAnonimo()
    {
        Debug.Log("Tentando conectar anonimamente...");

        try
        {
            var taskResult = await auth.SignInAnonymouslyAsync();
            user = taskResult.User;
            if (old_player.Equals(""))
            {
                PlayerPrefs.SetString(Player_UID_Key,user.UserId);
                PlayerPrefs.Save();
            }
            await SalvarDadosDoJogador(old_player.Equals("") ? user.UserId : old_player);
        } catch(Exception ex)
        {
            Debug.LogError($"Erro no login anônimo: {ex.Message}");
        }
    }

    async System.Threading.Tasks.Task SalvarDadosDoJogador(string uid)
    {
        // Criamos um objeto com os dados que queremos salvar
        PlayerDataFirebase novosDados = new PlayerDataFirebase();

        // Transforma o objeto em formato JSON que o Firebase entende
        string json = JsonUtility.ToJson(novosDados);

        await dbReference.Child("jogadores").Child(uid).SetRawJsonValueAsync(json);
    }

    // --- OBTENER DADOS (LEITURA) ---
    public async System.Threading.Tasks.Task<PlayerDataFirebase> getPlayerData(string uid)
    {
        try{
            DataSnapshot snapshot = await dbReference.Child("jogadores").Child(uid).GetValueAsync();

            if (snapshot.Exists){
                string json = snapshot.GetRawJsonValue();
                PlayerDataFirebase dadosCarregados = JsonUtility.FromJson<PlayerDataFirebase>(json);
                return dadosCarregados;
            }
            else{
                Debug.LogWarning($"Nenhum dado encontrado para o UID: {uid}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao obter dados do jogador: {ex.Message}");
            return null;
        }
    }

    // --- ATUALIZAR DADOS ESPECÍFICOS ---
    public async System.Threading.Tasks.Task UpdateData(string uid, PlayerDataFirebase data)
    {
        try{
            string novoJson = JsonUtility.ToJson(data);

            await dbReference.Child("jogadores").Child(uid).SetRawJsonValueAsync(novoJson);
            
        }
        catch (Exception ex){
            Debug.LogError($"[Firebase] Erro ao atualizar objeto completo: {ex.Message}");
        }
    }

    public string getUID(){ return PlayerPrefs.GetString(Player_UID_Key,""); }
}

// Uma classe simples para estruturar os dados que você quer salvar
[System.Serializable]
public class PlayerDataFirebase
{
    public string LastConnection;
    public string GameVersion;
    public string OperatingSystem;
    public float TimeBuyServer = 0;
    public List<float> ServerWatts = new List<float>();
    public List<QuestTime> TimeByQuest = new List<QuestTime>();
    public float TimeToDestroyEnemys = 0;
    public float TotalTime = 0;
    public PlayerDataFirebase()
    {
        LastConnection = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        GameVersion = Application.version;
        OperatingSystem = Application.platform.ToString();
    }
}

[System.Serializable]
public struct QuestTime
{
    public float time;
    public string[] technology;
}