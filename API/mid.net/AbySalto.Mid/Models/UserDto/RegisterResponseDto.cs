﻿namespace AbySalto.Mid.WebApi.Models.UserDto;

public class RegisterResponseDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Location { get; set; }
    public string Adress { get; set; }
    public string PhoneNumber { get; set; }
    public string AccessToken { get; set; }
}
