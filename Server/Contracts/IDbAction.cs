﻿using Server.Server.Responses;

namespace Server.Contracts;

internal interface IDbAction
{
    public ActionResponse Perform(Guid session);
}