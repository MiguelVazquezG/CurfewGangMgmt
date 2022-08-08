using GangManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GangManagementSystem.API
{
    public class ProcedureApi : BaseApi
    {
        public static UserSessionDto GetUserSession(string racf)
        {
            return GetEndpoint<UserSessionDto>("Procedure/GetUserSession/" + racf);
        }
    }
}