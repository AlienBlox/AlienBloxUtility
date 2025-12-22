using NLua;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;

namespace AlienBloxUtility
{
    public partial class AlienBloxUtility
    {
        public static Dictionary<string, LuaFunction> RegisteredFunctions;

        public static ConcurrentQueue<Action> mainThreadActions;

        public static CancellationTokenSource luaCts;

        public static void SetPlayerStat(string stat, int value, int playerWhoAmI)
        {
            var player = Main.player[playerWhoAmI];

            switch (stat)
            {
                case "life":
                    player.statLife = value;
                    break;

                case "maxLife":
                    player.statLifeMax2 = value;
                    break;

                case "mana":
                    player.statMana = value;
                    break;
            }

            SyncBasics(player, 0);
        }

        public static void AddPlayerStat(string stat, int amount, int playerWhoAmI)
        {
            var player = Main.player[playerWhoAmI];

            switch (stat)
            {
                case "life":
                    player.statLife += amount;
                    break;

                case "defense":
                    player.statDefense += amount;
                    break;
            }

            if (stat == "defense")
            {
                SyncBasics(player, amount);
            }
            else
            {
                SyncBasics(player, 0);
            }
        }

        public static int GetLocalPlayer() => Main.myPlayer;

        public static void RegisterFunction(string name, LuaFunction func)
        {
            if (!RegisteredFunctions.ContainsKey(name))
                RegisteredFunctions[name] = func;
            else
                throw new Exception($"Lua function '{name}' is already registered!");
        }

        public static void InitLua()
        {
            if (GlobalLua != null && RegisteredFunctions != null)
            {
                RegisterFunction("GetLocalPlayer", GlobalLua.RegisterFunction("GetLocalPlayer", null, typeof(AlienBloxUtility).GetMethod(nameof(GetLocalPlayer))));
                RegisterFunction("SetStat", GlobalLua.RegisterFunction("SetStat", null, typeof(AlienBloxUtility).GetMethod(nameof(SetPlayerStat))));
                RegisterFunction("AddStat", GlobalLua.RegisterFunction("AddStat", null, typeof(AlienBloxUtility).GetMethod(nameof(AddPlayerStat))));
            }
        }

        public static object RunLuaAsync(string lua)
        {


            return null;
        }

        public static object CallFunction(string name, params object[] args)
        {
            if (RegisteredFunctions.TryGetValue(name, out LuaFunction func))
                return func.Call(args);
            throw new Exception($"Lua function '{name}' not found");
        }

        public static void RunLuaAsync(string luaCode, Action<object[]> onResult)
        {
            // Cancel any previous script
            luaCts?.Cancel();
            luaCts = new CancellationTokenSource();
            var token = luaCts.Token;

            Task.Run(() =>
            {
                try
                {
                    token.ThrowIfCancellationRequested();

                    // Run Lua and get results
                    object[] results = GlobalLua.DoString(luaCode);

                    token.ThrowIfCancellationRequested();

                    // Queue results back to main thread
                    mainThreadActions.Enqueue(() =>
                    {
                        onResult?.Invoke(results);
                    });
                }
                catch (OperationCanceledException)
                {
                    mainThreadActions.Enqueue(() =>
                    {
                        Console.WriteLine("Lua execution canceled.");
                        onResult?.Invoke(null);
                    });
                }
                catch (Exception ex)
                {
                    mainThreadActions.Enqueue(() =>
                    {
                        Console.WriteLine("Lua error: " + ex.Message);
                        onResult?.Invoke(null);
                    });
                }
            }, token);
        }
    }
}