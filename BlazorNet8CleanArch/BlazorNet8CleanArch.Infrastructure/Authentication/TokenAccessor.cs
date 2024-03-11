using Blazored.LocalStorage;
using BlazorNet8CleanArch.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorNet8CleanArch.Infrastructure.Authentication
{
    public class TokenAccessor
    {
        readonly ILocalStorageService _localStorage;

        public TokenAccessor(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async void SaveToken(string jwtToken)
        {
            StorageConstants.Local.JWTToken = jwtToken;
            try
            {
                await _localStorage.SetItemAsStringAsync("jwtToken", jwtToken);
            }
            catch { }
        }

        public async Task<string?> GetToken()
        {
            try
            {
                return await _localStorage.GetItemAsStringAsync("jwtToken");
            }
            catch
            {
                return StorageConstants.Local.JWTToken;
            }
        }

        public async Task RemoveToken()
        {
            await _localStorage.RemoveItemAsync("jwtToken");
        }
    }
}
