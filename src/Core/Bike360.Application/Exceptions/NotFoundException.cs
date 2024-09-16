﻿namespace Bike360.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key) : base($"{name} with ID = {key} was not found")
    {

    }
}
