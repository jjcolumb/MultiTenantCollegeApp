using CollegeApp.Blazor.Server.Dtos;

namespace CollegeApp.Blazor.Server.Services
{
    public interface ICollegeSelectorService
    {
        ToolBarInfo ToolBarInfo { get; set; }

        event EventHandler ControllerActivated;
        void ExecuteControllerActivated();
        List<object> SelectedCollegeIds { get; set; }
    }
}
