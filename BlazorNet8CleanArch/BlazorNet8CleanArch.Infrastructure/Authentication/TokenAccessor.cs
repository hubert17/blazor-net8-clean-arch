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
            await _localStorage.SetItemAsStringAsync("jwtToken", jwtToken);
        }

        public async Task<string?> GetToken()
        {
            //var token = StorageConstants.Local.JWTToken;
            //if (!String.IsNullOrEmpty(token))
            //    return token;

            return await _localStorage.GetItemAsStringAsync("jwtToken");
        }

        public async Task RemoveToken()
        {
            await _localStorage.RemoveItemAsync("jwtToken");
        }
    }
}
