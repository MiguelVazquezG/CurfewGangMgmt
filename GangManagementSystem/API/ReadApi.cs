using GangManagementSystem.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GangManagementSystem.API
{
    public class ReadApi : BaseApi
    {
        public static List<GangsPGDto> GetGangsPG()
        {
            return GetEndpoint<List<GangsPGDto>>("Read/GangPG");
        }

        public static List<GangTypePGDto> GangTypePG()
        {
            return GetEndpoint<List<GangTypePGDto>>("Read/GangTypePG");
        }

        public static List<GangsMWSDto> GetGangsMWS()
        {
            return GetEndpoint<List<GangsMWSDto>>("Read/GangMWS");
        }

        public static List<GangTypeMWSDto> GetGangTypeMWS()
        {
            return GetEndpoint<List<GangTypeMWSDto>>("Read/GangTypeMWS");
        }

        public static List<GangsPGDto> GetGangDetailsById(string id)
        {
            return GetEndpoint<List<GangsPGDto>>("Read/GangDetailsById/" + id);
        }
    }
}