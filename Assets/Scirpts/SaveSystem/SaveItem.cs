public interface SaveItem
{
    public string getID();
    public void Load(string json);
    public void Update();
    public string toJSON();
}