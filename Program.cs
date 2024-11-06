using ParserClass;
var newPlayers = new List<Player>();
var batchNumber = Settings.StartBatchNumber;
do
{
	newPlayers = await JsonParser.GetPlayersAsync(Settings.BatchSize * batchNumber);
	batchNumber++;
	var strings = newPlayers.Select(x =>
		$"{x.rank},{x.last_rank},{x.gateway_id},{x.points},{x.wins},{x.loses},{x.dissconects},{x.toon},{x.battletag},{x.avatar},{x.feature_stat},{x.rating},{x.country}"
	).ToList();

	await File.AppendAllLinesAsync(Settings.OutputFilePath, strings);
} while (newPlayers.Any());
