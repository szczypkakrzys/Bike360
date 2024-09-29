﻿namespace Bike360.Domain;

public class Address : BaseEntity
{
    public string Country { get; set; }
    public string Voivodeship { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public Customer? Customer { get; set; }
}