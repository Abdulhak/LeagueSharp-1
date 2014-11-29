﻿#region LICENSE

// Copyright 2014 - 2014 SpellDetector
// TargetSpell.cs is part of SpellDetector.
// SpellDetector is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// SpellDetector is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// You should have received a copy of the GNU General Public License
// along with SpellDetector. If not, see <http://www.gnu.org/licenses/>.

#endregion

#region

using System;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

#endregion

namespace SpellDetector.Targeted
{
    public class TargetSpell
    {
        public Obj_AI_Hero Caster { get; set; }
        public Obj_AI_Base Target { get; set; }
        public TargetSpellData Spell { get; set; }
        public int StartTick { get; set; }
        public Vector2 StartPosition { get; set; }

        public int EndTick
        {
            get { return (int) (StartTick + Spell.Delay + 1000*(StartPosition.Distance(EndPosition)/Spell.Speed)); }
        }

        public Vector2 EndPosition
        {
            get { return Target.Position.To2D(); }
        }

        public Vector2 Direction
        {
            get { return (EndPosition - StartPosition).Normalized(); }
        }

        public double Damage
        {
            get { return Caster.GetSpellDamage(Target, Spell.Name); }
        }

        public bool IsActive
        {
            get { return Environment.TickCount <= EndTick; }
        }
    }

    public class TargetSpellData
    {
        public string ChampionName { get; set; }
        public SpellSlot Spellslot { get; set; }
        public Spelltype Type { get; set; }
        public CcType CcType { get; set; }
        public string Name { get; set; }
        public float Range { get; set; }
        public double Delay { get; set; }
        public double Speed { get; set; }

        public TargetSpellData(string champion, string name, SpellSlot slot, Spelltype type, CcType cc, float range,
            float delay, float speed)
        {
            ChampionName = champion;
            Name = name;
            Spellslot = slot;
            Type = type;
            CcType = cc;
            Range = range;
            Speed = speed;
            Delay = delay;
        }
    }

    public enum Spelltype
    {
        Skillshot,
        Targeted,
        Self,
        AutoAttack
    }

    public enum CcType
    {
        No,
        Stun,
        Silence,
        Taunt,
        Polymorph,
        Slow,
        Snare,
        Fear,
        Charm,
        Suppression,
        Blind,
        Flee,
        Knockup,
        Knockback,
        Pull
    }
}