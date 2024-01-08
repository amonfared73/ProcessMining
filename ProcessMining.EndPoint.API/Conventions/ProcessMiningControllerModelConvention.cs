using Microsoft.AspNetCore.Mvc.ApplicationModels;
using ProcessMining.Core.Domain.Attributes;
using System.Reflection;

namespace ProcessMining.EndPoint.API.Conventions
{
    public class ProcessMiningControllerModelConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.Attributes.Any(a => a.GetType().IsDefined(typeof(DisableBaseOperationsAttribute))))
            {
                foreach (var actionModel in controller.Actions)
                {
                    if(actionModel.ActionName == "GetAll")
                        actionModel.ApiExplorer.IsVisible = false;
                }
            }
        }
    }
}
