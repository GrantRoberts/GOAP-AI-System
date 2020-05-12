using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FSMState
{
	void Update(FiniteStateMachine fsm, GameObject gameObject);
}