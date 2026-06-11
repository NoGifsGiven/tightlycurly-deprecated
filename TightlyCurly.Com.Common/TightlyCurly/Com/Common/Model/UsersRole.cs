using System;
using System.ComponentModel.DataAnnotations;

namespace TightlyCurly.Com.Common.Model;

public class UsersRole : IModelEntity
{
    [Key]
    public int UserRoleId { get; set; }

    public int? UserId { get; set; }

    public int? RoleId { get; set; }

    public Role Role { get; set; }

    public User User { get; set; }

    public DateTimeOffset EnteredDate { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }
}
