using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
    [TaskCategory("Basic/Transform")]
    [TaskDescription("Stores the position of the Transform. Returns Success.")]
    public class GetPosition : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("Can the target GameObject be empty?")]
        public SharedBool allowEmptyTarget;
        [Tooltip("The position of the Transform")]
        [RequiredField]
        public SharedVector3 storeValue;

        private Transform targetTransform;
        private GameObject prevGameObject;

        public override void OnStart()
        {
            if (!allowEmptyTarget.Value) {
                var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
                if (currentGameObject != prevGameObject) {
                    targetTransform = currentGameObject.GetComponent<Transform>();
                    prevGameObject = currentGameObject;
                }
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (targetTransform == null) {
                UnityEngine.Debug.LogWarning("Transform is null");
                return TaskStatus.Failure;
            }

            storeValue.Value = targetTransform.position;

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            allowEmptyTarget = false;
            storeValue = Vector3.zero;
        }
    }
}