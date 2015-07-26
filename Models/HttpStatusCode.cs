using System;

namespace ocDownloader.Models
{
    public enum HttpStatusCode
    {
        OK = 200,
        NoContent = 204,
        Ambiguous = 300,
        Moved = 301,
        Redirect = 302,
        UseProxy = 305,
        BadRequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        ProxyAuthenticationRequired = 407,
        RequestTimeout = 408,
        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503,
        GatewayTimeout = 504,
        HttpVersionNotSupported = 505
    }
}
