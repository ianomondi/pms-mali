using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Domain.Resources.Response
{
    public class BaseResponse<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public T Resource { get; private set; }

        public BaseResponse(T resource)
        {
            Success = true;
            Message = string.Empty;
            Resource = resource;
        }

        public BaseResponse(T resource, string message)
        {
            Success = true;
            Message = message;
            Resource = resource;
        }

        public BaseResponse(string message)
        {
            Success = false;
            Message = message;
            Resource = default;
        }
    }

    public static class DefaultResponseMessages
    {
        public static string SavesSuccess { get { return "Record saved successfully"; } }
        public static string UpdateSuccess { get { return "Record updated successfully"; } }
        public static string DeleteSuccess { get { return "Record deleted successfully"; } }
        public static string ListSuccess { get { return "Record(s) fetched successfully"; } }
        public static string SaveException(Exception e) {
            return $"An error occurred while saving record: {ReadException(e)}";
        }
        public static string UpdateException(Exception e) {
            return $"An error occurred while updating record: {ReadException(e)}";
        }
        public static string DeleteException(Exception e) {
            return $"An error occurred while deleting record: {ReadException(e)}";
        }
        public static string ListException(Exception e) {
            return $"An error occurred while fetching record(s): {ReadException(e)}";
        }
        private static string ReadException(Exception e) {
            var exc = e;

            while(exc.InnerException != null) exc = exc.InnerException;

            return exc.Message;
        }
    }
}
