﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;


namespace Game03
{
    public class RainParticleSystem : ParticleSystem
    {
        Rectangle _source;

        public bool IsRaining { get; set; } = true;

        public RainParticleSystem(Game game, Rectangle source) : base(game, 5000)
        {
            _source = source;
        }

        protected override void InitializeConstants()
        {
            textureFilename = "drop";
            minNumParticles = 10;
            maxNumParticles = 20;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            p.Initialize(where, Vector2.UnitY * 260, Vector2.Zero, Color.LightSkyBlue, scale: RandomHelper.NextFloat(.05f, .2f), lifetime: 3);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsRaining) AddParticles(_source);
        }
    }
}
