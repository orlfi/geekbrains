using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace MetricsManager.DAL.DapperMapingHandlers
{
    public class DateTimeOffsetMappingHandler : SqlMapper.TypeHandler<DateTimeOffset>
    {
        public override DateTimeOffset Parse(object value) =>
            DateTimeOffset.FromUnixTimeSeconds((long)value).ToOffset(TimeZoneInfo.Local.BaseUtcOffset);

        public override void SetValue(IDbDataParameter parameter, DateTimeOffset value) =>
            parameter.Value = value.ToUnixTimeSeconds();
    }
}
