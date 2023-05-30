﻿using Server.Models.Statement;
using Server.Models.Statement.Utils;
using Server.Services;

namespace Server.Parser.Statements;

internal class Where
{
    private readonly WhereModel? _model;
    private readonly TableDetail _fromTable;

    public Where(string match, TableDetail fromTable)
    {
        _model = WhereModel.FromString(match);
        _fromTable = fromTable;
    }

    public HashSet<string> EvaluateWithoutJoin(string tableName, string databaseName)
    {
        if (_model is null)
        {
            throw new Exception("Cannot evaluate null where statement.");
        }

        return new StatementEvaluator(databaseName, tableName, _fromTable).EvaluateWithoutJoin(_model.Statement);
    }

    public List<Dictionary<string, Dictionary<string, dynamic>>> EvaluateWithJoin(TableService tableService, Join joinStatements)
    {
        if (_model is null)
        {
            throw new Exception("Cannot evaluate null where statement.");
        }

        return new StatementEvaluator(tableService, joinStatements, _fromTable).Evaluate(_model.Statement);
    }

    public bool IsEvaluatable() => _model is not null;
}