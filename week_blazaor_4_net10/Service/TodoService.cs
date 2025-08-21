using LocalService;
#pragma warning disable CS8616, CS8618, CS8603, CS8618,CS8601


    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }

    }
    public class TodoServer
    {
        private readonly LocalStorageService _ls;
        private readonly HttpClient _http;
        private List<TodoItem> _items = new();
        public TodoServer(LocalStorageService ls, HttpClient http)
        {
            _ls = ls;
            _http = http;
        }
        public async Task InitializeAsync()
        {
            var json = await _ls.GetItemAsync("todos");
            if (!string.IsNullOrEmpty(json)) _items = System.Text.Json.JsonSerializer.Deserialize<List<TodoItem>>(json);
        
        }
        public Task<List<TodoItem>> GetAllAsync() => Task.FromResult(_items);
        public async Task AddAsync(string title)
        {
            var id = _items.Count == 0 ? 1 : _items.Max(i => i.Id) + 1;
            _items.Add(new TodoItem { Id = id, Title = title, Done = false });
            await SaveAsync();
        }
        public async Task ToggleAsync(int id)
        {
            var t = _items.FirstOrDefault(x => x.Id == id);
            if (t != null) t.Done = !t.Done;
            await SaveAsync();
        }
        public async Task DeleteAsync(int id)
        {
            _items.RemoveAll(x => x.Id == id);
            await SaveAsync();
        }
        private async Task SaveAsync()
        {
            var json = System.Text.Json.JsonSerializer.Serialize(_items);
            await _ls.SetItemAsync("todos", json);
        }
    }

