namespace CoreBlog.Web.ViewModels.ControlPanel
{
    public class UserControlPanelViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool isBanned { get; set; }
    }
}