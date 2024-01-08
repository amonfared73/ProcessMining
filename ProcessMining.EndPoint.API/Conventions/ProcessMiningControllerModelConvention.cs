using Microsoft.AspNetCore.Mvc.ApplicationModels;
using ProcessMining.Core.Domain.Attributes;
using ProcessMining.Core.Domain.Enums;
using ProcessMining.Infra.Tools.Extentions;
using System.Reflection;

namespace ProcessMining.EndPoint.API.Conventions
{
    public class ProcessMiningControllerModelConvention : IControllerModelConvention
    {
        private List<string> baseOperations;
        public void Apply(ControllerModel controller)
        {
            baseOperations = new List<string>()
            {
                "GetAll",
                "GetById",
                "Insert",
                "Update",
                "Delete",
            };
            bool disableCrud = controller.Attributes.Any(a => a.GetType() == typeof(DisableBaseOperationsAttribute));
            if (disableCrud)
            {
                var crudActions = controller.Actions.Where(a => baseOperations.Contains(a.ActionName));
                foreach (var action in crudActions)
                {
                    action.ApiExplorer.IsVisible = false;
                }
            }
        }
    }
}
