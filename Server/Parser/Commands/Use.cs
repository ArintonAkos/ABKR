﻿using Server.Models.DQL;
using Server.Parser.Actions;
using System.Text.RegularExpressions;

namespace Server.Parser.Commands
{
    internal class Use : BaseDbAction
    {
        private readonly UseModel _model;

        public Use(Match match)
        {
            this._model = UseModel.FromMatch(match);
        }

        public override void PerformAction()
        {
            throw new NotImplementedException();
        }
    }
}
