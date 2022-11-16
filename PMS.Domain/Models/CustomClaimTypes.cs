using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Models
{
    public class CustomClaimTypes
    {
        public const string UserId = "UserId";
        public const string UserDisplayName = "UserDisplayName";
        public const string UserName = "UserName";


        //Permissions
        public const string PERMISSION_CAN_CREATE_COMPANY = "CAN_CREATE_COMPANY";
        public const string PERMISSION_CAN_DELETE_COMPANY = "CAN_DELETE_COMPANY";
    }

    public class RolePermissionsMapper
    {
        public const string PERMISSION_CAN_ADD_USER = "CAN_ADD_USER";
    }


    public static class RoleMapper
    {      
        public const string SUPER_ADMIN = "SUPER_ADMIN";
        public const string LANDLORD = "LANDLORD";
        public const string AGENCY = "AGENCY";
        public const string AGENT = "AGENT";
        public const string CARETAKER = "CARETAKER";
        public const string TENANT = "TENANT";
    }

    public static class PolicyMapper
    {
        public const string CAN_CREATE_COMPANY = "CAN_CREATE_COMPANY";
        public const string CAN_DELETE_COMPANY = "CAN_DELETE_COMPANY";
        public const string CAN_ROLE_ADD_USER = "ROLE_ADD_USER";
    }
}
