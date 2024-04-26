using DevExpress.Blazor;
using DevExpress.ExpressApp.Actions;
using Microsoft.AspNetCore.Components.Web;

namespace CollegeApp.Blazor.Server.Dtos
{
    public class ToolBarInfo : IToolbarItemInfo
    {
        public string Name {get; set;}

        public string Text {get; set;}

        public string AdaptiveText {get; set;}

        public string Tooltip {get; set;}

        public string IconCssClass {get; set;}

        public string IconUrl {get; set;}

        public string NavigateUrl {get; set;}

        public string Target {get; set;}

        public event EventHandler<EventArgs> ItemClick;
        public List<ChoiceActionItem> SelectedItems { get; set;} = new List<ChoiceActionItem>();
        public void Clicked((bool, ChoiceActionItem) item)
        {
            var itemInList = Items.FirstOrDefault(i => i.Item2.Data == item.Item2.Data);
            int indexOfItem = Items.IndexOf(itemInList);
            if (item.Item1)
            {
                SelectedItems.Add(item.Item2);
                itemInList.Item1 = true;
            }
            else
            {
                SelectedItems.Remove(item.Item2);
                itemInList.Item1 = false;
            }
            Items[indexOfItem] = itemInList;
            ItemClick?.Invoke(this, new EventArgs());
        }
        public List<(bool, ChoiceActionItem)> Items { get; set;}
        public Func<MouseEventArgs, Task> Click {get; set;}

        public bool Enabled {get; set;}

        public bool Visible {get; set;}

        public bool Checked {get; set;}

        public bool HiddenToAdaptiveMenu {get; set;}

        public bool SubmitFormOnClick {get; set;}

        public object Data {get; set;}

        public IToolbarItemInfo Parent { get; set; }
    }
}
