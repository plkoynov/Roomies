using System.Net;

namespace Roomies.Application.Models;

public class ServiceResponse<TData, TError>
{
    public TData Data { get; private set; }
    public TError Error { get; private set; }
    public int StatusCode { get; private set; }

    public bool IsSuccess { get; private set; }

    private ServiceResponse(TData data)
    {
        Data = data;
        IsSuccess = true;
    }

    private ServiceResponse(TError error, int statusCode)
    {
        Error = error;
        StatusCode = statusCode;
        IsSuccess = false;
    }

    public static ServiceResponse<TData, TError> Success(TData data)
    {
        return new ServiceResponse<TData, TError>(data);
    }

    public static ServiceResponse<TData, TError> Failure(TError error, HttpStatusCode statusCode)
    {
        return new ServiceResponse<TData, TError>(error, (int)statusCode);
    }
}
