﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Numerics;
using System.Text.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ParserClass
{
	internal class HttpRequest
	{
		private static readonly HttpClient client = new HttpClient();
		public static async Task<string> GetRequest(string url)
		{
			try
			{
			    HttpResponseMessage response = await client.GetAsync(url);
				response.EnsureSuccessStatusCode();
				string responseBody = await response.Content.ReadAsStringAsync();
				return responseBody;
			}
			catch (HttpRequestException e)
			{
				return $"Error: {e.Message}";
			}
		}
	}
	class Player
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int rank { get; set; }
		public int last_rank { get; set; }
		public int gateway_id { get; set; }
		public int points { get; set; }
		public int wins { get; set; }
		public int loses { get; set; }
		public int dissconects { get; set; }
		public string? toon { get; set; }
		public string? battletag { get; set; }
		public string? avatar { get; set; }
		public string? feature_stat { get; set; }
		public int rating { get; set; }
		public string country { get; set; }
	}
	internal class JsonParser
	{
		public static async Task<string> GetCountry(string player,int gateway_id)
		{
			var json = await HttpRequest.GetRequest($"http://127.0.0.1:49530/web-api/v2/aurora-profile-by-toon/{player}/{gateway_id}?request_flags=scr_profile");
			var jsonDoc = JsonDocument.Parse(json);
			var country = jsonDoc.RootElement.GetProperty("country_code");
			return country.ToString();
		}
		public static async Task<List<Player>> AddPlayersAsync()
		{
			var json = await HttpRequest.GetRequest("http://127.0.0.1:49530/web-api/v1/leaderboard/12966?offset=0&length=5");
			var jsonDoc = JsonDocument.Parse(json);
			var rows = jsonDoc.RootElement.GetProperty("rows");
			List<Player> players = new List<Player>();

			foreach (var row in rows.EnumerateArray())
			{
				players.Add(new Player
				{
					rank = row[0].GetInt32(),
					last_rank = row[1].GetInt32(),
					gateway_id = row[2].GetInt32(),
					points = row[3].GetInt32(),
					wins = row[4].GetInt32(),
					loses = row[5].GetInt32(),
					dissconects = row[6].GetInt32(),
					toon = row[7].GetString(),
					battletag = row[8].GetString(),
					avatar = row[9].GetString(),
					feature_stat = row[10].GetString(),
					rating = row[11].GetInt32(),
					country = await GetCountry(row[7].GetString(),row[2].GetInt32()),
				});
			}
			return players;
		}
	}
}
	
