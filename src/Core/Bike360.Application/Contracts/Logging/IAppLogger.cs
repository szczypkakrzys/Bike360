﻿namespace Bike360.Application.Contracts.Logging;

public interface IAppLogger<T>
{
    void LogInformation(string message, params object[] args);
    void LogError(Exception exception, string message, params object[] args);
    void LogError(string message, params object[] args);
}
