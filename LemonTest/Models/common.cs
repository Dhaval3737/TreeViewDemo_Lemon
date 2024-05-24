using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LemonTest.Models
{
    public static class common
    {
        public static object handleDBNull(this object foValue)
        {
            if (foValue == null)
            {
                return DBNull.Value;
            }
            return foValue;
        }

        public static void setAlertMessage(Controller foController, int fiType, string fsMessage)
        {
            foController.TempData["_alert.type"] = fiType;
            foController.TempData["_alert.message"] = fsMessage;
        }

        public enum MessageTypes
        {
            Error = 0,
            Success = 1,
            Info = 2,
            Warning = 3
        }
    }
}