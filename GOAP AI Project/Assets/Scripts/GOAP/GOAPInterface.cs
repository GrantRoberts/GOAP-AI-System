using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GOAPInterface
{
	/// <summary>
	/// Get the starting world state.
	/// </summary>
	/// <returns>The world state.</returns>
	HashSet<KeyValuePair<string, object>> GetWorldState();

	/// <summary>
	/// Give the planner a new goal.
	/// </summary>
	/// <returns>The goal that was created.</returns>
	HashSet<KeyValuePair<string, object>> CreateGoalState();

	/// <summary>
	/// If the plan failed
	/// </summary>
	/// <param name="failedGoal">The goal that failed.</param>
	void PlanFailed(HashSet<KeyValuePair<string, object>> failedGoal);

	/// <summary>
	/// A plan was created.
	/// </summary>
	/// <param name="goal">The goal that was created.</param>
	/// <param name="actions">The actions to achieve that goal.</param>
	void PlanFound(HashSet<KeyValuePair<string, object>> goal, Queue<GOAPAction> actions);

	/// <summary>
	/// All actions are complete.
	/// </summary>
	void ActionsFinished();

	/// <summary>
	/// The plan was aborted by an action.
	/// </summary>
	/// <param name="aborter">The action that aborted the plan.</param>
	void PlanAborted(GOAPAction aborter);

	/// <summary>
	/// Move the agent towards the target.
	/// </summary>
	/// <param name="nextAction">The next action to take after reaching the target.</param>
	/// <returns>If the agent is at the target.</returns>
	bool MoveAgent(GOAPAction nextAction);
}