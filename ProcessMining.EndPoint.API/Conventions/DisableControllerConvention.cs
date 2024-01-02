using Microsoft.AspNetCore.Mvc.ApplicationModels;
using ProcessMining.Core.Domain.Attributes.Conventions;

namespace ProcessMining.EndPoint.API.Conventions
{
    public class DisableControllerConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (IsConventionApplicable(controller))
            {
                var multipliedActions = new List<ActionModel>();
                foreach (var action in controller.Actions)
                {
                    var existingAction = action;
                    var controllerAction = new ActionModel(existingAction);
                    controllerAction.ActionName = controllerAction.ActionName;

                    multipliedActions.Add(controllerAction);
                }
                foreach (var action in multipliedActions)
                {
                    controller.Actions.Add(action);
                }
            }
        }
        private bool IsConventionApplicable(ControllerModel controller)
        {
            return controller.Attributes.OfType<IDisableControllerConvention>().Any();
        }

    }
}
