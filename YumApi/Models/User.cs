﻿namespace YumApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }

    }
}
