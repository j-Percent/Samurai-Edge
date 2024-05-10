using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RhythmManagerOne : MonoBehaviour {

    [SerializeField] AK.Wwise.Event musicEvent;
    uint playingID;

    public GameObject Player;
    public GameObject Enemy;

    public GameObject DoorLeft;
    public GameObject DoorRight;
    public GameObject Sun;
    public GameObject Temple;
    public GameObject DefenseMeter;
    public GameObject PlayerDefense;
    public GameObject EnemyDefense;
    public GameObject LeftPrompt;
    public GameObject LeftSheath;
    public GameObject RightPrompt;
    public GameObject RightSheath;
    public GameObject Countdown;

    [SerializeField] float beatDuration;
    [SerializeField] float barDuration;
    [SerializeField] bool durationSet = false;

    public float _doorL;
    public float _doorR;
    public float _sun;
    public float _temple;
    public float _defenseMeter;
    public float _defense;
    public float _prompts;
    public float _leftPromptDown;
    public float _leftPromptAway;
    public float _leftPromptSpeed;
    public float _rightPromptDown;
    public float _rightPromptAway;
    public float _rightPromptSpeed;


    #region Additional Parameters
    [SerializeField] SpawnObjectOne spawnObject;

    [SerializeField] Transform spawnLocation;

    public int OkWindowMillis = 200;
    public int GoodWindowMillis = 100;
    public int PerfectWindowMillis = 50;

    public List<SpawnObjectOne> spawnObjectList = new List<SpawnObjectOne>();

    #endregion

    #region Setup
    private void Start()
    {
        playingID = musicEvent.Post(gameObject, (uint)(AkCallbackType.AK_MusicSyncAll | AkCallbackType.AK_EnableGetMusicPlayPosition | AkCallbackType.AK_MusicSyncUserCue | AkCallbackType.AK_MIDIEvent), CallbackFunction);
        _sun = -10;
        _temple = -6;
        _defenseMeter = 3f;
        _defense = 6.21f;
        _prompts = 0;
        _leftPromptDown = 5f;
        _leftPromptAway = 0f;
        _leftPromptSpeed = 0.006f;
        _rightPromptDown = -5f;
        _rightPromptAway = 0f;
        _rightPromptSpeed = 0.006f;
    }
    #endregion

    private void Update()
    {
        _lerpAll();
    }

    private void _lerpAll ()
    {
        DoorLeft.transform.position = Vector2.Lerp(DoorLeft.transform.position, new Vector2(_doorL, DoorLeft.transform.position.y), 0.003f);
        DoorRight.transform.position = Vector2.Lerp(DoorRight.transform.position, new Vector2(_doorR, DoorRight.transform.position.y), 0.003f);
        Sun.transform.position = Vector2.Lerp(Sun.transform.position, new Vector2(Sun.transform.position.x, _sun), 0.0008f);
        Temple.transform.position = Vector2.Lerp(Temple.transform.position, new Vector2(Temple.transform.position.x, _temple), 0.006f);
        DefenseMeter.transform.position = Vector2.Lerp(DefenseMeter.transform.position, new Vector2(DefenseMeter.transform.position.x, _defenseMeter), 0.006f);
        PlayerDefense.transform.position = Vector2.Lerp(PlayerDefense.transform.position, new Vector2(PlayerDefense.transform.position.x, _defense), 0.006f);
        EnemyDefense.transform.position = Vector2.Lerp(EnemyDefense.transform.position, new Vector2(EnemyDefense.transform.position.x, _defense), 0.006f);
        LeftSheath.transform.position = Vector2.Lerp(LeftSheath.transform.position, new Vector2(_leftPromptAway, _leftPromptDown), _leftPromptSpeed);
        LeftPrompt.transform.position = Vector2.Lerp(LeftPrompt.transform.position, new Vector2(-_leftPromptAway, _leftPromptDown), _leftPromptSpeed);
        RightSheath.transform.position = Vector2.Lerp(RightSheath.transform.position, new Vector2(-_rightPromptAway, _rightPromptDown), _rightPromptSpeed);
        RightPrompt.transform.position = Vector2.Lerp(RightPrompt.transform.position, new Vector2(_rightPromptAway, _rightPromptDown), _rightPromptSpeed);
    }

    void CallbackFunction(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info) {
        AkMusicSyncCallbackInfo musicInfo;

        if (in_info is AkMusicSyncCallbackInfo) {
            musicInfo = (AkMusicSyncCallbackInfo)in_info;
            switch (in_type) {

                case AkCallbackType.AK_MusicSyncUserCue:
                    ManageUserCue(musicInfo.userCueName);
                    break;

                case AkCallbackType.AK_MusicSyncBeat:
                    //HERE IS WHERE YOU CAN DO SOMETHING ON THE BEAT
                    //OnTheBeat();
                    break;

                case AkCallbackType.AK_MusicSyncBar:
                    //HERE IS WHERE YOU CAN DO SOMETHING ON THE BAR
                    //OnTheBar();
                    break;
            }

            if (in_type is AkCallbackType.AK_MusicSyncBar) {
                if (!durationSet) {
                    beatDuration = musicInfo.segmentInfo_fBeatDuration;
                    barDuration = musicInfo.segmentInfo_fBarDuration;
                    durationSet = true;
                }
            }
        }
    }
    void ManageUserCue(string s)
    {
        switch (s)
        {
            case "a":
                //Debug.Log("Enemy Attack");
                Enemy.GetComponent<Enemy>()._attack();
                break;
            case "b":
                //Debug.Log("Enemy Block");
                Enemy.GetComponent<Enemy>()._block();
                break;

            case "w":
                //Debug.Log("Enemy Wait");
                Enemy.GetComponent<Enemy>()._wait();
                break;
            case "doors":
                Debug.Log("Doors Open");
                _doorL = -11;
                _doorR = 11;
                break;
            case "sun":
                Debug.Log("Sun");
                _sun = 0;
                break;
            case "temple":
                Debug.Log("Temple");
                _temple = -1f;
                break;
            case "playerIntro":
                Debug.Log("Player");
                Player.GetComponent<Player>()._playerEnter = true;
                Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>()._plStatic;
                break;
            case "enemyIntro":
                Debug.Log("Enemy");
                Enemy.GetComponent<Enemy>()._enemyEnter = true;
                Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyStatic;
                break;
            case "attackPrompt":
                Debug.Log("Attack Prompt Down");
                _leftPromptDown = 0;
                Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>()._plAttack;
                Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyHit;
                Player.GetComponent<Player>()._xStart += 0.2f;
                Enemy.GetComponent<Enemy>()._xStart += 0.2f;
                break;
            case "attackPromptOpen":
                Debug.Log("Attack Prompt Open");
                _leftPromptAway = 5;
                Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>()._plStatic;
                Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyStatic;
                break;
            case "attackPromptSlow":
                Debug.Log("Attack Prompt Slow");
                _leftPromptSpeed = 0.0002f;
                _leftPromptAway = 8;
                Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>()._plBlocked;
                Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyDefend;
                Player.GetComponent<Player>()._xStart += 0.05f;
                Enemy.GetComponent<Enemy>()._xStart += 0.05f;
                break;
            case "attackPromptAway":
                Debug.Log("Attack Prompt Away");
                _leftPromptSpeed = 0.006f;
                _leftPromptAway = 0;
                _leftPromptDown = 5;
                Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>()._plStatic;
                Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyStatic;
                break;
            case "defensePrompt":
                Debug.Log("Defense Prompt Down");
                _rightPromptDown = 0;
                Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>()._plHit;
                Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyAttack;
                Player.GetComponent<Player>()._xStart -= 0.2f;
                Enemy.GetComponent<Enemy>()._xStart -= 0.2f;
                break;
            case "defensePromptOpen":
                Debug.Log("Defense Prompt Open");
                _rightPromptAway = 5;
                Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>()._plStatic;
                Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyStatic;
                break;
            case "defensePromptSlow":
                Debug.Log("Defense Prompt Slow");
                _rightPromptSpeed = 0.0002f;
                _rightPromptAway = 8;
                Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>()._plDefend;
                Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyBlocked;
                Player.GetComponent<Player>()._xStart -= 0.05f;
                Enemy.GetComponent<Enemy>()._xStart -= 0.05f;
                break;
            case "defensePromptAway":
                Debug.Log("Defense Prompt Away");
                _rightPromptSpeed = 0.006f;
                _rightPromptAway = 0;
                _rightPromptDown = -5;
                Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>()._plStatic;
                Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyStatic;
                break;
            case "reset":
                Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>()._plStatic;
                Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyStatic;
                break;
            case "fakeb":
                Debug.Log("fakeblock");
                Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>()._plDefend;
                Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyAttack;
                Player.GetComponent<Player>()._xStart -= 0.05f;
                Enemy.GetComponent<Enemy>()._xStart -= 0.05f;
                break;
            case "faked":
                Debug.Log("fakedeflect");
                Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>()._plBlocked;
                Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyBlocked;
                Player.GetComponent<Player>()._xStart -= 0.05f;
                Enemy.GetComponent<Enemy>()._xStart -= 0.05f;
                break;
            case "fakea":
                Debug.Log("fakeattack");
                Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>()._plAttack;
                Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyDefend;
                Player.GetComponent<Player>()._xStart += 0.05f;
                Enemy.GetComponent<Enemy>()._xStart += 0.05f;
                break;
            case "fakedb":
                Debug.Log("fakedeflectback");
                Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>()._plBlocked;
                Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyBlocked;
                Player.GetComponent<Player>()._xStart += 0.05f;
                Enemy.GetComponent<Enemy>()._xStart += 0.05f;
                break;
            case "cross":
                Debug.Log("cross swords");
                Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>()._plAttack;
                Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyAttack;
                break;
            case "set3":
                Debug.Log("3");
                Countdown.GetComponent<Countdown>()._set3();
                Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>()._plAttack;
                Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyAttack;
                break;
            case "set2":
                Debug.Log("2");
                Countdown.GetComponent<Countdown>()._set2();
                Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>()._plAttack;
                Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyAttack;
                break;
            case "set1":
                Debug.Log("1");
                Countdown.GetComponent<Countdown>()._set1();
                Player.GetComponent<SpriteRenderer>().sprite = Player.GetComponent<Player>()._plAttack;
                Enemy.GetComponent<SpriteRenderer>().sprite = Enemy.GetComponent<Enemy>()._enemyAttack;
                break;
            case "set0":
                Debug.Log("0");
                Countdown.GetComponent<Countdown>()._set0();
                break;
            case "destroyCount":
                Debug.Log("Destroy Countdown");
                Countdown.GetComponent<Countdown>()._destroy();
                break;
            case "gameStart":
                Debug.Log("GameStart");
                Player.GetComponent<Player>()._gameStart = true;
                break;
        }
    }

    /// <summary>
    /// Here is an example function of a place where you can do something that reacts on the beat
    /// </summary>
    void OnTheBeat() {
        Debug.Log("Here is a beat event!");
    }

    /// <summary>
    /// Here is an example function of a place where you can do something that reacts on the bar
    /// </summary>
    void OnTheBar() {
        Debug.Log("Here is a bar event!");
    }


    #region Other
    public int GetMusicTimeInMS() {
        AkSegmentInfo segmentInfo = new AkSegmentInfo();
        AkSoundEngine.GetPlayingSegmentInfo(playingID, segmentInfo, true);
        return segmentInfo.iCurrentPosition;
    }

    //We're going to call this when we spawn a gem, in order to determine when it's crossing time should be
    //Crossing time is based on the current playback position, our beat duration, and our beat offset
    public int SetCrossingTimeInMS(int beatOffset) {
        AkSegmentInfo segmentInfo = new AkSegmentInfo();
        AkSoundEngine.GetPlayingSegmentInfo(playingID, segmentInfo, true);
        int offsetTime = Mathf.RoundToInt(1000 * beatDuration * beatOffset);
        //Debug.Log($"Crossing time: {segmentInfo.iCurrentPosition + offsetTime}");
        return segmentInfo.iCurrentPosition + offsetTime;
    }

    //void SpawnItem(AkMusicSyncCallbackInfo info) {
    //    int spawnTime = GetMusicTimeInMS();
    //    SpawnObjectOne s = Instantiate(spawnObject, new Vector3(spawnLocation.position.x, 0.0f, spawnLocation.position.z), Quaternion.identity);
    //    s.SetSpawnItemInfo(this, spawnTime, SetCrossingTimeInMS(1));
    //    s.name = $"{s.name} | {spawnTime}";
    //    spawnObjectList.Insert(0, s);
    //}
    #endregion

}

public enum HitTiming { None, Perfect, Good, Okay, Late, Miss }