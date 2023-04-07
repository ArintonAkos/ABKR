﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Server.Models.Statement.Node;

namespace Server.Utils
{
    public static class NodeValueTypExtensions
    {
        public static Boolean IsNumeric(this NodeValueType type)
        {
            return (type == NodeValueType.Int || type == NodeValueType.Double);
        }

        public static Type ToType(this NodeValueType type)
        {
            return type switch
            {
                NodeValueType.String => typeof(string),
                NodeValueType.Int => typeof(int),
                NodeValueType.Double => typeof(double),
                NodeValueType.Boolean => typeof(bool),
                NodeValueType.Null => typeof(int),
                NodeValueType.Operator => typeof(string),
                _ => throw new Exception("Unknown NodeValueType!")
            };
        }
    }
}