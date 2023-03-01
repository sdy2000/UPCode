﻿namespace Core.DTOs
{
    public class InformationUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string? UesrGender { get; set; }
        public string RegisterDate { get; set; }
        public string PhonNumber { get; set; }
    }
    public class SideBarUserPanelViewModel
    {
        public string UserName { get; set; }
        public string UserAvatar { get; set; }
        public string RegisterDate { get; set; }
    }
}