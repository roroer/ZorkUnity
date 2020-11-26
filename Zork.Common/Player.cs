﻿using Newtonsoft.Json;
using System;

namespace Zork
{
    public class Player
    {

        public event EventHandler<LocationChangedEventArgs> LocationChanged;
        public event EventHandler<int> ScoreChanged;
        public event EventHandler<int> MovesChanged;

        public World World { get; }

        [JsonIgnore]
        public Room Location {
            get => _location;
            private set {
                if (_location != value) {
                    _locationChangedEventArgs.PreviousLocation = _location;
                    _location = value;
                    _locationChangedEventArgs.NewLocation = _location;
                    LocationChanged?.Invoke(this, _locationChangedEventArgs);
                }

            }
        }
        [JsonIgnore]
        public int Score {
            get => _score;
            set {
                if (_score != value) {
                    _score = value;
                    ScoreChanged?.Invoke(this, _score);
                }
            }
        }
        [JsonIgnore]
        public int Moves {
            get => _moves;
            set {
                if (_moves != value) {
                    _moves = value;
                    MovesChanged?.Invoke(this, _moves);
                }
            }
        }

        public Player(World world, string startingLocation) {
            Assert.IsTrue(world != null);
            Assert.IsTrue(world.RoomsByName.ContainsKey(startingLocation));

            World = world;
            Location = world.RoomsByName[startingLocation];
        }

        public bool Move(Directions direction) {
            bool isValidMove = Location.Neighbors.TryGetValue(direction, out Room destination);
            if (isValidMove) {
                Location = destination;
            }

            return isValidMove;
        }

        private Room _location;
        private LocationChangedEventArgs _locationChangedEventArgs = new LocationChangedEventArgs();
        private int _moves;
        private int _score;
    }
}