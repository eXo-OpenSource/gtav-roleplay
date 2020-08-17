using System;
using System.Collections.Generic;
using AltV.Net.Data;
using AltV.Net.Enums;
using IPlayer = server.Players.IPlayer;

namespace server.Teams.State
{
	public class Jail
	{
		private static readonly List<List<Position>> CellPositions = new List<List<Position>>
		{
			new List<Position>
			{
				new Position(459.4174f, -995.2393f, 24.91485f),
				new Position(459.7959f, -994.5277f, 24.91485f),
				new Position(459.9504f, -993.0736f, 24.91485f),
				new Position(461.0427f, -993.1702f, 24.91485f),
				new Position(461.1523f, -995.1557f, 24.91488f)
			}
			/**
			new List<Position>
			{

			},
			new List<Position>
			{

			},
			**/
		};

		private static readonly Random Random = new Random();

		private readonly Dictionary<IPlayer, DateTime> _captives;

		public Jail()
		{
			_captives = new Dictionary<IPlayer, DateTime>();
		}

		public bool AddPrisoner(IPlayer prisoner, TimeSpan timeSpan)
		{
			if (_captives.ContainsKey(prisoner))
				return false;
			if (timeSpan <= TimeSpan.Zero)
				return false;

			_captives.Add(prisoner, DateTime.Now + timeSpan);
			prisoner.GetCharacter().SetTemporarySkin(PedModel.Prisoner01);
			prisoner.Position = GetCell();

			return true;
		}

		public bool RemovePrisoner(IPlayer prisoner, bool checkTime = true)
		{
			if (!_captives.ContainsKey(prisoner))
				return false;

			if (checkTime)
			{
				if (!_captives.TryGetValue(prisoner, out var time))
					return false;

				if (DateTime.Now > time)
					return false;
			}

			_captives.Remove(prisoner);
			prisoner.GetCharacter().ResetSkin();
			prisoner.Position = new Position(463.9635f, -993.4213f, 24.91487f);
			prisoner.Rotation = new Rotation(0f, 0f, 351.8221f);

			return true;
		}

		private Position GetCell()
		{
			var cellIndex = Random.Next(CellPositions.Count);
			var cellPositionIndex = Random.Next(CellPositions[cellIndex].Count);

			return CellPositions[cellIndex][cellPositionIndex];
		}
	}
}
