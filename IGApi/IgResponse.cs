﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace IGApi
{
    public class IgResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Response { get; set; }

        public static implicit operator bool(IgResponse<T> inst)
        {
            return inst.StatusCode == HttpStatusCode.OK;
        }

        public static implicit operator HttpStatusCode(IgResponse<T> inst)
        {
            return inst.StatusCode;
        }

        public static implicit operator T(IgResponse<T> inst)
        {
            return inst.Response;
        }
    }
}
