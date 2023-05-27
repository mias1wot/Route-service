using System;
using System.Collections.Generic;
using System.Text;

namespace RouteServiceAPP.Application.Exceptions
{
	public class RouteServiceAppException : Exception
	{
		public int StatusCode { get; set; }

		public RouteServiceAppException(string message, int code) : base(message)
		{
			StatusCode = code;
		}

		public RouteServiceAppException(string message, Exception innerException, int code) : base(message, innerException)
		{
			StatusCode = code;
		}
	}
}
