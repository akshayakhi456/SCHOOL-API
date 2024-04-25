using System.Runtime.Serialization;

namespace School.API.Core.Models.Wrappers
{
    [DataContract]
    public class APIResponse<T> where T : class
    {
        [DataMember]
        public string version { get; set; }
        [DataMember]
        public string Message { get; set; }
        [DataMember]
        public int StatusCode { get; set; }
        [DataMember(EmitDefaultValue = false)]
        public T Result { get; set; }
        public APIResponse(int statusCode, string mesage = "", T result = null, string verison = "1.0.0") {
            this.StatusCode = statusCode;
            this.Message = mesage;
            this.Result = result;
            this.version = verison;
        }

    }
}
