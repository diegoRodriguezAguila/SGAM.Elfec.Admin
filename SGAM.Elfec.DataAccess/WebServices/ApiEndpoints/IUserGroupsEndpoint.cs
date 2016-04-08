﻿using RestEase;
using SGAM.Elfec.Model;
using System.Threading.Tasks;

namespace SGAM.Elfec.DataAccess.WebServices.ApiEndpoints
{
    public interface IUserGroupsEndpoint : ISgamAuthenticatedEndpoint
    {
        // The "user_groups" is a relative path the a base URL, which we'll provide later
        [Post("/user_groups")]
        Task<UserGroup> RegisterUserGroup([Body] UserGroup UserGroup);

        // The "user_groups" is a relative path the a base URL, which we'll provide later
        [Post("/user_groups/{userGroupId}/members/{usernames}")]
        Task AddMembers([Path] string userGroupId, [Path] string usernames);
    }
}