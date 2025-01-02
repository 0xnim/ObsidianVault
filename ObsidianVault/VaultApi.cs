using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Obsidian.API;
using Obsidian.API.Plugins;

namespace VaultPluginNamespace
{
    public sealed class VaultApi
    {
        private Dictionary<string, double> playerVaults = new();

        public double GetBalance(string playerName)
        {
            return playerVaults.ContainsKey(playerName) ? playerVaults[playerName] : 0;
        }

        public void AddBalance(string playerName, double amount)
        {
            if (!playerVaults.ContainsKey(playerName))
            {
                playerVaults[playerName] = 0;
            }

            playerVaults[playerName] += amount;
        }

        public bool DeductBalance(string playerName, double amount)
        {
            if (playerVaults.ContainsKey(playerName) && playerVaults[playerName] >= amount)
            {
                playerVaults[playerName] -= amount;
                return true;
            }

            return false;
        }

        public Dictionary<string, double> GetData()
        {
            return new Dictionary<string, double>(playerVaults);
        }

        internal void LoadData(Dictionary<string, double> data)
        {
            playerVaults = data ?? throw new ArgumentNullException(nameof(data));
        }
    }
}
