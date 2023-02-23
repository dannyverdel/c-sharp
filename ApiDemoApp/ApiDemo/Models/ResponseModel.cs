using System;
using System.Net;

namespace ApiDemo.Models
{
				public class ResponseModel<T>
				{
								public HttpStatusCode StatusCode { get; set; }
								private T? Data { get; set; }
								public string? ErrorMessage { get; set; }

								public ResponseModel(HttpStatusCode status_code, T data) {
												StatusCode = status_code;
												Data = data;
												ErrorMessage = null;
								}

								public ResponseModel(HttpStatusCode status_code, string error_message) {
												StatusCode = status_code;
												Data = default(T);
												ErrorMessage = error_message;
								}

								public T? GetData() => Data;
				}
}

