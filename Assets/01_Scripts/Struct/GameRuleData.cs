using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FGameRuleData
{
    public bool confirmEjects;
    public int emergencyMeetings;
    public int emergencyMeetingCooldown;
    public int meetingsTime;
    public int voteTime;
    public bool anonymouseVotes;
    public float moveSpeed;
    public float crewSight;
    public float imposterSight;
    public float killCooldown;
    public EKillRange KillRange;
    public bool visualTasks;
    public ETaskBarUpdates taskBarUpdates;
    public int commonTask;
    public int complexTask;
    public int simpleTask;
}
