using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GangManagementSystem.API
{
    public class JsonResponse
    {
        public string Message = "";

        public bool Success = false;

        public long Timestamp = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);

        public object Result = null;

        public List<Exception> Errors = new List<Exception>();

        public T GetResult<T>()
        {
            if (Result == null)
                return default(T);
            else if (Result.GetType() == typeof(JObject))
                return ((JObject)Result).ToObject<T>();
            else if (Result.GetType() == typeof(JArray))
                return ((JArray)Result).ToObject<T>();
            else
                return (T)Result;
        }
    }
}