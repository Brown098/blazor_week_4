using System.Net.Http.Headers;
using System.Text.Json;
using LocalService;

    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly LocalStorageService _ls;
        public string Token { get; set; }
    public AuthService(HttpClient http, LocalStorageService ls)
    {
        _http = http;
        _ls = ls;
    }


        public async Task LogintAsync(string name,string fakeToken)
    {
        Token = fakeToken;
        await _ls.SetItemAsync("auth_token",Token);
        _http.DefaultRequestHeaders.Authorization = null;
    }
    public async Task LogoutAsync()
        {
            Token = null;
            await _ls.RemoveItemAsync("auth_token");
            _http.DefaultRequestHeaders.Authorization = null;
        }
        public async Task InitializeAsync()
        {
            Token = await _ls.GetItemAsync("auth_token");
            if (!string.IsNullOrEmpty(Token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            }
        }
    } 

