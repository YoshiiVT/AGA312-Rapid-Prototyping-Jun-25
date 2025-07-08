using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

[System.Serializable]
public class ThisGameSave
{
    public int highestScore;
    public float health;
    public Vector3 lastCheckpoint;
    public Color playerColour;
    public bool soundActive;
    public string playerName;
    public List<int> leaderboard;
    //public List<LeaderboardObject> leaderboard;

    //Time
    public int hoursPlayed;
    public int minutesPlayed;
    public int secondsPlayed;
    public float totalSeconds;
}

public class LeaderboardObject
{
    public string name;
    public int score;
    public string region;
}

public class SaveManager : SaveData
{
    //Singleton Setup
    public static SaveManager Instance;


    public ThisGameSave save = new ThisGameSave();

    //Time of Last Save
    public DateTime timeOfLastSave;
    #region Initialize
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("Game Save Manager already instanced");
            return;
        }
        Instance = this;

        //Load the save data
        save = LoadDataObject<ThisGameSave>();

        //Set the time of last save to now
        timeOfLastSave = DateTime.Now;

        if (save != null)
            return;

        CreateSaveObject();
    }

    private void CreateSaveObject()
    {
        save = new ThisGameSave();
        Debug.Log("New Save File Created");

        save.highestScore = 0;
        save.health = 100;
        save.playerColour = Color.plum;
        save.lastCheckpoint = new Vector3(0, 0, 0);
        save.soundActive = true;
        save.leaderboard = new List<int>(10);
    }
    #endregion

    #region Getters and Setters

    #region Getters
    public int GetHighestScore => save.highestScore;
    public float GetHealth => save.health;
    public Color GetPlayerColour => save.playerColour;
    public string GetPlayerName => save.playerName;
    public Vector3 GetLastCheckpoint => save.lastCheckpoint;
    public List<int> GetLeaderboard => save.leaderboard;
    #endregion

    #region Setters

    public void SetScore(int _score)
    {
        if (_score > save.highestScore)
            save.highestScore = _score;

        int last = save.leaderboard[save.leaderboard.Count - 1];
        if (_score < last) return;

        save.leaderboard.RemoveAt(save.leaderboard.Count);
        save.leaderboard.Add(_score);
        save.leaderboard.Sort();
    }

    public void SetHealth(float _health) => save.health = _health;
    public void SetPlayerColour(Color _colour) => save.playerColour = _colour;
    public void SetPlayerName(string _name) => save.playerName = _name;
    public Vector3 SetLastCheckpoint(Vector3 _last) => save.lastCheckpoint = _last;

    #endregion

    #endregion

    #region Game Time
    public void ElapsedTime()
    {
        DateTime now = DateTime.Now;
        int seconds = (now - timeOfLastSave).Seconds;
        save.totalSeconds += seconds;
        save.hoursPlayed = GetHours(save.totalSeconds);
        save.minutesPlayed = GetMinutes(save.totalSeconds);
        save.secondsPlayed = GetSeconds(save.totalSeconds);
        Debug.Log(save.hoursPlayed + " Hours, " + save.minutesPlayed +
                  " Minutes, " + save.secondsPlayed + " Seconds");
        timeOfLastSave = DateTime.Now;
    }
    int GetSeconds(float _seconds) { return Mathf.FloorToInt(_seconds % 60); }
    int GetMinutes(float _seconds) { return Mathf.FloorToInt(_seconds / 60); }
    int GetHours(float _seconds) { return Mathf.FloorToInt(_seconds / 3600); }
    #endregion

    #region Overrides
    public override void Save()
    {
        ElapsedTime();
        SaveDataObject(save);
    }

    public override void Delete()
    {
        DeleteDataObject();
    }
    #endregion
}
