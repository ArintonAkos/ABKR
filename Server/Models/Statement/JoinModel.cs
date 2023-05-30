﻿using Server.Models.Statement.Utils;
using Server.Services;
using System.Text.RegularExpressions;

namespace Server.Models.Statement;

public class JoinModel
{
    public class JoinColumn
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }

        public JoinColumn(string tableName, string columnName)
        {
            TableName = tableName;
            ColumnName = columnName;
        }
    }

    public class JoinCondition
    {
        public JoinColumn LeftColumn { get; set; }
        public JoinColumn RightColumn { get; set; }

        public JoinCondition(string leftTableName, string leftColumnName, string rightTableName, string rightColumnName)
        {
            LeftColumn = new(leftTableName, leftColumnName);
            RightColumn = new(rightTableName, rightColumnName);
        }
    }

    public Dictionary<string, TableDetail> JoinTableDetails { get; set; } = new();
    public List<JoinCondition> JoinConditions { get; set; } = new();

    public static JoinModel FromMatchGroup(Group group, TableService tableService)
    {
        var model = new JoinModel();

        var joinDetails = TableParserService.ParseJoinTablesAndConditions(
            group.Captures.Cast<Capture>().Select(c => c.Value),
            group.Captures.Cast<Capture>().Select(c => c.Value)
        );

        var joinTableNames = joinDetails.Item1;
        var joinConditionsRaw = joinDetails.Item2;

        int i = 0;
        foreach (var joinDetail in joinTableNames)
        {
            var joinedTableName = joinDetail.Key;
            var tableDetail = joinDetail.Value;

            if (!joinTableNames.ContainsKey(joinedTableName))
            {
                model.JoinTableDetails.Add(tableDetail.GetTableNameInUse(), tableDetail);

                var conditionParts = joinConditionsRaw[i].Split('=');
                
                if (conditionParts.Length != 2)
                {
                    throw new Exception("Invalid join condition");
                }

                var leftSide = tableService.ParseAndFindTableNameByColumn(conditionParts[0]);
                var rightSide = tableService.ParseAndFindTableNameByColumn(conditionParts[1]);

                var condition = new JoinCondition(
                    leftSide.Item1,
                    leftSide.Item2, 
                    rightSide.Item1, 
                    rightSide.Item2
                );

                model.JoinConditions.Add(condition);
            } 
            else
            {
                throw new Exception($"Table '{joinedTableName}' is already in use!");
            }
        }

        return model;
    }

}