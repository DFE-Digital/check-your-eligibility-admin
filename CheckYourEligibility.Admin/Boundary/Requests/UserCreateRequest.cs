﻿// Ignore Spelling: Fsm

namespace CheckYourEligibility.Admin.Boundary.Requests;

public class UserCreateRequest
{
    public UserData? Data { get; set; }
}

public class UserData
{
    public string Email { get; set; }
    public string Reference { get; set; }
}