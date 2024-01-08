using Microsoft.AspNetCore.Mvc.ApplicationModels;
using ProcessMining.Core.Domain.Attributes;
using ProcessMining.Core.Domain.Enums;
using ProcessMining.Infra.Tools.Extentions;
using System.Reflection;

namespace ProcessMining.EndPoint.API.Conventions
{
    public class ProcessMiningControllerModelConvention : IControllerModelConvention
    {
        private List<BaseOperations> baseOperations;
        public void Apply(ControllerModel controller)
        {
            baseOperations = new List<BaseOperations>()
            {
                BaseOperations.GetAll,
                BaseOperations.GetById,
                BaseOperations.Insert,
                BaseOperations.Update,
                BaseOperations.Delete,
            };
            bool disableCrud = controller.Attributes.Any(a => a.GetType() == typeof(DisableBaseOperationsAttribute));
            if (disableCrud)
            {
                foreach (var actionModel in controller.Actions.Where(a => baseOperations.Contains(a.ActionName.ToBaseOperation())))
                {
                    actionModel.ApiExplorer.IsVisible = false;
                }
            }
        }
    }
}
