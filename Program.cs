using ParserClass;



List<Player> players = await JsonParser.AddPlayersAsync();
foreach (var player in players)
{
	Console.WriteLine($"Rank: {player.rank}, Last Rank: {player.last_rank}, Gateway ID: {player.gateway_id}, Points: {player.points}, Wins: {player.wins}, " +
					  $"Loses: {player.loses}, Disconnects: {player.dissconects}, Toon: {player.toon}, Battletag: {player.battletag}, " +
					  $"Avatar: {player.avatar}, Feature Stat: {player.feature_stat}, Rating: {player.rating},Country:{player.country}");
}
