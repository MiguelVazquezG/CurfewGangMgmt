using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GangManagementSystem.Views.ViewModels
{
    public class HomeViewModel
    {
        public bool Authorized { get; set; } = false;
        public string env { get; set; } = string.Empty;
        public string racfid { get; set; } = string.Empty;
    }
}