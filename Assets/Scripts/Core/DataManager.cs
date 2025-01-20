using UnityEngine;

public class DataManager : MonoBehaviour
{
    private const string MaxZombieKillsKey = "MaxZombieKills";
    
    public static void SaveMaxZombieKills(int currentKills)
    {
        var maxKills = PlayerPrefs.GetInt(MaxZombieKillsKey, 0);
        if (currentKills > maxKills)
        {
            PlayerPrefs.SetInt(MaxZombieKillsKey, currentKills);
            PlayerPrefs.Save();
        }
    }
    
    public static int LoadMaxZombieKills()
    {
        return PlayerPrefs.GetInt(MaxZombieKillsKey, 0);
    }
}