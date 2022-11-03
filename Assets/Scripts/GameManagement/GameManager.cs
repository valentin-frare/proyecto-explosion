using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class LevelConfig
{
    public Level level;
    public LevelData data;
}

public enum GameState
{
    Playing,
    Crashed,
    MainMenu,
    Paused,
    Victory,
    Menu
}

public enum Level
{
    Level1,
    Level2,
    Level3,
    Level4,
    Level5
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance {get; private set;}
    public GameState gameState;
    public Level level;
    public Action<GameState> OnGameStateChanged;
    public float multiplyTorque = 1f;
    public int addEndurance = 0;
    public float multiplyHandling = 1f;
    public float finishLine;
    public bool wonLevel = false;
    public bool vibration = true;

    [SerializeField] private List<LevelData> levels;

    private void Awake() {
        instance = this;

        GameEvents.OnGameStart += OnGameStart;
        GameEvents.OnGameEnd += OnGameEnd;
    }

    private void OnGameStart()
    {
        RespawnManager.instance.SpawnPlayer();

        gameState = GameState.Playing;
    }

    private void OnGameEnd()
    {

    }

    public void SetGameState(GameState state)
    {
        gameState = state;

        OnGameStateChanged?.Invoke(state);
    }

    public LevelData GetActualLevel()
    {
        return levels.Find(l => l.level == level);
    }

    public IEnumerator Victory(float x, float final, float timer, float torque = 200)
    {
        yield return new WaitForSeconds(x);
        GameManager.instance.SetGameState(GameState.Menu);
        Transform go = GameObject.FindGameObjectWithTag("GeneralMenu").transform;
        go.GetChild(0).gameObject.SetActive(true);
        go.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "GANASTE";
        go.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "CONTINUAR";
        go.GetChild(0).GetChild(3).gameObject.SetActive(true);
        torque = RespawnManager.instance.GetPlayer().GetComponent<VehicleController>().vehicleConfig.torque / multiplyTorque;
        EndLevelCoins.instance.GenerateCoinsEndLevel(final, torque, timer);
        go.GetChild(0).GetChild(3).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "OBJETOS: $ " + EndLevelCoins.instance.objectCoins;
        go.GetChild(0).GetChild(3).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = "TIEMPO: $ " + EndLevelCoins.instance.timerCoins;
        go.GetChild(0).GetChild(3).GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = "DESTRUCCION: $ " + EndLevelCoins.instance.destructionCoins;
        go.GetChild(0).GetChild(3).GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>().text = "TOTAL: $ " + EndLevelCoins.instance.levelCoins;
    }

    public IEnumerator Defeat(float time){
        yield return new WaitForSeconds(time);
        GameManager.instance.SetGameState(GameState.Crashed); 
        Transform go = GameObject.FindGameObjectWithTag("GeneralMenu").transform;
        go.GetChild(0).gameObject.SetActive(true);
        go.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "PERDISTE";
        go.GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text = "REINICIAR";
        go.GetChild(0).GetChild(3).gameObject.SetActive(false);
    }

    private void OnDrawGizmos() 
    {
        var l = levels.Find(l => l.level == level);
        
        foreach (var lane in l.lanes)
        {
            Gizmos.color = (lane >= 0) ? Color.blue : Color.red;
            Gizmos.DrawRay(new Vector3(lane,5,0), Vector3.forward * -50);
        }
    }

    public Level NextLevel()
    {
        int x = (int)GameManager.instance.level;
        x++;
        if (x > 4)
        {
            x = UnityEngine.Random.Range(1,5);
        }
        return (Level)x;
    }
}
