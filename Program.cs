using Starvaders_Seed_Analyzer.Content;
using Starvaders_Seed_Analyzer.Enums;

namespace Starvaders_Seed_Analyzer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Creating new run...");
                RunState runState = InitSetSeedDemoRun();
                Console.WriteLine("Run initialized!");

                Console.WriteLine();
                StandardConsole(runState);
            }
        }

        //this was just a fun thing I wanted to check with this program
        //the goal was to get as many artifacts as possible. It's theoretically possible to get every common artifact in a single run, but it requires good rng and money
        static void CheckArtifactRun(RunState runState)
        {
            List<ArtifactInfo> sampleArtifacts = new()
            {
                runState.campaign.artifacts["Quantum Burn"],
                runState.campaign.artifacts["Threat Radar"],
                runState.campaign.artifacts["Thermal Beam"],
                runState.campaign.artifacts["Shrapnel"],
                runState.campaign.artifacts["Insulation"],
                runState.campaign.artifacts["Bomber Mech"],
                runState.campaign.artifacts["Supply Drop"],
                runState.campaign.artifacts["Mine Field"],
                runState.campaign.artifacts["Volcanic Ash"],
                runState.campaign.artifacts["Haywire"],
                runState.campaign.artifacts["Blueprint"],
                runState.campaign.artifacts["Nemesis"],
                runState.campaign.artifacts["Multi-Pass"],
                runState.campaign.artifacts["Gift Card"],
                runState.campaign.artifacts["Bandages"],
                runState.campaign.artifacts["Mystery Bear"],
                runState.campaign.artifacts["Interlinked"],
                runState.campaign.artifacts["AntiMag"],
                runState.campaign.artifacts["Storm Chaser"],
                runState.campaign.artifacts["Robo Parrot"],
                runState.campaign.artifacts["Peashooter"],
            };

            List<int>[] availableArtifactRewards = new List<int>[9];
            for(int act = 1; act <= 3; act++)
            {
                runState.actNumber = act;
                for(int day = 1; day <= 3; day++)
                {
                    runState.dayNumber = day;
                    if (act == 1)
                        runState.dayNumber++;

                    int index = (act - 1) * 3 + day - 1;
                    availableArtifactRewards[index] = new List<int>();
                    for(int tokens = 0; tokens <= 3; tokens++)
                    {
                        runState.chronoTokens = tokens;
                        List<EncounterInfo> encounters = EncounterSelector.CreateEncounters(runState);
                        if (encounters.Select(encounter => encounter.rewardType).Contains(RewardType.Artifact))
                            availableArtifactRewards[index].Add(tokens);
                    }
                    if (availableArtifactRewards[index].Count == 0)
                        availableArtifactRewards[index] = null!;
                }
            }

            int[,,,] minGachaArtifacts = new int[3, 3, sampleArtifacts.Count + 1, 4];
            for(int numChronoArtifacts = 0; numChronoArtifacts <= 2; numChronoArtifacts++)
            {
                runState.artifactDeck.Clear();
                if (numChronoArtifacts >= 1)
                    runState.artifactDeck.Add(runState.campaign.artifacts["Lizard Soul"]);
                if (numChronoArtifacts >= 2)
                    runState.artifactDeck.Add(runState.campaign.artifacts["Wormhole"]);

                for(int numNeutralArtifacts = 0; numNeutralArtifacts <= sampleArtifacts.Count; numNeutralArtifacts++)
                {
                    if (numNeutralArtifacts != 0)
                        runState.artifactDeck.Add(sampleArtifacts[numNeutralArtifacts - 1]);

                    for(int act = 0; act < 3; act++)
                    {
                        runState.actNumber = act;
                        runState.dayNumber = act == 0 ? 5 : 4;

                        for(int numTokens = 0; numTokens <= 3; numTokens++)
                        {
                            runState.chronoTokens = numTokens;
                            MinShop shop = new MinShop(runState, false);

                            bool wasCard = false;
                            int gachaIndex = 0;
                            while(!wasCard || shop.gachaSequence[gachaIndex] is ArtifactInfo)
                            {
                                if (shop.gachaSequence[gachaIndex] is ArtifactInfo)
                                {
                                    wasCard = false;
                                    minGachaArtifacts[act, numChronoArtifacts, numNeutralArtifacts, numTokens]++;
                                }
                                else
                                {
                                    wasCard = true;
                                }
                                gachaIndex++;
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Seed Artifact Summary:");
            Console.WriteLine();
            Console.WriteLine("Encounter artifact rewards:");
            for(int act = 1; act <= 3; act++)
            {
                for(int day = 1; day <= 3; day++)
                {
                    int index = (act - 1) * 3 + day - 1;
                    Console.Write($"Act {act} day {(act == 1 ? day + 1 : day)}: ");
                    if (availableArtifactRewards[index] == null)
                        Console.WriteLine("None");
                    else
                    {
                        foreach (int tokenNum in availableArtifactRewards[index])
                            Console.Write(tokenNum + ", ");
                        Console.WriteLine();
                    }
                }
            }
            Console.ReadKey(true);
            Console.WriteLine();
            Console.WriteLine("Min Gacha Results:");
            for(int act = 0; act < 3; act++)
            {
                Console.WriteLine("--- Act " + (act + 1) + " ---");
                Console.WriteLine();
                for(int tokens = 3; tokens >= 0; tokens--)
                {
                    for(int chronoArtifacts = 0; chronoArtifacts <= 2; chronoArtifacts++)
                    {
                        for(int normalArtifacts = 0; normalArtifacts <= sampleArtifacts.Count; normalArtifacts++)
                        {
                            int gachaArtifacts = minGachaArtifacts[act, chronoArtifacts, normalArtifacts, tokens];
                            if (gachaArtifacts == 0)
                                continue;
                            Console.Write($"{gachaArtifacts} gacha artifacts w/ {chronoArtifacts+normalArtifacts} owned artifacts ");
                            if(chronoArtifacts > 0)
                                Console.Write($"(including {chronoArtifacts} chrono) ");
                            Console.WriteLine($"and {tokens} tokens.");
                        }
                    }
                }
                Console.ReadKey(true);
                Console.WriteLine();
            }
            Console.WriteLine("Type 'go' to analyze the seed with the standard console.");
            if(Console.ReadLine()!.Trim().ToLower() == "go")
            {
                Console.WriteLine();
                runState.actNumber = 1;
                runState.dayNumber = 1;
                runState.chronoTokens = 3;
                runState.artifactDeck.Clear();
                StandardConsole(runState);
            }
            Console.Clear();
        }

        static void StandardConsole(RunState runState)
        {
            Dictionary<string, Action<RunState, string[]>> consoleCommands = new()
            {
                { "list", ListItems },
                { "goto", ChangeDay },
                { "add", Add },
                { "remove", Remove },
                { "setupgrade", SetUpgrade }
            };
            Console.WriteLine("Type 'help' to see a list of commands.");
            Console.WriteLine("Type 'reset' to start a new run.");
            while (true)
            {
                Console.Write(">> ");
                string[] command = Console.ReadLine()!.Trim().Split();
                if (command.Length == 0)
                    continue;
                if (command[0] == "help")
                {
                    Console.Write("Commands: ");
                    foreach (string key in consoleCommands.Keys)
                        Console.Write(key + ", ");
                    Console.WriteLine();
                    Console.WriteLine("For more details on how to use each command, simply type that command with no extra parameters.");
                }
                else if (command[0] == "reset")
                {
                    Console.Clear();
                    break;
                }
                else if (consoleCommands.ContainsKey(command[0]))
                    consoleCommands[command[0]](runState, command);
                else
                    Console.WriteLine("Invalid command. Type help to list all commands.");

                Console.WriteLine();
            }
        }

        static void ListItems(RunState runState, string[] command)
        {
            if(command.Length <= 1)
            {
                Console.WriteLine("Available lists: ObtainableCards, BudOfferings, MinOfferings, OfferedEncounters, CurrentDeck, CardReward, AttackCardReward, MoveTacticCardReward, RareCardReward, ComponentReward, ArtifactReward, BossReward");
                return;
            }
            switch (command[1].ToLower())
            {
                case "obtainablecards":
                    {
                        List<CardInfo> obtainableCards = runState.GetAvailableCards().Where(cardModel => RNGHelper.GetAcceptedCardRarities(cardModel.rarity)).ToList();

                        Console.WriteLine($"All {obtainableCards.Count} obtainable cards this run:");
                        foreach (var card in obtainableCards)
                            Console.WriteLine(card.name);
                        break;
                    }
                case "budofferings":
                    {
                        Console.WriteLine("Bud's Factory Offerings:");
                        for (int chronoTokens = 3; chronoTokens >= 0; chronoTokens--)
                        {
                            Console.WriteLine($"At {chronoTokens} chrono tokens...");
                            int modifiedSeed = RNGHelper.GetModifiedSeed(runState.seed, 1, 1, chronoTokens);
                            Random rng = new Random(modifiedSeed);
                            Console.WriteLine("Seed = " + modifiedSeed);
                            List<CardInstance> cards = BudFactory.GetCardDraft(runState, rng);
                            foreach (CardInstance card in cards)
                            {
                                Console.WriteLine(card);
                            }
                            Console.WriteLine();
                        }
                        break;
                    }
                case "minofferings":
                    {
                        int tokens = runState.chronoTokens;
                        Console.WriteLine("Min's Shop Offerings:");
                        for(int chronoTokens = 3; chronoTokens >= 0; chronoTokens--)
                        {
                            runState.chronoTokens = chronoTokens;
                            MinShop shop = new MinShop(runState);
                            Console.WriteLine();
                            Console.WriteLine($"At {chronoTokens} chrono tokens...");
                            Console.WriteLine();
                            Console.WriteLine(shop);
                        }
                        runState.chronoTokens = tokens;
                        break;
                    }
                case "offeredencounters":
                    {
                        if (runState.actNumber == 1 && runState.dayNumber == 1)
                        {
                            Console.WriteLine("Act 1 day 1 contains Bud's Factory.");
                        }
                        else if (runState.dayNumber == (runState.actNumber == 1 ? 5 : 4))
                        {
                            Console.WriteLine($"Act {runState.actNumber} day {runState.dayNumber} contains Min's Workshop.");
                        }
                        else if (runState.dayNumber == (runState.actNumber == 1 ? 6 : 5))
                        {
                            Console.WriteLine($"Act {runState.actNumber} day {runState.dayNumber} contains a boss.");
                        }
                        else
                        {
                            Console.Write($"Available encounters for act {runState.actNumber} day {runState.dayNumber}:");
                            int tokens = runState.chronoTokens;
                            for (int chronoTokens = 3; chronoTokens >= 0; chronoTokens--)
                            {
                                Console.WriteLine();
                                Console.WriteLine($"At {chronoTokens} chrono tokens...");
                                Console.WriteLine();
                                runState.chronoTokens = chronoTokens;
                                var list = EncounterSelector.CreateEncounters(runState);
                                foreach (var item in list)
                                {
                                    Console.WriteLine(item);
                                    Console.WriteLine();
                                }
                            }
                            runState.chronoTokens = tokens;
                        }
                        break;
                    }
                case "currentdeck":
                    {
                        Console.WriteLine();
                        Console.WriteLine($"{runState.cardDeck.Count} cards:");
                        foreach(CardInstance card in runState.cardDeck)
                            Console.WriteLine(card);
                        Console.WriteLine();
                        Console.WriteLine($"{runState.artifactDeck.Count} artifacts:");
                        foreach(ArtifactInfo artifact in runState.artifactDeck)
                            Console.WriteLine(artifact);
                        Console.WriteLine();
                        break;
                    }
                case "cardreward":
                    {
                        Console.Write($"Possible card rewards for act {runState.actNumber} day {runState.dayNumber}:");
                        int tokens = runState.chronoTokens;
                        for (int chronoTokens = 3; chronoTokens >= 0; chronoTokens--)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"At {chronoTokens} chrono tokens...");
                            Console.WriteLine();
                            runState.chronoTokens = chronoTokens;
                            var list = RewardController.GetCardReward(runState);
                            foreach (var item in list)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        runState.chronoTokens = tokens;
                        break;
                    }
                case "attackcardreward":
                    {
                        Console.Write($"Possible attack card rewards for act {runState.actNumber} day {runState.dayNumber}:");
                        int tokens = runState.chronoTokens;
                        for (int chronoTokens = 3; chronoTokens >= 0; chronoTokens--)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"At {chronoTokens} chrono tokens...");
                            Console.WriteLine();
                            runState.chronoTokens = chronoTokens;
                            var list = RewardController.GetCardReward(runState, CardTraitFlag.Attack);
                            foreach (var item in list)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        runState.chronoTokens = tokens;
                        break;
                    }
                case "movetacticcardreward":
                    {
                        Console.Write($"Possible move/tactic card rewards for act {runState.actNumber} day {runState.dayNumber}:");
                        int tokens = runState.chronoTokens;
                        for (int chronoTokens = 3; chronoTokens >= 0; chronoTokens--)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"At {chronoTokens} chrono tokens...");
                            Console.WriteLine();
                            runState.chronoTokens = chronoTokens;
                            var list = RewardController.GetMoveTacticCardReward(runState);
                            foreach (var item in list)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        runState.chronoTokens = tokens;
                        break;
                    }
                case "rarecardreward":
                    {
                        Console.Write($"Possible rare card rewards for act {runState.actNumber} day {runState.dayNumber}:");
                        int tokens = runState.chronoTokens;
                        for (int chronoTokens = 3; chronoTokens >= 0; chronoTokens--)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"At {chronoTokens} chrono tokens...");
                            Console.WriteLine();
                            runState.chronoTokens = chronoTokens;
                            var list = RewardController.GetRareCardReward(runState);
                            foreach (var item in list)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        runState.chronoTokens = tokens;
                        break;
                    }
                case "componentreward":
                    {
                        if(runState.cardDeck.Where(card => card.component == null).Count() < 3)
                        {
                            Console.WriteLine("Not enough unupgraded cards; component reward not available.");
                            Console.WriteLine();
                            break;
                        }
                        Console.WriteLine($"Possible component rewards for act {runState.actNumber} day {runState.dayNumber}:");
                        Console.WriteLine("(Note that rewards can change if you add/remove/upgrade other cards beforehand)");
                        int tokens = runState.chronoTokens;
                        for (int chronoTokens = 3; chronoTokens >= 0; chronoTokens--)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"At {chronoTokens} chrono tokens...");
                            Console.WriteLine();
                            runState.chronoTokens = chronoTokens;
                            var list = RewardController.GetComponentReward(runState);
                            foreach (var item in list)
                            {
                                Console.WriteLine($"Card {item.Item1+1} ({runState.cardDeck[item.Item1].name}) getting {item.Item2.name}");
                            }
                        }
                        runState.chronoTokens = tokens;
                        break;
                    }
                case "artifactreward":
                    {
                        if(runState.artifactDeck.Count >= runState.campaign.maxArtifactCount)
                        {
                            Console.WriteLine("Already at artifact limit; artifact reward not available.");
                            Console.WriteLine();
                            break;
                        }
                        Console.WriteLine($"Possible artifact rewards for act {runState.actNumber} day {runState.dayNumber}:");
                        Console.WriteLine("(Note that rewards can change if you pick up other artifacts beforehand)");
                        int tokens = runState.chronoTokens;
                        for (int chronoTokens = 3; chronoTokens >= 0; chronoTokens--)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"At {chronoTokens} chrono tokens...");
                            Console.WriteLine();
                            runState.chronoTokens = chronoTokens;
                            var list = RewardController.GetArtifactReward(runState, Rarity.Common);
                            foreach (var item in list)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        runState.chronoTokens = tokens;
                        break;
                    }
                case "bossreward":
                    {
                        if (runState.artifactDeck.Count >= runState.campaign.maxArtifactCount)
                        {
                            Console.WriteLine("Already at artifact limit; artifact reward not available.");
                            Console.WriteLine("I think the game would just crash in this scenario.");
                            Console.WriteLine();
                            break;
                        }
                        Console.WriteLine($"Possible legendary artifact rewards for act {runState.actNumber} day {runState.dayNumber}:");
                        Console.WriteLine("(Note that rewards can change if you pick up other artifacts beforehand)");
                        int tokens = runState.chronoTokens;
                        for (int chronoTokens = 3; chronoTokens >= 0; chronoTokens--)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"At {chronoTokens} chrono tokens...");
                            Console.WriteLine();
                            runState.chronoTokens = chronoTokens;
                            var list = RewardController.GetArtifactReward(runState, Rarity.Legendary);
                            foreach (var item in list)
                            {
                                Console.WriteLine(item);
                            }
                        }
                        runState.chronoTokens = tokens;
                        break;
                    }
                default:
                    Console.WriteLine("Invalid list. Enter 'list' by itself to see what lists are available.");
                    break;
            }
        }

        static void ChangeDay(RunState runState, string[] command)
        {
            if (command.Length < 3)
            {
                Console.WriteLine("goto command: goto [act] [day]");
                return;
            }

            if (int.TryParse(command[1], out int actNum) && int.TryParse(command[2], out int dayNum) && actNum >= 1 && actNum <= 3 && dayNum >= 1 && dayNum <= (actNum == 1 ? 6 : 5))
            {
                runState.actNumber = actNum;
                runState.dayNumber = dayNum;
                Console.WriteLine($"Moved to act {actNum} day {dayNum}.");
            }
            else
            {
                Console.WriteLine("Invalid act/day.");
            }
        }

        static void Add(RunState runState, string[] command)
        {
            if(command.Length < 3)
            {
                Console.WriteLine("add [\"card\"/\"artifact\"] [itemName] (component)");
                return;
            }

            if (command[1].ToLower() == "card")
            {
                if (runState.campaign.cards.TryGetValue(command[2].Replace("_", "").Replace("-", "").Replace("!", "").ToLower(), out var card))
                {
                    if(command.Length >= 4)
                    {
                        if (runState.campaign.components.TryGetValue(command[3].Replace("_", "").Replace("-", "").Replace("!", "").ToLower(), out var component))
                        {
                            CardInstance instance = card.CreateInstance(component);
                            runState.cardDeck.Add(instance);
                            Console.WriteLine("Added card " + instance);
                        }
                        else
                        {
                            Console.WriteLine($"Component {command[3]} not found.");
                        }
                    }
                    else
                    {
                        CardInstance instance = card.CreateInstance();
                        runState.cardDeck.Add(instance);
                        Console.WriteLine("Added card " + instance);
                    }
                }
                else
                {
                    Console.WriteLine($"Card {command[2]} not found.");
                }
            }
            else if (command[1].ToLower() == "artifact")
            {
                if (runState.campaign.artifacts.TryGetValue(command[2].Replace("_", "").Replace("-", "").Replace("!", "").ToLower(), out var artifact))
                {
                    runState.artifactDeck.Add(artifact);
                    Console.WriteLine("Added artifact " + artifact);
                }
                else
                {
                    Console.WriteLine($"Artifact {command[2]} not found.");
                }
            }
            else
            {
                Console.WriteLine("Can only add card or artifact.");
            }
        }

        static void Remove(RunState runState, string[] command)
        {
            if (command.Length != 3)
            {
                Console.WriteLine("remove [\"card\"/\"artifact\"] [index]");
                return;
            }
            if (command[1].ToLower() == "card")
            {
                if (int.TryParse(command[2], out int index) && index > 0 && index <= runState.cardDeck.Count)
                {
                    Console.WriteLine("Removing card " + runState.cardDeck[index - 1]);
                    runState.cardDeck.RemoveAt(index - 1);
                }
                else
                {
                    Console.WriteLine("Index must be an integer from 1 to " + runState.cardDeck.Count);
                }
            }
            else if (command[1].ToLower() == "artifact")
            {
                if (int.TryParse(command[2], out int index) && index > 0 && index <= runState.artifactDeck.Count)
                {
                    Console.WriteLine("Removing artifact " + runState.artifactDeck[index - 1]);
                    runState.artifactDeck.RemoveAt(index - 1);
                }
                else
                {
                    Console.WriteLine("Index must be an integer from 1 to " + runState.artifactDeck.Count);
                }
            }
            else
            {
                Console.WriteLine("Can only remove card or artifact.");
            }
        }

        static void SetUpgrade(RunState runState, string[] command)
        {
            if (command.Length != 3)
            {
                Console.WriteLine("setupgrade [cardIndex] [component/\"none\"]");
                return;
            }

            int index = -1;
            if (!int.TryParse(command[1], out index) || index <= 0 || index > runState.cardDeck.Count)
            {
                Console.WriteLine("Index must be an integer from 1 to " + runState.cardDeck.Count);
                return;
            }

            if (command[2].ToLower() == "none")
            {
                Console.WriteLine($"Set card {index} ({runState.cardDeck[index-1].name}) component to none. Was {runState.cardDeck[index-1].component?.name ?? "none"}.");
                runState.cardDeck[index-1].component = null;
            }
            else if (runState.campaign.components.TryGetValue(command[2].Replace("_", "").Replace("-", "").Replace("!", "").ToLower(), out var component))
            {
                Console.WriteLine($"Set card {index} ({runState.cardDeck[index-1].name}) component to {component.name}. Was {runState.cardDeck[index-1].component?.name ?? "none"}.");
                runState.cardDeck[index-1].component = component;
            }
            else
            {
                Console.WriteLine("Component not found.");
            }
        }

        static RunState InitSetSeedDemoRun()
        {
            ContentManager content = new ContentManager();
            List<(RewardType, float)> act1EasyWeights =
            [
                (RewardType.Card, 2),
                (RewardType.Component, 2),
                (RewardType.AttackCard, 1),
                (RewardType.TacticMoveCard, 1)
            ];
            List<(RewardType, float)> normalRewardWeights =
            [
                (RewardType.Component, 5),
                (RewardType.Card, 3),
                (RewardType.AttackCard, 1),
                (RewardType.TacticMoveCard, 1)
            ];
            List<(RewardType, float)> eliteRewardWeights =
            [
                (RewardType.Special, 2),
                (RewardType.Artifact, 1),
                (RewardType.RareCard, 1)
            ];
            List<EncounterTemplate> act1Encounters =
            [
                new("Act 1 Easy Room", 2, 3,
                [
                    (content.enemies["Migo"], 6),
                    (content.enemies["Shooter"], 4),
                    (content.enemies["Rocketeer"], 2),
                    (content.enemies["Airship"], 3),
                    (content.enemies["Swarmer"], 3),
                    (content.enemies["Vengoid"], 4),
                    (content.enemies["Boomer"], 3),
                    (content.enemies["Fallen Angel"], 2)
                ], act1EasyWeights, 5, 8, 8),
                new("Act 1 Hard Room", 3, 4,
                [
                    (content.enemies["Migo"], 6),
                    (content.enemies["Shooter"], 4),
                    (content.enemies["Rocketeer"], 2),
                    (content.enemies["Airship"], 3),
                    (content.enemies["Swarmer"], 3),
                    (content.enemies["Vengoid"], 4),
                    (content.enemies["Boomer"], 3),
                    (content.enemies["Fallen Angel"], 3)
                ], normalRewardWeights, 5, 8, 8),
                new("Act 1 Swarm Room", 1, 5,
                [
                    (content.enemies["Migo"], 16),
                    (content.enemies["Shooter"], 9),
                    (content.enemies["Rocketeer"], 7),
                    (content.enemies["Airship"], 7),
                    (content.enemies["Swarmer"], 7),
                    (content.enemies["Boomer"], 6)
                ], eliteRewardWeights, 5, 8, 8, EncounterType.Swarm),
                new("Act 1 Cloudy Room", 3, 4,
                [
                    (content.enemies["Migo"], 6),
                    (content.enemies["Shooter"], 4),
                    (content.enemies["Rocketeer"], 2),
                    (content.enemies["Swarmer"], 2),
                    (content.enemies["Airship"], 4),
                    (content.enemies["Vengoid"], 4),
                    (content.enemies["Boomer"], 3),
                    (content.enemies["Fallen Angel"], 3)
                ], eliteRewardWeights, 5, 8, 8, EncounterType.Cloudy)
            ];
            List<EncounterTemplate> act2Encounters =
            [
                new("Act 2 Easy Room", 2, 3,
                [
                    (content.enemies["Rusher"], 3),
                    (content.enemies["Orchid"], 1),
                    (content.enemies["Broodmother"], 2),
                    (content.enemies["Shelled"], 3),
                    (content.enemies["Blob"], 2),
                    (content.enemies["Spitter"], 3),
                    (content.enemies["Virus"], 4),
                    (content.enemies["Weaver"], 3),
                ], normalRewardWeights, 5, 9, 10),
                new("Act 2 Hard Room", 3, 4,
                [
                    (content.enemies["Rusher"], 4),
                    (content.enemies["Orchid"], 2),
                    (content.enemies["Broodmother"], 3),
                    (content.enemies["Shelled"], 3),
                    (content.enemies["Blob"], 3),
                    (content.enemies["Spitter"], 4),
                    (content.enemies["Virus"], 4),
                    (content.enemies["Weaver"], 3),
                ], normalRewardWeights, 5, 9, 10),
                new("Act 2 Swarm Room", 1, 5,
                [
                    (content.enemies["Orchid"], 8),
                    (content.enemies["Broodmother"], 6),
                    (content.enemies["Shelled"], 8),
                    (content.enemies["Rusher"], 14),
                    (content.enemies["Spitter"], 10),
                    (content.enemies["Weaver"], 10),
                ], eliteRewardWeights, 5, 9, 10, EncounterType.Swarm),
                new("Act 2 Holes Room", 3, 4,
                [
                    (content.enemies["Rusher"], 6),
                    (content.enemies["Orchid"], 2),
                    (content.enemies["Broodmother"], 5),
                    (content.enemies["Shelled"], 5),
                    (content.enemies["Blob"], 4),
                    (content.enemies["Spitter"], 4),
                    (content.enemies["Virus"], 4),
                    (content.enemies["Weaver"], 3),
                ], eliteRewardWeights, 5, 9, 10, EncounterType.Holes)
            ];
            List<EncounterTemplate> act3Encounters =
            [
                new("Act 3 Easy Room", 2, 3,
                [
                    (content.enemies["Pulsar"], 5),
                    (content.enemies["Astromancer"], 5),
                    (content.enemies["Mimic"], 5),
                    (content.enemies["Ghost"], 5),
                    (content.enemies["Voidspawn"], 4),
                    (content.enemies["Leviathan"], 2),
                    (content.enemies["Echo"], 1),
                ], normalRewardWeights, 7, 9, 10),
                new("Act 3 Hard Room", 3, 4,
                [
                    (content.enemies["Pulsar"], 5),
                    (content.enemies["Astromancer"], 5),
                    (content.enemies["Mimic"], 5),
                    (content.enemies["Ghost"], 5),
                    (content.enemies["Voidspawn"], 4),
                    (content.enemies["Leviathan"], 2),
                    (content.enemies["Echo"], 1),
                ], normalRewardWeights, 7, 9, 10),
                new("Act 3 Swarm Room", 1, 5,
                [
                    (content.enemies["Pulsar"], 15),
                    (content.enemies["Astromancer"], 12),
                    (content.enemies["Mimic"], 12),
                    (content.enemies["Voidspawn"], 15),
                ], eliteRewardWeights, 7, 9, 10, EncounterType.Swarm),
                new("Act 3 Void Room", 3, 4,
                [
                    (content.enemies["Pulsar"], 5),
                    (content.enemies["Astromancer"], 5),
                    (content.enemies["Mimic"], 5),
                    (content.enemies["Ghost"], 5),
                    (content.enemies["Voidspawn"], 4),
                    (content.enemies["Leviathan"], 2),
                    (content.enemies["Echo"], 1),
                ], eliteRewardWeights, 7, 9, 10, EncounterType.Void),
            ];

            CampaignRoomGenerator.GenData act1EasyGen = new([(act1Encounters[0], 3), (act1Encounters[0], 3), (act1Encounters[3], 1), (act1Encounters[1], 1), (act1Encounters[2], 1),]);
            CampaignRoomGenerator.GenData act1HardGen = new([(act1Encounters[1], 3), (act1Encounters[1], 3), (act1Encounters[3], 1), (act1Encounters[0], 1), (act1Encounters[2], 1),]);
            CampaignRoomGenerator.GenData act2EasyGen = new([(act2Encounters[0], 3), (act2Encounters[0], 3), (act2Encounters[1], 1), (act2Encounters[3], 1), (act2Encounters[2], 1),]);
            CampaignRoomGenerator.GenData act2HardGen = new([(act2Encounters[1], 3), (act2Encounters[1], 3), (act2Encounters[0], 1), (act2Encounters[3], 1), (act2Encounters[2], 1),]);
            CampaignRoomGenerator.GenData act3EasyGen = new([(act3Encounters[0], 3), (act3Encounters[0], 3), (act3Encounters[1], 1), (act3Encounters[2], 1), (act3Encounters[3], 1),]);
            CampaignRoomGenerator.GenData act3HardGen = new([(act3Encounters[1], 3), (act3Encounters[1], 3), (act3Encounters[0], 1), (act3Encounters[2], 1), (act3Encounters[3], 1),]);

            CampaignRoomGenerator generator = new CampaignRoomGenerator([[act1EasyGen, act1EasyGen, act1HardGen], [act2EasyGen, act2EasyGen, act2HardGen], [act3EasyGen, act3EasyGen, act3HardGen],]);
            CampaignSettings settings = new CampaignSettings(content, generator, new()
            {
                { RunMod.AdvancedInvaders, 0 },
                { RunMod.BossLevel, 1 },
                { RunMod.ShopPriceScaling, 5 },
                { RunMod.StartingMoney, 75 }
            });

            Dictionary<CharClassFlag, (List<CardInstance>, ArtifactInfo)> startingLoadouts = new()
            {
                { CharClassFlag.Roxy, (
                    [
                        content.cards["Shift"].CreateInstance(),
                        content.cards["Shift"].CreateInstance(),
                        content.cards["Shift"].CreateInstance(),
                        content.cards["Shift"].CreateInstance(),
                        content.cards["Shift"].CreateInstance(),
                        content.cards["Fire!"].CreateInstance(),
                        content.cards["Fire!"].CreateInstance(),
                        content.cards["Fire!"].CreateInstance(),
                        content.cards["Fire!"].CreateInstance(),
                        content.cards["Nova Bomb"].CreateInstance(),
                    ], content.artifacts["Fire Starter"]) },
                { CharClassFlag.Noel, (
                    [
                        content.cards["Shift"].CreateInstance(),
                        content.cards["Shift"].CreateInstance(),
                        content.cards["Shift"].CreateInstance(),
                        content.cards["Shift"].CreateInstance(),
                        content.cards["Flurry Fire!"].CreateInstance(),
                        content.cards["Flurry Fire!"].CreateInstance(),
                        content.cards["Flurry Fire!"].CreateInstance(),
                        content.cards["Flurry Fire!"].CreateInstance(),
                        content.cards["Cold Gust"].CreateInstance(),
                        content.cards["Novalanche"].CreateInstance(),
                    ], content.artifacts["Perma-Frost"]) },
                { CharClassFlag.Zeke, (
                    [
                        content.cards["Shift"].CreateInstance(),
                        content.cards["Shift"].CreateInstance(),
                        content.cards["Shift"].CreateInstance(),
                        content.cards["Shift"].CreateInstance(),
                        content.cards["Shift"].CreateInstance(),
                        content.cards["Fire!"].CreateInstance(content.components["Heated"]),
                        content.cards["Fire!"].CreateInstance(content.components["Heated"]),
                        content.cards["Fire!"].CreateInstance(),
                        content.cards["Fire!"].CreateInstance(),
                        content.cards["Fire!"].CreateInstance(),
                        content.cards["Nova Bomb"].CreateInstance(content.components["Heated"]),
                        content.cards["Tinker"].CreateInstance(),
                    ], content.artifacts["The Machine"]) }
            };

            CharClassFlag character = 0;
            while (character == 0)
            {
                Console.Write("Choose your character: ");
                string choice = Console.ReadLine()!.Trim().ToLower();

                switch (choice)
                {
                    case "roxy":
                        character = CharClassFlag.Roxy;
                        break;
                    case "noel":
                        character = CharClassFlag.Noel;
                        break;
                    case "zeke":
                        character = CharClassFlag.Zeke;
                        break;
                    default:
                        Console.WriteLine("This character does not exist or is not supported.");
                        break;
                }
            }

            Console.WriteLine();

            Console.WriteLine("Starting Loadout:");
            Console.WriteLine($"Artifact: {startingLoadouts[character].Item2.name}");
            Console.WriteLine("Cards:");
            foreach (CardInstance card in startingLoadouts[character].Item1)
                Console.WriteLine(card);

            Console.WriteLine();

            int seed = -1;
            while (seed == -1)
            {
                Console.Write("Enter seed: ");
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    if (result > 99999)
                    {
                        Console.WriteLine("Seeds can't be longer than 5 digits long.");
                        continue;
                    }
                    if(result < 0)
                    {
                        Console.WriteLine("Seeds must be positive.");
                        continue;
                    }
                    seed = result;
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
            return new RunState(settings, seed, character, startingLoadouts[character].Item1, [startingLoadouts[character].Item2], [content.packs["Tempest"], content.packs["Repeater"]]);
        }
    }
}
