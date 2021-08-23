using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class GameRuleStore : NetworkBehaviour
{
    [SerializeField] private Toggle bIsRecommendRuleToggle;
    [SerializeField] private Toggle confirmEjectsToggle;
    [SerializeField] private Text emergencyMeetingsText;
    [SerializeField] private Text emergencyMeetingCooldownText;
    [SerializeField] private Text meetingsTimeText;
    [SerializeField] private Text voteTimeText;
    [SerializeField] private Toggle anonymouseVotesToggle;
    [SerializeField] private Text moveSpeedText;
    [SerializeField] private Text crewSightText;
    [SerializeField] private Text imposterSightText;
    [SerializeField] private Text killCooldownText;
    [SerializeField] private Text killRangeText;
    [SerializeField] private Toggle visualTasksToggle;
    [SerializeField] private Text taskBarUpdatesText;
    [SerializeField] private Text commonTaskText;
    [SerializeField] private Text complexTaskText;
    [SerializeField] private Text simpleTaskText;
    [SerializeField] private Text gameRuleOverview;
    
    // ---------------------------------------------- SyncVar ---------------------------------------------
    
    [SyncVar(hook = nameof(SetIsRecommendRule_Hook))] private bool bIsRecommendRule;
    [SyncVar(hook = nameof(SetConfirmEjects_Hook))] private bool confirmEjects;
    [SyncVar(hook = nameof(SetEmergencyMeetings_Hook))] private int emergencyMeetings;
    [SyncVar(hook = nameof(SetEmergencyMeetingCooldown_Hook))] private int emergencyMeetingCooldown;
    [SyncVar(hook = nameof(SetMeetingsTime_Hook))] private int meetingsTime;
    [SyncVar(hook = nameof(SetVoteTime_Hook))] private int voteTime;
    [SyncVar(hook = nameof(SetAnonymouseVotes_Hook))] private bool anonymouseVotes;
    [SyncVar(hook = nameof(SetMoveSpeed_Hook))] private float moveSpeed;
    [SyncVar(hook = nameof(SetCrewSight_Hook))] private float crewSight;
    [SyncVar(hook = nameof(SetImposterSight_Hook))] private float imposterSight;
    [SyncVar(hook = nameof(SetKillColldown_Hook))] private float killCooldown;
    [SyncVar(hook = nameof(SetKillRange_Hook))] private EKillRange killRange;
    [SyncVar(hook = nameof(SetVisualTasks_Hook))] private bool visualTasks;
    [SyncVar(hook = nameof(SetTaskBarUpdates_Hook))] private ETaskBarUpdates taskBarUpdates;
    [SyncVar(hook = nameof(SetCommonTask_Hook))] private int commonTask;
    [SyncVar(hook = nameof(SetComplexTask_Hook))] private int complexTask;
    [SyncVar(hook = nameof(SetSimpleTask_Hook))] private int simpleTask;
    [SyncVar(hook = nameof(SetImposterCount_Hook))] private int imposterCount;
    
    // --------------------------------------------------------------------------------------------------------
    // ---------------------------------------------- Method --------------------------------------------------
    // --------------------------------------------------------------------------------------------------------
    
    void Start()
    {
        if (isServer)
        {
            var manager = NetworkManager.singleton as RoomManager;
            imposterCount = manager.imposterCount;
            anonymouseVotes = false;
            taskBarUpdates = ETaskBarUpdates.Always;
            
            SetRecommendGameRule();
        }
    }
    
    // ----------------------------------------- Custom Method -----------------------------------------
    
    public void UpdateGameRuleOverview()
    {
        var manager = NetworkManager.singleton as RoomManager;
        StringBuilder sb = new StringBuilder(bIsRecommendRule ? "추천 설정\n" : "커스텀 설정\n");
        sb.Append("맵 : The Skeld\n");
        sb.Append($"#임포스터 : {imposterCount}\n");
        sb.Append(string.Format("Confirm Ejects: {0}\n", confirmEjects ? "켜짐" : "꺼짐"));
        sb.Append($"긴급 회의 : {emergencyMeetings}\n");
        sb.Append(string.Format("Anonymous Votes : {0}\n", anonymouseVotes ? "켜짐" : "꺼짐"));
        sb.Append($"긴급 회의 쿨타임: {emergencyMeetingCooldown}\n");
        sb.Append($"회의 제한 시간 : {meetingsTime}\n");
        sb.Append($"투표 제한 시간 : {voteTime}\n");
        sb.Append($"이동 속도 : {moveSpeed}\n");
        sb.Append($"크루원 시야 : {crewSight}\n");
        sb.Append($"임포스터 시야 : {imposterSight}\n");
        sb.Append($"킬 쿨타임 : {killCooldown}\n");
        sb.Append($"킬 범위 : {killRange}\n");
        sb.Append($"Task Bar Updates : {taskBarUpdates}\n");
        sb.Append(string.Format("Visual Tasks : {0}\n", visualTasks ? "켜짐" : "꺼짐"));
        sb.Append($"공통 임무 : {commonTask}\n");
        sb.Append($"복잡한 임무 : {complexTask}\n");
        sb.Append($"간단한 임무 : {simpleTask}\n");
        gameRuleOverview.text = sb.ToString();
    }

    private void SetRecommendGameRule()
    {
        bIsRecommendRule = true;
        confirmEjects = true;
        emergencyMeetings = 1;
        emergencyMeetingCooldown = 15;
        meetingsTime = 15;
        voteTime = 120;
        moveSpeed = 1.0f;
        crewSight = 1.0f;
        imposterSight = 1.5f;
        killCooldown = 45.0f;
        killRange = EKillRange.Normal;
        visualTasks = true;
        commonTask = 1;
        complexTask = 1;
        simpleTask = 2;
    }
    
    // ---------------------------------------------- Hook ---------------------------------------------

    public void SetIsRecommendRule_Hook(bool _, bool value)
    {
        UpdateGameRuleOverview();
    }

    public void SetConfirmEjects_Hook(bool _, bool value)
    {
        UpdateGameRuleOverview();
    }

    public void SetAnonymouseVotes_Hook(bool _, bool value)
    {
        UpdateGameRuleOverview();
    }

    public void SetVisualTasks_Hook(bool _, bool value)
    {
        UpdateGameRuleOverview();
    }

    public void SetEmergencyMeetings_Hook(int _, int value)
    {
        emergencyMeetingsText.text = value.ToString();
        UpdateGameRuleOverview();
    }

    public void SetEmergencyMeetingCooldown_Hook(int _, int value)
    {
        emergencyMeetingCooldownText.text = string.Format("{0}s", value);
        UpdateGameRuleOverview();
    }

    public void SetMeetingsTime_Hook(int _, int value)
    {
        meetingsTimeText.text = string.Format("{0}s", value);
        UpdateGameRuleOverview();
    }

    public void SetVoteTime_Hook(int _, int value)
    {
        voteTimeText.text = string.Format("{0}s", value);
        UpdateGameRuleOverview();
    }

    public void SetMoveSpeed_Hook(float _, float value)
    {
        moveSpeedText.text = string.Format("{0:0.0}x", value);
        UpdateGameRuleOverview();
    }

    public void SetCrewSight_Hook(float _, float value)
    {
        crewSightText.text = string.Format("{0:0.0}x", value);
        UpdateGameRuleOverview();
    }
    
    public void SetImposterSight_Hook(float _, float value)
    {
        imposterSightText.text = string.Format("{0:0.0}x", value);
        UpdateGameRuleOverview();
    }
    
    public void SetKillColldown_Hook(float _, float value)
    {
        killCooldownText.text = string.Format("{0:0.0}x", value);
        UpdateGameRuleOverview();
    }

    public void SetKillRange_Hook(EKillRange _, EKillRange value)
    {
        killRangeText.text = value.ToString();
        UpdateGameRuleOverview();
    }

    public void SetTaskBarUpdates_Hook(ETaskBarUpdates _, ETaskBarUpdates value)
    {
        taskBarUpdatesText.text = value.ToString();
        UpdateGameRuleOverview();
    }

    public void SetCommonTask_Hook(int _, int value)
    {
        commonTaskText.text = value.ToString();
        UpdateGameRuleOverview();
    }

    public void SetComplexTask_Hook(int _, int value)
    {
        complexTaskText.text = value.ToString();
        UpdateGameRuleOverview();
    }

    public void SetSimpleTask_Hook(int _, int value)
    {
        simpleTaskText.text = value.ToString();
        UpdateGameRuleOverview();
    }

    public void SetImposterCount_Hook(int _, int value)
    {
        UpdateGameRuleOverview();
    }
  
    // ---------------------------------------- On Toggle / Change ------------------------------------------
    
    public void OnRecommendToggle(bool value)
    {
        bIsRecommendRule = value;
        if (bIsRecommendRule)
        {
            SetRecommendGameRule();
        }
    }

    public void OnConfirmEjectsToggle(bool value)
    {
        bIsRecommendRule = false;
        bIsRecommendRuleToggle.isOn = false;
        confirmEjects = value;
    }

    public void OnAnonymouseVotesToggle(bool value)
    {
        bIsRecommendRule = false;
        bIsRecommendRuleToggle.isOn = false;
        anonymouseVotes = value;
    }

    public void OnVisualTasksToggle(bool value)
    {
        bIsRecommendRule = false;
        bIsRecommendRuleToggle.isOn = false;
        visualTasks = value;
    }

    public void OnChangeEmergencyMeetings(bool isPlus)
    {
        emergencyMeetings = Mathf.Clamp(emergencyMeetings + (isPlus ? 1 : -1), 0, 9);
        bIsRecommendRule = false;
        bIsRecommendRuleToggle.isOn = false;
    }
    
    public void OnChangeEmergencyMeetingCooldown(bool isPlus)
    {
        emergencyMeetingCooldown = Mathf.Clamp(emergencyMeetingCooldown + (isPlus ? 5 : -5), 0, 60);
        bIsRecommendRule = false;
        bIsRecommendRuleToggle.isOn = false;
    }
    
    public void OnChangemMeetingsTime(bool isPlus)
    {
        meetingsTime = Mathf.Clamp(meetingsTime + (isPlus ? 5 : -5), 0, 120);
        bIsRecommendRule = false;
        bIsRecommendRuleToggle.isOn = false;
    }
    
    public void OnChangeVoteTime(bool isPlus)
    {
        voteTime = Mathf.Clamp(voteTime + (isPlus ? 5 : -5), 0, 300);
        bIsRecommendRule = false;
        bIsRecommendRuleToggle.isOn = false;
    }
    
    public void OnChangeMoveSpeed(bool isPlus)
    {
        moveSpeed = Mathf.Clamp(moveSpeed + (isPlus ? 0.25f : -0.25f), 0.5f, 3.0f);
        bIsRecommendRule = false;
        bIsRecommendRuleToggle.isOn = false;
    }
    
    public void OnChangeCrewSight(bool isPlus)
    {
        crewSight = Mathf.Clamp(crewSight + (isPlus ? 0.25f : -0.25f), 0.25f, 5.0f);
        bIsRecommendRule = false;
        bIsRecommendRuleToggle.isOn = false;
    }
    
    public void OnChangeImposterSight(bool isPlus)
    {
        imposterSight = Mathf.Clamp(imposterSight + (isPlus ? 0.25f : -0.25f), 0.25f, 5.0f);
        bIsRecommendRule = false;
        bIsRecommendRuleToggle.isOn = false;
    }
    
    public void OnChangeKillCooldown(bool isPlus)
    {
        killCooldown = Mathf.Clamp(killCooldown + (isPlus ? 2.5f : -2.5f), 10.0f, 60.0f);
        bIsRecommendRule = false;
        bIsRecommendRuleToggle.isOn = false;
    }
    
    public void OnChangeKillRange(bool isPlus)
    {
        killRange = (EKillRange) Mathf.Clamp((int) killRange + (isPlus ? 1 : -1), 0, 2);
        bIsRecommendRule = false;
        bIsRecommendRuleToggle.isOn = false;
    }
    
    public void OnChangeTaskBarUpdates(bool isPlus)
    {
        taskBarUpdates = (ETaskBarUpdates) Mathf.Clamp((int) taskBarUpdates + (isPlus ? 1 : -1), 0, 2);
        bIsRecommendRule = false;
        bIsRecommendRuleToggle.isOn = false;
    }
    
    public void OnChangeCommonTask(bool isPlus)
    {
        commonTask = Mathf.Clamp(commonTask + (isPlus ? 1 : -1), 0, 2);
        bIsRecommendRule = false;
        bIsRecommendRuleToggle.isOn = false;
    }
    
    public void OnChangeComplexTask(bool isPlus)
    {
        complexTask = Mathf.Clamp(complexTask + (isPlus ? 1 : -1), 0, 3);
        bIsRecommendRule = false;
        bIsRecommendRuleToggle.isOn = false;
    }
    
    public void OnChangeSimpleTask(bool isPlus)
    {
        simpleTask = Mathf.Clamp(simpleTask + (isPlus ? 1 : -1), 0, 5);
        bIsRecommendRule = false;
        bIsRecommendRuleToggle.isOn = false;
    }
    
}
