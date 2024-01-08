using Microsoft.AspNetCore.Mvc.ApplicationModels;
using ProcessMining.Core.Domain.Attributes;
using ProcessMining.Core.Domain.Attributes.Conventions;
using ProcessMining.Core.Domain.Enums;
using ProcessMining.EndPoint.API.Controllers;
using System;
using System.Reflection;

namespace ProcessMining.EndPoint.API.Conventions
{
    public class DisableControllerConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {

        }
    }
}
