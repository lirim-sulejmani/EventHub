using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carmax.Application.Common.Models;
using Carmax.Domain.Enums;

namespace Carmax.Application.Features.User.Dtos;
public class UsersResponseDto : ResponseDto
{
    public DateTime createdOn;
    public StatusEnum statusId;

    public List<UserDto> Users { get; set; }
    public int TotalItems { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public Guid Id { get; internal set; }
    public string? LastName { get; internal set; }
    public string? FirstName { get; internal set; }
    public string? Address { get; internal set; }
    public DateTime CreatedOn { get; internal set; }
    public UserRole? RoleId { get; internal set; }
    public string? City { get; internal set; }
    public string? RefreshToken { get; internal set; }
    public DateTime RefreshTokenExpiryTime { get; internal set; }
    public string? Email { get; internal set; }
    public UserStatus StatusId { get; internal set; }

   
}