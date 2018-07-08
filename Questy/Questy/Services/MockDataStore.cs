using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(Questy.MockDataStore))]
namespace Questy
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>();
            var mockItems = new List<Item>
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Create new Quest", Description="Under construction. Be available till the end of 2018." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Join the Quest", Description="Under construction. Be available till the end of 2018." },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Create your avatar", Description="Create your avatar with your cam...", Page = new CameraPage() },
                new Item { Id = Guid.NewGuid().ToString(), Text = "Ask your question", Description="Send your question to developer...", Page = new ChatPage() },
                new Item { Id = Guid.NewGuid().ToString(), Text = "About", Description="", Page = new AboutPage() },
            };

            foreach (var item in mockItems)
            {
                if (item.Page == null) item.Page = new ItemDetailPage(new ItemDetailViewModel(item));
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var _item = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
