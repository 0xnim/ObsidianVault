using System;
using System.Collections.Generic;

namespace ObsidianVault
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
