using System;
using System.Collections.Generic;
using System.Linq;

namespace ObsidianVault
{
    public sealed class VaultApi
    {
        private Dictionary<string, double> playerVaults = new();

        public double GetBalance(string playerName)
        {
            return playerVaults.ContainsKey(playerName) ? playerVaults[playerName] : 0;
        }
        
        public void SetBalance(string playerName, double amount)
        {
            if (amount < 0)
                throw new ArgumentException("Balance cannot be negative.", nameof(amount));

            playerVaults[playerName] = amount;
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
        
        public bool TransferBalance(string fromPlayer, string toPlayer, double amount)
        {
            if (amount < 0)
                throw new ArgumentException("Transfer amount must be positive.", nameof(amount));

            if (playerVaults.ContainsKey(fromPlayer) && playerVaults[fromPlayer] >= amount)
            {
                DeductBalance(fromPlayer, amount);
                AddBalance(toPlayer, amount);
                return true;
            }

            return false;
        }
        
        public List<KeyValuePair<string, double>> GetTopBalances(int count)
        {
            if (count <= 0)
                throw new ArgumentException("Count must be greater than zero.", nameof(count));

            return playerVaults
                .OrderByDescending(kv => kv.Value)
                .Take(count)
                .ToList();
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
