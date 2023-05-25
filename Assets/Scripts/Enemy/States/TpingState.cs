using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Enemy
{
    public class TpingState : FSMState<EnemyController>
    {
        public TpingState(EnemyController controller) : base(controller)
        {
            Transitions.Add(new FSMTransition<EnemyController>(
                isValid: () =>
                {
                    return mController.TpingEnd;
                },
                getNextState: () =>
                {
                    return new IdleState(mController);
                }
            ));
        }

        public override void OnEnter()
        {
            Debug.Log("OnEnter TpingState");
            mController.TpTime = 0f;
            mController.IsTping = true;
            mController.animator.SetTrigger("Tp");

            if (mController.Player.GetComponent<PlayerMovement>().lastKey == 'W')
            {
                mController.transform.position = mController.Player.transform.position + Vector3.up * 1.5f;
            }
            else if (mController.Player.GetComponent<PlayerMovement>().lastKey == 'A')
            {
                mController.transform.position = mController.Player.transform.position + Vector3.left * 1.5f;
            }
            else if (mController.Player.GetComponent<PlayerMovement>().lastKey == 'S')
            {
                mController.transform.position = mController.Player.transform.position + Vector3.down * 1.5f;
            }
            else if (mController.Player.GetComponent<PlayerMovement>().lastKey == 'D')
            {
                mController.transform.position = mController.Player.transform.position + Vector3.right * 1.5f;
            }
        }

        public override void OnExit()
        {
            Debug.Log("OnExit TpingState");
            mController.IsTping = false;
        }

        public override void OnUpdate(float deltaTime) { }
    }
}
