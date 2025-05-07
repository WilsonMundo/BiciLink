using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public class ResultAPI<T>
    {
        public bool error { get; set; }
        public string message { get; set; } = null!;
        public StatusHttpResponse code { get; set; }
        public T? result { get; set; } = default(T);
        public ResultAPI() { }
        public ResultAPI(bool error)
        {
            this.error = error;
            this.message = "Error no Definido";
            this.code = StatusHttpResponse.InternalServerError;
        }
        public void  Ok(StatusHttpResponse code)
        {        
            error = false;
            this.code = code;
        }
        public void OkData(StatusHttpResponse code, T data)
        {
            error = false;
            this.code = code;
            this.result = data;
        }

    }
}
