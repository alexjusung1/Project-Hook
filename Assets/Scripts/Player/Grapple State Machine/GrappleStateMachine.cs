using ASK.Core;
using ASK.Helpers;

using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public partial class GrappleStateMachine : PlayerStateMachine<GrappleStateMachine, GrappleStateMachine.GrappleState, GrappleStateInput> {
        private SpriteRenderer _spriteR;

        //Expose to inspector
        public UnityEvent<GrappleStateMachine> OnAbilityStateChange;

        #region Overrides
        protected override void SetInitialState() 
        {
            SetState<Idle>();
        }

        protected override void Init()
        {
            base.Init();
            _spriteR = GetComponentInChildren<SpriteRenderer>();
        }

        protected void OnEnable()
        {
            StateTransition += InvokeUnityStateChangeEvent;
        }

        protected void OnDisable()
        {
            StateTransition -= InvokeUnityStateChangeEvent;
        }

        private void InvokeUnityStateChangeEvent()
        {
            OnAbilityStateChange?.Invoke(this);
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected override void Update()
        {
            base.Update();
            
            if (MyCore.Input.GrappleStarted())
            {
                CurrState.GrappleStarted();
            }

            if (MyCore.Input.GrappleFinished())
            {
                CurrState.GrappleFinished();
            }
        }

        public void CollideHorizontal() {
            CurrState.CollideHorizontal();
        }

        public void CollideVertical() {
            CurrState.CollideVertical();
        }

        public Vector2 ProcessMoveX(PlayerActor p, Vector2 velocity, int direction) {
            return CurrState.MoveX(p, velocity, direction);
        }

        public virtual Vector2 ProcessCollideHorizontal(Vector2 oldV, Vector2 newV) => CurrState.ProcessCollideHorizontal(oldV, newV);

        #endregion

        public bool IsGrappling() => IsOnState<Grappling>();

        public bool IsGrappleExtending() => IsOnState<ExtendGrapple>();

        public Vector2 GetGrappleInputPos() => MyCore.Input.GetMousePos();

        public Vector2 GetGrapplePos() => CurrInput.currentGrapplePos;

        public Vector2 GetGrappleExtendPos() => CurrInput.curGrappleExtendPos;
    }
}