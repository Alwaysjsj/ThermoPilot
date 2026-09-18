using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using ThermoPilotClient.Entities;

namespace ThermoPilotClient.Services
{
    public class MenuService
    {
        public ObservableCollection<MenuItemEntity> GetMenus()
        {
            var path =
                "Config/MenuConfig.json";


            if (!File.Exists(path))
            {
                return new ObservableCollection<MenuItemEntity>();
            }


            var json =
                File.ReadAllText(path);


            var menus =
                JsonSerializer.Deserialize<List<MenuItemEntity>>(json);


            return new ObservableCollection<MenuItemEntity>(
                menus ?? new List<MenuItemEntity>());
        }
    }
}
