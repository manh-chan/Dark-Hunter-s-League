[System.Serializable]
public class PlayerData
{
    public string name;
    public int level;
    public int hp;
    public int xp;

    // Constructor để khởi tạo dữ liệu nhân vật
    public PlayerData(string name, int level, int hp, int xp)
    {
        this.name = name;
        this.level = level;
        this.hp = hp;
        this.xp = xp;
    }
}