using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace MetricsManager.DAL.DapperMapingHandlers
{
    public class UriMappingHandler : SqlMapper.TypeHandler<Uri>
    {
        public override Uri Parse(object value) =>
            new Uri(value as string);

        public override void SetValue(IDbDataParameter parameter, Uri value) =>
            parameter.Value = value.ToString();
    }
}
