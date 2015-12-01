﻿using System;

namespace Ballz.GameSession.World
{
    /// <summary>
    /// Represents a Ball character. 
    /// </summary>
    /// <remarks>
    /// We do not distinguish between player-controlled and other balls in the world state.
    /// Use <see cref="Session.PlayerBallId"/> to get the ball that is currently controlled by the local player.
    /// </remarks>
    public class Ball : Entity
    {
        public Ball() : base(EntityType.Player)
        {
            Material = PhysicsMaterial.Ball;
        }

        /// <summary>
        /// The health value of the ball. Typical value ranges are 0-100.
        /// </summary>
        public double Health { get; set; }
    }
}