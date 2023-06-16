using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Systems
{
    [Serializable, BluePrint]
    [RequiredAtContainer(typeof(InputActionsComponent))]
    public class InputListenController 
    {
        private List<UpdateableAction> actions = new List<UpdateableAction>();
        private ConcurrencyList<IEntity> inputListeners;
        private HECSMask InputListenerTagComponentMask = HMasks.GetMask<InputListenerTagComponent>();

        public int Priority { get; } = -5;

        public override void InitSystem()
        {
            inputListeners = EntityManager.Filter(InputListenerTagComponentMask);
            LinkActions();
        }

        private void LinkActions()
        {
            var actionsComponent = Owner.GetHECSComponent<InputActionsComponent>();
            var defaultActionMap = actionsComponent.Actions.actionMaps[0];
            defaultActionMap.Enable();

            foreach (var action in defaultActionMap.actions)
            {
                var neededIndex = actionsComponent.InputActionSettings.FirstOrDefault(x => x.ActionName == action.name);
                action.Enable();
                var updateableAction = new UpdateableAction(neededIndex.Identifier.Id, action);
                updateableAction.OnStart += OnActionStart;
                updateableAction.OnEnd += OnActionEnd;
                updateableAction.OnUpdate += OnActionUpdate;
                actions.Add(updateableAction);
            }
        }

        private void OnActionStart(int index, InputAction.CallbackContext context)
        {
            var command = new InputStartedCommand { Index = index, Context = context };
            SendCommandToAllListeners(command);
        }

        private void OnActionUpdate(int index, InputAction.CallbackContext context)
        {
            var command = new InputCommand { Index = index, Context = context };
            SendCommandToAllListeners(command);
        }

        public void OnActionEnd(int index, InputAction.CallbackContext context)
        {
            var command = new InputEndedCommand { Index = index,  Context = context };
            SendCommandToAllListeners(command);
        }

        private void SendCommandToAllListeners<T>(T command) where T : struct, IGlobalCommand
        {
            if (!EntityManager.IsAlive) return;

            foreach (var w in EntityManager.Worlds)
            {
                var collection = w.Filter(InputListenerTagComponentMask);
                var lenght = collection.Count;
                for (int i = 0; i < lenght; i++)
                {
                    IEntity listener = collection.Data[i];
                    listener.Command(command);
                }
                //w.Command(command);
            }
        }

        public override void Dispose()
        {
            foreach (var action in actions)
                action.Dispose();
        }

        public void PriorityUpdateLocal()
        {
            foreach (var action in actions)
                action.UpdateAction();
        }
    }
}