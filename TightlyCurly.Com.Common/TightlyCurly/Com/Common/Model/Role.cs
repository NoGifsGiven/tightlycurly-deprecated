using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TightlyCurly.Com.Common.Model;

public class Role
{
    [Key]
    public int RoleId { get; set; }

    public string RoleName { get; set; }

    public string Description { get; set; }

    public DateTimeOffset CreatedDate { get; set; }

    public DateTimeOffset UpdatedDate { get; set; }

    public List<UsersRole> UsersRoles { get; set; } = new List<UsersRole>();
}
