namespace ThermoPilotClient.Entities
{
    public class MenuItemEntity
    {
        public string Name { get; set; } = string.Empty;


        public string Icon { get; set; } = string.Empty;


        public string ViewModelType { get; set; } = string.Empty;


        public int Order { get; set; }


        public bool IsVisible { get; set; } = true;
    }
}
