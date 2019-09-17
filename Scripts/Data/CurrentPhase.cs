/// <summary>
/// Enums for the current attempt phase of a lift.
/// </summary>
/// 
public enum CurrentPhaseSquat
{
    /// <summary>
    /// Squat none phase.
    /// </summary>
    SquatNone = -1,

    /// <summary>
    /// Squat "squat/start" phase.
    /// </summary>
    SquatStart = 0,

    /// <summary>
    /// Squat sticking phase.
    /// </summary>
    SquatSticking = 1,

    /// <summary>
    /// Squat "rack" phase.
    /// </summary>
    SquatRack = 2,

    /// <summary>
    /// Squat phase ended.
    /// </summary>
    SquatEnded = 3

}

public enum CurrentPhaseBench
{
    /// <summary>
    /// Bench no phase.
    /// </summary>
    BenchNone = -1,

    /// <summary>
    /// Bench "start" phase.
    /// </summary>
    BenchStart = 0,

    /// <summary>
    /// Bench "press" phase.
    /// </summary>
    BenchPress = 1,

    /// <summary>
    /// Bench sticking phase.
    /// </summary>
    BenchSticking = 2,

    /// <summary>
    /// Bench "rack" phase.
    /// </summary>
    BenchRack = 3,

    /// <summary>
    /// Bench phase ended.
    /// </summary>
    BenchEnded = 4
}

public enum CurrentPhaseDeadlift
{
    /// <summary>
    /// Deadlift none phase.
    /// </summary>
    DeadliftNone = -1,

    /// <summary>
    /// Deadlift breaking floor phase.
    /// </summary>
    DeadliftStart = 0,

    /// <summary>
    /// Deadlift sticking phase.
    /// </summary>
    DeadliftSticking = 1,

    /// <summary>
    /// Deadlift "down" phase.
    /// </summary>
    DeadliftDown = 2,

    /// <summary>
    /// Squat phase ended.
    /// </summary>
    DeadliftEnded = 3

}
