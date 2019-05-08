using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Basic.UnityTransform
{
    [TaskCategory("Basic/Transform")]
    [TaskDescription("Sets the local euler angles of the Transform. Returns Success.")]
    public class SetLocalEulerAngles : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("The local euler angles of the Transform")]
        public SharedVector3 localEulerAngles;

        private Transform targetTransform;
        private GameObject prevGameObject;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                targetTransform = currentGameObject.GetComponent<Transform>();
                prevGameObject = currentGameObject;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (targetTransform == null) {
                UnityEngine.Debug.LogWarning("Transform is null");
                return TaskStatus.Failure;
            }

            targetTransform.localEulerAngles = localEulerAngles.Value;

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            localEulerAngles = Vector3.zero;
        }
    }
}