using CollegeApp.Blazor.Server.Dtos;

namespace CollegeApp.Blazor.Server.Services
{
    public class CollegeSelectorService : ICollegeSelectorService
    {
        public ToolBarInfo ToolBarInfo { get; set; } = new ToolBarInfo();
        public List<object> SelectedCollegeIds { get; set; } = new List<object>();

        public event EventHandler ControllerActivated;
        public void ExecuteControllerActivated()
        {
            ControllerActivated?.Invoke(this, EventArgs.Empty);
        }
    }
}
